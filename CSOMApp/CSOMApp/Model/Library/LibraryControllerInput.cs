using Microsoft.SharePoint.Client;
using System.Collections.Generic;

namespace CSOMApp.Model
{
    public struct LibraryControllerInput
    {
        public ClientContext Context { get; set; }
        public List List { get; set; }
        public List<FolderItemInput> ListItems { get; set; }
    }
}
