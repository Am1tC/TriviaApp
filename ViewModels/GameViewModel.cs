﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Services;

using TriviaAppClean.Models;
using TriviaAppClean.Views;

namespace TriviaAppClean.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
            private ProfileView profileView;
            private TriviaWebAPIProxy service;
            private User u;

        // Private field to store the current user
        public User CurrentUser
        {
            get => u;
            set
            {
                u = value;
                OnPropertyChanged("CurrentUser");
            }
        }
        // Constructor to initialize the ViewModel with service proxy and profile view
        public GameViewModel(TriviaWebAPIProxy service, ProfileView profileView)
        {
            this.u = ((App)Application.Current).LoggedInUser;// Get the logged-in user
            this.service = service;

            // Initialize commands
            this.CorrectCommand = new Command(this.IfCorrect);
            this.WrongCommand = new Command(this.IfWrong);
            this.NextCommand = new Command(this.IfNextAsync);
            this.QuitCommand = new Command(this.IfQuit);
            // Initialize question
            InitQues();

            // Initialize colors and visibility
            CorrectColor = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1);
            W1Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1);
            W2Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1);
            W3Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1);
            Enabled = true;
            Visible = false;
            this.profileView = profileView;

        }
        // Async method to initialize a question from the service
        private async void InitQues()
            {
                AmericanQuestion amq = await service.GetRandomQuestion();
                QuestionContent = amq.QText;
                CorrectAnswer = amq.CorrectAnswer;
                WrongAnswer1 = amq.Bad1;
                WrongAnswer2 = amq.Bad2;
                WrongAnswer3 = amq.Bad3;
            }
             #region questionContent


            private string questionContent;

            public string QuestionContent
            {
                get => questionContent;
                set
                {
                    questionContent = value;
                    OnPropertyChanged("QuestionContent");
                }
            }
            #endregion

            #region CorrectAnswer
             private string correctAnswer;

            public string CorrectAnswer
            {
                get => correctAnswer;
                set
                {
                    correctAnswer = value;
                OnPropertyChanged("CorrectAnswer");

            }
            }
            #endregion

            #region wrongAnswer1

            private string wrongAnswer1;

            public string WrongAnswer1
            {
                get => wrongAnswer1;
                set
                {
                    wrongAnswer1 = value;
                    OnPropertyChanged("WrongAnswer1");
                }
            }
            #endregion


            #region wrongAnswer2

            private string wrongAnswer2;

            public string WrongAnswer2
            {
                get => wrongAnswer2;
                set
                {
                    wrongAnswer2 = value;
                    OnPropertyChanged("WrongAnswer2");
                }
            }

            #endregion


            #region wrongAnswer3


            private string wrongAnswer3;

            public string WrongAnswer3
            {
                get => wrongAnswer3;
                set
                {
                    wrongAnswer3 = value;
                    OnPropertyChanged("WrongAnswer3");
                }
            }
            #endregion
            // Private field for the dialog message
            private string dialog;
            public string Dialog
            {
                get => dialog;
                set
                {
                    dialog = value;
                    OnPropertyChanged("Dialog");
                }
            }
            private Color dialogColor;
            public Color DialogColor
            {
                get => dialogColor;
                set
                {
                    dialogColor = value;
                    OnPropertyChanged("DialogColor");
                }
            }
            private Color correctColor;
            public Color CorrectColor
            {
                get => correctColor;
                set
                {
                    correctColor = value;
                    OnPropertyChanged("CorrectColor");
                }
            }
            private Color w1Color;
            public Color W1Color
            {
                get => w1Color;
                set
                {
                    w1Color = value;
                    OnPropertyChanged("W1Color");
                }
            }
            private Color w2Color;
            public Color W2Color
            {
                get => w2Color;
                set
                {
                    w2Color = value;
                    OnPropertyChanged("W2Color");
                }
            }
            private Color w3Color;
            public Color W3Color
            {
                get => w3Color;
                set
                {
                    w3Color = value;
                    OnPropertyChanged("W3Color");
                }
            }
            
            // Private field for enabling/disabling controls
            private bool enabled;
            public bool Enabled
            {
                get => enabled;
                set
                {
                    enabled = value;
                    OnPropertyChanged("Enabled");
                }
            }

            // Private field for visibility of the dialog
            private bool visible;
            public bool Visible
            {
                get => visible;
                set
                {
                    visible = value;
                    OnPropertyChanged("Visible");
                }
            }
            public Command SaveQuestionCommand { protected set; get; }
            public Command CorrectCommand { protected set; get; }

            // Async method to handle correct answer
            public async void IfCorrect()
            {
                CurrentUser.Score += 100;// Increase user's score
                await service.UpdateUser(CurrentUser); // Update user on the server
                Dialog = "Correct Answer!";
                DialogColor = Colors.Green;
                CorrectColor = Colors.Green;
                W1Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1);
                W2Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1);
                W2Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1);
                Enabled = false; // Disable further interactions
                Visible = true;// Show dialog
            }
            public Command WrongCommand { protected set; get; }
            // Method to handle wrong answer
            public void IfWrong()
            {
                //User u = ((App)Application.Current).LoggedInUser;
                Dialog = "Wrong Answer!";
                DialogColor = Colors.Red;
                CorrectColor = Colors.Green;
                W1Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1);
                W2Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1);
                W2Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1);
                Enabled = false; // Disable further interactions
                Visible = true;// Show dialog
            }
            public Command NextCommand { protected set; get; }
            // Async method to handle next question
            public async void IfNextAsync()
            {
                Visible = false;// Hide dialog
                Dialog = "";
                AmericanQuestion amq = await service.GetRandomQuestion();
                QuestionContent = amq.QText;
                CorrectAnswer = amq.CorrectAnswer;
                WrongAnswer1 = amq.Bad1;
                WrongAnswer2 = amq.Bad2;
                WrongAnswer3 = amq.Bad3;
                CorrectColor = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1);
                Enabled = true; // Enable interactions
        }

            public Command QuitCommand { protected set; get; }
            // Async method to handle quitting the game
            public async void IfQuit()
            {
                await Application.Current.MainPage.Navigation.PushAsync(profileView); // Navigate to profile view

        }
        }
}

