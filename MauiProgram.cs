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
        return builder;
    }
    public static MauiAppBuilder AddQuestionView(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<LoginViewModel>();
        return builder;
    }
    public static MauiAppBuilder GameView(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<LoginViewModel>();
        return builder;
    }
    public static MauiAppBuilder LoginView(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<LoginViewModel>();
        return builder;
    }
    public static MauiAppBuilder ProfileView(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<LoginViewModel>();
        return builder;
    }
    public static MauiAppBuilder RecordsView(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<LoginViewModel>();
        return builder;
    }
    public static MauiAppBuilder RegisterView(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<LoginViewModel>();
        return builder;
    }
    public static MauiAppBuilder ShowQuestionView(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<LoginViewModel>();
        return builder;
    }
    public static MauiAppBuilder UserListView(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<LoginViewModel>();
        return builder;
    }
   
}
