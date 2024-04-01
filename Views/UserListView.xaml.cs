using TriviaAppClean.ViewModels;

namespace TriviaAppClean.Views;

public partial class UserListView : ContentPage
{
	public UserListView(UsersListViewModel vm)
	{
        this.BindingContext = vm;
        InitializeComponent();
    }
}