using Microsoft.SharePoint;
using System;
using System.Web.UI;

namespace Components.Webpart.ExampleVisualWebPart
{
    public partial class ExampleVisualWebPartUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LbWebTitle.Text = SPContext.Current.Web.Title;
        }
    }
}
