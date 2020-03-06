using Microsoft.SharePoint.Client;

namespace CSOMApp.Model
{
    public struct ListControllerInput
    {
        public ClientContext Context { get; set; }
        public string ListTitle { get; set; }
        public ListTemplateType Type { get; set; }
    }
}
