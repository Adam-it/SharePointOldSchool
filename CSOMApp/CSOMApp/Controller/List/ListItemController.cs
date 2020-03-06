using CSOMApp.Model;
using Microsoft.SharePoint.Client;
using System;

namespace CSOMApp.Controller
{
    public class ListItemController : SPController
    {
        private readonly ListItemControllerInput input;

        public ListItemController(ListItemControllerInput input)
            : base(input.Context) =>
            this.input = input;

        /// <summary>
        /// method goes through all ListItems and performs operation specified
        /// </summary>
        public void Run()
        {
            try
            {
                input.ListItems.ForEach(item =>
                {
                    var operation = item.operation;
                    switch (operation)
                    {
                        case OperationType.Add:
                            Add(item);
                            break;
                        case OperationType.Edit:
                            Edit(item);
                            break;
                        case OperationType.Delete:
                            Delete(item);
                            break;
                        case OperationType.Recycle:
                            Recycle(item);
                            break;
                        default:
                            throw new NotSupportedException($"{operation} is not supported");
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(Run)} - {ex}");
            }
        }

        /// <summary>
        /// method moves Item with specified Id to recycle bin
        /// </summary>
        /// <param name="itemInput"></param>
        private void Recycle(ListItemInput itemInput)
        {
            try
            {
                ListItem item = input.List.GetItemById((int)itemInput.Id);

                item.Recycle();

                context.ExecuteQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(Recycle)} - {ex}");
                throw;
            }
        }

        /// <summary>
        /// method Deletes Item with specified Id
        /// </summary>
        /// <param name="itemInput"></param>
        private void Delete(ListItemInput itemInput)
        {
            try
            {
                ListItem item = input.List.GetItemById((int)itemInput.Id);

                item.DeleteObject();

                context.ExecuteQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(Delete)} - {ex}");
                throw;
            }
        }

        /// <summary>
        /// method Edits Item with specified Id
        /// </summary>
        /// <param name="itemInput"></param>
        private void Edit(ListItemInput itemInput)
        {
            try
            {
                ListItem item = input.List.GetItemById((int)itemInput.Id);
                item["Title"] = itemInput.Title;

                item.Update();

                context.ExecuteQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(Edit)} - {ex}");
                throw;
            }
        }

        /// <summary>
        /// method Adds Item to list
        /// </summary>
        /// <param name="itemInput"></param>
        private void Add(ListItemInput itemInput)
        {
            try
            {
                ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                ListItem item = input.List.AddItem(itemCreateInfo);
                item["Title"] = itemInput.Title;

                item.Update();

                context.ExecuteQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(Add)} - {ex}");
                throw;
            }
        }
    }
}
