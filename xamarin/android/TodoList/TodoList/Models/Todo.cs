using System;
using GalaSoft.MvvmLight;

namespace TodoList.Models
{
    public class ToDo : ObservableObject
    {
            public Guid Id { get; set; }
            public string Task { get; set; }
            public string Notes { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime DueDate { get; set; }
            public DateTime? CompleteDate { get; set; }
            public int PercentageComplete { get; set; }
            public bool IsCompleted { get; set; }
        }
}