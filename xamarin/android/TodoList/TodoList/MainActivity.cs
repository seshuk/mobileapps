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
using TodoList.ViewModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Helpers;

namespace TodoList
{
    [Activity(Label = "TodoList", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        FloatingActionButton fab;
        ListView listView;
        EditText newToDoText;

        private Button addTaskButton;

        private Binding<string, string> newTodo;

        public ListView ToDoList
        {
            get
            {
                return listView
                    ?? (listView = FindViewById<ListView>(Resource.Id.listView1));
            }
        }

        public Button AddTaskButton
        {
            get
            {
                return addTaskButton
                    ?? (addTaskButton = FindViewById<Button>(Resource.Id.addTaskButton));
            }
        }


        public TodoListViewModel Vm
        {
            get
            {
                return App.Locator.TodoList;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            //Toolbar will now take on default actionbar characteristics
            SetSupportActionBar(toolbar);

            SupportActionBar.Title = "List of ToDos";

            newToDoText = FindViewById<EditText>(Resource.Id.edittext1);

            Vm.Initialize();
            ToDoList.Adapter = Vm.Todos.GetAdapter(GetTodoAdapter);

            fab = FindViewById<FloatingActionButton>(Resource.Id.faButton);
            fab.AttachToListView(listView);

            fab.Click += (sender, e) => { };

            //ensure that the Event will be present
            AddTaskButton.Click += (sender, e) => { };

            newTodo = this.SetBinding(() => newToDoText.Text, BindingMode.TwoWay);
            // Actuate the AddTaskCommand on the VM.
            AddTaskButton.SetCommand(
                "Click",
                Vm.AddTaskCommand, newTodo);

            fab.SetCommand(
               "Click",
               Vm.AddTaskCommand, newTodo);
        }


        private View GetTodoAdapter(int position, ToDo todoModel, View convertView)
        {
            // Not reusing views here
            convertView = LayoutInflater.Inflate(Resource.Layout.ListViewTemplate, null);

            var contactName = convertView.FindViewById<TextView>(Resource.Id.textItem);

            var taskCompleteCheckBox = convertView.FindViewById<CheckBox>(Resource.Id.chkTaskComplete);

            var percentCompletePgBar = convertView.FindViewById<ProgressBar>(Resource.Id.percentComplete);

            percentCompletePgBar.Max = 100;

            var dueDateText = convertView.FindViewById<TextView>(Resource.Id.dueDate);

            contactName.Text = todoModel.Task;

            taskCompleteCheckBox.Checked = todoModel.IsCompleted;

            dueDateText.Text = todoModel.DueDate.ToShortDateString();

            percentCompletePgBar.Progress = todoModel.PercentageComplete;

            return convertView;

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

