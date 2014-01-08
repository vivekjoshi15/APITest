using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;

[ServiceContract]
public interface IUserService
{
    [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "RegisterRegId")]
    [OperationContract]
    string RegisterRegId(System.IO.Stream pStream);

    [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "sendGCMPost")]
    [OperationContract]
    ResponeGCMPass sendGCMPost(RequestGCMPass objRequestGCMPass);
}

[DataContract]
public class RequestGCMPass
{
    [DataMember]
    public string result { get; set; }
    [DataMember]
    public string tickerText { get; set; }
    [DataMember]
    public string contentTitle { get; set; }
    [DataMember]
    public string message { get; set; }
}

[DataContract]
public class ResponeGCMPass
{
    [DataMember]
    public string result { get; set; }
    [DataMember]
    public string message { get; set; }
}