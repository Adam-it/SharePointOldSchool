using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Text;
using static TimerJob.Mapping;

namespace TimerJob
{
    public class CustomTimerJob : SPJobDefinition
    {
        public CustomTimerJob() : base() { }
        public CustomTimerJob(string jobName, SPService service) : base(jobName, service, null, SPJobLockType.None)
        {
            this.Title = jobName;
        }

        public CustomTimerJob(string jobName, SPWebApplication webapp) : base(jobName, webapp, null, SPJobLockType.ContentDatabase)
        {
            this.Title = jobName;
        }

        /// <summary>
        /// Custom Timer Job
        /// </summary>
        /// <param name="targetInstanceId"></param>
        public override void Execute(Guid targetInstanceId)
        {
            SPWebApplication webApp = this.Parent as SPWebApplication;
            try
            {
                if (webApp.Sites.Count > 0)
                {
                    SPSiteCollection sitesCollection = webApp.Sites;
                    foreach (SPSite site in sitesCollection)
                    {
                        // check if in properties of web a custom property is added (this property is added via powershell)
                        // if present and set to string true then run job
                        SPWeb web = site.OpenWeb();
                        if (web.Properties.ContainsKey(PROP_CUSTOM_TIMER_JOB) && web.Properties[PROP_CUSTOM_TIMER_JOB] == "true")
                        {
                            // check if log list exists and proceed
                            SPList list = null;
                            list = web.Lists.TryGetList(SPLIST_LOG_LIST);
                            if (list == null)
                                throw new Exception(new StringBuilder().Append(SPLIST_LOG_LIST).Append(" list does not exist on this web").ToString());

                            // add log item
                            SPListItem item = list.AddItem();
                            item[COLUMN_TITLE] = new StringBuilder().Append("CustomTimerJob Was runing ").Append(DateTime.Now.ToLongDateString()).ToString();
                            item.Update();
                        }
                        web.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("CustomTimerJob - Execute", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }
        }
    }
}
