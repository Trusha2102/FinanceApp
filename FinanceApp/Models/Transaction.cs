namespace FinanceApp.Models;

public class Transaction
{
    public int Id { get; set; }
    public string? AccountName { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    public string? Remarks { get; set; }
    public DateTime? Date { get; set; }
}

