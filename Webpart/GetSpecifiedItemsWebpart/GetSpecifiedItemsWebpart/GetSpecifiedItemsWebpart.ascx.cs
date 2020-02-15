using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Publishing;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls.WebParts;
using static GetSpecifiedItemsWebpart.Helper.Helper;
using static GetSpecifiedItemsWebpart.Mapping;

namespace GetSpecifiedItemsWebpart.GetSpecifiedItemsWebpart
{
    [ToolboxItemAttribute(false)]
    public partial class GetSpecifiedItemsWebpart : WebPart
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #region Events

        protected void BtnGetItems_Click(object sender, EventArgs e)
        {
            try
            {
                var txtEmployeeIdListString = TxtEmployeeIdList.Text;

                if (txtEmployeeIdListString != string.Empty)
                {
                    var time = GetCurrentTimestamp();

                    var spWeb = SPContext.Current.Web;
                    var spList = spWeb.Lists.TryGetList(LIST_NAME);
                    if (spList != null)
                    {
                        CrossListQueryInfo crossQuery = new CrossListQueryInfo();
                        crossQuery.Query = GetQueryString(whereConditionsList: txtEmployeeIdListString.Split(',').ToList());
                        crossQuery.UseCache = true;
                        crossQuery.Lists = string.Format("<Lists><List ID='{0}' /></Lists>", spList.ID.ToString("D"));
                        crossQuery.ViewFields = GetViewFields();

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
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(
                    0,
                    new SPDiagnosticsCategory(
                        new StringBuilder()
                        .Append(nameof(GetSpecifiedItemsWebpart))
                        .Append(" ")
                        .Append(nameof(BtnGetItems_Click))
                        .ToString(),
                        TraceSeverity.Unexpected,
                        EventSeverity.Error),
                    TraceSeverity.Unexpected,
                    ex.Message,
                    ex.StackTrace);
            }
        }

        #endregion
    }
}
