using CSOMApp.Model;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSOMApp.Controller
{
    public class QueryController : SPController
    {
        public QueryController(ClientContext context)
            : base(context) { }

        /// <summary>
        /// this method executes a caml query with paginated option to overcome threshold
        /// </summary>
        /// <param name="input"></param>
        /// <returns>list of items</returns>
        public List<ListItem> Query(QueryControllerInput input)
        {
            List<ListItem> result = new List<ListItem>();

            try
            {
                ListItemCollectionPosition position = null;
                int page = 1;
                do
                {
                    List OrderList = context.Web.Lists.GetByTitle(input.ListTitle);
                    CamlQuery query = new CamlQuery();

                    query.ViewXml = new StringBuilder()
                        .Append("<View Scope=\"RecursiveAll\">")
                        .Append("<Query>")
                        .Append(input.Query)
                        .Append("</Query>")
                        .Append("<RowLimit>5000</RowLimit>")
                        .Append("</View>")
                        .ToString();

                    query.ListItemCollectionPosition = position;
                    ListItemCollection items = OrderList.GetItems(query);
                    context.Load(items);
                    context.ExecuteQuery();

                    position = items.ListItemCollectionPosition;
                    if (items.Count > 0)
                        result.AddRange(items);

                    context.ExecuteQuery();
                    page++;
                }
                while (position != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(Query)} - {ex}");
            }

            return result;
        }
    }
}
