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
using Java.Lang;
using TodoList.Models;

namespace TodoList.Adapters
{
    class TodoListAdapter : BaseAdapter
    {

        List<ToDo> _todoList;
        Activity _activity;

        public TodoListAdapter(Activity activity)
        {
            _activity = activity;
            FillTodos();
        }

        public TodoListAdapter(Activity a, List<ToDo> t)
        {
            _activity = a;
            _todoList = t;
        }

        private void FillTodos()
        {
            _todoList = new List<ToDo>();
            _todoList.Add(new ToDo()
            {
                Id = new Guid(),
                Task = "Task 1",
                StartDate = DateTime.Now,
                DueDate = DateTime.Now.Add(new TimeSpan(5,0,0))
               
            });

            _todoList.Add(new ToDo()
            {
                Id = new Guid(),
                Task = "Task 2",
                StartDate = DateTime.Now,
                DueDate = DateTime.Now.Add(new TimeSpan(5, 0, 0))

            });

            _todoList.Add(new ToDo()
            {
                Id = new Guid(),
                Task = "Task 3  ",
                StartDate = DateTime.Now,
                DueDate = DateTime.Now.Add(new TimeSpan(5, 0, 0))

            });
        }

        public override int Count
        {
            get
            {
                if (_todoList != null)
                    return _todoList.Count;
                else
                    return 0;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _activity.LayoutInflater.Inflate(
                Resource.Layout.ListViewTemplate, parent, false);

            var contactName = view.FindViewById<TextView>(Resource.Id.textItem);

            var taskCompleteCheckBox = view.FindViewById<CheckBox>(Resource.Id.chkTaskComplete);

            contactName.Text = _todoList[position].Task;

            taskCompleteCheckBox.Checked = _todoList[position].IsCompleted;

            return view;
        }


        
    }
}