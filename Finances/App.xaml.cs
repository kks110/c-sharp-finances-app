using Finances.Repositories;

namespace Finances;

public partial class App : Application
{
    public static ExpenseRepository ExpenseRepo { get; private set; }
    public static UserRepository UserRepo { get; private set; }
    
    public App(ExpenseRepository expenseRepo, UserRepository userRepo)
    {
        InitializeComponent();

        MainPage = new MainPage();
        
        ExpenseRepo = expenseRepo;
        UserRepo = userRepo;
    }
}