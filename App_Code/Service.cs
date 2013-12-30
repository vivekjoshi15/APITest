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

[ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Single)]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{    
    DataClassesDataContext dc = new DataClassesDataContext();
    public List<ResponseEmployee> addDetails(string fname, string lname, string email, string phone, string addresss)
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
                objEmp.Address = addresss;                
                objResult.Add(objEmp);                
                tbEmployee obj = new tbEmployee();
                obj.FirstName = objEmp.FirstName; 
                obj.LastNAme=objEmp.LastNAme;
                obj.Email=objEmp.Email;
                obj.Phone=objEmp.Phone;
                obj.Address=objEmp.Address;
                dc.tbEmployees.InsertOnSubmit(obj);
                dc.SubmitChanges();                
                return objResult;
            }
            catch (Exception ex)
            {
                objEmp.result = "fail";
                objEmp.message = ex.Message;
                objResult.Add(objEmp);
                return objResult;
            }
            
    }

    public List<ResponseEmployee> updatedetails(string UserId, string fname, string lname, string email, string phone, string addresss)
    {
        int uid = Convert.ToInt32(UserId);
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
            objEmp.Address = addresss;
            objResult.Add(objEmp);
            tbEmployee obj = (from a in dc.GetTable<tbEmployee>()
                                   where a.Id == uid
                                   select a).FirstOrDefault();
            obj.FirstName = objEmp.FirstName;
            obj.LastNAme = objEmp.LastNAme;
            obj.Email = objEmp.Email;
            obj.Phone = objEmp.Phone;
            obj.Address = objEmp.Address;
            dc.SubmitChanges();
            return objResult;
        }
        catch (Exception ex)
        {
            objEmp.result = "fail";
            objEmp.message = ex.Message;
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
           
            var editEmp = (from a in dc.tbEmployees
                           where a.Id == uid
                           select a).SingleOrDefault();
            objEmp.Id = editEmp.Id;
            objEmp.FirstName = editEmp.FirstName;
            objEmp.LastNAme = editEmp.LastNAme;
            objEmp.Phone = editEmp.Phone;
            objEmp.Address = editEmp.Address;
            objEmp.Email = editEmp.Email;
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

    public List<ResponseEmployee> getAll()
    {
        List<ResponseEmployee> objResult = new List<ResponseEmployee>();
        ResponseEmployee objEmp = new ResponseEmployee();
        try
        {
            var editEmp = (from a in dc.tbEmployees
                           select a).ToList();
            foreach (tbEmployee item in editEmp)
            {
                objEmp = new ResponseEmployee();
                objEmp.Id = item.Id;
                objEmp.FirstName = item.FirstName;
                objEmp.LastNAme = item.LastNAme;
                objEmp.Phone = item.Phone;
                objEmp.Address = item.Address;
                objEmp.Email = item.Email;
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
  
    public List<ResponseEmployee> updateUser(ResponseEmployee objPass, string UserId)
    {
        int uid = Convert.ToInt32(UserId);
        List<ResponseEmployee> objResult = new List<ResponseEmployee>();
        ResponseEmployee objResponsePass = new ResponseEmployee();

        if (objPass == null)
        {
            objResponsePass.result = "fail";
            objResponsePass.message = "Request is empty";
            objResult.Add(objResponsePass);
            return objResult;
        }

        try
        {
            if (UserId == null)
            {
                objResponsePass.result = "fail";
                objResult.Add(objResponsePass);
                return objResult;
            }
            else
            {
                objResponsePass.Address = objPass.Address;
                objResponsePass.Phone = objPass.Phone;
                objResponsePass.FirstName = objPass.FirstName;
                objResponsePass.LastNAme = objPass.LastNAme;
                objResponsePass.Email = objPass.Email;               
                objResponsePass.result = "success";
                objResult.Add(objResponsePass);
                tbEmployee newtbemp = (from a in dc.GetTable<tbEmployee>()
                                   where a.Id == uid
                                   select a).Single();
                newtbemp.FirstName = objResponsePass.FirstName;
                newtbemp.LastNAme =objResponsePass.LastNAme;
                newtbemp.Email = objResponsePass.Email;
                newtbemp.Phone = objResponsePass.Phone;
                newtbemp.Address = objResponsePass.Address;
                dc.SubmitChanges();
                return objResult;
            }
        }
        catch (Exception ex)
        {
            objResponsePass.result = "fail";
            objResponsePass.message = ex.Message;
            objResult.Add(objResponsePass);
            return objResult;
        }
    }

    public List<ResponseEmployee> deleteuser(string id)
    {
        int uid = Convert.ToInt32(id);
        List<ResponseEmployee> objResult = new List<ResponseEmployee>();
        ResponseEmployee objEmp = new ResponseEmployee();
        try
        {
            var delEmp = (from a in dc.tbEmployees
                           where a.Id == uid
                           select a).FirstOrDefault();
            dc.tbEmployees.DeleteOnSubmit(delEmp);
            dc.SubmitChanges();
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

    public List<ResponseBarcode> chkBarcode(string code)
    {
        List<ResponseBarcode> objResult = new List<ResponseBarcode>();
        ResponseBarcode objbar = new ResponseBarcode();
        try
        {
            var chkbar = (from a in dc.tbBarcodes
                          where a.barCode == code
                          select a).FirstOrDefault();
            if (chkbar != null)
            {
                objbar.result = "success";
                objResult.Add(objbar);
                return objResult;
            }
            else
            {
                objbar.result = "fail";
                objResult.Add(objbar);
                return objResult;
            }

        }
        catch
        {
            objbar.result = "fail";
            objResult.Add(objbar);
            return objResult;
        }
    
    }

    //private string SendGCMNotification(string apiKey, string postData, string postDataContentType = "application/json")
    //{
    //    ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateServerCertificate);

    //    //
    //    //  MESSAGE CONTENT
    //    byte[] byteArray = Encoding.UTF8.GetBytes(postData);

    //    //
    //    //  CREATE REQUEST
    //    HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://android.googleapis.com/gcm/send");
    //    Request.Method = "POST";
    //    Request.KeepAlive = false;
    //    Request.ContentType = postDataContentType;
    //    Request.Headers.Add(string.Format("Authorization: key={0}", apiKey));
    //    Request.ContentLength = byteArray.Length;

    //    Stream dataStream = Request.GetRequestStream();
    //    dataStream.Write(byteArray, 0, byteArray.Length);
    //    dataStream.Close();

    //    //
    //    //  SEND MESSAGE
    //    try
    //    {
    //        WebResponse Response = Request.GetResponse();
    //        HttpStatusCode ResponseCode = ((HttpWebResponse)Response).StatusCode;
    //        if (ResponseCode.Equals(HttpStatusCode.Unauthorized) || ResponseCode.Equals(HttpStatusCode.Forbidden))
    //        {
    //            var text = "Unauthorized - need new token";
    //        }
    //        else if (!ResponseCode.Equals(HttpStatusCode.OK))
    //        {
    //            var text = "Response from web service isn't OK";
    //        }

    //        StreamReader Reader = new StreamReader(Response.GetResponseStream());
    //        string responseLine = Reader.ReadToEnd();
    //        Reader.Close();

    //        return responseLine;
    //    }
    //    catch (Exception e)
    //    {
    //    }
    //    return "error";
    //}

    //public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    //{
    //    return true;
    //}

    public string RegisterRegId(System.IO.Stream pStream)
    {
        string result = string.Empty;
        try
        {
            StreamReader sr = new StreamReader(pStream);
            string request = sr.ReadToEnd();
            NameValueCollection objParams=System.Web.HttpUtility.ParseQueryString(request);
            string regId = objParams["regId"];            
            //tbGoogleId obj = new tbGoogleId();
            //obj.RegId = regId;           
            //dc.tbGoogleIds.InsertOnSubmit(obj);
            //dc.SubmitChanges();
            result = "successfully added device!"+regId;
            return result;
        }
        catch (Exception ex)
        {
            result = "fail: "+ ex.Message;
            return result;
        }

    }
}
