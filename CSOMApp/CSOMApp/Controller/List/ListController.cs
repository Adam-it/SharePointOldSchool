using CSOMApp.Model;
using Microsoft.SharePoint.Client;
using System;
using System.Linq;

namespace CSOMApp.Controller
{
    public class ListController : SPController
    {
        private readonly ListControllerInput input;

        public ListController(ListControllerInput input) 
            : base (input.Context) =>
            this.input = input;


        /// <summary>
        /// returns list with given title, if the list did not exist it is created
        /// </summary>
        /// <returns></returns>
        public List List() =>
            CheckIfListExist() ? GetList() : CreateList();

        /// <summary>
        /// checks if list with this title exists
        /// </summary>
        /// <returns></returns>
        private bool CheckIfListExist()
        {
            try
            {
                ListCollection listCollection = context.Web.Lists;
                context.Load(listCollection, lists => lists.Include(list => list.Title));
                context.ExecuteQuery();

                if (listCollection.Any(list => list.Title == input.ListTitle))
                    return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(CheckIfListExist)} - {ex}");
            }

            return false;
        }

        /// <summary>
        /// retrives the list from the context
        /// </summary>
        /// <returns></returns>
        private List GetList()
        {
            List list = null;
            try
            {
                var web = context.Web;
                list = web.Lists.GetByTitle(input.ListTitle);
                context.Load(list, includes => includes.Title);
                context.ExecuteQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(GetList)} - {ex}");
            }

            return list;
        }

        /// <summary>
        /// method creates Lib/List with a given title
        /// </summary>
        /// <returns></returns>
        private List CreateList()
        {
            List list = null;
            try
            {
                var web = context.Web;

                var listCreationInfo = new ListCreationInformation();
                listCreationInfo.Title = input.ListTitle;
                listCreationInfo.TemplateType = (int)input.Type;

                list = web.Lists.Add(listCreationInfo);

                context.ExecuteQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(CreateList)} - {ex}");
            }

            return list;
        }
    }
}
