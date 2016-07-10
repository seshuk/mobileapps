using Android.App;
using Android.Views;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using com.refractored.fab;
using System.Collections;
using System.Collections.Generic;
using TodoList.Adapters;
using TodoList.Models;
using System;

namespace TodoList
{
    [Activity(Label = "TodoList", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        FloatingActionButton fab;
        ListView listView;
        List<ToDo> _todoList;

        protected override void OnCreate(Bundle bundle)
        {

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


                _todoList.Add(
                    new ToDo()
                    {
                        Task = txt.Text,
                        Id = new Guid(),
                        StartDate = DateTime.Now,
                        DueDate = DateTime.Now.Add(new TimeSpan(5, 0, 0))
                    }
                    );
                listView.Adapter = new TodoListAdapter(this, _todoList); 
                txt.Text = "";
            };


            FillTodos();

            listView = FindViewById<ListView>(Resource.Id.listView1);

            listView.Adapter = new TodoListAdapter(this, _todoList); 
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

        private void FillTodos()
        {
            _todoList = new List<ToDo>();
            _todoList.Add(new ToDo()
            {
                Id = new Guid(),
                Task = "Task 1",
                StartDate = DateTime.Now,
                DueDate = DateTime.Now.Add(new TimeSpan(5, 0, 0)),
                PercentageComplete = 10
                

            });

            _todoList.Add(new ToDo()
            {
                Id = new Guid(),
                Task = "Task 2",
                StartDate = DateTime.Now,
                DueDate = DateTime.Now.Add(new TimeSpan(5, 0, 0)),
                PercentageComplete = 60
            });

            _todoList.Add(new ToDo()
            {
                Id = new Guid(),
                Task = "Task 3  ",
                StartDate = DateTime.Now,
                DueDate = DateTime.Now.Add(new TimeSpan(5, 0, 0))

            });
        }
    }
}

