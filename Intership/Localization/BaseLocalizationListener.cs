namespace Intership.Localization
{
    using System;
    using System.Windows;

    /// <summary>
    /// Base class for culture change listeners.
    /// </summary>
    public abstract class BaseLocalizationListener : IWeakEventListener, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseLocalizationListener"/> class.
        /// </summary>
        protected BaseLocalizationListener()
        {
            CultureChangedEventManager.AddListener(LocalizationManager.Instance, this);
        }

        /// <summary>
        /// Receives events from the centralized even manager.
        /// </summary>
        /// <param name="managerType"> Type of manager.</param>
        /// <param name="sender">Sender.</param>
        /// <param name="e">EventArgs.</param>
        /// <returns>True or false.</returns>
        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (managerType == typeof(CultureChangedEventManager))
            {
                this.OnCultureChanged();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Finalize.
        /// </summary>
        public void Dispose()
        {
            CultureChangedEventManager.RemoveListener(LocalizationManager.Instance, this);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// On culture changed method.
        /// </summary>
        protected abstract void OnCultureChanged();
    }
}
