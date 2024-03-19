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
            //ReadStudents();
            if (!String.IsNullOrEmpty(SearchText))
            {
                foreach (User us in this.users)
                {
                    if (us.Name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) == -1)
                    {
                        temp.Add(us);
                    }
                }

                foreach (User us in temp)
                {
                    if (this.users.Contains(us))
                    {
                        this.users.Remove(us);
                    }
                }
            }
        }

        private async void ReadUsers(TriviaWebAPIProxy service)
        {
            this.triviaService = service;
            UsersService service = new UsersService();
            List<User> list = await service.GetUsers();
            this.Users = new ObservableCollection<User>(list);
        }
    }
}
