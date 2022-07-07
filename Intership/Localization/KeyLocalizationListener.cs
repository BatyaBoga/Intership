namespace Intership.Localization
{
    using System.ComponentModel;

    /// <summary>
    /// Culture change listener for localization by key.
    /// </summary>
    public class KeyLocalizationListener : BaseLocalizationListener, INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyLocalizationListener"/> class.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="args">Arguments.</param>
        public KeyLocalizationListener(string key, object[] args)
        {
            this.Key = key;
            this.Args = args;
        }

        /// <summary>
        /// Property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets Localization object.
        /// </summary>
        public object Value
        {
            get
            {
                var value = LocalizationManager.Instance.Localize(this.Key);
                if (value is string && this.Args != null)
                {
                    value = string.Format((string)value, this.Args);
                }

                return value;
            }
        }

        private string Key { get; }

        private object[] Args { get; }

        /// <summary>
        /// On culture changed method.
        /// </summary>
        protected override void OnCultureChanged()
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Value)));
        }
    }
}
