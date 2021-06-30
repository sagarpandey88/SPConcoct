using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Microsoft.SharePoint.Administration;

namespace SP10Concoct.Webparts.CustomContentEditor
{
    public partial class CustomContentEditorUserControl : UserControl
    {
        public Panel BodyContentEdit { get { return plhContentEdit; } }
        public Panel BodyContentDisplay { get { return plhContentDisplay; } }
        public Panel BodyNoContent { get { return plhNoContent; } }


        public CustomContentEditor CustomContentEditorControl { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                tblHeader.Attributes.Add("class", CustomContentEditorControl._HeaderColor.ToString());
                spnWPheader.InnerText = CustomContentEditorControl.Title;
            }
            catch (Exception ex)
            {

            }

        }
    }
}
