﻿using System.ComponentModel.DataAnnotations;
using Finances.Services;
using SQLite;

namespace Finances.Models;

public class Expense
{
    [PrimaryKey, AutoIncrement, Column("_id")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public int Amount { get; set; }
    
    public bool Weekly { get; set; }
    
    public bool Variable { get; set; }

    public string Notes { get; set; }
    
    public int CalculateMonthlyAmount()
    {
        return Weekly ? (Amount * 52) / 12 : Amount;
    }
}
