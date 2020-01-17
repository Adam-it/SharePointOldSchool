using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Text;
using static SharedLogic.Mapping.Mapping;

namespace SharedLogic.Helpers
{
    public static class ListHelpers
    {
        /// <summary>
        /// log item data to log list
        /// </summary>
        /// <param name="targetItem"></param>
        /// <param name="web"></param>
        /// <param name="customMessage"></param>
        /// <returns></returns>
        public static bool AddLogListItem(this SPListItem targetItem, SPWeb web, string customMessage = "")
        {
            bool LogAdded = true;

            try
            {
                string itemId = targetItem == null ? "null" : targetItem.ID.ToString();
                AddLog(new StringBuilder().Append(itemId).Append(" ").Append(customMessage).ToString(), web);
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0,
                    new SPDiagnosticsCategory(nameof(AddLogListItem), TraceSeverity.Unexpected, EventSeverity.Error),
                    TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
                LogAdded = false;
            }

            return LogAdded;
        }

        /// <summary>
        /// add user data to log list
        /// </summary>
        /// <param name="targetProperties"></param>
        /// <param name="web"></param>
        /// <param name="customMessage"></param>
        /// <returns></returns>
        public static bool AddLogListItem(this SPSecurityEventProperties targetProperties, SPWeb web, string customMessage = "")
        {
            bool LogAdded = true;

            try
            {
                string stringLoginName = targetProperties.Web.AllUsers.GetByID(targetProperties.GroupUserId).LoginName;
                AddLog(new StringBuilder().Append(stringLoginName).Append(" ").Append(customMessage).ToString(), web);
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0,
                    new SPDiagnosticsCategory(nameof(AddLogListItem), TraceSeverity.Unexpected, EventSeverity.Error),
                    TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
                LogAdded = false;
            }

            return LogAdded;
        }

        private static void AddLog(string message, SPWeb web)
        {
            try
            {
                if (web == null)
                    throw new ArgumentNullException(nameof(web));

                SPList list = web.Lists.TryGetList(SPLIST_LOG_LIST);
                if (list == null)
                    throw new Exception("log list does not exist on this web");

                SPListItem item = list.Items.Add();
                item[COLUMN_TITLE] = message;
                item.Update();
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0,
                    new SPDiagnosticsCategory(nameof(AddLog), TraceSeverity.Unexpected, EventSeverity.Error),
                    TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
                throw;
            }
        }
    }
}
