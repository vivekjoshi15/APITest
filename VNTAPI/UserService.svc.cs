using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Net;
using System.Net.Security;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Specialized;
using VNTAPI;

[ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Single)]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class UserService : IUserService
{
    VNTEntities dc = new VNTEntities();
    public string RegisterRegId(System.IO.Stream pStream)
    {
        string result = string.Empty;
        try
        {
            StreamReader sr = new StreamReader(pStream);
            string request = sr.ReadToEnd();
            NameValueCollection objParams = System.Web.HttpUtility.ParseQueryString(request);
            string regId = objParams["regId"];
            var chkId = (from a in dc.gcmRegistrations
                         where a.deviceRegistrationId == regId
                         select a).FirstOrDefault();
            if (chkId == null)
            {
                gcmRegistration obj = new gcmRegistration();
                obj.deviceRegistrationId = regId;
                obj.createdDate = DateTime.Now;
                dc.gcmRegistrations.Add(obj);
                dc.SaveChanges();
                result = "Successfully added device!";
                return result;
            }
            else
            {
                result = "Device Already Added!!";
                return result;
            }
        }
        catch (Exception ex)
        {
            result = "fail: " + ex.Message;
            return result;
        }

    }

    public string ChkUserLogin(System.IO.Stream pStream)
    {
        string result = string.Empty;
        try
        {
            StreamReader sr = new StreamReader(pStream);
            string request = sr.ReadToEnd();
            NameValueCollection objParams = System.Web.HttpUtility.ParseQueryString(request);
            string UserName = objParams["uName"];
            string Password = objParams["uPass"];
            var chkId = (from a in dc.tbUserDetails
                         where a.userEmail == UserName && a.userPassword== Password
                         select a).FirstOrDefault();
            if (chkId != null)
            {    
                result = "True";
                return result;
            }
            else
            {
                result = "False";
                return result;
            }
        }
        catch (Exception ex)
        {
            result = "fail: " + ex.Message;
            return result;
        }

    }

    public List<ResponseEmployee> addDetails(string fname, string lname, string email, string phone, string password)
    {
        List<ResponseEmployee> objResult = new List<ResponseEmployee>();
        ResponseEmployee objEmp = new ResponseEmployee();
        try
        {
            objEmp.result = "success";
            objEmp.FirstName = fname;
            objEmp.FirstName = fname;
            objEmp.LastNAme = lname;
            objEmp.Email = email;
            objEmp.Phone = phone;
            objEmp.Password = password;
            objResult.Add(objEmp);
            tbUserDetail obj = new tbUserDetail();
            obj.userFirstName = objEmp.FirstName;
            obj.userLastName = objEmp.LastNAme;
            obj.userEmail = objEmp.Email;
            obj.userPhone = objEmp.Phone;
            obj.userPassword = objEmp.Password;
            dc.tbUserDetails.Add(obj);
            dc.SaveChanges();
            return objResult;
        }
        catch (Exception ex)
        {
            objEmp.result = "fail";            
            objResult.Add(objEmp);
            return objResult;
        }

    }

    public List<ResponseEmployee> getAll()
    {
        List<ResponseEmployee> objResult = new List<ResponseEmployee>();
        ResponseEmployee objEmp = new ResponseEmployee();
        try
        {
            var editEmp = (from a in dc.tbUserDetails
                           select a).ToList();
            foreach (tbUserDetail item in editEmp)
            {
                objEmp = new ResponseEmployee();
                objEmp.Id = item.userId;
                objEmp.FirstName = item.userFirstName;
                objEmp.LastNAme = item.userLastName;
                objEmp.Phone = item.userPhone;
                objEmp.Password = item.userPassword;
                objEmp.Email = item.userEmail;
                objEmp.result = "true";
                objResult.Add(objEmp);
            }
            return objResult;
        }
        catch
        {
            objEmp.result = "fail";
            objResult.Add(objEmp);
            return objResult;
        }
    }

    public List<ResponseEmployee> getUserByID(string id)
    {
        int uid = Convert.ToInt32(id);
        List<ResponseEmployee> objResult = new List<ResponseEmployee>();
        ResponseEmployee objEmp = new ResponseEmployee();
        try
        {

            var editEmp = (from a in dc.tbUserDetails
                           where a.userId == uid
                           select a).SingleOrDefault();
            objEmp.Id = editEmp.userId;
            objEmp.FirstName = editEmp.userFirstName;
            objEmp.LastNAme = editEmp.userLastName;
            objEmp.Phone = editEmp.userPhone;
            objEmp.Password = editEmp.userPassword;
            objEmp.Email = editEmp.userEmail;
            objEmp.result = "success";
            objResult.Add(objEmp);
            return objResult;
        }
        catch
        {
            objEmp.result = "fail";
            objResult.Add(objEmp);
            return objResult;
        }
    }

    public List<ResponseCompanyList> getCompanyList(string id)
    {
        int uid = Convert.ToInt32(id);
        List<ResponseCompanyList> objResult = new List<ResponseCompanyList>();
        ResponseCompanyList objEmp = new ResponseCompanyList();
        try
        {

            var editEmp = (from a in dc.tbCompanyDetails
                           where a.userId == uid
                           select a).SingleOrDefault();
            objEmp.companyId = editEmp.companyId;
            objEmp.companyName = editEmp.companyName;
            objEmp.companyDsc = editEmp.companyDsc;           
            objEmp.result = "success";
            objResult.Add(objEmp);
            return objResult;
        }
        catch
        {
            objEmp.result = "fail";
            objResult.Add(objEmp);
            return objResult;
        }
    }

    public ResponeGCMPass sendGCMPost(RequestGCMPass objRequestGCMPass)
    {   
        ResponeGCMPass response = new ResponeGCMPass();

        string GCMAPIKey = System.Web.Configuration.WebConfigurationManager.AppSettings["GCMAPIKey"].ToString();    
        string registrationIds = string.Empty;

        var deviceRegistrationIds = (from a in dc.gcmRegistrations
                     select a.deviceRegistrationId).ToList();

        foreach (var item in deviceRegistrationIds)
        {
            registrationIds += item + ",";
        }

        if (registrationIds.Length > 0)
        {
            registrationIds = registrationIds.Substring(0, registrationIds.Length - 1);
        }

        string postData = "{ \"registration_ids\": [ \"" + registrationIds + "\" ], \"data\": {\"tickerText\":\"" + objRequestGCMPass.tickerText + "\", \"contentTitle\":\"" + objRequestGCMPass.contentTitle + "\", \"message\": \"" + objRequestGCMPass.message + "\"}}";

        ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateServerCertificate);

        //  MESSAGE CONTENT
        byte[] byteArray = Encoding.UTF8.GetBytes(postData);

        //  CREATE REQUEST
        HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://android.googleapis.com/gcm/send");
        Request.Method = "POST";
        Request.KeepAlive = false;
        Request.ContentType = "application/json";
        Request.Headers.Add(string.Format("Authorization: key={0}", GCMAPIKey));
        Request.ContentLength = byteArray.Length;

        Stream dataStream = Request.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();

        //  SEND MESSAGE
        try
        {
            WebResponse Response = Request.GetResponse();
            HttpStatusCode ResponseCode = ((HttpWebResponse)Response).StatusCode;
            if (ResponseCode.Equals(HttpStatusCode.Unauthorized) || ResponseCode.Equals(HttpStatusCode.Forbidden))
            {
                response.result = "201";
                response.message = "Unauthorized - need new token";
                return response;   
            }
            else if (!ResponseCode.Equals(HttpStatusCode.OK))
            {
                response.result = "201";
                response.message = "Response from web service isn't OK";
                return response;
            }

            StreamReader Reader = new StreamReader(Response.GetResponseStream());
            string responseLine = Reader.ReadToEnd();
            Reader.Close();

            response.result = "200";
            response.message = "Message Send Successfull!!";
            return response;
        }
        catch (Exception e)
        {
            response.result = "201";
            response.message = "Error: "+e.Message;
            return response;
        }
    } 

    public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }

}