using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Linq;
using TodoList.Models;
using System;
using GalaSoft.MvvmLight.Views;
using TodoList.Helpers;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace TodoList.ViewModels
{
    public class TodoListViewModel : ViewModelBase
    {
        AzureDataService azureService;

        public ObservableCollection<ToDo> Todos { get; private set; }

        private readonly INavigationService navigationService;

        

        public TodoListViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
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

        public async Task Initialize()
        {
            if (Todos != null)
            {
                // Prevent memory leak in Android
                var tasksCopy = Todos.ToList();
                Todos = new ObservableCollection<ToDo>(tasksCopy);
                return;
            }

            Todos = new ObservableCollection<ToDo>();

            if (!Settings.IsLoggedIn)
            {
                await azureService.Initialize();
                var user = await DependencyService.Get<IAuthentication>().LoginAsync(azureService.MobileService, MobileServiceAuthenticationProvider.MicrosoftAccount);
                if (user == null)
                    return;
            }

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

        private RelayCommand navigateCommand;

        /// <summary>
		/// Gets the NavigateCommand.
		/// Goes to the second page, using the navigation service.
		/// Use the "mvvmr*" snippet group to create more such commands.
		/// </summary>
		public RelayCommand NavigateCommand
        {
            get
            {
                return navigateCommand
                    ?? (navigateCommand = new RelayCommand(() => navigationService.NavigateTo(
                            ViewModelLocator.DetailsPageKey)));
            }
        }
    }
}