## CustomFieldType

#### Description

This project may give one of the biggest advantage to SharePoint extending what is given OOTB. Using this kind o project we may add a new field type (column which may be used in list or libraries). This field may have custom validation, custom render in edit mode and view mode of the page. The users with edit permissions which then create new applications in SharePoint, when creating list and library architecture may use those custom fields in their solutions
This project adds custom filed (column) to SharePoint. This kind of column may be used in custom list. 
- Custom.ValidationRule.cs - provides custom validation for the field (a very simple one.. just as an example)
- CustomFieldControl.ascx - provides custom UI for the field

----
#### MSDN 

MSDN resource helpful to understand the used technology

https://docs.microsoft.com/en-us/previous-versions/office/developer/sharepoint-2010/gg132914(v%3doffice.14)

---
#### Example

When adding column to list new column is visible as one of the options
![](../../../Images/CustomFieldTypeScreen3.png)

When no value is provided then the field is populated with default value
<pre>
if (this.ControlMode == SPControlMode.New)
{
    textBox.Text = "some sample text";
}
</pre>
![](../../../Images/CustomFieldTypeScreen2.png)

The field has custom validation 
![](../../../Images/CustomFieldTypeScreen1.png)
