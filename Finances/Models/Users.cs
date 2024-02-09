using System.ComponentModel.DataAnnotations;
using Finances.Services;
using SQLite;

namespace Finances.Models;

public class User
{
    [PrimaryKey, AutoIncrement, Column("_id")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    
    public int MonthlySalary { get; set; }
}
