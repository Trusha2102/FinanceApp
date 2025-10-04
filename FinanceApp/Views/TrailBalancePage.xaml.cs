using FinanceApp.Models;
using FinanceApp.Services;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;

namespace FinanceApp.Views;

public partial class TrialBalancePage : ContentPage
{
    private readonly TransactionService _service;
    private readonly ObservableCollection<AccountBalance> _balances = new();

    public ObservableCollection<AccountBalance> Balances => _balances;
    public ObservableCollection<Transaction> Transactions { get; private set; }

    public decimal TotalDebit { get; private set; }
    public decimal TotalCredit { get; private set; }

    public TrialBalancePage() : this(App.TransactionService) { }

    public TrialBalancePage(TransactionService service)
    {
        InitializeComponent();
        _service = service;

        Transactions = _service.GetAllTransactions();

        // Listen for new/deleted transactions
        Transactions.CollectionChanged += (s, e) =>
        {
            // Subscribe to property changes for new transactions
            if (e.NewItems != null)
                foreach (Transaction t in e.NewItems)
                    t.PropertyChanged += Transaction_PropertyChanged;

            // Unsubscribe removed transactions
            if (e.OldItems != null)
                foreach (Transaction t in e.OldItems)
                    t.PropertyChanged -= Transaction_PropertyChanged;

            CalculateTrialBalance();
        };

        // Subscribe existing transactions
        foreach (var t in Transactions)
            t.PropertyChanged += Transaction_PropertyChanged;

        CalculateTrialBalance();
        BindingContext = this;
    }

    private void Transaction_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        // Recalculate trial balance whenever any property of a transaction changes
        CalculateTrialBalance();
    }

    private void CalculateTrialBalance()
    {
        _balances.Clear();
        TotalDebit = 0;
        TotalCredit = 0;

        var grouped = Transactions.GroupBy(t => t.AccountName)
                                  .Select(g => new AccountBalance
                                  {
                                      AccountName = g.Key,
                                      TotalDebit = g.Sum(t => t.Debit),
                                      TotalCredit = g.Sum(t => t.Credit)
                                  });

        foreach (var item in grouped)
        {
            _balances.Add(item);
            TotalDebit += item.TotalDebit;
            TotalCredit += item.TotalCredit;
        }

        if (balanceLabel != null)
            balanceLabel.Text = TotalDebit == TotalCredit ? "Balanced ✅" : "Not Balanced ❌";

        if (totalDebitLabel != null)
            totalDebitLabel.Text = $"Total Debit: {TotalDebit:C}";

        if (totalCreditLabel != null)
            totalCreditLabel.Text = $"Total Credit: {TotalCredit:C}";
    }
}

public class AccountBalance
{
    public string AccountName { get; set; }
    public decimal TotalDebit { get; set; }
    public decimal TotalCredit { get; set; }
}
