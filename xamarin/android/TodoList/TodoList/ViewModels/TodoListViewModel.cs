using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Linq;
using TodoList.Models;
using System;

namespace TodoList.ViewModels
{
    public class TodoListViewModel : ViewModelBase
    {
        public ObservableCollection<ToDo> Todos { get; private set; }

        public TodoListViewModel()
        {
            AddTaskCommand = new RelayCommand<string>((txt) => AddTask(txt));
        }

        private List<ToDo> SeedData()
        {
            var tasks = new List<ToDo>()
                        {
                            new ToDo()
                            {
                                Id = new Guid(),
                                Task = "Task 1",
                                StartDate = DateTime.Now,
                                DueDate = DateTime.Now.Add(new TimeSpan(5, 0, 0)),
                                PercentageComplete = 10


                            },
                            new ToDo()
                            {
                                Id = new Guid(),
                                Task = "Task 2",
                                StartDate = DateTime.Now,
                                DueDate = DateTime.Now.Add(new TimeSpan(5, 0, 0)),
                                PercentageComplete = 30


                            },
                                            new ToDo()
                            {
                                Id = new Guid(),
                                Task = "Task 3",
                                StartDate = DateTime.Now,
                                DueDate = DateTime.Now.Add(new TimeSpan(5, 0, 0)),
                                PercentageComplete = 10


                            },
                                            new ToDo()
                            {
                                Id = new Guid(),
                                Task = "Task 4",
                                StartDate = DateTime.Now,
                                DueDate = DateTime.Now.Add(new TimeSpan(5, 0, 0)),
                                PercentageComplete = 80


                            }
                        };

            return tasks;
        }

        public void Initialize()
        {
            if (Todos != null)
            {
                // Prevent memory leak in Android
                var tasksCopy = Todos.ToList();
                Todos = new ObservableCollection<ToDo>(tasksCopy);
                return;
            }

            Todos = new ObservableCollection<ToDo>();

            var people = SeedData();
            Todos.Clear();
            foreach (var person in people)
            {
                Todos.Add(person);
            }
        }

        public RelayCommand<string> AddTaskCommand { get; set; }

        private void AddTask(string task)
        {
            var count = Todos.Count;
            var taskText = string.IsNullOrEmpty(task) ? string.Format("Task {0}", count + 1) : task;

            Todos.Add(new ToDo()
            {
                Id = new Guid(),
                Task = taskText,
                StartDate = DateTime.Now,
                DueDate = DateTime.Now.Add(new TimeSpan(5, 0, 0)),
                PercentageComplete = 0


            });
        }
    }
}