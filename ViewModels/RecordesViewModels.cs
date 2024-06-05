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

        private List<User> allUsers;

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
                OnSearchTextChanged();
                OnPropertyChanged();
            }
        }
        
        

        private void OnSearchTextChanged()
        {
            ObservableCollection<User> temp = new ObservableCollection<User>();
            ReadUsers();
            if (!String.IsNullOrEmpty(SearchText))
            {
                foreach (User us in this.allUsers)
                {
                    if (us.Name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) == -1)
                    {
                        temp.Add(us);
                    }
                }

                foreach (User us in temp)
                {
                    if (this.Users.Contains(us))
                    {
                        this.Users.Remove(us);
                    }
                }
            }
        }
        public RecordesViewModels(TriviaWebAPIProxy service)
        {
            triviaService = service;
            ReadUsersFromServer();
        }


        private async void ReadUsersFromServer()
        {
            await Shell.Current.GoToAsync("connectingToServer");
            List<User> list = await triviaService.GetAllUsers();
            list = list.OrderByDescending(u => u.Score).ToList();
            this.allUsers = list;
            this.Users = new ObservableCollection<User>(list);
            await Shell.Current.Navigation.PopModalAsync();

        }

        private void ReadUsers()
        {

            this.Users = new ObservableCollection<User>(allUsers);

        }
    }
}
