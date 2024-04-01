using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaAppClean.ViewModels
{
    public class ShowQuestionDetailsViewModel: ViewModelBase
    {
        private string correctAns;
        private string wrongAns1;
        private string qtext;
        private string wrongAns2;
        private string wrongAns3;
        private string status;
        private Command update;

        public string CorrectAns
        {
            get { return this.correctAns; }
            set { this.correctAns = value; OnPropertyChanged(); }

        }
        public string WrongAns1
        { 
            get { return this.wrongAns1; }
            set { this.wrongAns1 = value; OnPropertyChanged(); }
        }
        public string Qtext
        {
            get { return this.Qtext; }
            set { this.qtext = value; OnPropertyChanged(); }
        }
        public string WrongAns2
        {
            get { return this.wrongAns2; }
            set { this.wrongAns2 = value; OnPropertyChanged(); }
        }
        public string WrongAns3
        {
            get { return this.wrongAns3; }
            set { this.wrongAns3 = value; OnPropertyChanged(); }
        }
        public string Status
        {
            get { return this.status; }
            set { this.status = value; OnPropertyChanged(); }
        }
        public Command Update;







    }
}
