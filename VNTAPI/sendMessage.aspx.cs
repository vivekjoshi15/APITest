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
            {
                AndroidPush();  // calling android push method                                                                               
            }
        }

        private void AndroidPush()
        {
            // your RegistrationID paste here which is received from GCM server.                                                               
            string regId = DropDownList1.SelectedItem.Text;
            // applicationID means google Api key                                                                                                     
            var applicationID = "AIzaSyAJkf4VSYCFeFmpc3j2hVUWTrIQMGd66HY";
            // SENDER_ID is nothing but your ProjectID (from API Console- google code)//                                          
            var SENDER_ID = "287088860819";

            var value = TextBox1.Text; //message text box                                                                               

            WebRequest tRequest;

            tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");

            tRequest.Method = "post";

            tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";

            tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));

            tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

            //Data post to server                                                                                                                                         
            string postData =
           "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message="
                  + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" +
                     regId + "";

            Console.WriteLine(postData);

            Byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            tRequest.ContentLength = byteArray.Length;

            Stream dataStream = tRequest.GetRequestStream();   
            dataStream.Write(byteArray, 0, byteArray.Length);  
            dataStream.Close();

            WebResponse tResponse = tRequest.GetResponse();

            dataStream = tResponse.GetResponseStream();

            StreamReader tReader = new StreamReader(dataStream);

            String sResponseFromServer = tReader.ReadToEnd();   //Get response from GCM server.
            if (sResponseFromServer == "Error=InvalidRegistration")
            {
                Label1.Text = "Error=InvalidRegistration";//Assigning GCM response to Label text 
            }
            else
            {
                Label1.Text = "Message Sent";
            }

            tReader.Close();

            dataStream.Close();
            tResponse.Close();
        }    
    }
}