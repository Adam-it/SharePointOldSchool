using CSOMApp.Model;

using Microsoft.SharePoint.Client;
using System;
using System.IO;

namespace CSOMApp.Controller
{
    public class LibraryController : SPController
    {
        private readonly LibraryControllerInput input;

        public LibraryController(LibraryControllerInput input)
            : base(input.Context) =>
            this.input = input;

        /// <summary>
        /// this method adds files to library
        /// </summary>
        /// <param name="fileInput"></param>
        public void AddFile(AddFileInput fileInput)
        {
            try
            {
                byte[] fileBytes;
                using (var fs = new FileStream(fileInput.filePath, FileMode.Open, FileAccess.Read))
                {
                    fileBytes = new byte[fs.Length];
                    int bytesRead = fs.Read(fileBytes, 0, fileBytes.Length);
                }

                using (var fileStream = new System.IO.MemoryStream(fileBytes))
                {
                    Microsoft.SharePoint.Client.File.SaveBinaryDirect(context, $"{fileInput.siteRelativeUrl}/{fileInput.LibName}/{fileInput.fileName}", fileStream, true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(AddFile)} - {ex}");
            }
        }

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
                            AddFolder(item);
                            break;
                        case OperationType.Edit:
                            EditFolder(item);
                            break;
                        case OperationType.Delete:
                            DeleteFolder(item);
                            break;
                        case OperationType.Recycle:
                            RecycleFolder(item);
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
        private void RecycleFolder(FolderItemInput itemInput)
        {
            try
            {
                ListItem item = input.List.GetItemById((int)itemInput.Id);

                item.Recycle();

                context.ExecuteQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(RecycleFolder)} - {ex}");
                throw;
            }
        }

        /// <summary>
        /// method Deletes Item with specified Id
        /// </summary>
        /// <param name="itemInput"></param>
        private void DeleteFolder(FolderItemInput itemInput)
        {
            try
            {
                ListItem item = input.List.GetItemById((int)itemInput.Id);

                item.DeleteObject();

                context.ExecuteQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(DeleteFolder)} - {ex}");
                throw;
            }
        }

        /// <summary>
        /// method Edits Item with specified Id
        /// </summary>
        /// <param name="itemInput"></param>
        private void EditFolder(FolderItemInput itemInput)
        {
            try
            {
                var title = itemInput.Title;
                ListItem item = input.List.GetItemById((int)itemInput.Id);
                item["FileLeafRef"] = title;
                item["Title"] = title;

                item.Update();

                context.ExecuteQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(EditFolder)} - {ex}");
                throw;
            }
        }

        /// <summary>
        /// method Adds folder to lib
        /// </summary>
        /// <param name="itemInput"></param>
        private void AddFolder(FolderItemInput itemInput)
        {
            try
            {
                var title = itemInput.Title;
                ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                itemCreateInfo.UnderlyingObjectType = FileSystemObjectType.Folder;
                itemCreateInfo.LeafName = title;

                ListItem item = input.List.AddItem(itemCreateInfo);
                item["Title"] = title;

                item.Update();

                context.ExecuteQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(AddFolder)} - {ex}");
                throw;
            }
        }
    }
}
