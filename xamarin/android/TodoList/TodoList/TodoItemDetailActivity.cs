using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Android.Support.V7.App;

namespace TodoList
{
    [Activity(Label = "ToDo Details")]
    class TodoItemDetailActivity : AppCompatActivity
    {
        private Button backButton;

        public Button BackButton
        {
            get
            {
                return backButton
                    ?? (backButton = FindViewById<Button>(Resource.Id.backButton));
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "Second" layout resource
            SetContentView(Resource.Layout.Details);

            // Retrieve navigation service
            var nav = (NavigationService)ServiceLocator.Current.GetInstance<INavigationService>();
            BackButton.Click += (s, e) => nav.GoBack();
        }
    }
}