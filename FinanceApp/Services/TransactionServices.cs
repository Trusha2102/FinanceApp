using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using FinanceApp.Models;

namespace FinanceApp.Services;

public class TransactionService
{
    private readonly SQLiteAsyncConnection _db;

    public TransactionService(string dbPath)
    {
        _db = new SQLiteAsyncConnection(dbPath);
        _db.CreateTableAsync<Transaction>().Wait();
    }

    public Task<List<Transaction>> GetTransactionsAsync() =>
        _db.Table<Transaction>().ToListAsync();

    public Task<int> SaveTransactionAsync(Transaction transaction)
    {
        if (transaction.Id != 0)
            return _db.UpdateAsync(transaction);
        else
            return _db.InsertAsync(transaction);
    }

    public Task<int> DeleteTransactionAsync(Transaction transaction) =>
        _db.DeleteAsync(transaction);
}

