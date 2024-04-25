using TriviaAppClean.ViewModels;
namespace TriviaAppClean.Views;
public partial class GameView : ContentPage
{
    public GameView(GameViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
        //RandomizeChildren();
    }
    private void RandomizeChildren()
    {
        var children = stackLayout.Children.ToList();
        stackLayout.Children.Clear();

        var rnd = new Random();
        while (children.Count > 0)
        {
            var index = rnd.Next(0, children.Count);
            var child = children[index];
            children.RemoveAt(index);
            stackLayout.Children.Add(child);
        }
    }
}