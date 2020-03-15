using System;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using ISAPI.Controller;
using ISAPI.Model;
using ISAPI.Model.Data;
using Microsoft.SharePoint;
using static ISAPI.Model.Mapping;

namespace ISAPI.ISAPI.CustomISAPI
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class CustomISAPI : ICustomISAPI
    {
        /// <summary>
        /// adds item to list
        /// </summary>
        /// <param name="listName"></param>
        /// <param name="itemTitle"></param>
        /// <returns></returns>
        public ServiceResponse<bool> AddListItem(string listName, string itemTitle, bool isElevated)
        {
            var response = new ServiceResponse<bool>()
            {
                IsError = false
            };

            try
            {
                if (listName == null || listName == "")
                    throw new ArgumentException($"{nameof(listName)} is empty");

                if (itemTitle == null || itemTitle == "")
                    throw new ArgumentException($"{nameof(itemTitle)} is empty");

                using (SPSite site = new SPSite(SITE_URL))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        var previousAllowUnsafeUpdates = web.AllowUnsafeUpdates;
                        web.AllowUnsafeUpdates = true;
                        var itemWasAdded = new ListController(web, LIST_NAME).AddListItem(itemTitle, isElevated);
                        if (itemWasAdded)
                            response.Message = "item added";
                        response.Response = itemWasAdded;
                        web.AllowUnsafeUpdates = previousAllowUnsafeUpdates;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// returns all items from list
        /// https://testowy.local.umed.pl/sites/AdamTest/_vti_bin/CustomISAPI/CustomISAPI.svc/GetListItems
        /// </summary>
        /// <param name="listName"></param>
        /// <returns></returns>
        public ServiceResponse<List<ListItem>> GetListItems(string listName)
        {
            var response = new ServiceResponse<List<ListItem>>()
            {
                IsError = false
            };

            try
            {
                if (listName == null || listName == "")
                    throw new ArgumentException($"{nameof(listName)} is empty");

                using (SPSite site = new SPSite(SITE_URL))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        var previousAllowUnsafeUpdates = web.AllowUnsafeUpdates;
                        web.AllowUnsafeUpdates = true;
                        response.Response = new ListController(web, LIST_NAME).GetListItems();
                        web.AllowUnsafeUpdates = previousAllowUnsafeUpdates;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// https://testowy.local.umed.pl/sites/AdamTest/_vti_bin/CustomISAPI/CustomISAPI.svc/TestCustomISAPI
        /// </summary>
        /// <returns></returns>
        public string TestCustomISAPI() =>
            $"{nameof(CustomISAPI)} is working";
    }
}