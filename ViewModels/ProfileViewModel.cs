using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaAppClean.Models;
using System.Text.RegularExpressions;
using TriviaAppClean.Services;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;

namespace TriviaAppClean.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {

        #region declareAttributesAndProperties
        private int id;
        private string email;
        private string name;
        private string pass;
        private int score;
        private int rank;
        private User u;
        private TriviaWebAPIProxy service;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }
        public int Rank
        {
            get { return rank; }
            set { rank = value; }
        }

       

        public User CurrentUser
        {
            get { return u; }
            set { CurrentUser = value; }
        }

        #region Name
        //since name is a more complex field, which requres more properties
        //it has a region dedicated to declaring its properties
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

        public string Pass
        {
            get => pass;
            set
            {
                pass = value;
                ValidatePassword();
                OnPropertyChanged("Password");
            }
        }

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
            this.ShowPasswordError = string.IsNullOrEmpty(Pass) ||  Pass.Length<8;
        }
        #endregion


        #region email
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                ValidateEmail();
                OnPropertyChanged("Email");
            }
        }

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
            this.ShowEmailError = !match.Success;
        }

        #endregion
        #endregion

        #region Commands
        public Command UpdateCommand { get; set; }
        public Command NameSwitchCommand { get; set; }
        public Command EmailSwitchCommand { get; set; }
        public Command PasswordSwitchCommand { get; set; }
        #endregion

        public ProfileViewModel(TriviaWebAPIProxy service)
        {
            this.u = ((App)Application.Current).LoggedInUser;
            Id = this.u.Id;
            this.Name = this.u.Name;
            this.pass = this.u.Password;
            Score = this.u.Score;
            Rank = this.u.Rank;
            Email = this.u.Email;
            this.Qs = new ObservableCollection<AmericanQuestion>(u.Questions);

            NameError = "Must have a name";
            PasswordError = "The password must include at least 8 digits and with at least 1 letter";
            EmailError = "The email format is incorrect";

            this.service = service;

            this.UpdateCommand = new Command(Update);
            NameSwitchCommand = new Command(editName);
            EmailSwitchCommand = new Command(editEmail);
            PasswordSwitchCommand = new Command(editPassword);

            nameEdit = false;
            passwordEdit = false;
            emailEdit = false;

        }

        private async void Update()
        {
            ValidateEmail();
            ValidateName();
            ValidatePassword();

            if (!ShowNameError && !showEmailError && !showPasswordError)
            {
                CurrentUser.Email = Email;
                CurrentUser.Password = this.Pass;
                CurrentUser.Name = this.Name;

                bool isUpdated;
                isUpdated = await service.UpdateUser(CurrentUser);
                if (isUpdated)
                {

                    await Application.Current.MainPage.DisplayAlert("Succes!","Updated your information to be as follows","Close");
                   ((App)Application.Current).LoggedInUser = CurrentUser;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Failed to update", "Error, try again, and if repeats several times, check your information", "Close");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Failed to update", "Invalid input", "Close");
            }
        }

        #region EnableEdit
        private bool nameEdit;
        private bool emailEdit;
        private bool passwordEdit;

        public bool NameEdit
        {
            get { return nameEdit; }
            set
            {
                nameEdit = value;
                OnPropertyChanged("NameEdit");
            }
        }

        public bool EmailEdit
        {
            get { return emailEdit; }
            set
            {
                emailEdit = value;
                OnPropertyChanged("EmailEdit");
            }
        }

        public bool PasswordEdit
        {
            get { return passwordEdit; }
            set
            {
                passwordEdit = value;
                OnPropertyChanged("PasswordEdit");
            }
        }

        private void editName()
        {
            NameEdit = !NameEdit;
        }
        private void editPassword()
        {
            PasswordEdit = !PasswordEdit;
        }
        private void editEmail()
        {
            EmailEdit = !EmailEdit;
        }

        #endregion




        #region QuestionList
        private ObservableCollection<AmericanQuestion> qs;
        public ObservableCollection<AmericanQuestion> Qs
        {
            get
            {
                return this.qs;
            }
            set
            {
                this.qs = value;
                OnPropertyChanged();
            }
        }


        #endregion


    }
}
