using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Models;
using TriviaAppClean.Services;
using TriviaAppClean.Views;

namespace TriviaAppClean.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        #region Construction

        //declaring fields
        private TriviaWebAPIProxy triviaService;
        private RegisterView register;
        private AppShell shell;

        //constructor method
        public LoginViewModel(TriviaWebAPIProxy service, RegisterView register, AppShell shell)
        {
            InServerCall = false;
            this.triviaService = service;
            this.shell = shell;
            this.register = register;

            this.PasswordError = "This is a required field";
            this.EmailError = "This is a required field";

            this.LoginCommand = new Command(OnLogin);//linking LoginCommand command to its respective method
            this.TapCommand = new Command(Tap); //linking TapCommand command to its respective method
        }
        #endregion

        #region password

        //field & property relating to whether an error should be shown
        private bool showPasswordError;

        public bool ShowPasswordError
        {
            get => showPasswordError;
            set
            {
                showPasswordError = value;
                OnPropertyChanged("ShowPasswordError");
            }
        }

        //field & property relating to the password itself
        private string password;

        public string Password
        {
            get => password;
            set
            {
                password = value;
                ValidatePassword();
                OnPropertyChanged("Password");
            }
        }

        //field & property relating to what error should be shown
        private string passwordError;

        public string PasswordError
        {
            get => passwordError;
            set
            {
                passwordError = value;
                OnPropertyChanged("PasswordError");
            }
        }

        //method to check whether a password is valid according to set criteria 
        private void ValidatePassword()
        {
            this.ShowPasswordError = string.IsNullOrEmpty(Password);
        }
        #endregion

        #region email

        //field & property relating to whether an error should be shown
        private bool showEmailError;

        public bool ShowEmailError
        {
            get => showEmailError;
            set
            {
                showEmailError = value;
                OnPropertyChanged("ShowEmailError");
            }
        }

        //field & property relating to the email itself
        private string email;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                ValidateEmail();
                OnPropertyChanged("Email");
            }
        }

        //field & property relating to what error should be shown
        private string emailError;

        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged("EmailError");
            }
        }

        //method to check whether an email is valid according to set criteria
        private void ValidateEmail()
        {
            string email = Email;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            this.showEmailError = !match.Success;
        }
        #endregion

        #region Commands
        //Declaring the commands
        public Command TapCommand { get; set; }
        public ICommand LoginCommand { get; set; }

        private async void Tap()
        {
            await Application.Current.MainPage.Navigation.PushAsync(register);
        }

        private async void OnLogin()
        {
            
            InServerCall = true;// Indicate that a server call is in progress
            //await Shell.Current.GoToAsync("connectingToServer");
            User u = await this.triviaService.LoginAsync(this.Email, this.Password);
            //await Shell.Current.Navigation.PopModalAsync();
            InServerCall = false;// Indicate that the server call has completed

            //Set the application logged in user to be whatever user returned (null or real user)
            ((App)Application.Current).LoggedInUser = u;
            if (u == null)//if the user is null
            {

                await Application.Current.MainPage.DisplayAlert("Login Faild:(", "Try again", "ok");//Display failed alert
            }
            else//if the user isnt null
            {
                await Application.Current.MainPage.DisplayAlert("Success :)", $"Login Succeed! for {u.Name} with {u.Questions.Count} Questions", "ok");//Display success alert
                Password = "";
                Email = "";
                Application.Current.MainPage = shell;
            }
        }
        #endregion

        #region ServerCall
        //Private field to track if a server call is in progress

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

        //Public property to get the inverse of inServerCall
        public bool NotInServerCall
        {
            get
            {
                return !this.InServerCall;//switching the value to be the opposite of the current
            }
        }
        #endregion
        
    }
}
