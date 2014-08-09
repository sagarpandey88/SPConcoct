using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace SPConcoct.Branding.Features.MasterPages
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("7240acdf-1642-4155-8aaf-999008ac9221")]
    public class MasterPagesEventReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            using (SPSite siteCollection = properties.Feature.Parent as SPSite)
            {
                if (siteCollection != null)
                {
                    // Calculate relative path to site from Web Application root.
                    string WebAppRelativePath = siteCollection.RootWeb.ServerRelativeUrl;
                    if (!WebAppRelativePath.EndsWith("/"))
                    {
                        WebAppRelativePath += "/";
                    }

                    // Enumerate through each site and apply branding.
                    foreach (SPWeb site in siteCollection.AllWebs)
                    {
                        try
                        {
                            site.MasterUrl = WebAppRelativePath + "_catalogs/masterpage/HomeMasterPage.master";
                            site.CustomMasterUrl = WebAppRelativePath + "_catalogs/masterpage/HomeMasterPage.master";
                            //site.AlternateCssUrl = WebAppRelativePath + "Style Library/HomeMasterPageResources/css/style.css";
                            site.SiteLogoUrl = "/Style Library/HomeMasterPageResources/img/logo.png";
                            site.UIVersion = 4;
                            site.Update();
                        }
                        finally
                        {
                            if (site != null)
                                site.Dispose();
                        }
                    }
                }

            }
        }

        // Uncomment the method below to handle the event raised before a feature is deactivated.
        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            using (SPSite siteCollection = properties.Feature.Parent as SPSite)
            {
                if (siteCollection != null)
                {

                    //Reset to default the Master Page settings
                    // Calculate relative path of site from Web Application root.
                    string WebAppRelativePath = siteCollection.RootWeb.ServerRelativeUrl;
                    if (!WebAppRelativePath.EndsWith("/"))
                    {
                        WebAppRelativePath += "/";
                    }

                    // Enumerate through each site and remove custom branding.
                    foreach (SPWeb _Web in siteCollection.AllWebs)
                    {
                        try
                        {
                            if (_Web.IsRootWeb)
                            {

                            }

                            #region Reset Master Page settings to default
                            _Web.MasterUrl = WebAppRelativePath + "_catalogs/masterpage/v4.master";
                            _Web.CustomMasterUrl = WebAppRelativePath + "_catalogs/masterpage/v4.master";
                            _Web.AlternateCssUrl = "";
                            _Web.SiteLogoUrl = "_catalogs/masterpage/_layouts/images/siteIcon.png";
                            _Web.Update();
                            #endregion
                        }
                        catch (Exception _Ex)
                        {
                            SPDiagnosticsService diagSvc = SPDiagnosticsService.Local;
                            diagSvc.WriteTrace(0, new SPDiagnosticsCategory("category", TraceSeverity.Monitorable, EventSeverity.Error), TraceSeverity.Monitorable, "Writing to the ULS log:  {0}", new object[] { " Error occurred while cleaning up custom pages and resetting master page settings " + _Ex.Message });
                        }
                        finally
                        {
                            if (_Web != null)
                                _Web.Dispose();
                        }
                    }
                }
            }
        }




        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
