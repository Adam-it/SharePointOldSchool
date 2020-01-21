using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace TimerJob.Features.CustomTimerJobFeature
{
    /// <summary>
    /// Ta klasa obsługuje zdarzenia zgłaszane podczas aktywacji, dezaktywacji, instalacji, odinstalowywania i uaktualniania funkcji.
    /// </summary>
    /// <remarks>
    /// Identyfikator GUID podłączony do tej klasy może być używany podczas pakowania; nie powinien być modyfikowany.
    /// </remarks>

    [Guid("d14d3b9e-eee7-4765-b640-a56f458b7f57")]
    public class CustomTimerJobFeatureEventReceiver : SPFeatureReceiver
    {
        const string JobName = "CustomTimerJob";
        // Usuń komentarz z poniższej metody, aby obsłużyć zdarzenie zgłoszone po aktywacji funkcji.
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    // add job
                    SPWebApplication parentWebApp = (SPWebApplication)properties.Feature.Parent;
                    DeleteExistingJob(JobName, parentWebApp);
                    CreateJob(parentWebApp);
                });
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("CustomJobEventReceiver-FeatureActivated", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }
        }


        // Usuń komentarz z poniższej metody, aby obsłużyć zdarzenie zgłoszone przed dezaktywacją funkcji.
        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            lock (this)
            {
                try
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate ()
                    {
                        // delete job
                        SPWebApplication parentWebApp = (SPWebApplication)properties.Feature.Parent;
                        DeleteExistingJob(JobName, parentWebApp);
                    });
                }
                catch (Exception ex)
                {
                    SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("CustomJobEventReceiver-FeatureDeactivating", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
                }
            }
        }

        #region addRemoveJobHelpers

        private bool CreateJob(SPWebApplication site)
        {
            bool jobCreated = false;
            try
            {
                // schedule job for once a day
                CustomTimerJob job = new CustomTimerJob(JobName, site);
                SPDailySchedule schedule = new SPDailySchedule();
                schedule.BeginHour = 0;
                schedule.EndHour = 1;
                job.Schedule = schedule;

                job.Update();
            }
            catch (Exception)
            {
                return jobCreated;
            }
            return jobCreated;
        }

        public bool DeleteExistingJob(string jobName, SPWebApplication site)
        {
            bool jobDeleted = false;
            try
            {
                foreach (SPJobDefinition job in site.JobDefinitions)
                {
                    if (job.Name == jobName)
                    {
                        job.Delete();
                        jobDeleted = true;
                    }
                }
            }
            catch (Exception)
            {
                return jobDeleted;
            }
            return jobDeleted;
        }

        #endregion

        // Usuń komentarz z poniższej metody, aby obsłużyć zdarzenie zgłoszone po zainstalowaniu funkcji.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Usuń komentarz z poniższej metody, aby obsłużyć zdarzenie zgłoszone przed odinstalowaniem funkcji.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Usuń komentarz z poniższej metody, aby obsłużyć zdarzenie zgłoszone w trakcie uaktualniania funkcji.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
