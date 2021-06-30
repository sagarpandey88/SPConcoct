using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace SP10Concoct.Webparts.ImageSlider
{
    [ToolboxItemAttribute(false)]
    public class ImageSlider : WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/SP10Concoct.Webparts/ImageSlider/ImageSliderUserControl.ascx";

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            if (control != null)
            {
                ((ImageSliderUserControl)control).ImageSliderCustomControl = this;

            }
            Controls.Add(control);
            this.ChromeType = PartChromeType.None;
        }


        #region Custom Properties

        public string _ListName = "CarouselConfig";
        [Category("SPConcoct Custom Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        WebDisplayName("List Name"),
        WebDescription("Please enter a list name for the slider source")]
        public string ListName
        {
            get { return _ListName; }
            set
            {
                _ListName = value;
            }
        }

        public bool _OpenLinkInNewWindow;
        [Category("SPConcoct Custom Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        WebDisplayName("Open link in new window"),
        WebDescription("Please choose wheather to open  link in new window")]
        public bool OpenLinkInNewWindow
        {
            get { return _OpenLinkInNewWindow; }
            set { _OpenLinkInNewWindow = value; }
        }

        #endregion

    }
}
