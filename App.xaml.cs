using TriviaAppClean.Models;
using TriviaAppClean.Views;

namespace TriviaAppClean;

public partial class App : Application
{
	//Use this class to store global application data that should be accessible throughout the entire app!
	public User LoggedInUser { get; set; }
    public LoginView Login { get; set; }


    public App(LoginView v)
	{
		LoggedInUser = null;
		InitializeComponent();
		Login = v;
		MainPage = new NavigationPage(v);
	}
	//הצעה לבעיה שיש לנו עם הLOGIN
	// צריך לסדר את זה - שגיא דרורי
}
