using Finances.Models;
using SQLite;

namespace Finances.Repositories;

public class MonthlyExpenseRepository
{
    string _dbPath;

    public string StatusMessage { get; set; }

    private SQLiteConnection conn;

    private void Init()
    {
        if (conn != null)
            return;

        conn = new SQLiteConnection(_dbPath);
        conn.CreateTable<MonthlyExpense>();
    }

    public MonthlyExpenseRepository(string dbPath)
    {
        _dbPath = dbPath;                        
    }

    public int AddNewMonthlyExpense(string name, int amount, string? notes)
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

            result = conn.Insert(new MonthlyExpense { Name = name, Amount = amount, Notes = notes });

            StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, name);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
        }

        return result;

    }

    public List<MonthlyExpense> GetAllMonthlyExpense()
    {
        try
        {
            Init();
            return conn.Table<MonthlyExpense>().ToList();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new List<MonthlyExpense>();
    }
    
    public MonthlyExpense FindMonthlyExpenseById(int id)
    {
        try
        {
            Init();
            var monthlyExpense = from monExp in conn.Table<MonthlyExpense>()
                                 where monExp.Id == id
                                 select monExp;
            return monthlyExpense.FirstOrDefault();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new MonthlyExpense();
    }

    public int UpdateMonthlyExpense(MonthlyExpense monthlyExpense)
    {
        int result = 0;
        try
        {
            result = conn.Update(monthlyExpense);
            return result;
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return result;
    }
    
    public int DeleteMonthlyExpense(int id)
    {
        int result = 0;
        try
        {
            result = conn.Delete<MonthlyExpense>(id);
            return result;
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return result;
    }
}
