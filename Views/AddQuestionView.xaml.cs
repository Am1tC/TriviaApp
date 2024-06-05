namespace TriviaAppClean.Views;
using TriviaAppClean.ViewModels;


public partial class AddQuestionView : ContentPage
{
    public AddQuestionView(AddQuestionViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
}