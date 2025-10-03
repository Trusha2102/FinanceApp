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
            existing.Remarks = transaction.Remarks;
        }
    }

    public void DeleteTransaction(int id)
    {
        var transaction = _transactions.FirstOrDefault(t => t.Id == id);
        if (transaction != null)
            _transactions.Remove(transaction);
    }

    // Async wrapper for Add (optional)
    public Task SaveTransactionAsync(Transaction transaction)
    {
        AddTransaction(transaction);
        return Task.CompletedTask;
    }
}
