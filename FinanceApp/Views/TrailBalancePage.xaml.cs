using FinanceApp.Models;
using FinanceApp.Services;

namespace FinanceApp.Views;

public partial class TrialBalancePage : ContentPage
{
    private readonly TransactionService _service;

    public TrialBalancePage()
        : this(App.TransactionService) { }

    public TrialBalancePage(TransactionService service)
    {
        InitializeComponent();
        _service = service;

        LoadTrialBalance();
    }

    private void LoadTrialBalance()
    {
        var grouped = _service.GetAllTransactions()
                              .GroupBy(t => t.AccountName)
                              .Select(g => new
                              {
                                  AccountName = g.Key,
                                  TotalDebit = g.Sum(x => x.Debit),
                                  TotalCredit = g.Sum(x => x.Credit)
                              }).ToList();

        trialBalanceCollectionView.ItemsSource = grouped;

        decimal totalDebit = grouped.Sum(x => x.TotalDebit);
        decimal totalCredit = grouped.Sum(x => x.TotalCredit);

        balanceStatusLabel.Text = totalDebit == totalCredit
            ? "✅ Balanced"
            : "⚠️ Not Balanced";
    }
}
