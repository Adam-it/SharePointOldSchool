using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace Components.Webpart.ExampleVisualWebPart
{
    [ToolboxItemAttribute(false)]
    public class ExampleVisualWebPart : WebPart
    {
        // Program Visual Studio może automatycznie zaktualizować tę ścieżkę, gdy zmienisz element projektu Visual Web Part.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/Components.Webpart/ExampleVisualWebPart/ExampleVisualWebPartUserControl.ascx";

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            Controls.Add(control);
        }
    }
}
