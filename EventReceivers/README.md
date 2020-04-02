## EventReceivers

#### Description

This solution contains 4 projects:
- EventReceiver - this project adds list item event receiver to custom list. It is attached to synchronous and asynchronous events. Here I also have a global disable event receiver class that may be used in using() statement, thanks to it the eventreceiver stops from firing recurrently. Eventreceivers are perfect project types to implement custom validation added to lists or to implement custom workflow steps managed by events on lists, groups and other.
- ContentTypeWithEventReceiver - this projects adds a content type with attached event receiver. Only elements with this CT will rise an event. Here I use local disable of event receiver with this.EventFiringEnabled = false;. This is perfect approach to implement different event c# logic for different items that may be stored on the same list or library
- SharedLogic - this project has helper classes that may be shared between projects
- SharePointSharedLibrary – this project is to include and deploy SharedLogic c# library to GAC so that it may be reused in other farm type projects

----
#### MSDN 

MSDN resource helpful to understand the used technology
https://docs.microsoft.com/en-us/previous-versions/office/developer/sharepoint-2010/ff408183(v%3Doffice.14)

---
#### Example

When some validation is not met then the event receiver cancels deletion of item via OOTB SharePoint behavior 
![](../Images/EventRecieverScreen1.png	)

Here I present some of the events that are saved in a custom list as logs from event receiver
![](../Images/EventRecieverScreen2.png	)

