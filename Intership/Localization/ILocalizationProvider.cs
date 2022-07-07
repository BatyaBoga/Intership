namespace Intership.Localization
{
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Интерфейс для реализации поставщика локализованных строк.
    /// </summary>
    public interface ILocalizationProvider
    {
        /// <summary>
        /// Gets available crops.
        /// </summary>
        IEnumerable<CultureInfo> Cultures { get; }

        /// <summary>
        /// Returns a localized object by key.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <returns>Localized object.</returns>
        object Localize(string key);
    }
}
