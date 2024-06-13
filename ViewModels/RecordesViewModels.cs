using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaAppClean.Models;
using TriviaAppClean.Services;

namespace TriviaAppClean.ViewModels
{
    public class RecordesViewModels : ViewModelBase
    {
        private TriviaWebAPIProxy triviaService;
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

        // List of all users fetched from the server
        private List<User> allUsers;

        // Text for searching users
        private string searchText;
        public string SearchText
        {
            get
            {
                return this.searchText;
            }
            set
            {
                this.searchText = value;
                OnSearchTextChanged();// Trigger search when text changes
                OnPropertyChanged();
            }
        }


        // Method to handle changes in search text and filter the users list
        private void OnSearchTextChanged()
        {
            ObservableCollection<User> temp = new ObservableCollection<User>();
            ReadUsers();// Refresh the users list
            if (!String.IsNullOrEmpty(SearchText))
            {
                // Filter users based on search text
                foreach (User us in this.allUsers)
                {
                    if (us.Name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) == -1)
                    {
                        temp.Add(us);
                    }
                }

                // Remove filtered users from the observable collection
                foreach (User us in temp)
                {
                    if (this.Users.Contains(us))
                    {
                        this.Users.Remove(us);
                    }
                }
            }
        }

        // Constructor to initialize the ViewModel with service proxy
        public RecordesViewModels(TriviaWebAPIProxy service)
        {
            triviaService = service;
            ReadUsersFromServer();// Fetch users from the server
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

        // Async method to read users from the server and update the observable collection
        private async void ReadUsersFromServer()
        {
            InServerCall = true; // Indicate that a server call is in progress
            List<User> list = await triviaService.GetAllUsers();
            // Order users by score in descending order
            list = list.OrderByDescending(u => u.Score).ToList();
            // Store all users and update the observable collection
            this.allUsers = list;
            this.Users = new ObservableCollection<User>(list);
            // Indicate that the server call has completed
            InServerCall = false;
        }

        // Method to refresh the users list from the allUsers list
        private void ReadUsers()
        {

            this.Users = new ObservableCollection<User>(allUsers);

        }
    }
}
