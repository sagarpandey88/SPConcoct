using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using SPConcoct.Common;

namespace SPConcoct.SPCommon
{

    public class SPCommon
    {

        const string IDColumnName = "ID";
        const string DateFormat = "dd-MM-yyyy";
        public static string _rootSiteUrl = ConfigurationManager.AppSettings["RootSiteUrl"];


        public static int GetLookupId(string lookupText)
        {
            var val = new SPFieldLookupValue(lookupText);
            int intID = val.LookupId;
            return intID;
        }

        public static string GetLookupText(string lookupText)
        {
            var val = new SPFieldLookupValue(lookupText);
            string intID = val.LookupValue;
            return intID;
        }

        /// <summary>
        /// Gets the lookup ID from the field
        /// </summary>
        /// <param name="lookupObject">SPListItem[FieldName] object</param>
        /// <returns>integer based id of the lookup</returns>
        public static int GetLookupId(object lookupObject)
        {

            string lookupString = Convert.ToString(lookupObject);
            var val = new SPFieldLookupValue(lookupString);
            int intID = val.LookupId;
            return intID;
        }

        /// <summary>
        /// Gets the lookup text
        /// </summary>
        /// <param name="lookupObject">SPListItem[FieldName] object</param>
        /// <returns>string based value of the field</returns>
        public static string GetLookupText(object lookupObject)
        {
            string lookupString = Convert.ToString(lookupObject);
            var val = new SPFieldLookupValue(lookupString);
            string intID = val.LookupValue;
            return intID;
        }


        public static List<DropDownDTO> GetChoiceFieldValues
                                (string siteCollection, string listName, string fieldName)
        {
            List<DropDownDTO> fieldList;

            SPSite spSite = null;
            SPWeb spWeb = null;

            try
            {
                if (siteCollection != null)
                    spSite = new SPSite(siteCollection);
                else
                    spSite = SPContext.Current.Site;

                spWeb = spSite.OpenWeb();


                SPList spList = spWeb.Lists[listName];


                SPFieldChoice field = (SPFieldChoice)spList.Fields.GetFieldByInternalName(fieldName); //spList.Fields[fieldName];

                fieldList = new List<DropDownDTO>();

                foreach (string str in field.Choices)
                {
                    DropDownDTO item = new DropDownDTO();
                    item.Data = str;
                    item.Id = str;
                    fieldList.Add(item);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                if (spWeb != null)
                    spWeb.Close();

                if (spSite != null)
                    spSite.Close();
            }

            return fieldList;
        }




        #region Db Access



        /// <summary>
        /// This method return List of data in key value pair format.
        /// TextField,ValueField are the returned list properties
        /// </summary>
        /// <param name="siteUrl"> url so the site to connect</param>
        /// <param name="listName">Name of the Source List</param>
        /// <param name="columnName">Name of the Lookup column</param>
        /// <returns>List of DropdownSource where  TextField is the column required,ValueField is Id column of list are the properties of the List Item</returns>

        public List<DropDownDTO> GetLookupValues(string siteUrl, string listName, string columnName)
        {
            List<DropDownDTO> dropdownSourceList = new List<DropDownDTO>();
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite objSite = new SPSite(siteUrl))
                {
                    using (SPWeb objWeb = objSite.OpenWeb())
                    {
                        SPList objList = objWeb.Lists.TryGetList(listName);

                        if (objList != null)
                        {
                            SPListItemCollection objItemCollection = objList.GetItems(IDColumnName, columnName);

                            if (objItemCollection != null)
                            {
                                if (objItemCollection.Count > 0)
                                {
                                    foreach (SPListItem lstItem in objItemCollection)
                                    {
                                        dropdownSourceList.Add((new DropDownDTO(Convert.ToString(lstItem[IDColumnName]), Convert.ToString(lstItem[columnName]))));
                                    }
                                }
                            }
                        }

                    }

                }
            });


            return dropdownSourceList;
        }

        /// <summary>
        ///  This method return List of data in key value pair format.
        /// TextField,ValueField are the returned list properties
        /// Filtered by the filtercolumn specified
        /// </summary>
        /// <param name="siteUrl">url so the site to connect</param>
        /// <param name="listName">Name of the Source List</param>
        /// <param name="columnName">Name of the Lookup column</param>
        /// <param name="filterByColumnName">Name of the column by which the result has to be filtered</param>
        /// <param name="filterByColumnValue">Values of  the column to be filtered</param>
        /// <param name="valueColumnName"></param>
        /// <returns></returns> 
        public List<DropDownDTO> GetLookupValues(string siteUrl, string listName, string columnName, string filterByColumnName, string filterByColumnValue)
        {


            List<DropDownDTO> dropdownSourceList = new List<DropDownDTO>();
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite objSite = new SPSite(siteUrl))
                {
                    using (SPWeb objWeb = objSite.OpenWeb())
                    {
                        SPList objList = objWeb.Lists.TryGetList(listName);
                        SPQuery objQuery = new SPQuery();
                        objQuery.Query = "<Where><Eq><FieldRef Name='" + filterByColumnName + "'/><Value Type='Text'>" + filterByColumnValue + "</Value></Eq></Where>";
                        if (objList != null)
                        {
                            SPListItemCollection objItemCollection = objList.GetItems(objQuery);

                            if (objItemCollection != null)
                            {
                                if (objItemCollection.Count > 0)
                                {
                                    foreach (SPListItem lstItem in objItemCollection)
                                    {
                                        dropdownSourceList.Add((new DropDownDTO(Convert.ToString(lstItem[IDColumnName]), Convert.ToString(lstItem[columnName]))));
                                    }
                                }
                            }
                        }

                    }

                }
            });

            return dropdownSourceList;
        }


        public List<DropDownDTO> GetLookupValuesByLookupFilter(string siteUrl, string listName, string columnName, string filterByColumnName, string filterByColumnValue)
        {


            List<DropDownDTO> dropdownSourceList = new List<DropDownDTO>();
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite objSite = new SPSite(siteUrl))
                {
                    using (SPWeb objWeb = objSite.OpenWeb())
                    {
                        SPList objList = objWeb.Lists.TryGetList(listName);
                        SPQuery objQuery = new SPQuery();
                        objQuery.Query = "<Where><Eq><FieldRef Name='" + filterByColumnName + "' LookupId='TRUE' /><Value Type='Text'>" + filterByColumnValue + "</Value></Eq></Where>";
                        if (objList != null)
                        {
                            SPListItemCollection objItemCollection = objList.GetItems(objQuery);

                            if (objItemCollection != null)
                            {
                                if (objItemCollection.Count > 0)
                                {
                                    foreach (SPListItem lstItem in objItemCollection)
                                    {
                                        dropdownSourceList.Add((new DropDownDTO(Convert.ToString(lstItem[IDColumnName]), Convert.ToString(lstItem[columnName]))));
                                    }
                                }
                            }
                        }

                    }

                }
            });

            return dropdownSourceList;
        }

        public List<DropDownDTO> GetLookupValuesAppended(string siteUrl, string listName, List<string> appendedDisplayColumnName, bool ascending)
        {
            List<DropDownDTO> dropdownSourceList = new List<DropDownDTO>();
            //    appendedDisplayColumnName.Add(IDColumnName);
            string[] appendedColumnArray = appendedDisplayColumnName.ToArray();
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite objSite = new SPSite(siteUrl))
                {
                    using (SPWeb objWeb = objSite.OpenWeb())
                    {
                        SPList objList = objWeb.Lists.TryGetList(listName);

                        if (objList != null)
                        {
                            SPListItemCollection objItemCollection = objList.GetItems(appendedColumnArray);


                            if (objItemCollection != null)
                            {
                                if (objItemCollection.Count > 0)
                                {
                                    foreach (SPListItem lstItem in objItemCollection)
                                    {
                                        dropdownSourceList.Add((new DropDownDTO(Convert.ToString(lstItem[IDColumnName]), CreateAppendedString(appendedDisplayColumnName, lstItem))));
                                    }
                                }
                            }
                        }

                    }

                }
            });

            //dropdownSourceList.Sort(x => x.Data);
            dropdownSourceList = dropdownSourceList.OrderBy(x => x.Data).ToList();


            return dropdownSourceList;
        }


        protected string CreateAppendedString(List<string> columnNames, SPListItem objItem)
        {

            StringBuilder appendedData = new StringBuilder();

            foreach (string colName in columnNames)
            {
                appendedData.Append(Convert.ToString(objItem[colName]) + " ");
            }


            return appendedData.ToString();

        }

        #endregion



        #region Commented Functions

        //public static void PopulateDropdownWithChoice(SPList objListTimeSpent, DropDownList ddl, string columnName)
        //{

        //    try
        //    {
        //        SPFieldChoice fieldActivityStage = (SPFieldChoice)objListTimeSpent.Fields.GetFieldByInternalName(columnName);
        //        int choiceIndex = 0;
        //        List<ChoiceValueDTO> lstChoiceValues = new List<ChoiceValueDTO>();
        //        foreach (string chVal in fieldActivityStage.Choices)
        //        {
        //            lstChoiceValues.Add(new ChoiceValueDTO { ChoiceKey = chVal, ChoiceValue = choiceIndex });

        //            choiceIndex++;
        //        }

        //        ddl.DataSource = lstChoiceValues;
        //        ddl.DataTextField = "ChoiceKey";
        //        ddl.DataValueField = "ChoiceValue";
        //        ddl.DataBind();

        //    }
        //    catch (Exception ex)
        //    {


        //    }

        //}

        #endregion


    }
}
