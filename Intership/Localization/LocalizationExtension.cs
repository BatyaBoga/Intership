namespace Intership.Localization
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    /// <summary>
    /// A markup extension that returns a localized string given a key or binding.
    /// </summary>
    [ContentProperty(nameof(ArgumentBindings))]
    public class LocalizationExtension : MarkupExtension
    {
        private Collection<BindingBase> arguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizationExtension"/> class.
        /// </summary>
        public LocalizationExtension()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizationExtension"/> class.
        /// </summary>
        /// <param name="key">Key.</param>
        public LocalizationExtension(string key)
        {
            this.Key = key;
        }

        /// <summary>
        /// Gets or Sets localized string key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or Sets binding for localized string key.
        /// </summary>
        public Binding KeyBinding { get; set; }

        /// <summary>
        /// Gets or Sets localized string formatted arguments.
        /// </summary>
        public IEnumerable<object> Arguments { get; set; }

        /// <summary>
        /// Gets or Sets localized string formatted argument bindings.
        /// </summary>
        public Collection<BindingBase> ArgumentBindings
        {
            get { return this.arguments ?? (this.arguments = new Collection<BindingBase>()); }
            set { this.arguments = value; }
        }

        /// <summary>
        /// Provide value.
        /// </summary>
        /// <param name="serviceProvider">Service provider.</param>
        /// <returns> Object.</returns>
        /// <exception cref="ArgumentException">Exception.</exception>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (this.Key != null && this.KeyBinding != null)
            {
                throw new ArgumentException($"Нельзя одновременно задать {nameof(this.Key)} и {nameof(this.KeyBinding)}");
            }

            if (this.Key == null && this.KeyBinding == null)
            {
                throw new ArgumentException($"Необходимо задать {nameof(this.Key)} или {nameof(this.KeyBinding)}");
            }

            if (this.Arguments != null && this.ArgumentBindings.Any())
            {
                throw new ArgumentException($"Нельзя одновременно задать {nameof(this.Arguments)} и {nameof(this.ArgumentBindings)}");
            }

            var target = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
            if (target.TargetObject.GetType().FullName == "System.Windows.SharedDp")
            {
                return this;
            }

            if (this.KeyBinding != null || this.ArgumentBindings.Any())
            {
                var listener = new BindingLocalizationListener();

                var listenerBinding = new Binding { Source = listener };

                var keyBinding = this.KeyBinding ?? new Binding { Source = this.Key };

                var multiBinding = new MultiBinding
                {
                    Converter = new BindingLocalizationConverter(),
                    ConverterParameter = this.Arguments,
                    Bindings = { listenerBinding, keyBinding },
                };

                foreach (var binding in this.ArgumentBindings)
                {
                    multiBinding.Bindings.Add(binding);
                }

                var value = multiBinding.ProvideValue(serviceProvider);

                listener.SetBinding(value as BindingExpressionBase);
                return value;
            }

            if (!string.IsNullOrEmpty(this.Key))
            {
                var listener = new KeyLocalizationListener(this.Key, this.Arguments?.ToArray());

                if ((target.TargetObject is DependencyObject && target.TargetProperty is DependencyProperty) ||
                    target.TargetObject is Setter)
                {
                    var binding = new Binding(nameof(KeyLocalizationListener.Value))
                    {
                        Source = listener,
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    };
                    return binding.ProvideValue(serviceProvider);
                }

                var targetBinding = target.TargetObject as Binding;
                if (targetBinding != null && target.TargetProperty != null &&
                    target.TargetProperty.GetType().FullName == "System.Reflection.RuntimePropertyInfo")
                {
                    targetBinding.Path = new PropertyPath(nameof(KeyLocalizationListener.Value));
                    targetBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    return listener;
                }

                return listener.Value;
            }

            return null;
        }
    }
}
