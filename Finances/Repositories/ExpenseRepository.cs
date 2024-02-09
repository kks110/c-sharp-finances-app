using Finances.Models;
using SQLite;

namespace Finances.Repositories;

public class ExpenseRepository
{
    string _dbPath;

    public string StatusMessage { get; set; }

    private SQLiteConnection conn;

    private void Init()
    {
        if (conn != null)
            return;

        conn = new SQLiteConnection(_dbPath);
        conn.CreateTable<Expense>();
    }

    public ExpenseRepository(string dbPath)
    {
        _dbPath = dbPath;                        
    }

    public int AddNewExpense(string name, int amount, string? notes, bool weekly = false, bool variable = false)
    {            
        int result = 0;
        try
        {
            Init();

            // basic validation to ensure a name was entered
            if (string.IsNullOrEmpty(name))
                throw new Exception("Valid name required");
            
            // basic validation to ensure an amount was entered
            if (int.IsPositive(amount) == false)
                throw new Exception("Valid amount required");

            result = conn.Insert(
                new Expense
                {
                    Name = name, 
                    Amount = amount, 
                    Notes = notes, 
                    Weekly = weekly,
                    Variable = variable
                }
            );

            StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, name);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
        }

        return result;

    }

    public List<Expense> GetAllExpenses()
    {
        try
        {
            Init();
            return conn.Table<Expense>().ToList();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new List<Expense>();
    }

    public List<Expense> GetAllStaticExpenses()
    {
        try
        {
            Init();
            var expenses = (
                from Exp in conn.Table<Expense>()
                where Exp.Variable == false
                select Exp).ToList();
            return expenses;
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new List<Expense>();
    }
    
    public List<Expense> GetAllVariableExpenses()
    {
        try
        {
            Init();
            var expenses = (
                from Exp in conn.Table<Expense>()
                where Exp.Variable == true
                select Exp).ToList();
            return expenses;
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new List<Expense>();
    }
    
    
    public Expense FindExpenseById(int id)
    {
        try
        {
            Init();
            var expense = from monExp in conn.Table<Expense>()
                                 where monExp.Id == id
                                 select monExp;
            return expense.FirstOrDefault();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new Expense();
    }

    public int UpdateExpense(Expense expense)
    {
        int result = 0;
        try
        {
            result = conn.Update(expense);
            return result;
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return result;
    }
    
    public int DeleteExpense(int id)
    {
        int result = 0;
        try
        {
            result = conn.Delete<Expense>(id);
            return result;
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return result;
    }
}
