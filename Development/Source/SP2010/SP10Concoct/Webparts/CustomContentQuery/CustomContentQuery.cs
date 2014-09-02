using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
//using Microsoft.SharePoint.Publishing.WebControls;
using Microsoft.SharePoint.WebPartPages;
using System.Xml.Serialization;
using System.Text;
using System.Web.UI;

using Microsoft.SharePoint.Administration;

namespace SP10Concoct.Webparts.CustomContentQuery
{

    public class CustomContentQuery : WebPart // ContentByQueryWebPart
    {
        #region Custom properties


        const bool c_ShowReadMore = true;
        const string c_ReadMoreURL = "#";
        const string c_HeaderColor = "purple";


        // Private variables
        private string _ReadMoreUrl;
        private bool _ShowReadMore;
        private string _HeaderColor;

        public string ReadMoreUrl
        {
            get
            {
                return _ReadMoreUrl;
            }
            set
            {
                _ReadMoreUrl = value;
            }
        }

        public bool ShowReadMore
        {
            get
            {
                return _ShowReadMore;
            }
            set
            {
                _ShowReadMore = value;
            }
        }

        public string HeaderColor
        {
            get
            {
                return _HeaderColor;
            }
            set
            {
                _HeaderColor = value;
            }
        }


        #endregion
        // Constructor
        public CustomContentQuery()
        {
            _HeaderColor = c_HeaderColor;
            _ReadMoreUrl = c_ReadMoreURL;
            _ShowReadMore = c_ShowReadMore;
        }


        public override ToolPart[] GetToolParts()
        {
            List<ToolPart> toolPart = new List<ToolPart>(base.GetToolParts());
            toolPart.Insert(0, new CustomToolPart());
            return toolPart.ToArray();
        }

        protected override void CreateChildControls()
        {
            try
            {
                this.ChromeType = PartChromeType.None;

                base.CreateChildControls();
            }
            catch (Exception ex)
            {


            }

        }
        protected override void Render(HtmlTextWriter writer)
        {
            try
            {
                string wpHeader = string.Empty;
                string wpFooter = string.Empty;
                string wPFooterWithReadMore = string.Empty;
                string wPFooterWithoughtReadMore = string.Empty;

                #region format HTML for WebPart header and footer

                wpHeader = @"<table class='{0}' border='0' cellspacing='0' cellpadding='0'>
                            <tr>
                                <td align='left' valign='top'>
                                    <h1><span>{1}</span></h1>
                                </td>
                            </tr>
                            <tr>
                                <td>";

                wPFooterWithReadMore =
                            @" </td>
                        </tr>
                        <tr>
                            <td class='read_more'>
                                <a href='{0}' target='_self'>Read more >></a>
                            </td>
                        </tr>
                    </table>";

                wPFooterWithoughtReadMore = @"     </td>
                        </tr>
                    </table>";

                #endregion

                //Dynamically change css class 
                wpHeader = string.Format(wpHeader, HeaderColor.ToString(), this.Title);
                wPFooterWithReadMore = string.Format(wPFooterWithReadMore, ReadMoreUrl);
                wpFooter = (ShowReadMore == true ? wPFooterWithReadMore : wPFooterWithoughtReadMore);

                // Render Wbpart
                writer.Write(wpHeader);
                base.Render(writer);
                writer.Write(wpFooter);
            }
            catch (Exception ex)
            {


            }
        }

    }
}
