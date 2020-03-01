## CustomRibbonAndApplicationPage

- ApplicationPage - This project adds image to the _layouts which is used as an image to ribbon button.
This project also adds custom Application Page wich is then used in javascript SharePoint dialog.
The application page may have any serwer side logic added 
- CustomRibbon - This project is a sandbox solution which adds custom master page, list and content type to site collection.
This project also adds custom javascript with functions used in ribbon buttons. One of the function presents an application page in a SharePoint dialog.
Finally this solution modifies Ribbon for a specified content type added to a list:
-List tab is hidden 
-ListItem action group is hidden
-Custom button added to existing group
-New tab added with new group and two new buttons

## Example

this image presents the changes made to the ootb list ribbon which is present only when the content type is added to the list 
![](../Images/CustomRibbonScreen1.png	)

this image present a cutom application page shown in javascript SharePoint dialog
![](../Images/CustomDialogScreen1.png	)

