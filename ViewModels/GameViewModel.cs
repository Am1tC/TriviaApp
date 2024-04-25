using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TriviaAppClean.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        public class GameStartViewModel : ViewModelBase
        {
            public GameStartViewModel()
            {
                this.StartGameCommand = new Command(Start);
            }
            public ICommand StartGameCommand { get; set; }
            public async void Start()
            {
                await Shell.Current.GoToAsync("triviaGame");
            }
        }

    }
}
