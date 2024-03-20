using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaAppClean.Models;

namespace TriviaAppClean.ViewModels
{
    public class ProfileViewModel:ViewModelBase
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Score { get; set; }
        public int Rank { get; set; }
        private User u;

        public ProfileViewModel()
        {
            this.u = ((App)Application.Current).LoggedInUser;
            Id= this.u.Id;
            this.Name = this.u.Name;
            this.Password = this.u.Password;
            Score = this.u.Score;
            Rank = this.u.Rank;
        }
        
        
    }
}
