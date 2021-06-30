using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;

namespace SP10Concoct.Webparts.Carousel
{
    [ToolboxItemAttribute(false)]
    public partial class Carousel : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public Carousel()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }


        #region Properties

        private string _siteUrl;
        [WebBrowsable(true), Personalizable(PersonalizationScope.Shared)]
        public string SiteUrl
        {
            get
            {
                return _siteUrl;
            }
            set
            {
                _siteUrl = value;
            }
        }


        #endregion
    }
}
