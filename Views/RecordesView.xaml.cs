using TriviaAppClean.ViewModels;

namespace TriviaAppClean.Views;

public partial class RecordesView : ContentPage
{
	public RecordesView(RecordesViewModels vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}