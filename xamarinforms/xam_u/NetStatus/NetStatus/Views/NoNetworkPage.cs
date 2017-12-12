using System;

using Xamarin.Forms;

namespace NetStatus
{   
    public class NoNetworkPage : ContentPage
    {
        public NoNetworkPage()
        {
			BackgroundColor = new Color(0xf0, 0xf0, 0xf0);

            Content = 
                    new Label { 
                        Text = "No network connection Available!", 
                        BackgroundColor= new Color(0x40, 0x40, 0x40),
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center
                     };
          
        }
    }
}

