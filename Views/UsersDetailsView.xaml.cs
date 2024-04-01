namespace TriviaAppClean.Views;
using TriviaAppClean.ViewModels;
public partial class UsersDetailsView : ContentPage
{
	public UsersDetailsView(UsersDetailsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}