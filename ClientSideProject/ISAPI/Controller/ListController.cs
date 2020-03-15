using ISAPI.Model.Data;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ISAPI.Controller
{
    public class ListController
    {
        private readonly SPWeb web;
        private readonly SPList list;

        public ListController(SPWeb web, string listName)
        {
            if (web == null)
                throw new ArgumentNullException(nameof(web));
            if (listName == null)
                throw new ArgumentNullException(nameof(listName));

            this.web = web;
            list = GetList(listName);
        }

        /// <summary>
        /// gets collection of list items
        /// </summary>
        /// <returns>list item collection</returns>
        public List<ListItem> GetListItems()
        {
            var result = new List<ListItem>();

            try
            {
                result = list
                    .GetItems()
                    .Cast<SPListItem>()
                    .Select(item => new ListItem()
                    {
                        Id = item.ID,
                        Title = item.Title
                    }).ToList();
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, 
                    new SPDiagnosticsCategory($"{nameof(ListController)} - {nameof(GetListItems)}", TraceSeverity.Unexpected, EventSeverity.Error), 
                    TraceSeverity.Unexpected, 
                    ex.Message, 
                    ex.StackTrace);
                throw;
            }

            return result;
        }

        /// <summary>
        /// adds new list item to list
        /// </summary>
        /// <param name="title">default value: New Item</param>
        /// <param name="isElevated">if true adds as farm administrator - default value: false</param>
        /// <returns>true if item was created without error</returns>
        public bool AddListItem(string title = "New Item", bool isElevated = false)
        {
            var result = true;

            try
            {
                if (isElevated)
                    AddItemElevated(title);
                else
                    AddItem(title);
            }
            catch (Exception ex)
            {
                result = false;
                SPDiagnosticsService.Local.WriteTrace(0,
                    new SPDiagnosticsCategory($"{nameof(ListController)} - {nameof(AddListItem)}", TraceSeverity.Unexpected, EventSeverity.Error),
                    TraceSeverity.Unexpected,
                    ex.Message,
                    ex.StackTrace);
                throw;
            }

            return result;
        }

        #region privateMethods

        private void AddItem(string title)
        {
            try
            {
                var newItem = list.AddItem();
                newItem["Title"] = title;
                newItem.Update();
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0,
                    new SPDiagnosticsCategory($"{nameof(ListController)} - {nameof(AddItem)}", TraceSeverity.Unexpected, EventSeverity.Error),
                    TraceSeverity.Unexpected,
                    ex.Message,
                    ex.StackTrace);
                throw;
            }
        }

        private void AddItemElevated(string title)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite elevSite = new SPSite(web.Site.Url))
                    {
                        using (SPWeb elevWeb = elevSite.OpenWeb())
                        {
                            var previousAllowUnsafeUpdates = elevWeb.AllowUnsafeUpdates;
                            elevWeb.AllowUnsafeUpdates = true;
                            var newItem = elevWeb.Lists.TryGetList(list.Title).AddItem();
                            newItem["Title"] = title;
                            newItem.Update();
                            elevWeb.AllowUnsafeUpdates = previousAllowUnsafeUpdates;
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0,
                    new SPDiagnosticsCategory($"{nameof(ListController)} - {nameof(AddItemElevated)}", TraceSeverity.Unexpected, EventSeverity.Error),
                    TraceSeverity.Unexpected,
                    ex.Message,
                    ex.StackTrace);
                throw;
            }
        }

        private SPList GetList(string listName) =>
            web.Lists.TryGetList(listName);

        #endregion
    }
}
