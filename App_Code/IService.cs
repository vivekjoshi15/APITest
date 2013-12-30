using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface IService
{
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "registerUser/{fname}/{lname}/{email}/{phone}/{addresss}")]
    [OperationContract]
    List<ResponseEmployee> addDetails(string fname, string lname, string email, string phone, string addresss);

    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdateUser/{UserId}/{fname}/{lname}/{email}/{phone}/{addresss}")]
    [OperationContract]
    List<ResponseEmployee> updatedetails(string UserId, string fname, string lname, string email, string phone, string addresss);

    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "getUserByID/{id}")]
    [OperationContract]
    List<ResponseEmployee> getUserByID(string id);

    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "getAll")]
    [OperationContract]
    List<ResponseEmployee> getAll();

    [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, UriTemplate = "updateUser/{UserId}")]
    [OperationContract]
    List<ResponseEmployee> updateUser(ResponseEmployee objPass, string UserId);

    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "deleteuser/{id}")]
    [OperationContract]
    List<ResponseEmployee> deleteuser(string id);

    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "chkBarcode/{code}")]
    [OperationContract]
    List<ResponseBarcode> chkBarcode(string code);

    [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "RegisterRegId")]
    [OperationContract]
    string RegisterRegId(System.IO.Stream pStream);

}

[DataContract]
public partial class ResponseEmployee
{
    [DataMember]
    public string result { get; set; }
    [DataMember]
    public string message { get; set; }
    [DataMember]
    public string FirstName { get; set; }
    [DataMember]
    public string LastNAme { get; set; }
    [DataMember]
    public string Email { get; set; }
    [DataMember]
    public string Phone { get; set; }
    [DataMember]
    public string Address { get; set; }
    [DataMember]
    public int Id { get; set; }
}

[DataContract]
public partial class ResponseBarcode
{
    [DataMember]
    public string result { get; set; }
    [DataMember]
    public string message { get; set; }
    [DataMember]
    public string barCode { get; set; }
    [DataMember]   
    public int Id { get; set; }
}

[DataContract]
public partial class RequestRegister
{
    [DataMember]
    public string regId { get; set; }
}


	