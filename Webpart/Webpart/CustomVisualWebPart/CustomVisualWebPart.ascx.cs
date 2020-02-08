using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Publishing;
using Microsoft.SharePoint.Utilities;
using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI.WebControls.WebParts;
using static Webpart.Mapping;
using static Webpart.Helper.UserHelper;

namespace Webpart.CustomVisualWebPart
{
    [ToolboxItemAttribute(false)]
    public partial class CustomVisualWebPart : WebPart
    {      
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                SPUser user = SPContext.Current.Web.CurrentUser;
                LbUserData.Text = new StringBuilder()
                    .Append("LoginName: ")
                    .Append(user.LoginName)
                    .Append(" IsSiteAdmin: ")
                    .Append(user.IsSiteAdmin)
                    .ToString();

                LbUserGroups.Text = user.GetUserGroups();

                LbUserProperties.Text = user.GetUserProfileProperties(SPContext.Current);
            }
        }

        protected void BtnGetItemFirstMethod_Click(object sender, EventArgs e)
        {
            try
            {
                var time = GetCurrentTimestamp();

                var spWeb = SPContext.Current.Web;
                var spList = spWeb.Lists.TryGetList(LIST_NAME);
                if (spList != null)
                {
                    var spQuery = new SPQuery();
                    spQuery.Query = "<Where></Where><OrderBy><FieldRef Name='ID' Ascending='False' /></OrderBy>";

                    var spListItemCollection = spList.GetItems(spQuery);

                    if (spListItemCollection.Count > 0)
                    {
                        GridView.DataSource = spListItemCollection.GetDataTable();
                        GridView.DataBind();
                    }
                }

                LbTime.Text = Convert.ToString(GetCurrentTimestamp() - time);
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(
                    0, 
                    new SPDiagnosticsCategory(
                        new StringBuilder().Append(nameof(CustomVisualWebPart)).Append(" ").Append(nameof(BtnGetItemFirstMethod_Click)).ToString(),
                        TraceSeverity.Unexpected,
                        EventSeverity.Error), 
                    TraceSeverity.Unexpected, 
                    ex.Message, 
                    ex.StackTrace);
            }
        }

        protected void BtnGetItemSecondMethod_Click(object sender, EventArgs e)
        {
            try
            {
                var time = GetCurrentTimestamp();

                var spWeb = SPContext.Current.Web;
                var spList = spWeb.Lists.TryGetList(LIST_NAME);
                if (spList != null)
                {
                    CrossListQueryInfo crossQuery = new CrossListQueryInfo();
                    crossQuery.Query = "<Where></Where><OrderBy><FieldRef Name='ID' Ascending='False' /></OrderBy>";
                    crossQuery.UseCache = true;
                    crossQuery.Lists = string.Format("<Lists><List ID='{0}' /></Lists>", spList.ID.ToString("D"));
                    crossQuery.ViewFields = "<FieldRef Name=\"Title\" /><FieldRef Name=\"ID\" /><FieldRef Name=\"Author\" />";

                    CrossListQueryCache cache = new CrossListQueryCache(crossQuery);
                    cache.GetSiteData(spWeb.Site);

                    var resultTable = cache.GetSiteData(spWeb);

                    if (resultTable.Rows.Count > 0)
                    {
                        GridView.DataSource = resultTable;
                        GridView.DataBind();
                    }
                }

                LbTime.Text = Convert.ToString(GetCurrentTimestamp() - time);
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(
                    0,
                    new SPDiagnosticsCategory(
                        new StringBuilder().Append(nameof(CustomVisualWebPart)).Append(" ").Append(nameof(BtnGetItemSecondMethod_Click)).ToString(),
                        TraceSeverity.Unexpected,
                        EventSeverity.Error),
                    TraceSeverity.Unexpected,
                    ex.Message,
                    ex.StackTrace);
            }
        }

        protected void BtnAddNewItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChAddItemAsFarmAdmin.Checked)
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate ()
                    {
                        using (SPSite site = new SPSite(SPContext.Current.Web.Site.Url))
                        {
                            using (SPWeb web = site.OpenWeb())
                            {
                                var previousAllowUnsafeUpdates = web.AllowUnsafeUpdates;
                                web.AllowUnsafeUpdates = true;

                                AddCustomItem(web.Lists.TryGetList(LIST_NAME));

                                web.AllowUnsafeUpdates = previousAllowUnsafeUpdates;
                            }
                        }
                    });
                }
                else
                {
                    AddCustomItem(SPContext.Current.Web.Lists.TryGetList(LIST_NAME));
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(
                    0,
                    new SPDiagnosticsCategory(
                        new StringBuilder().Append(nameof(CustomVisualWebPart)).Append(" ").Append(nameof(BtnAddNewItem_Click)).ToString(),
                        TraceSeverity.Unexpected,
                        EventSeverity.Error),
                    TraceSeverity.Unexpected,
                    ex.Message,
                    ex.StackTrace);
            }
        }

        private void AddCustomItem(SPList spList)
        {
            if (spList == null)
                throw new ArgumentNullException(nameof(spList));

            var spItem = spList.Items.Add();
            spItem[COLUMN_TITLE] = "newItem" + GetCurrentTimestamp();
            spItem.Update();
        }

        protected void BtnAddMonitoredScopeToDevDashboard_Click(object sender, EventArgs e)
        {
            using (SPMonitoredScope scope = new SPMonitoredScope(
                new StringBuilder().Append(nameof(CustomVisualWebPart)).Append(".").Append(nameof(BtnAddMonitoredScopeToDevDashboard_Click)).ToString()))
            {
                System.Threading.Thread.Sleep(2000);
            }
        }

        private double GetCurrentTimestamp() => Convert.ToDouble(DateTime.Now.ToString("yyyyMMddHHmmssffff"));
    }
}
