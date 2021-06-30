using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.SharePoint;
using System.IO;
using System.Xml;

namespace SPListScriptGenerator
{
    public partial class frmScriptGenerator : Form
    {
        int listCount = 0;

        /*Pending Task
         *  1. Create seperate files for seperate lists
         *  2. Generate views code
         *  3. 
         */

        public frmScriptGenerator()
        {
            InitializeComponent();
        }

        #region Events

        private void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (e.RowIndex != -1)
                {
                    dgLists.Rows[e.RowIndex].Selected = true;
                    DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();

                    ch1 = (DataGridViewCheckBoxCell)dgLists.Rows[dgLists.CurrentRow.Index].Cells[0];

                    if (ch1.Value == null)

                        ch1.Value = false;

                    switch (ch1.Value.ToString())
                    {

                        case "True":

                            ch1.Value = false;

                            break;

                        case "False":

                            ch1.Value = true;

                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                rtbMessage.Text = ex.Message;

            }

        }


        private void btnShowList_Click(object sender, EventArgs e)
        {

            try
            {
                PopulateLists();
                btnGenerate.Visible = true;
                dgLists.Enabled = true;
            }

            catch (Exception ex)
            {
                rtbMessage.Text = ex.Message;

            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {

                CreateListScript();
            }
            catch (Exception ex)
            {
                rtbMessage.Text = ex.Message;

            }
        }



        #endregion

        #region Methods

        protected void PopulateLists()
        {
            string siteUrl = txtSiteUrl.Text;
            try
            {

                SPSecurity.RunWithElevatedPrivileges(delegate
                {
                    using (SPSite objsite = new SPSite(siteUrl))
                    {
                        using (SPWeb objWeb = objsite.OpenWeb())
                        {

                            SPListCollection lstColl = objWeb.Lists;
                            dgLists.DataSource = GetListsDatatable(lstColl);
                            dgLists.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                            dgLists.Visible = true;
                            btnGenerate.Visible = true;


                        }
                    }
                });
            }
            catch (Exception ex)
            {
                rtbMessage.Text = ex.Message;
                MessageBox.Show("Error Occured While retrieving the Site" + Environment.NewLine + "Please check the Site Url"
                                + Environment.NewLine + "Detailed Error Message: " + ex.Message);


            }

        }

        protected DataTable GetListsDatatable(SPListCollection lstColl)
        {
            DataTable dtLists = new DataTable();
            dtLists.Columns.Add("ListName");
            dtLists.Columns.Add("Author");
            dtLists.Columns.Add("DefaultView");

            foreach (SPList lst in lstColl)
            {
                DataRow drNew = dtLists.NewRow();
                drNew["ListName"] = lst.Title;
                // drNew["Author"] = lst.Author;
                drNew["DefaultView"] = lst.DefaultView;
                dtLists.Rows.Add(drNew);
            }


            return dtLists;
        }

        protected void CreateListScript()
        {
            StringBuilder strMainFile = new StringBuilder();
            StringBuilder strMainListNames = new StringBuilder();

            strMainFile.AppendLine("Add-PSSnapin Microsoft.SharePoint.PowerShell");
            //  strMainFile.AppendLine("$web = Get-SPWeb -Identity $ListSiteURL");

            foreach (DataGridViewRow dgvr in dgLists.Rows)
            {
                DataGridViewCheckBoxCell chkSelect = new DataGridViewCheckBoxCell();
                chkSelect = (DataGridViewCheckBoxCell)dgvr.Cells["SelectList"];


                if (dgvr.Cells["SelectList"].FormattedValue.ToString().ToLower() == "true")//(chkSelect.Selected  )
                {
                    string listName = Convert.ToString(dgvr.Cells["ListName"].Value);
                    //get the list name and create entites in a string builder
                    strMainListNames.AppendLine(listName);
                    //Entity Classes
                    strMainFile.AppendLine(GetListGenerationScript(listName).ToString());
                    listCount++;
                }

            }


            CreateFile(strMainFile.ToString());
            MessageBox.Show("Entities created for" + Environment.NewLine + strMainListNames.ToString());
            OpenFolder(GeneratedFolderPath);
        }

        protected StringBuilder GetListGenerationScript(string listName)
        {
            string siteUrl = txtSiteUrl.Text;
            StringBuilder strEntities = new StringBuilder();
            string listVariableName = "$list_" + listCount;
            string listGUIDVariable = "$myListGuid_" + listCount;

            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite objsite = new SPSite(siteUrl))
                {
                    using (SPWeb objWeb = objsite.OpenWeb())
                    {
                        SPList lst = objWeb.Lists.TryGetList(listName);

                        strEntities.AppendLine("$webURL = '" + siteUrl + "';");
                        strEntities.AppendLine("$spSite = [Microsoft.SharePoint.SPSite]($webURL)");
                        strEntities.AppendLine("$spWeb = $spSite.OpenWeb()");

                        //  strEntities.AppendLine(listVariableName + " = $spWeb.GetList('" + lst.RootFolder.Url + "')");

                        strEntities.AppendLine(listGUIDVariable + "= $spWeb.Lists.Add('" + lst.Title + "', '" + lst.Description + "', '" + (int)lst.BaseTemplate + "')");

                        strEntities.AppendLine(listVariableName + " = $spweb.Lists[" + listGUIDVariable + "]");


                        foreach (SPField fld in lst.Fields)
                        {
                            if (UseDefaultView(lst, fld))
                            {
                                string internalName = fld.InternalName;
                                string displayName = fld.Title.Replace(' ', '_');
                                if (internalName.ToLower() == "linktitle")
                                {
                                    internalName = "Title";
                                }

                                if (fld.Type == SPFieldType.Lookup && ((SPFieldLookup)fld).LookupList != "")
                                {
                                    int lookupFieldCount = 0;
                                    SPFieldLookup lookupField = (SPFieldLookup)fld;
                                    Guid listGuid = new Guid(lookupField.LookupList);
                                    SPList objLookupList = objWeb.Lists[listGuid];

                                    string lookupListVariableName = "$spLookupList_" + lookupFieldCount;

                                    strEntities.AppendLine(lookupListVariableName + " = $spWeb.GetList('" + objLookupList.RootFolder.Url + "')");

                                    //strEntities.AppendLine(" $fieldXml = '<Field Type='Lookup' DisplayName='" + fld.Title + "' ShowField='"lookupField.showfi"' StaticName='SName' List='$spLookupList.id' Name='SName'></Field>'");

                                    string xml = ReplaceXMLAttribute(lookupField.SchemaXml, "List", lookupListVariableName + ".id");

                                    strEntities.AppendLine(" $fieldXml = '" + xml + "'");

                                    strEntities.AppendLine(listVariableName + ".Fields.AddFieldAsXml($fieldXml,$true,[Microsoft.SharePoint.SPAddFieldOptions]::AddFieldToDefaultView)");


                                }
                                else
                                {

                                    strEntities.AppendLine(listVariableName + ".Fields.AddFieldAsXml('" + fld.SchemaXml + "',$true,[Microsoft.SharePoint.SPAddFieldOptions]::AddFieldToDefaultView)");

                                    //   strEntities.AppendLine("$list.Fields.Add('" + internalName + "', [microsoft.sharepoint.SPFieldType]::" + fieldType + ", $false");
                                }


                                strEntities.AppendLine(listVariableName + ".OnQuickLaunch = $True");
                                strEntities.AppendLine(listVariableName + ".Update()");

                            }


                        }



                        strEntities.AppendLine("$spWeb.Dispose()");
                        strEntities.AppendLine("$spSite.Dispose()");
                    }
                }
            });

            return strEntities;


        }

        protected void CreateDataScripts()
        {

            //loop through all the selected list
            //get the list name and iterate through the list items
            //create a script for specific item
       

        }


        #region Utility Methods

        protected void CreateFile(string content)
        {
            WriteToFile(content);

        }

        public void WriteToFile(string content)
        {
            try
            {
                string fileName = GetUniqueFileName();
                if (!File.Exists(fileName))
                {

                    StreamWriter sw = File.CreateText(fileName);
                    sw.WriteLine(content);
                    sw.Close();
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected string GetUniqueFileName()
        {


            if (!Directory.Exists(GeneratedFolderPath))
            {
                Directory.CreateDirectory(GeneratedFolderPath);
            }
            int i = 1;
            string unqFileName = GeneratedFolderPath + "\\" + "SPScript_0" + ".txt";
            while (File.Exists(unqFileName))
            {
                unqFileName = GeneratedFolderPath + "\\" + "SPScript_" + i + ".txt";
                i++;
            }

            return unqFileName;

        }

        protected void ReadXMLSchemaAttribute(string attributeName, string xmlDoc)
        {

            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<reply success=\"true\">More nodes go here</reply>");

            XmlElement root = doc.DocumentElement;

            string s = root.Attributes["succes"].Value;
        }

        protected string ReplaceXMLAttribute(string xmlText, string replaceAttribute, string replacingValue)
        {

            //Here is the variable with which you assign a new value to the attribute
            //string newValue = string.Empty;
            //System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();

            //xmlDoc.LoadXml(xmlText);

            //System.Xml.XmlNode node = xmlDoc.SelectSingleNode("Root/Node/Element");
            //node.Attributes[0].Value = newValue;

            //xmlDoc.Save(xmlFile);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlText);

            XmlElement root = doc.DocumentElement;

            root.Attributes[replaceAttribute].Value = replacingValue;

            return doc.OuterXml;

        }

        protected bool UseDefaultView(SPList lst, SPField fld)
        {
            bool existInDV = true;
            //if (chkDefaultView.Checked)
            //{
            existInDV = lst.DefaultView.ViewFields.Exists(fld.InternalName);
            if (existInDV)
            {
                existInDV = ValidateColumn(fld.StaticName);
            }
            //  }

            return existInDV;
        }

        protected bool ValidateColumn(string columnName)
        {
            List<string> columnNames = new List<string>();
            columnNames.Add("Title");
            columnNames.Add("ID");
            columnNames.Add("Attachments");
            columnNames.Add("LinkTitle");
            columnNames.Add("SelectTitle");



            if (columnNames.Contains(columnName))
            {
                return false;
            }

            return true;





        }


        protected void OpenFolder(string folderPath)
        {
            System.Diagnostics.Process.Start(folderPath);

        }
        #endregion


        #region Property
        public string GeneratedFolderPath
        {
            get
            {
                const string PSSScript = "PSScripts";
                return Environment.CurrentDirectory + "\\" + PSSScript;

            }
        }
        #endregion

        #endregion
    }
}
