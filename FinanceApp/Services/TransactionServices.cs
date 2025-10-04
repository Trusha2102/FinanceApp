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
        // Find the index of the existing transaction
        var index = _transactions.IndexOf(_transactions.FirstOrDefault(t => t.Id == transaction.Id));
        if (index >= 0)
        {
            // Replace the object in collection so UI refreshes automatically
            _transactions[index] = transaction;
        }
    }

    public void DeleteTransaction(Transaction transaction)
    {
        _transactions.Remove(transaction);
    }

    public Task SaveTransactionAsync(Transaction transaction)
    {
        if (transaction.Id == 0)
            AddTransaction(transaction);
        else
            UpdateTransaction(transaction);

        return Task.CompletedTask;
    }
}
