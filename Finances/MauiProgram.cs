using Finances.Models;
using Finances.Repositories;
using Finances.Services;
using Microsoft.Extensions.Logging;
using SQLite;

namespace Finances;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        string dbPath = FileSystem.AppDataDirectory;
        dbPath = Path.Combine(dbPath + "finances.db3");
        builder.Services.AddSingleton<MonthlyExpenseRepository>(s => ActivatorUtilities.CreateInstance<MonthlyExpenseRepository>(s, dbPath));
        
        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}