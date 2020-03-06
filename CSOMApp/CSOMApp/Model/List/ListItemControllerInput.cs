using Microsoft.SharePoint.Client;
using System.Collections.Generic;

namespace CSOMApp.Model
{
    public struct ListItemControllerInput
    {
        public ClientContext Context { get; set; }
        public List List { get; set; }
        public List<ListItemInput> ListItems { get; set; }
    }
}
