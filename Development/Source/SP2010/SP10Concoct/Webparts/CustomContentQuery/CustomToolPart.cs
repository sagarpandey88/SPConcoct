// -----------------------------------------------------------------------
// <copyright file="CustomToolPart.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SP10Concoct.Webparts.CustomContentQuery
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.UI.WebControls;
    using System.Web.UI;

    using Microsoft.SharePoint.Administration;

    //custom toolpart class to display the toolpart
    public class CustomToolPart : Microsoft.SharePoint.WebPartPages.ToolPart
    {
        //controls to be added on custom toolpart
        DropDownList ddHeaderColor;
        TextBox txtReadMoreURL;
        CheckBox chkShowReadMore;
        Panel pnl;


        protected override void CreateChildControls()
        {
            try
            {
                pnl = new Panel();

                pnl.Controls.Add(new LiteralControl("<b>Set Custom properties</b>"));
                pnl.Controls.Add(new LiteralControl("<br />"));
                pnl.Controls.Add(new LiteralControl("------------------------------------------"));
                pnl.Controls.Add(new LiteralControl("<br />"));

                pnl.Controls.Add(new LiteralControl("Select Header Color :"));
                ddHeaderColor = new DropDownList();
                ddHeaderColor.ID = "ddColor";
                ddHeaderColor.Items.Add("purple");
                ddHeaderColor.Items.Add("green");
                ddHeaderColor.Items.Add("dark_green");
                ddHeaderColor.Items.Add("blue");
                pnl.Controls.Add(new LiteralControl("<br />"));
                pnl.Controls.Add(ddHeaderColor);
                pnl.Controls.Add(new LiteralControl("<br />"));
                pnl.Controls.Add(new LiteralControl("------------------------------------------"));
                pnl.Controls.Add(new LiteralControl("<br />"));

                pnl.Controls.Add(new LiteralControl("Read More URL:"));
                txtReadMoreURL = new TextBox();
                txtReadMoreURL.ID = "txtReadMoreLink";
                pnl.Controls.Add(new LiteralControl("<br />"));
                pnl.Controls.Add(txtReadMoreURL);
                pnl.Controls.Add(new LiteralControl("<br />"));
                pnl.Controls.Add(new LiteralControl("------------------------------------------"));
                pnl.Controls.Add(new LiteralControl("<br />"));

                chkShowReadMore = new CheckBox();
                chkShowReadMore.ID = "chkShowReadMore";
                pnl.Controls.Add(chkShowReadMore);
                pnl.Controls.Add(new LiteralControl("Show Read More"));


                //Adding panel in custom toolpart
                this.Controls.Add(pnl);
                //setting the last set values as 
                //current values on custom toolpart
                CustomContentQuery wp = (CustomContentQuery)this.ParentToolPane.SelectedWebPart;
                if (wp != null)
                {
                    this.ddHeaderColor.SelectedValue = wp.HeaderColor;
                    this.txtReadMoreURL.Text = wp.ReadMoreUrl;
                    this.chkShowReadMore.Checked = wp.ShowReadMore;
                }

                base.CreateChildControls();
            }
            catch (Exception ex)
            {


            }

        }
        //ApplyChanges() is called to save the changes made by the user
        //through custom toolpart properties
        public override void ApplyChanges()
        {
            try
            {
                CustomContentQuery wp = (CustomContentQuery)this.ParentToolPane.SelectedWebPart;
                wp.ReadMoreUrl = txtReadMoreURL.Text;
                wp.HeaderColor = ddHeaderColor.SelectedValue;
                wp.ShowReadMore = chkShowReadMore.Checked;
                base.ApplyChanges();
            }
            catch (Exception ex)
            {


            }
        }
    }

}
