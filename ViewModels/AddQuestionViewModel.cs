using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Models;
using TriviaAppClean.Services;
using TriviaAppClean.Views;

namespace TriviaAppClean.ViewModels
{
    public class AddQuestionViewModel : ViewModelBase
    {
        private string errorComment;
        // Public property for getting and setting error comment, with property change notification
        public string ErrorComment
        {
            get
            {
                return errorComment;
            }
            set
            {
                errorComment = value;
                OnPropertyChanged();
            }
        }

        private bool showErrorComment;
        // Public property for getting and setting showErrorComment, with property change notification
        public bool ShowErrorComment
        {
            get
            {
                return showErrorComment;
            }
            set
            {
                showErrorComment = value;
                OnPropertyChanged();
            }
        }

        private bool isAddingEnabled;
        // Public property for getting and setting  if is adding a question is enabled 
        public bool IsAddingEnabled
        {
            get
            {
                return isAddingEnabled;
            }
            set
            {
                isAddingEnabled = value;
                OnPropertyChanged();
            }
        }

        // Method to check if the user is eligible to add a question
        private bool AddQuestionEligible()
        {
            if (((App)Application.Current).LoggedInUser.Rank == 2)
            {
                return true;
            }
            else if (((App)Application.Current).LoggedInUser.Questions.Count < ((App)Application.Current).LoggedInUser.Score / 100)
            {
                return true;

            }
            else
            {
                return false;

            }
        }


        private string question;
        public string Question
        {
            get
            {
                return question;
            }
            set
            {
                question = value;
                OnPropertyChanged();
            }
        }

        private string rightAnswer;
        public string RightAnswer
        {
            get
            {
                return rightAnswer;
            }
            set
            {
                rightAnswer = value;
                OnPropertyChanged();
            }
        }

        private string wrongAnswer1;
        public string WrongAnswer1
        {
            get
            {
                return wrongAnswer1;
            }
            set
            {
                wrongAnswer1 = value;
                OnPropertyChanged();
            }
        }

        private string wrongAnswer2;
        public string WrongAnswer2
        {
            get
            {
                return wrongAnswer2;
            }
            set
            {
                wrongAnswer2 = value;
                OnPropertyChanged();
            }
        }

        private string wrongAnswer3;
        public string WrongAnswer3
        {
            get
            {
                return wrongAnswer3;
            }
            set
            {
                wrongAnswer3 = value;
                OnPropertyChanged();
            }
        }

        private TriviaWebAPIProxy triviaService;
        private ConnectingToServerView connectingToServerView;
        public AddQuestionViewModel(TriviaWebAPIProxy service, ConnectingToServerView connect)
        {
            this.triviaService = service;
            this.connectingToServerView = connect;
            this.ErrorComment = "For every 100 points you make, you can add a Q !!, unless you are a manager";
            // Check if user is eligible to add questions and set properties accordingly
            if (AddQuestionEligible())
            {
                this.isAddingEnabled = true;
                this.ShowErrorComment = false;
            }
            else
            {
                this.isAddingEnabled = false;
                this.ShowErrorComment = true;
            }
            this.AddQuestionCommand = new Command(OnAddQuestion);
        }

        public ICommand AddQuestionCommand { get; set; }

        // Private field to track if a server call is in progress
        private bool inServerCall;
        // Public property for getting and setting inServerCall, with property change notification
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
        public bool NotInServerCall
        {
            get
            {
                return !this.InServerCall;
            }
        }

        private async void OnAddQuestion()
        {
            AmericanQuestion quest = new AmericanQuestion();
            quest.QText = question;
            quest.CorrectAnswer = rightAnswer;
            quest.Bad1 = wrongAnswer1;
            quest.Bad2 = wrongAnswer2;
            quest.Bad3 = wrongAnswer3;
            quest.UserId = ((App)Application.Current).LoggedInUser.Id;
            quest.Status = 0;
            // Indicate that a server call is in progress
            InServerCall = true;
            // Post the new question to the server and await the result
            bool a = await this.triviaService.PostNewQuestion(quest);
            // Indicate that the server call has completed
            InServerCall = false;


            if (a == true)
            {
                // Display success message
                await Shell.Current.DisplayAlert("Add Qustion", "Question is added to the game database successfully !!", "ok");
                // Update eligibility and error comment visibility
                if (AddQuestionEligible())
                {
                    this.isAddingEnabled = true;
                    this.ShowErrorComment = false;
                }
                else
                {
                    this.isAddingEnabled = false;
                    this.ShowErrorComment = true;
                }
                // Clear the question and answer fields
                this.Question = "";
                this.RightAnswer = "";
                this.WrongAnswer1 = "";
                this.WrongAnswer2 = "";
                this.WrongAnswer3 = "";
            }
            else
            {
                // Display failure message
                await Shell.Current.DisplayAlert("Add Qustion", "Question has failed to enter the gane database", "ok");
                // Update eligibility and error comment visibility
                if (AddQuestionEligible())
                {
                    this.isAddingEnabled = true;
                    this.ShowErrorComment = false;
                }
                else
                {
                    this.isAddingEnabled = false;
                    this.ShowErrorComment = true;
                }
                // Clear the question and answer fields
                this.Question = "";
                this.RightAnswer = "";
                this.WrongAnswer1 = "";
                this.WrongAnswer2 = "";
                this.WrongAnswer3 = "";
            }

        }

    }
}
