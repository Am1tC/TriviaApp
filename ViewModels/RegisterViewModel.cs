using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Models;
using TriviaAppClean.Services;
using TriviaAppClean.ViewModels;


namespace TriviaAppClean.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        #region FormValidation
        #region Name
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
                ValidatePassword();
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
            this.ShowPasswordError = Password == null || Password.Length < 8; // need to inclode one letter  
        }
        #endregion
        #region Email
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
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(Email);
           ShowEmailError = !match.Success;
        }
        #endregion
        
        public Command SaveDataCommand { protected set; get; }
        private TriviaWebAPIProxy triviaService;
        public RegisterViewModel(TriviaWebAPIProxy service)
        {
            this.NameError = "This is must";
            this.ShowNameError = false;
            this.PasswordError = "The password must include at least 8 digits and with at least 1 letter";
            this.ShowPasswordError = false;
            this.EmailError = "The email format is incorrect";
            this.ShowEmailError = false;
            this.SaveDataCommand = new Command(() => SaveData());
            this.triviaService = service;
            ;
        }
        //This function validate the entire form upon submit!
        private bool ValidateForm()
        {
            //Validate all fields first
            ValidatePassword();
            ValidateEmail();
            ValidateName();

            //check if any validation failed
            if (ShowPasswordError || ShowEmailError ||
                ShowNameError)
                return false;
            return true;
        }


        private async void SaveData()
        {
            if (ValidateForm())
            {
                User u = new User();
                u.Email = this.Email;
                u.Password = this.Password;
                u.Name = this.Name;
                u.Rank = 1;
                u.Score = 0;
                if (await this.triviaService.RegisterUser(u))
                {
                    await App.Current.MainPage.DisplayAlert("Save deta", "your deta is saved", "Confirm");
                    await App.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Save deta", "there is a problem with your deta", "Confirm");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Save deta", "there is a problem with your deta", "Confirm");
            }

        }
        #endregion
    }
}
