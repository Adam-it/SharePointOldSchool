## EventReceivers

- EventReceiver - this project adds list item event receiver to custom list. Here I also have a global disbale event receiver class to use with using statement
- ContentTypeWithEventReceiver - this projects adds a content type with attached event receiver. Only elements with this CT will rise an event. Here I use local disable of event receiver with this.EventFiringEnabled = false;
- SharedLogic - this project has helper classes that may be shared between projects
- SharePointSharedLibrary - is to include and deploy SharedLogic c# library to GAC
- Scripts - store helper PS1 scripts

## Example

When some validation is not met then the event receiver cancels deletion of item
![](../Images/EventRecieverScreen1.png	)

How some of the events are logged in a custom list by the event receiver
![](../Images/EventRecieverScreen2.png	)

