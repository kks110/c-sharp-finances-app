using Finances.Repositories;

namespace Finances;

public partial class App : Application
{
    public static MonthlyExpenseRepository MonthlyExpenseRepo { get; private set; }
    
    public App(MonthlyExpenseRepository repo)
    {
        InitializeComponent();

        MainPage = new MainPage();
        
        MonthlyExpenseRepo = repo;
    }
}