using Microsoft.SharePoint;
using System;

namespace EventReceiver
{
    class DisabledEventsScope : SPItemEventReceiver, IDisposable
    {
        // Boolean to hold the original value of the EventFiringEnabled property
        bool _originalValue;
        public DisabledEventsScope()
        {
            // Save off the original value of EventFiringEnabled
            _originalValue = base.EventFiringEnabled;

            // Set EventFiringEnabled to false to disable it
            base.EventFiringEnabled = false;
        }
        public void Dispose()
        {
            // Set EventFiringEnabled back to its original value
            base.EventFiringEnabled = _originalValue;
        }
    }
}
