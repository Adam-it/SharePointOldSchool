using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System;

namespace CustomField.SharePoint.WebControls
{
    public class CustomFieldControl : TextField
    {
        protected Label CustomPrefix;
        protected Label CustomValueForDisplay;

        protected override string DefaultTemplateName
        {
            get
            {
                if (this.ControlMode == SPControlMode.Display)
                {
                    return this.DisplayTemplateName;
                }
                else
                {
                    return "CustomFieldControl";
                }
            }
        }

        public override string DisplayTemplateName
        {
            get
            {
                return "CustomFieldControlForDisplay";
            }
            set
            {
                base.DisplayTemplateName = value;
            }
        }

        protected override void CreateChildControls()
        {
            if (this.Field != null)
            {
                // Make sure inherited child controls are completely rendered.
                base.CreateChildControls();

                // Associate child controls in the .ascx file with the 
                // fields allocated by this control.
                this.CustomPrefix = (Label)TemplateContainer.FindControl("CustomPrefix");
                this.textBox = (TextBox)TemplateContainer.FindControl("TextField");
                this.CustomValueForDisplay = (Label)TemplateContainer.FindControl("CustomValueForDisplay");

                if (this.ControlMode != SPControlMode.Display)
                {
                    if (!this.Page.IsPostBack)
                    {
                        if (this.ControlMode == SPControlMode.New)
                        {
                            textBox.Text = "some sample text";

                        } // end assign default value in New mode

                    }// end if this is not a postback 

                    // Do not reinitialize on a postback.

                }// end if control mode is not Display
                else // control mode is Display 
                {
                    // Assign current value from database to the label control
                    CustomValueForDisplay.Text = (String)this.ItemFieldValue;

                }// end control mode is Display
            }
        }
    }
}
