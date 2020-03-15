using ISAPI.Model;
using ISAPI.Model.Data;
using Microsoft.SharePoint;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace ISAPI.ISAPI.CustomISAPI
{
    [ServiceContract]
    interface ICustomISAPI
    {
        [OperationContract]
        [WebGet(UriTemplate = "TestCustomISAPI", ResponseFormat = WebMessageFormat.Json)]
        string TestCustomISAPI();

        [OperationContract]
        [WebGet(UriTemplate = "GetListItems/{listName}", ResponseFormat = WebMessageFormat.Json)]
        ServiceResponse<List<ListItem>> GetListItems(string listName);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "AddListItem",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ServiceResponse<bool> AddListItem(string listName, string itemTitle, bool isElevated);
    }
}
