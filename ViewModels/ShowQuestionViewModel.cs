using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaAppClean.Models;
using TriviaAppClean.Services;

namespace TriviaAppClean.ViewModels
{
    public class ShowQuestionViewModel : ViewModelBase
    {
        string searchBar;
        private TriviaWebAPIProxy triviaService;
        private List<AmericanQuestion> q;
        string SerachBar
        {
            get
            { 
                return this.searchBar;
            }
            set 
            { 
                this.searchBar = value;
            }
        }
        public string SearchText
        {
            get
            {
                return this.searchBar;
            }
            set
            {
                this.searchBar = value;
                OnSearchTextChanged();
                OnPropertyChanged();
            }
        }

        private void OnSearchTextChanged()
        {
            ObservableCollection<AmericanQuestion> tmp = new ObservableCollection<AmericanQuestion>();
            ReadQuestions();
            if (!String.IsNullOrEmpty(SearchText))
            {
                foreach (AmericanQuestion qu in this.q)
                {
                    if (qu.QText.IndexOf(searchBar, StringComparison.OrdinalIgnoreCase) == -1)
                    {
                        tmp.Add(qu);
                    }
                }

                foreach (AmericanQuestion que in tmp)
                {
                    if (!this.Questions.Contains(que))
                    {
                        this.Questions.Remove(que);
                    }
                }
            }




        }
        private void ReadQuestions()
        {

            this.Questions = new ObservableCollection<AmericanQuestion>(questions);

        }

        ObservableCollection<AmericanQuestion> questions;
        ObservableCollection<AmericanQuestion> Questions
        {
            get
            {
                return this.questions;
            }
            set
            {
                this.questions = value;
                OnPropertyChanged();
            }
        }




    
    }
}
