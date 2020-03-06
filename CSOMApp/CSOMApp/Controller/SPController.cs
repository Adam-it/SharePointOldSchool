using Microsoft.SharePoint.Client;
using System;

namespace CSOMApp.Controller
{
    public abstract class SPController
    {
        protected readonly ClientContext context;

        public SPController(ClientContext context) =>
            this.context = context ?? throw new ArgumentNullException(nameof(context));
    }
}
