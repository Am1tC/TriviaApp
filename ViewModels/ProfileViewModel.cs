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

        //declaring fields
        private int id;
        private string email;
        private string name;
        private string pass;
        private int score;
        private int rank;
        private User u;
        private TriviaWebAPIProxy service;

        //declaring fields' respective properties
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

        //Propety for the field name
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

        //field & property relating to whether an error should be shown
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

        //field & property relating to which error should be shown
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

        //method to check whether a name is valid according to set criteria 
        private void ValidateName()
        {
            this.ShowNameError = string.IsNullOrEmpty(Name);
        }
        #endregion

        #region Password
        //since password is a more complex field, which requres more properties
        //it has a region dedicated to declaring its properties

        //Propety for the field pass
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

        //field & property relating to which error should be shown
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
            this.ShowPasswordError = string.IsNullOrEmpty(Pass) ||  Pass.Length<8;
        }
        #endregion

        #region Email
        //since email is a more complex field, which requres more properties
        //it has a region dedicated to declaring its properties

        //Propety for the field email
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

        //field & property relating to which error should be shown
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
            this.ShowEmailError = !match.Success;
        }

        #endregion
        #endregion

        #region Commands
        public Command UpdateCommand { get; set; } //Declaring a command responsible for updating user information
        public Command NameSwitchCommand { get; set; }//Declaring a command responsible for switching whether the name should be editable
        public Command EmailSwitchCommand { get; set; }//Declaring a command responsible for switching whether the email should be editable
        public Command PasswordSwitchCommand { get; set; }//Declaring a command responsible for switching whether the password should be editable
        #endregion

        #region Constructor
        //Constructing the class
        public ProfileViewModel(TriviaWebAPIProxy service)
        {
            this.u = ((App)Application.Current).LoggedInUser; //saving the current user
            Id = this.u.Id;
            this.Name = this.u.Name;
            this.pass = this.u.Password;
            Score = this.u.Score;
            Rank = this.u.Rank;
            Email = this.u.Email;
            this.Qs = new ObservableCollection<AmericanQuestion>(u.Questions); //Creating a collection of the object's questions

            NameError = "Must have a name";
            PasswordError = "The password must include at least 8 digits and with at least 1 letter";
            EmailError = "The email format is incorrect";

            this.service = service;//saving the service

            //Linking the commands to their respective methods
            this.UpdateCommand = new Command(Update); 
            NameSwitchCommand = new Command(editName);
            EmailSwitchCommand = new Command(editEmail);
            PasswordSwitchCommand = new Command(editPassword);

            //Setting the values to be false upon start
            nameEdit = false;
            passwordEdit = false;
            emailEdit = false;

        }
        #endregion

        #region UpdateInfo
        private async void Update()
        {
            //Validating inputed information
            ValidateEmail();
            ValidateName();
            ValidatePassword();

            //checking whether the information is valid
            if (!ShowNameError && !showEmailError && !showPasswordError)
            {
                CurrentUser.Email = Email;
                CurrentUser.Password = this.Pass;
                CurrentUser.Name = this.Name;

                bool isUpdated;
                isUpdated = await service.UpdateUser(CurrentUser);//trying to update information in server
                if (isUpdated)//if the information was updated
                {
                   await Application.Current.MainPage.DisplayAlert("Success!","Updated your information to be as follows","Close");//show succes alert
                   ((App)Application.Current).LoggedInUser = CurrentUser;//update the logged in user to have the updated information
                }
                else//if the update wasnt successful 
                {
                    await Application.Current.MainPage.DisplayAlert("Failed to update", "Error, try again, and if repeats several times, check your information", "Close");//display fail to update alert
                }
            }
            else//if the input information was invalid
            {
                await Application.Current.MainPage.DisplayAlert("Failed to update", "Invalid input", "Close");//dispaly fail to update alert
            }
        }
        #endregion

        #region EnableEdit
        //declaring variables relating to whether the input fields should be editable
        private bool nameEdit;
        private bool emailEdit;
        private bool passwordEdit;

        //declaring their respective variables
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

        //Methods to switch between whether the fields should and shouldn't be editable
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
        //declaring a list of questions
        private ObservableCollection<AmericanQuestion> qs;

        //declaring the list's property
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
