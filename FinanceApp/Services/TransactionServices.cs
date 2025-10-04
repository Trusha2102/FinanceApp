using FinanceApp.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceApp.Services;

public class TransactionService
{
    private readonly ObservableCollection<Transaction> _transactions = new();
    private int _nextId = 1;

    public ObservableCollection<Transaction> GetAllTransactions() => _transactions;

    public void AddTransaction(Transaction transaction)
    {
        transaction.Id = _nextId++;
        _transactions.Add(transaction);
    }

    public void UpdateTransaction(Transaction transaction)
    {
        var existing = _transactions.FirstOrDefault(t => t.Id == transaction.Id);
        if (existing != null)
        {
            existing.Date = transaction.Date ?? System.DateTime.Today;
            existing.AccountName = transaction.AccountName;
            existing.Debit = transaction.Debit;
            existing.Credit = transaction.Credit;
            existing.Remarks = transaction.Remarks ?? string.Empty;
        }
    }

    public void DeleteTransaction(Transaction transaction)
    {
        _transactions.Remove(transaction);
    }

    public Task SaveTransactionAsync(Transaction transaction)
    {
        _transactions.Add(transaction);
        return Task.CompletedTask;
    }
}
