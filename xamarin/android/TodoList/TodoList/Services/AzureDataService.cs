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
using System.Threading.Tasks;
using System.Collections;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using TodoList.Models;
using TodoList.Services;
using TodoList.Helpers;

namespace TodoList
{
    public class AzureDataService
    {
        public MobileServiceClient MobileService { get; set; }
        IMobileServiceSyncTable<ToDo> todoTable;

        bool isInitialized;


        public async Task Initialize()
        {
            if (isInitialized)
                return;

            //var time = Xamarin.Insights.TrackTime("InitializeTime");
            //time.Start();


            var handler = new AuthHandler();
            //Create our client
            MobileService = new MobileServiceClient("https://sktodolisttest.azurewebsites.net", handler);
            handler.Client = MobileService;

            if (!string.IsNullOrWhiteSpace(Settings.AuthToken) && !string.IsNullOrWhiteSpace(Settings.UserId))
            {
                MobileService.CurrentUser = new MobileServiceUser(Settings.UserId);
                MobileService.CurrentUser.MobileServiceAuthenticationToken = Settings.AuthToken;
            }

            const string path = "syncstore.db";
            //setup our local sqlite store and intialize our table
            var store = new MobileServiceSQLiteStore(path);

            store.DefineTable<ToDo>();

            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            //Get our sync table that will call out to azure
            todoTable = MobileService.GetSyncTable<ToDo>();

            isInitialized = true;
            //time.Stop();

        }

        public async Task<IEnumerable<ToDo>> GetToDos()
        {
            await Initialize();
            await SyncToDo();
            return await todoTable.OrderBy(c => c.StartDate).ToEnumerableAsync();
        }

        public async Task<ToDo> AddToDo(string task)
        {
            await Initialize();

           // var time = Xamarin.Insights.TrackTime("AddCoffeeTime");
            //time.Start();
            //create and insert coffee
            var todo = new ToDo()
            {
                Id = new Guid(),
                Task = task,
                StartDate = DateTime.Now,
                DueDate = DateTime.Now.Add(new TimeSpan(5, 0, 0)),
                PercentageComplete = 0


            };

            await todoTable.InsertAsync(todo);
            //time.Stop();

            //Synchronize coffee
            await SyncToDo();

            return todo;
        }

        public async Task SyncToDo()
        {
            try
            {
                //pull down all latest changes and then push current coffees up
                await todoTable.PullAsync("allToDos", todoTable.CreateQuery());
                await MobileService.SyncContext.PushAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to sync coffees, that is alright as we have offline capabilities: " + ex);
            }
        }
    }
}