using System;
using System.Collections.Generic;
using Plugin.Connectivity;
using Xamarin.Forms;
using System.Linq;
using Plugin.Connectivity.Abstractions;

namespace NetStatus.Views
{
    public partial class NetworkViewPage : ContentPage
    {
        public NetworkViewPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (CrossConnectivity.Current == null)
                return;
            
            ConnectionDetails.Text = CrossConnectivity.Current.ConnectionTypes.First().ToString();

            CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (CrossConnectivity.Current != null)
                CrossConnectivity.Current.ConnectivityChanged -= Current_ConnectivityChanged;
        }

        private void Current_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (CrossConnectivity.Current != null && CrossConnectivity.Current.ConnectionTypes != null)
            {
                var connectivityType = CrossConnectivity.Current.ConnectionTypes.FirstOrDefault();
                ConnectionDetails.Text = connectivityType.ToString();
            }
        }
    }
}
