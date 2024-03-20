using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Models;
using TriviaAppClean.Services;

namespace TriviaAppClean.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        //private string email;
        //public string Email
        //{
        //    get
        //    {
        //        return email;
        //    }
        //    set
        //    {
        //        email = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private string name;
        //public string Name
        //{
        //    get
        //    {
        //        return name;
        //    }
        //    set
        //    {
        //        name = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private string password;
        //public string Password
        //{
        //    get
        //    {
        //        return password;
        //    }
        //    set
        //    {
        //        password = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private string passwordApprove;
        //public string PasswordApprove
        //{
        //    get
        //    {
        //        return passwordApprove;
        //    }
        //    set
        //    {
        //        passwordApprove = value;
        //        OnPropertyChanged();
        //    }
        //}

        private int id;
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        private TriviaWebAPIProxy triviaService;
        public RegisterViewModel(TriviaWebAPIProxy service)
        {
            this.triviaService = service;
            this.SignUpCommand = new Command(OnSignUp);
        }

        public ICommand SignUpCommand { get; set; }

        private async void OnSignUp()
        {

            User u = new User();
            u.Email = Email;
            u.Password = Password;
            u.Name = Name;
            u.Score = 0;
            await Shell.Current.GoToAsync("connectingToServer");
            bool a = await this.triviaService.RegisterUser(u);
            await Shell.Current.Navigation.PopModalAsync();
            if (a == true)
            {
                ((App)Application.Current).LoggedInUser = u;
                await Shell.Current.DisplayAlert("Sign Up", $"Sign Up succeed !! for {u.Name}", "ok");
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await Shell.Current.DisplayAlert("Sign Up", "Sign Up Failed", "ok");
            }
            
        }
        #region name
        private bool showNameError;

        public bool ShowNameError
        {
            get => showNameError;
            set
            {
                showNameError = value;
                OnPropertyChanged("ShowNameError");
            }
        }

        private string name;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                ValidateName();
                OnPropertyChanged("Name");
            }
        }

        private string nameError;

        public string NameError
        {
            get => nameError;
            set
            {
                nameError = value;
                OnPropertyChanged("NameError");
            }
        }

        private void ValidateName()
        {
            this.ShowNameError = string.IsNullOrEmpty(Name);
        }
        #endregion

        #region password
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

        private string password;

        public string Password
        {
            get => password;
            set
            {
                password = value;
                ValidateName();
                OnPropertyChanged("Password");
            }
        }

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

        private void ValidatePassword()
        {
            this.ShowPasswordError = string.IsNullOrEmpty(Password);
        }
        #endregion


        #region email
        private bool showEmailError;

        public bool ShowEmailError
        {
            get => showNameError;
            set
            {
                showNameError = value;
                OnPropertyChanged("ShowEmailError");
            }
        }

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

        private void ValidateEmail()
        {
            string email = Email;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            this.ShowNameError = !match.Success;
        }
        #endregion



    }
}
