using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaAppClean.Models;
using System.Text.RegularExpressions;
using TriviaAppClean.Services;

namespace TriviaAppClean.ViewModels
{
    public class ProfileViewModel:ViewModelBase
    {
        private bool nameEdit;
        private bool emailEdit;
        private bool passwordEdit;


        private int id;
        private string email;
        private string name;
        private string pass;
        private int score;
        private int rank;
        private User u;
        private TriviaWebAPIProxy service;
        private string updateMsg;

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




        //public string Email
        //{
        //    get { return email; }
        //    set { this.email = value; }
        //}
        //public string Name
        //{
        //    get { return name; }
        //    set { name = value; }
        //}
        //public string Pass
        //{
        //    get { return pass; }
        //    set { pass = value; }
        //}
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
            this.ShowPasswordError = string.IsNullOrEmpty(pass);
        }
        #endregion


        #region email
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
            this.ShowEmailError = !match.Success;
        }
        #endregion

        public ProfileViewModel(TriviaWebAPIProxy service)
        {
            this.u = ((App)Application.Current).LoggedInUser;
            Id= this.u.Id;
            this.Name = this.u.Name;
            this.pass = this.u.Password;
            Score = this.u.Score;
            Rank = this.u.Rank;

            this.UpdateCommand = new Command(Update);

            NameSwitchCommand = new Command(editName);
            EmailSwitchCommand = new Command(editEmail);
            PasswordSwitchCommand = new Command(editPassword);

            updateMsg = "";
            nameEdit = false;
            passwordEdit = false;
            emailEdit = false;

        }
        public Command UpdateCommand { get; set; }
        public Command NameSwitchCommand { get; set; }
        public Command EmailSwitchCommand { get; set; }
        public Command PasswordSwitchCommand { get; set; }

        private async void Update()
        {
            ValidateEmail();
            ValidateName();
            ValidatePassword();

            if(!ShowNameError && !showEmailError && !showPasswordError)
            {
                u.Email = Email;
                u.Password = this.Pass;
                u.Name = this.Name;

                bool isUpdated;
                isUpdated = await service.UpdateUser(u);
                if (isUpdated)
                {
                    updateMsg = "Updated Successfully!";
                }
                else
                {
                    updateMsg = "Failed to update";
                }
            }
        }

        private void editName()
        {
            nameEdit = !nameEdit;
        }
        private void editPassword()
        {
            passwordEdit = !passwordEdit;
        }
        private void editEmail()
        {
            emailEdit = !emailEdit;
        }
    }
}