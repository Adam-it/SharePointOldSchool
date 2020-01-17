using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Text;
using static SharedLogic.Helpers.ListHelpers;
using static SharedLogic.Mapping.Mapping;

namespace EventReceiver.ListEventReceiver
{
    /// <summary>
    /// Wyświetl listę zdarzeń elementów
    /// </summary>
    public class ListEventReceiver : SPItemEventReceiver
    {
        /// <summary>
        /// Trwa dodawanie elementu.
        /// </summary>
        public override void ItemAdding(SPItemEventProperties properties)
        {
            base.ItemAdding(properties);
            bool result = properties.ListItem.AddLogListItem(properties.Web, nameof(ItemAdding));
            if (result)
            {
                SPDiagnosticsService.Local.WriteTrace(0,
                    new SPDiagnosticsCategory(new StringBuilder().Append(nameof(ListEventReceiver)).Append(nameof(ItemAdding)).ToString(),
                    TraceSeverity.Unexpected, EventSeverity.Error),
                    TraceSeverity.Unexpected, "log item error", null);
            }
        }

        /// <summary>
        /// Trwa aktualizowanie elementu.
        /// </summary>
        public override void ItemUpdating(SPItemEventProperties properties)
        {
            base.ItemUpdating(properties);
            bool result = properties.ListItem.AddLogListItem(properties.Web, nameof(ItemUpdating));
            if (result)
            {
                SPDiagnosticsService.Local.WriteTrace(0,
                    new SPDiagnosticsCategory(new StringBuilder().Append(nameof(ListEventReceiver)).Append(nameof(ItemUpdating)).ToString(),
                    TraceSeverity.Unexpected, EventSeverity.Error),
                    TraceSeverity.Unexpected, "log item error", null);
            }
        }

        /// <summary>
        /// Trwa usuwanie elementu.
        /// </summary>
        public override void ItemDeleting(SPItemEventProperties properties)
        {
            base.ItemDeleting(properties);

            // validate item if may be deleted
            if (properties.ListItem.Title.Contains("NoDelete"))
            {
                properties.ErrorMessage = "this item may not be deleted";
                properties.Status = SPEventReceiverStatus.CancelWithError;
            }

            bool result = properties.ListItem.AddLogListItem(properties.Web, nameof(ItemDeleting));
            if (result)
            {
                SPDiagnosticsService.Local.WriteTrace(0,
                    new SPDiagnosticsCategory(new StringBuilder().Append(nameof(ListEventReceiver)).Append(nameof(ItemDeleting)).ToString(),
                    TraceSeverity.Unexpected, EventSeverity.Error),
                    TraceSeverity.Unexpected, "log item error", null);
            }
        }

        /// <summary>
        /// Dodano element.
        /// </summary>
        public override void ItemAdded(SPItemEventProperties properties)
        {
            base.ItemAdded(properties);
            bool result = properties.ListItem.AddLogListItem(properties.Web, nameof(ItemAdded));
            if (result)
            {
                SPDiagnosticsService.Local.WriteTrace(0,
                    new SPDiagnosticsCategory(new StringBuilder().Append(nameof(ListEventReceiver)).Append(nameof(ItemAdded)).ToString(),
                    TraceSeverity.Unexpected, EventSeverity.Error),
                    TraceSeverity.Unexpected, "log item error", null);
            }
        }

        /// <summary>
        /// Zaktualizowano element.
        /// </summary>
        public override void ItemUpdated(SPItemEventProperties properties)
        {
            base.ItemUpdated(properties);
            bool result = properties.ListItem.AddLogListItem(properties.Web, nameof(ItemUpdated));
            if (result)
            {
                SPDiagnosticsService.Local.WriteTrace(0,
                     new SPDiagnosticsCategory(new StringBuilder().Append(nameof(ListEventReceiver)).Append(nameof(ItemUpdated)).ToString(),
                     TraceSeverity.Unexpected, EventSeverity.Error),
                     TraceSeverity.Unexpected, "log item error", null);
            }

            try
            {
                using (DisabledEventsScope scope = new DisabledEventsScope())
                {
                    SPListItem listItem = properties.ListItem;
                    listItem[COLUMN_TITLE] += " added content without never ending loop";
                    listItem.Update();
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0,
                    new SPDiagnosticsCategory(new StringBuilder().Append(nameof(ListEventReceiver)).Append(nameof(ItemUpdated)).ToString(), TraceSeverity.Unexpected, EventSeverity.Error),
                    TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Usunięto element.
        /// </summary>
        public override void ItemDeleted(SPItemEventProperties properties)
        {
            base.ItemDeleted(properties);
            bool result = properties.ListItem.AddLogListItem(properties.Web, nameof(ItemDeleted));
            if (result)
            {
                SPDiagnosticsService.Local.WriteTrace(0,
                    new SPDiagnosticsCategory(new StringBuilder().Append(nameof(ListEventReceiver)).Append(nameof(ItemDeleted)).ToString(),
                    TraceSeverity.Unexpected, EventSeverity.Error),
                    TraceSeverity.Unexpected, "log item error", null);
            }
        }


    }
}