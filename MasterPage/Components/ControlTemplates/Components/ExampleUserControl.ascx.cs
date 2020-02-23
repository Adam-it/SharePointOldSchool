using Microsoft.SharePoint;
using System;
using System.Web.UI;

namespace Components.ControlTemplates.Components
{
    public partial class ExampleUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LbUserLoginName.Text = SPContext.Current.Web.CurrentUser.LoginName;
        }
    }
}
