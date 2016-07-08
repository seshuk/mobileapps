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

namespace TodoList.Models
{
        public class ToDo
        {
            public Guid Id { get; set; }
            public string Task { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime DueDate { get; set; }
            public DateTime? CompleteDate { get; set; }
            public int PercentageComplete { get; set; }
            public bool IsCompleted { get; set; }
        }
}