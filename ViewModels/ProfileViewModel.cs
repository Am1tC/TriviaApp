using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
System.Windows.Input;
using TriviaAppClean.Models;
using TriviaAppClean.Services;
using TriviaAppClean.Views;
System.Text.RegularExpressions;

namespace TriviaAppClean.ViewModels
{
    public class ProfileViewModel:ViewModelBase
    {

        public int id;
        public string email;
        public string name;
        public string pass;
        public int score;
        public int rank;
        private User u;

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

        public ProfileViewModel()
        {
            this.u = ((App)Application.Current).LoggedInUser;
            Id= this.u.Id;
            this.Name = this.u.Name;
            this.Password = this.u.Password;
            Score = this.u.Score;
            Rank = this.u.Rank;

            this.UpdateCommand = new Command(Update);
        }
        public Command UpdateCommand { get; set; }
        private async void Update()
        {

        }
    }
}