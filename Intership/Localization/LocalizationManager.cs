namespace Intership.Localization
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// The main class for working with localization.
    /// </summary>
    public class LocalizationManager
    {
        private static LocalizationManager localizationManager;

        private LocalizationManager()
        {
        }

        /// <summary>
        /// Culture change event.
        /// </summary>
        public event EventHandler CultureChanged;

        /// <summary>
        /// Gets implementation of the Singleton pattern.
        /// </summary>
        public static LocalizationManager Instance => localizationManager ?? (localizationManager = new LocalizationManager());

        /// <summary>
        /// Gets or Sets Current culture.
        /// </summary>
        public CultureInfo CurrentCulture
        {
            get
            {
                return Thread.CurrentThread.CurrentUICulture;
            }

            set
            {
                if (Equals(value, Thread.CurrentThread.CurrentUICulture))
                {
                    return;
                }

                Thread.CurrentThread.CurrentUICulture = value;
                CultureInfo.DefaultThreadCurrentUICulture = value;
                this.OnCultureChanged();
            }
        }

        /// <summary>
        /// Gets collection of cultures.
        /// </summary>
        public IEnumerable<CultureInfo> Cultures => this.LocalizationProvider?.Cultures ?? Enumerable.Empty<CultureInfo>();

        /// <summary>
        /// Gets or Sets LocalizationProvider.
        /// </summary>
        public ILocalizationProvider LocalizationProvider { get; set; }

        /// <summary>
        /// Localize string by key.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <returns>The method returns an object by key from the  LocalizationProvider.</returns>
        public object Localize(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return "[NULL]";
            }

            var localizedValue = this.LocalizationProvider?.Localize(key);
            return localizedValue ?? $"[{key}]";
        }

        private void OnCultureChanged()
        {
            this.CultureChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
