using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaAppClean.Models;
using TriviaAppClean.Services;

namespace TriviaAppClean.ViewModels
{
    [QueryProperty(nameof(SelectedUser), "selectedUser")]
    public class UsersDetailsViewModel : ViewModelBase
    {
        private User selectedUser;
        public User SelectedUser
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
    }
}
