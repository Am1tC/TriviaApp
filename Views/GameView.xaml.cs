using TriviaAppClean.ViewModels;

namespace TriviaAppClean.Views;

public partial class GameView : ContentPage
{
    public GameView(GameViewModel vm)
    {
        InitializeComponent();
        this.BindingContext = vm;
    }
}