using CSOMApp.Controller;
using CSOMApp.Model;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Text;
using static CSOMApp.Model.Mapping;

namespace CSOMApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (ClientContext context = new ClientContext(URL))
                {
                    new ListItemController(new ListItemControllerInput
                    {
                        Context = context,
                        List = new ListController(new ListControllerInput
                        {
                            Context = context,
                            ListTitle = LIST_NAME,
                            Type = ListTemplateType.GenericList
                        }).List(),
                        ListItems = new List<ListItemInput>()
                        {
                            //new ListItemInput{ Id = 1, Title = "A1", operation = OperationType.Delete },
                            //new ListItemInput{ Id = 2, Title = "OOOO", operation = OperationType.Edit },
                            //new ListItemInput{ Id = 3, Title = "C3", operation = OperationType.Recycle },
                            new ListItemInput{ Id = null, Title = "test1", operation = OperationType.Add }
                        }
                    }).Run();

                    new QueryController(context).Query(new QueryControllerInput
                    {
                        ListTitle = LIST_NAME,
                        Query = new StringBuilder()
                        .Append("<Where>")
                        .Append("</Where>")
                        .ToString()
                    }).ForEach(item => 
                    {
                        Console.WriteLine($"{item.Id} - {item["Title"]}");
                    });

                    var lib = new ListController(new ListControllerInput
                    {
                        Context = context,
                        ListTitle = LIB_NAME,
                        Type = ListTemplateType.DocumentLibrary
                    }).List();

                    var library = new LibraryController(new LibraryControllerInput
                    {
                        Context = context,
                        List = lib,
                        ListItems = new List<FolderItemInput>()
                        {
                            new FolderItemInput{ Id = 3, Title = "test2", operation = OperationType.Edit }
                        }
                    });

                    library.Run();

                    library.AddFile(new AddFileInput
                    {
                        siteRelativeUrl = RELATIVE_URL,
                        fileName = "test.txt",
                        LibName = lib.Title,
                        filePath = @"..\..\ExampleFiles\example.txt"
                    });

                    var currentUser = new UserController(context).GetCurrentUser();
                    Console.WriteLine("Account Name: " + currentUser.AccountName);
                    Console.WriteLine("Email: " + currentUser.Email);
                    Console.WriteLine("Display Name: " + currentUser.DisplayName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.WriteLine("=====================================");
            Console.WriteLine("hit any key to close");
            Console.ReadLine();
        }
    }
}
