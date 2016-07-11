using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;


namespace TodoList.ViewModels
{
    public class ViewModelLocator
    {

        public const string DetailsPageKey = "DetailsPage";

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<TodoListViewModel>();
        }

        public TodoListViewModel TodoList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TodoListViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}