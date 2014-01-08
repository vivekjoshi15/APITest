using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VNTAPI
{
    public partial class sendMessage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                VNTEntities dc = new VNTEntities();
                var listdevices = (from a in dc.gcmRegistrations
                                   select a).ToList(); 
                DropDownList1.DataTextField = "deviceRegistrationId";
                DropDownList1.DataValueField = "registrationId";
                DropDownList1.DataSource = listdevices;
                DropDownList1.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            UserService objUS = new UserService();
            RequestGCMPass objGCMPass = new RequestGCMPass();
            objGCMPass.tickerText = "Ticker text for GCM";
            objGCMPass.message = txtMessage.Text;             
            objGCMPass.contentTitle = txtTitle.Text;
            objUS.sendGCMPost(objGCMPass);
        }
    }
}