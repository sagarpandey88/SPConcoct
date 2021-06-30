using System;
using System.ComponentModel;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebPartPages;
using Microsoft.SharePoint.WebControls;
using Microsoft.Web.CommandUI;

namespace SP10Concoct.Webparts.CustomContentEditor
{
    [ToolboxItemAttribute(false)]
    public class CustomContentEditor : Microsoft.SharePoint.WebPartPages.WebPart
    {
        #region Properties


        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/SPConcoct.WebParts/CustomContentEditor/CustomContentEditorUserControl.ascx";

        private string _content;
        private CustomContentEditorUserControl control;

        private HtmlGenericControl editableRegion = new HtmlGenericControl();
        private HtmlGenericControl emptyPanel = new HtmlGenericControl();


        private bool IsInEditMode
        {
            get
            {
                SPWebPartManager currentWebPartManager = (SPWebPartManager)WebPartManager.GetCurrentWebPartManager(this.Page);
                return (((currentWebPartManager != null) && !base.IsStandalone) && currentWebPartManager.GetDisplayMode().AllowPageDesign);
            }
        }
        #endregion
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (this.IsInEditMode)
            {
                SPRibbon current = SPRibbon.GetCurrent(this.Page);
                if (current != null)
                {
                    current.MakeTabAvailable("Ribbon.EditingTools.CPEditTab");
                    current.MakeTabAvailable("Ribbon.Image.Image");
                    current.MakeTabAvailable("Ribbon.EditingTools.CPInsert");
                    current.MakeTabAvailable("Ribbon.Link.Link");
                    current.MakeTabAvailable("Ribbon.Table.Layout");
                    current.MakeTabAvailable("Ribbon.Table.Design");
                    if (!(this.Page is WikiEditPage))
                    {
                        current.TrimById("Ribbon.EditingTools.CPEditTab.Layout");
                        current.TrimById("Ribbon.EditingTools.CPEditTab.EditAndCheckout");
                    }
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Prevent default display of webpart chrome in standard view mode
            this.ChromeType = PartChromeType.None;

            control = (CustomContentEditorUserControl)Page.LoadControl(_ascxPath);
            ((CustomContentEditorUserControl)control).CustomContentEditorControl = this;
            Controls.Add(control);
            control.BodyContentDisplay.Controls.Add(new LiteralControl(this.Content));
            control.BodyContentEdit.Controls.Add(this.editableRegion);
            control.BodyNoContent.Controls.Add(this.emptyPanel);

            string strUpdatedContent = this.Page.Request.Form[this.ClientID + "content"];
            if ((strUpdatedContent != null) && (this.Content != strUpdatedContent))
            {
                this.Content = strUpdatedContent;
                try
                {
                    SPWebPartManager currentWebPartManager = (SPWebPartManager)WebPartManager.GetCurrentWebPartManager(this.Page);
                    Guid storageKey = currentWebPartManager.GetStorageKey(this);
                    currentWebPartManager.SaveChanges(storageKey);
                    SPUtility.Redirect(System.Web.HttpContext.Current.Request.Url.ToString(), SPRedirectFlags.Trusted, System.Web.HttpContext.Current, null);
                }
                catch (Exception exception)
                {
                    Label child = new Label();
                    child.Text = exception.Message;
                    this.Controls.Add(child);
                }
            }
            if (this.IsInEditMode)
            {
                this.Page.ClientScript.RegisterHiddenField(this.ClientID + "content", this.Content);

                control.BodyContentDisplay.Visible = false;

                this.emptyPanel.TagName = "DIV";
                this.emptyPanel.Style.Add(HtmlTextWriterStyle.Cursor, "hand");
                this.emptyPanel.Controls.Add(new LiteralControl("Click here to Edit"));
                this.emptyPanel.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
                base.Attributes["RteRedirect"] = this.editableRegion.ClientID;
                ScriptLink.RegisterScriptAfterUI(this.Page, "SP.UI.Rte.js", false);
                ScriptLink.RegisterScriptAfterUI(this.Page, "SP.js", false);
                ScriptLink.RegisterScriptAfterUI(this.Page, "SP.Runtime.js", false);
                this.editableRegion.TagName = "DIV";
                this.editableRegion.InnerHtml = this.Content;
                this.editableRegion.Attributes["class"] = "ms-rtestate-write ms-rtestate-field";
                this.editableRegion.Attributes["contentEditable"] = "true";
                this.editableRegion.Attributes["InputFieldId"] = this.ClientID + "content";
                this.editableRegion.Attributes["EmptyPanelId"] = this.emptyPanel.ClientID;
                this.editableRegion.Attributes["ContentEditor"] = "True";
                this.editableRegion.Attributes["AllowScripts"] = "True";
                this.editableRegion.Attributes["AllowWebParts"] = "False";
                string script = "RTE.RichTextEditor.transferContentsToInputField('" + SPHttpUtility.EcmaScriptStringLiteralEncode(this.editableRegion.ClientID) + "');";
                this.Page.ClientScript.RegisterOnSubmitStatement(base.GetType(), "transfer" + this.editableRegion.ClientID, script);
            }
        }

        // Properties
        [WebPartStorage(Storage.Shared)]
        public string Content
        {
            get
            {
                return this._content;
            }
            set
            {
                _content = value;
            }
        }
        protected override void CreateChildControls()
        {


            //Control control = Page.LoadControl(_ascxPath);
            //if (control != null)
            //{
            //    ((CustomContentEditorUserControl)control).CustomContentEditorControl = this;

            //}
            //Controls.Add(control);
            //this.ChromeType = PartChromeType.None;

        }
    }
}
