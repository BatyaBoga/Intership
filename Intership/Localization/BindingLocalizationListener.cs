namespace Intership.Localization
{
    using System.Windows.Data;

    /// <summary>
    /// Localization-by-binding culture change listener.
    /// </summary>
    public class BindingLocalizationListener : BaseLocalizationListener
    {
        private BindingExpressionBase BindingExpression { get; set; }

        /// <summary>
        /// Set binding expression.
        /// </summary>
        /// <param name="bindingExpression"> Expression.</param>
        public void SetBinding(BindingExpressionBase bindingExpression)
        {
            this.BindingExpression = bindingExpression;
        }

        /// <summary>
        /// Update target on binding expression.
        /// </summary>
        protected override void OnCultureChanged()
        {
            try
            {
                this.BindingExpression?.UpdateTarget();
            }
            catch
            {
                // ignored
            }
        }
    }
}
