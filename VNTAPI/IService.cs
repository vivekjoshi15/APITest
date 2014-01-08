﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;

namespace VNTAPI
{
    public interface IService
    {
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "RegisterRegId")]
        [OperationContract]
        string RegisterRegId(System.IO.Stream pStream);
    }
}
