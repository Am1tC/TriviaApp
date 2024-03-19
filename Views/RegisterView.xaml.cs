using TriviaAppClean.ViewModels;

namespace TriviaAppClean.Views;

public partial class RegisterView : ContentPage
{
	public RegisterView(RegisterViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}