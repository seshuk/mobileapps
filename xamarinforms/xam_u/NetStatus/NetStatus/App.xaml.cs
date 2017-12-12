using System.Collections.Generic;
using Plugin.Connectivity;
using Xamarin.Forms;
using NetStatus.Views;
using Plugin.Connectivity.Abstractions;
using System;

namespace NetStatus
{
    public partial class App : Application
    {
        public static bool UseMockDataStore = true;
        public static string BackendUrl = "https://localhost:5000";

        public static IDictionary<string, string> LoginParameters => null;

        public App()
        {
            InitializeComponent();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<CloudDataStore>();

            //SetMainPage();

            GoToMainPage();
        }

        public static void SetMainPage()
        {
            if (!UseMockDataStore && !Settings.IsLoggedIn)
            {
                Current.MainPage = new NavigationPage(new LoginPage())
                {
                    BarBackgroundColor = (Color)Current.Resources["Primary"],
                    BarTextColor = Color.White
                };
            }
            else
            {
                GoToMainPage();
            }
        }

        public static void GoToMainPage()
        {
            
            Current.MainPage = CrossConnectivity.Current.IsConnected ? (Page) new NetworkViewPage() : new NoNetworkPage();

            //Current.MainPage = new TabbedPage
            //{
            //    Children = {
            //        new NavigationPage(new ItemsPage())
            //        {
            //            Title = "Browse",
            //            Icon = Device.OnPlatform("tab_feed.png", null, null)
            //        },
            //        new NavigationPage(new AboutPage())
            //        {
            //            Title = "About",
            //            Icon = Device.OnPlatform("tab_about.png", null, null)
            //        },
            //    }
            //};
        }

        protected override void OnStart()
        {
            base.OnStart();

            CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
        }

        private void Current_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var currentPage = Current.MainPage.GetType();

            if(e.IsConnected && currentPage != typeof(NetworkViewPage))
            {
                Current.MainPage = new NetworkViewPage(); 
            }
            else if(!e.IsConnected && currentPage != typeof(NoNetworkPage))
            {
                Current.MainPage = new NoNetworkPage();
			}
        }
    }
}
