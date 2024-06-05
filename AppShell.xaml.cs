using TriviaAppClean.Views;
using System.Windows.Input;
using System;
using TriviaAppClean.ViewModels;
namespace TriviaAppClean;

public partial class AppShell : Shell
{
	public AppShell(ShellViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;
        RegisterRoutes();
	}

	private void RegisterRoutes()
	{
        Routing.RegisterRoute("connectingToServer", typeof(ConnectingToServerView));
        Routing.RegisterRoute("AQV", typeof(AddQuestionView));
        Routing.RegisterRoute("GV", typeof(GameView));
        Routing.RegisterRoute("LV", typeof(LoginView));
        Routing.RegisterRoute("PV", typeof(ProfileView));
        Routing.RegisterRoute("RecV", typeof(RecordesView));
        Routing.RegisterRoute("RegV", typeof(RegisterView));
        Routing.RegisterRoute("SQV", typeof(ShowQuestionView));
        Routing.RegisterRoute("ULV", typeof(UserListView));
        Routing.RegisterRoute("userDetails", typeof(UsersDetailsView));

    }
}
