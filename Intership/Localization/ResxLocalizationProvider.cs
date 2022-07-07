namespace Intership.Localization
{
    using System.Collections.Generic;
    using System.Globalization;
    using Intership.Properties;

    /// <summary>
    /// Implementing a Localized String Provider Through Application Resources.
    /// </summary>
    public class ResxLocalizationProvider : ILocalizationProvider
    {
        private IEnumerable<CultureInfo> cultures;

        /// <inheritdoc/>
        public IEnumerable<CultureInfo> Cultures => this.cultures ?? (this.cultures = new List<CultureInfo>
        {
            new CultureInfo("ru-RU"),
            new CultureInfo("en-US"),
        });

        /// <summary>
        /// Method to look up a value by key in a resource table.
        /// </summary>
        /// <param name="key">Name(key) in resource table.</param>
        /// <returns>The method returns an object by key from the resource table.</returns>
        public object Localize(string key)
        {
            return Resources.ResourceManager.GetObject(key);
        }
    }
}
