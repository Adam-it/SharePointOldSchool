using Microsoft.SharePoint;

namespace ContentTypeWithEventReceiver.EventReceiver
{
    /// <summary>
    /// Wyświetl listę zdarzeń elementów
    /// </summary>
    public class EventReceiver : SPItemEventReceiver
    {
        /// <summary>
        /// Trwa dodawanie elementu.
        /// </summary>
        public override void ItemAdding(SPItemEventProperties properties)
        {
            this.EventFiringEnabled = false;
            base.ItemAdding(properties);
            this.EventFiringEnabled = true;
        }

        /// <summary>
        /// Trwa aktualizowanie elementu.
        /// </summary>
        public override void ItemUpdating(SPItemEventProperties properties)
        {
            this.EventFiringEnabled = false;
            base.ItemUpdating(properties);
            this.EventFiringEnabled = true;
        }

        /// <summary>
        /// Trwa usuwanie elementu.
        /// </summary>
        public override void ItemDeleting(SPItemEventProperties properties)
        {
            this.EventFiringEnabled = false;
            base.ItemDeleting(properties);
            this.EventFiringEnabled = true;
        }

        /// <summary>
        /// Dodano element.
        /// </summary>
        public override void ItemAdded(SPItemEventProperties properties)
        {
            this.EventFiringEnabled = false;
            base.ItemAdded(properties);
            this.EventFiringEnabled = true;
        }

        /// <summary>
        /// Zaktualizowano element.
        /// </summary>
        public override void ItemUpdated(SPItemEventProperties properties)
        {
            this.EventFiringEnabled = false;
            base.ItemUpdated(properties);
            this.EventFiringEnabled = true;
        }

        /// <summary>
        /// Usunięto element.
        /// </summary>
        public override void ItemDeleted(SPItemEventProperties properties)
        {
            this.EventFiringEnabled = false;
            base.ItemDeleted(properties);
            this.EventFiringEnabled = true;
        }


    }
}