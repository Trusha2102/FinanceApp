using FinanceApp.Models;
using FinanceApp.Services;
using System.Collections.ObjectModel;

namespace FinanceApp.Views;

public partial class TransactionListPage : ContentPage
{
    private readonly TransactionService _service;
    public ObservableCollection<Transaction> Transactions { get; }

    public TransactionListPage() : this(App.TransactionService) { }

    public TransactionListPage(TransactionService service)
    {
        InitializeComponent();
        _service = service;
        Transactions = _service.GetAllTransactions();
        BindingContext = this;
    }

private async void OnEditClicked(object sender, EventArgs e)
{
    if (sender is Button button && button.BindingContext is Transaction transaction)
    {
        await Navigation.PushAsync(new AddTransactionPage(App.TransactionService, transaction));
    }
}

private async void OnDeleteClicked(object sender, EventArgs e)
{
    if (sender is Button button && button.BindingContext is Transaction transaction)
    {
        bool confirm = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this transaction?", "Yes", "No");
        if (confirm)
        {
            App.TransactionService.DeleteTransaction(transaction);
        }
    }
}

}
