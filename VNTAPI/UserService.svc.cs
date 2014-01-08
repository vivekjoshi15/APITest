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
}