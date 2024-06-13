using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Models;
using TriviaAppClean.Services;

namespace TriviaAppClean.ViewModels
{
    public class UsersListViewModel:ViewModelBase
    {
        private ObservableCollection<User> users;
        public ObservableCollection<User> Users
        {
            get
            {
                return this.users;
            }
            set
            {
                this.users = value;
                OnPropertyChanged();
            }
        }
        private TriviaWebAPIProxy usersService;
        public UsersListViewModel(TriviaWebAPIProxy service)
        {
            this.usersService = service;
            users = new ObservableCollection<User>();
            ReadUsers();
        }

        // Private field to track if a server call is in progress
        private bool inServerCall;
        public bool InServerCall
        {
            get
            {
                return this.inServerCall;
            }
            set
            {
                this.inServerCall = value;
                OnPropertyChanged("NotInServerCall");
                OnPropertyChanged("InServerCall");
            }
        }

        // Public property to get the inverse of inServerCall
        public bool NotInServerCall
        {
            get
            {
                return !this.InServerCall;
            }
        }
        private async void ReadUsers()
        {
            InServerCall = true;// Indicate that a server call is in progress

            // Fetch all users from the server
            TriviaWebAPIProxy service = this.usersService;
            List<User> list = await service.GetAllUsers();

            // Update the observable collection with the fetched users
            this.Users = new ObservableCollection<User>(list);
            InServerCall = false;// Indicate that the server call has completed
        }

        // Private field for storing the selected user
        private Object selectedUser;
        public Object SelectedUser
        {
            get
            {
                return selectedUser;
            }
            set
            {
                this.selectedUser = value;
                OnPropertyChanged();
            }
        }
        public ICommand SingleSelectCommand => new Command(OnSingleSelectUser);

        // Async method to handle user selection and navigate to user details page
        async void OnSingleSelectUser()
        {
            if (SelectedUser != null)
            {
                // Prepare navigation parameters
                var navParam = new Dictionary<string, object>()
                {
                    {"selectedUser", SelectedUser }
                };

                // Navigate to the user details page with the selected user
                await Shell.Current.GoToAsync($"userDetails", navParam);

                // Clear the selected user
                SelectedUser = null;
            }
        }
    }
}
