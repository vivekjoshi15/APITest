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

    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "registerUser/{fname}/{lname}/{Email}/{phone}/{password}")]
    [OperationContract]
    List<ResponseEmployee> addDetails(string fname, string lname, string email, string phone, string password);

    [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "ChkUserLogin")]
    [OperationContract]
    string ChkUserLogin(System.IO.Stream pStream);

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

[DataContract]
public partial class ResponseEmployee
{
    [DataMember]
    public string result { get; set; }    
    [DataMember]
    public string FirstName { get; set; }
    [DataMember]
    public string LastNAme { get; set; }
    [DataMember]
    public string Email { get; set; }
    [DataMember]
    public string Phone { get; set; }
    [DataMember]
    public string Password { get; set; }
    [DataMember]
    public int Id { get; set; }
}