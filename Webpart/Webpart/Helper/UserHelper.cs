using Microsoft.Office.Server.UserProfiles;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Linq;
using System.Text;

namespace Webpart.Helper
{
    public static class UserHelper
    {
        public static string GetUserGroups(this SPUser user)
        {
            string groupsString = "";

            try
            {
                user.Groups.Cast<SPGroup>().ToList().ForEach(group =>
                    {
                        groupsString += group.Name + "; ";
                    });
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(
                   0,
                   new SPDiagnosticsCategory(
                       new StringBuilder().Append(nameof(CustomVisualWebPart)).Append(" ").Append(nameof(GetUserGroups)).ToString(),
                       TraceSeverity.Unexpected,
                       EventSeverity.Error),
                   TraceSeverity.Unexpected,
                   ex.Message,
                   ex.StackTrace);
                throw;
            }

            return groupsString;
        }

        public static string GetUserProfileProperties(this SPUser user, SPContext context)
        {
            string propertiesString = "";

            try
            {
                SPServiceContext serviceContext = SPServiceContext.GetContext(context.Site);
                UserProfileManager userProfileManager = new UserProfileManager(serviceContext);
                UserProfile userProfile = userProfileManager.GetUserProfile(user.LoginName);

                foreach (Property p in userProfile.ProfileManager.Properties)
                {
                    propertiesString += p.DisplayName + " -> " + userProfile[p.Name].Value;
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(
                   0,
                   new SPDiagnosticsCategory(
                       new StringBuilder().Append(nameof(CustomVisualWebPart)).Append(" ").Append(nameof(GetUserProfileProperties)).ToString(),
                       TraceSeverity.Unexpected,
                       EventSeverity.Error),
                   TraceSeverity.Unexpected,
                   ex.Message,
                   ex.StackTrace);
                throw;
            }

            return propertiesString;
        }
    }
}
