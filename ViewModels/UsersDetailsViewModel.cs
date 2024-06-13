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
        // Public property to get and set the selected user, with property change notification
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
