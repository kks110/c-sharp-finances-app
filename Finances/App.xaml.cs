using Finances.Repositories;

namespace Finances;

public partial class App : Application
{
    public static ExpenseRepository ExpenseRepo { get; private set; }
    
    public App(ExpenseRepository repo)
    {
        InitializeComponent();

        MainPage = new MainPage();
        
        ExpenseRepo = repo;
    }
}