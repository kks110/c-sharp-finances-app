namespace Finances.Services;
using SQLite;

public class SqlConnector
{
    public static SQLiteConnection GetConnection()
    {
        string path = FileSystem.AppDataDirectory;
        path = Path.Combine(path + "finances.db3");
        return new SQLiteConnection(path);
    }
}