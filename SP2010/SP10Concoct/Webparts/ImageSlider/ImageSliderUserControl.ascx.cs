using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace SP10Concoct.Webparts.ImageSlider
{
    public partial class ImageSliderUserControl : UserControl
    {

        public ImageSlider ImageSliderCustomControl { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GetSliderItemsFromList();

                }
            }
            catch (Exception ex)
            {


            }
        }


        protected void GetSliderItemsFromList()
        {
            try
            {

                SPList carouselConfig = SPContext.Current.Web.Lists.TryGetList(ImageSliderCustomControl.ListName);
                StringBuilder sbMarkup = new StringBuilder();
                if (carouselConfig != null)
                {
                    SPListItemCollection sliderItemColl = ExecuteQuery(carouselConfig);
                    foreach (SPListItem item in sliderItemColl)
                    {
                        string redirectionUrl = string.Empty;
                        string imageTitleText = Convert.ToString(item["Title"]);
                        string imageDescriptionText = Convert.ToString(item["SummaryText"]);
                        string imageUrl = GetHyperLinkFieldUrl(Convert.ToString(item["ImageLink"]));
                        if (!string.IsNullOrEmpty(Convert.ToString(item["ArticlePageLink"])))
                        {
                            redirectionUrl = Convert.ToString(item["ArticlePageLink"].ToString().Split(',')[0]);
                        }
                        string linkAttributes = "";
                        if (ImageSliderCustomControl.OpenLinkInNewWindow)
                        {
                            linkAttributes = "target='_blank'";
                        }

                        sbMarkup.AppendLine(CreateSliderMarkup(imageUrl, imageTitleText, redirectionUrl, linkAttributes));

                    }

                    ltrImageSlider.Text = sbMarkup.ToString();

                }
            }
            catch (Exception ex)
            {


            }

        }

        private SPListItemCollection ExecuteQuery(SPList list)
        {
            SPQuery qry = new SPQuery();
            qry.Query = @"<Where>
                            <Eq>
                                <FieldRef Name='IsActive' />
                                <Value Type='Boolean'>1</Value>
                            </Eq>
                        </Where>
                        <OrderBy>
                            <FieldRef Name='DisplayOrder' Ascending='True' />
                        </OrderBy>
                        <ViewFields>
                            <FieldRef Name='Title' />
                            <FieldRef Name='SummaryText' />
                            <FieldRef Name='ImageLink' />
                            <FieldRef Name='ArticlePageLink' />
                        </ViewFields>";

            return list.GetItems(qry);
        }


        protected string CreateSliderMarkup(string imageUrl, string imageDescription, string redirectionUrl, string linkAttributes)
        {
            StringBuilder sbSliderMarkup = new StringBuilder();
            sbSliderMarkup.Append("<li>");
            sbSliderMarkup.Append("<img src='" + imageUrl + "' title='' />"); // add image linkj here
            sbSliderMarkup.Append("<span style='float:left;'>" + imageDescription);  //appendimage desc here
            sbSliderMarkup.Append("<br/>");
            sbSliderMarkup.Append("<br/>");
            sbSliderMarkup.Append("<span class='links'>");
            sbSliderMarkup.Append("<a href='" + redirectionUrl + "' " + linkAttributes + "  > find out more ></a>");//add redirection url here
            sbSliderMarkup.Append("</span>");
            sbSliderMarkup.Append("</span>");
            sbSliderMarkup.Append("</li>");

            return sbSliderMarkup.ToString();



        }



        #region Utilities

        protected string GetHyperLinkFieldUrl(object fieldValue)
        {

            SPFieldUrlValue value = new SPFieldUrlValue(Convert.ToString(fieldValue));

            string URL = value.Url;

            return URL;

        }




        #endregion
    }
}
