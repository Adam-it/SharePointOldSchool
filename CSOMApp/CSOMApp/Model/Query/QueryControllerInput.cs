using Microsoft.SharePoint.Client;

namespace CSOMApp.Model
{
    public struct QueryControllerInput
    {
        public string ListTitle { get; set; }
        public string Query { get; set; }
    }
}
