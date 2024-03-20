using Microsoft.Extensions.Logging;
using TriviaAppClean.Services;
using TriviaAppClean.ViewModels;
using TriviaAppClean.Views;

namespace TriviaAppClean;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
            .RegisterDataServices()
            .RegisterPages()
            .RegisterViewModels(); 

#if DEBUG
		builder.Logging.AddDebug();
#endif
		
		return builder.Build();
	}
    

    public static MauiAppBuilder RegisterPages(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<LoginView> ();
        builder.Services.AddTransient<RegisterView> ();
        builder.Services.AddTransient<AddQuestionView>();
        builder.Services.AddTransient<ConnectingToServerView>();
        builder.Services.AddTransient<GameView>();
        builder.Services.AddTransient<ProfileView>();
        builder.Services.AddTransient<RecordesView>();
        builder.Services.AddTransient<ShowQuestionView>();
        builder.Services.AddTransient<UserListView>();

        return builder;
    }

    public static MauiAppBuilder RegisterDataServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<TriviaWebAPIProxy>();
        return builder;
    }
    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<RegisterViewModel>();
        builder.Services.AddTransient<AddQuestionViewModel>();
        builder.Services.AddTransient<ConnectingToServerViewModel>();
        builder.Services.AddTransient<GameViewModel>();
        builder.Services.AddTransient<ProfileViewModel>();
        builder.Services.AddTransient<RecordesViewModels>();
        builder.Services.AddTransient<ShowQuestionViewModel>();
        builder.Services.AddTransient<UsersListViewModel>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<AppShell>();


        return builder;
    }
}
