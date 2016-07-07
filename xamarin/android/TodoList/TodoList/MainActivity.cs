using Android.App;
using Android.Views;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using com.refractored.fab;
using System.Collections;
using System.Collections.Generic;

namespace TodoList
{
    [Activity(Label = "TodoList", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        int count = 1;
        System.Collections.IList items;
        FloatingActionButton fab;
        ListView listView;


        protected override void OnCreate(Bundle bundle)
        {

            items = new List<string> { "Vegetables", "Fruits", "Flower Buds", "Legumes", "Bulbs", "Tubers" , "Vegetables", "Fruits", "Flower Buds", "Legumes", "Bulbs"};
            
            

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            //Toolbar will now take on default actionbar characteristics
            SetSupportActionBar(toolbar);

            SupportActionBar.Title = "List of ToDos";


            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            EditText txt = FindViewById<EditText>(Resource.Id.edittext1);

            button.Click += delegate {

                //button.Text = string.Format("{0} clicks!", count++);
                items.Add(txt.Text);
                listView.Adapter = new ArrayAdapter(this, Resource.Layout.ListViewTemplate, items);
                txt.Text = "";
            };

            

            listView = FindViewById<ListView>(Resource.Id.listView1);

            listView.Adapter = new ArrayAdapter(this, Resource.Layout.ListViewTemplate, items);
            fab = FindViewById<FloatingActionButton>(Resource.Id.faButton);
            fab.AttachToListView(listView);

        }

        /// <Docs>The options menu in which you place your items.</Docs>
		/// <returns>To be added.</returns>
		/// <summary>
		/// This is the menu for the Toolbar/Action Bar to use
		/// </summary>
		/// <param name="menu">Menu.</param>
		public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.home, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Toast.MakeText(this, "Top ActionBar pressed: " + item.TitleFormatted, ToastLength.Short).Show();
            return base.OnOptionsItemSelected(item);
        }
    }
}

