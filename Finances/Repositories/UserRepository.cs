using Finances.Models;
using SQLite;

namespace Finances.Repositories;

public class UserRepository
{
    string _dbPath;

    public string StatusMessage { get; set; }

    private SQLiteConnection conn;

    private void Init()
    {
        if (conn != null)
            return;

        conn = new SQLiteConnection(_dbPath);
        conn.CreateTable<User>();
    }

    public UserRepository(string dbPath)
    {
        _dbPath = dbPath;                        
    }

    public int AddNewUser(string name, int monthlySalary)
    {            
        int result = 0;
        try
        {
            Init();

            // basic validation to ensure a name was entered
            if (string.IsNullOrEmpty(name))
                throw new Exception("Valid name required");
            
            // basic validation to ensure an amount was entered
            if (int.IsPositive(monthlySalary) == false)
                throw new Exception("Valid amount required");

            result = conn.Insert(
                new User
                {
                    Name = name, 
                    MonthlySalary = monthlySalary, 
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

    public List<User> GetAllUsers()
    {
        try
        {
            Init();
            return conn.Table<User>().ToList();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new List<User>();
    }

    public int UpdateUser(User user)
    {
        int result = 0;
        try
        {
            result = conn.Update(user);
            return result;
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return result;
    }
    
    public int DeleteUser(int id)
    {
        int result = 0;
        try
        {
            result = conn.Delete<User>(id);
            return result;
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return result;
    }
}
