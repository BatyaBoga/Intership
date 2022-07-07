// <copyright file="App.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Intership
{
    using System.Windows;
    using Intership.Localization;

    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Start App and initialize LocalizationManager.
        /// </summary>
        /// <param name="e">Param of OnStartup event.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            LocalizationManager.Instance.LocalizationProvider = new ResxLocalizationProvider();
        }
    }
}
