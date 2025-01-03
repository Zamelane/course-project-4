﻿using ClientApp.Src.Storage;

namespace ClientApp
{
    public partial class App : Application
    {
        // Windows: window size
        const int newWidth = 450;
        const int newHeight = 800;

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            if (AuthData.User != null && AuthData.Token != null)
                Provider.appShell.setEnabledTabsAll(true);
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            window.Width = newWidth;
            window.Height = newHeight;

            return window;
        }
    }
}
