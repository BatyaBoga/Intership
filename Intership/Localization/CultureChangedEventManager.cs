namespace Intership.Localization
{
    using System;
    using System.Windows;

    /// <summary>
    /// Manager of event. WeakEvent pattern implementation.
    /// </summary>
    public class CultureChangedEventManager : WeakEventManager
    {
        private static CultureChangedEventManager CurrentManager
        {
            get
            {
                var managerType = typeof(CultureChangedEventManager);
                var manager = (CultureChangedEventManager)GetCurrentManager(managerType);
                if (manager == null)
                {
                    manager = new CultureChangedEventManager();
                    SetCurrentManager(managerType, manager);
                }

                return manager;
            }
        }

        /// <summary>
        /// Add Listener to event.
        /// </summary>
        /// <param name="source">Manager.</param>
        /// <param name="listener">Listener.</param>
        public static void AddListener(LocalizationManager source, IWeakEventListener listener)
        {
            CurrentManager.ProtectedAddListener(source, listener);
        }

        /// <summary>
        /// Remove Listener to event.
        /// </summary>
        /// <param name="source">Manager.</param>
        /// <param name="listener">Listener.</param>
        public static void RemoveListener(LocalizationManager source, IWeakEventListener listener)
        {
            CurrentManager.ProtectedRemoveListener(source, listener);
        }

        /// <summary>
        /// Start listening event.
        /// </summary>
        /// <param name="source"> source.</param>
        protected override void StartListening(object source)
        {
            var manager = (LocalizationManager)source;
            manager.CultureChanged += this.OnCultureChanged;
        }

        /// <summary>
        /// Stoplistening event.
        /// </summary>
        /// <param name="source"> source.</param>
        protected override void StopListening(object source)
        {
            var manager = (LocalizationManager)source;
            manager.CultureChanged -= this.OnCultureChanged;
        }

        private void OnCultureChanged(object sender, EventArgs e)
        {
            this.DeliverEvent(sender, e);
        }
    }
}
