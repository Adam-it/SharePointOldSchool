using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.UserProfiles;
using System;

namespace CSOMApp.Controller
{
    public class UserController : SPController
    {
        public UserController(ClientContext context) 
            : base(context){ }

        /// <summary>
        /// gets current user properties
        /// </summary>
        /// <returns></returns>
        public PersonProperties GetCurrentUser()
        {
            PersonProperties personProperties = null;

            try
            {
                PeopleManager peopleManager = new PeopleManager(context);
                personProperties = peopleManager.GetMyProperties();
                context.Load(personProperties, p => p.AccountName, p => p.Email, p => p.DisplayName);
                context.ExecuteQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(GetCurrentUser)} - {ex}");
            }

            return personProperties;
        }
    }
}
