using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Security;
using System.Windows.Controls;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using CustomField.SharePoint.WebControls;
using CustomField.System.Windows.Controls;
using System;

namespace CustomField.SharePoint
{
    public class CustomField : SPFieldText
    {
        public CustomField(SPFieldCollection fields, string fieldName)
            : base(fields, fieldName)
        {
        }

        public CustomField(SPFieldCollection fields, string typeName, string displayName) 
            : base(fields, typeName, displayName)
        {
        }

        public override BaseFieldControl FieldRenderingControl
        {
            [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
            get
            {
                BaseFieldControl fieldControl = new CustomFieldControl();
                fieldControl.FieldName = this.InternalName;

                return fieldControl;
            }
        }

        public override string GetValidatedString(object value)
        {
            if ((this.Required == true) && ((value == null) || ((String)value == "")))
            {
                throw new SPFieldValidationException(this.Title + " must have a value.");
            }
            else
            {
                CustomValidationRule rule = new CustomValidationRule();
                ValidationResult result = rule.Validate(value, CultureInfo.InvariantCulture);

                if (!result.IsValid)
                {
                    throw new SPFieldValidationException((String)result.ErrorContent);
                }
                else
                {
                    return base.GetValidatedString(value);
                }
            }
        }
    }
}
