//using Microsoft.IdentityModel.Tokens;
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
    internal class UserListViewModel : ViewModelBase
    {
        private Service service;
        private ObservableCollection<User> players;
        public ObservableCollection<User> Players { get { return players; } set { players = value; OnPropertyChanged(); } }
        private bool isRefreshing;
        public bool IsRefreshing { get { return isRefreshing; } set { isRefreshing = value; OnPropertyChanged(); } }
        private Color errorColor;
        public Color ErrorColor { get { return errorColor; } set { errorColor = value; OnPropertyChanged(); } }
        private string errorMsg;
        public string ErrorMsg { get { return errorMsg; } set { this.errorMsg = value; OnPropertyChanged(); } }
        private string emailEntry;
        public string EmailEntry { get { return emailEntry; } set { this.emailEntry = value; OnPropertyChanged(); ((Command)AddPlayerCommand).ChangeCanExecute(); } }
        private string passwordEntry;
        public string PasswordEntry { get { return passwordEntry; } set { this.passwordEntry = value; OnPropertyChanged(); ((Command)AddPlayerCommand).ChangeCanExecute(); } }
        private string playerNameEntry;
        public string PlayerNameEntry { get { return playerNameEntry; } set { this.playerNameEntry = value; OnPropertyChanged(); ((Command)AddPlayerCommand).ChangeCanExecute(); } }
        public ICommand AddPlayerCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand SortByRankCommand { get; private set; }
        public ICommand ResetPlayerPointsCommand { get; private set; }
        public ICommand RemovePlayerCommand { get; private set; }


    }
}
