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

namespace VNTAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service.svc or Service.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Single)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service : IService
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
                             select a).ToList();
                if (chkId == null)
                {
                    gcmRegistration obj = new gcmRegistration();
                    obj.deviceRegistrationId = regId;
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
}
