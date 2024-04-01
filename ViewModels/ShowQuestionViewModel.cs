using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaAppClean.Services;

namespace TriviaAppClean.ViewModels
{
    public class ShowQuestionViewModel : ViewModelBase
    {
        string searchBar;
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
          
        }

        ObservableCollection<Questions> questions;
        ObservableCollection<Questions> Questions
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
