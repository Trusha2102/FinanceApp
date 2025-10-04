using System;
using System.Text.RegularExpressions;
using FinanceApp.Models;
using FinanceApp.Services;
using Microsoft.Maui.Controls;

namespace FinanceApp.Views;

public partial class AddTransactionPage : ContentPage
{
    private readonly TransactionService _service;
    private readonly Transaction _editingTransaction;

    // Add mode
    public AddTransactionPage() : this(App.TransactionService) { }

    public AddTransactionPage(TransactionService service)
    {
        InitializeComponent();
        _service = service;
        SetupValidation();
    }

    // Edit mode
    public AddTransactionPage(TransactionService service, Transaction transaction) : this(service)
    {
        _editingTransaction = transaction;

        accountEntry.Text = transaction.AccountName;
        amountEntry.Text = (transaction.Debit > 0 ? transaction.Debit : transaction.Credit).ToString();
        remarksEntry.Text = transaction.Remarks ?? string.Empty;
        datePicker.Date = transaction.Date ?? DateTime.Today;

        if (transaction.Debit > 0)
            DebitRadio.IsChecked = true;
        else if (transaction.Credit > 0)
            CreditRadio.IsChecked = true;

        recordButton.Text = "Update Record"; // show edit mode
    }

    private void SetupValidation()
    {
        remarksEntry.TextChanged += (s, e) =>
        {
            if (e.NewTextValue?.Length > 60)
            {
                remarksEntry.Text = e.NewTextValue.Substring(0, 60);
                remarksErrorLabel.IsVisible = true;
                remarksErrorLabel.Text = "Maximum 60 characters allowed";
            }
            else
            {
                remarksErrorLabel.IsVisible = false;
            }
        };

        amountEntry.TextChanged += (s, e) =>
        {
            string text = amountEntry.Text;
            if (string.IsNullOrEmpty(text)) return;

            if (!Regex.IsMatch(text, @"^\d*\.?\d{0,2}$"))
            {
                int pos = amountEntry.CursorPosition - 1;
                if (pos >= 0)
                    amountEntry.Text = text.Remove(pos, 1);
                amountEntry.CursorPosition = Math.Max(pos, 0);
            }
        };

        recordButton.Pressed += async (s, e) => await recordButton.ScaleTo(0.95, 50);
        recordButton.Released += async (s, e) => await recordButton.ScaleTo(1, 50);
    }

    private void OnRadioCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (CreditRadio.IsChecked)
        {
            CreditRadioFrame.BackgroundColor = Color.FromArgb("#9B5DE0");
            CreditRadio.TextColor = Colors.White;
        }
        else
        {
            CreditRadioFrame.BackgroundColor = Color.FromArgb("#E0D9FF");
            CreditRadio.TextColor = Color.FromArgb("#4E56C0");
        }

        if (DebitRadio.IsChecked)
        {
            DebitRadioFrame.BackgroundColor = Color.FromArgb("#D78FEE");
            DebitRadio.TextColor = Colors.White;
        }
        else
        {
            DebitRadioFrame.BackgroundColor = Color.FromArgb("#FFE0F0");
            DebitRadio.TextColor = Color.FromArgb("#4E56C0");
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        bool hasError = false;

        // Account validation
        string accountPattern = @"^[A-Za-z0-9 ]{3,30}$";
        if (string.IsNullOrWhiteSpace(accountEntry.Text) || !Regex.IsMatch(accountEntry.Text, accountPattern))
        {
            accountErrorLabel.Text = "Enter a valid account name (3-30 alphanumeric chars)";
            accountErrorLabel.IsVisible = true;
            hasError = true;
        }
        else accountErrorLabel.IsVisible = false;

        // Amount validation
        if (!decimal.TryParse(amountEntry.Text, out decimal amount) || amount <= 0)
        {
            amountErrorLabel.Text = "Enter a valid positive amount";
            amountErrorLabel.IsVisible = true;
            hasError = true;
        }
        else amountErrorLabel.IsVisible = false;

        // Radio validation
        string transactionType = null;
        if (CreditRadio.IsChecked) transactionType = "Credit";
        else if (DebitRadio.IsChecked) transactionType = "Debit";

        if (transactionType == null)
        {
            amountErrorLabel.Text = "Select Credit or Debit";
            amountErrorLabel.IsVisible = true;
            hasError = true;
        }

        DateTime selectedDate = (DateTime)datePicker.Date;

        if (hasError) return;

        if (_editingTransaction != null)
        {
            // Edit mode: update transaction and close page
            _editingTransaction.Date = selectedDate;
            _editingTransaction.AccountName = accountEntry.Text;
            _editingTransaction.Debit = transactionType == "Debit" ? amount : 0;
            _editingTransaction.Credit = transactionType == "Credit" ? amount : 0;
            _editingTransaction.Remarks = remarksEntry.Text ?? string.Empty;

            _service.UpdateTransaction(_editingTransaction);

            await DisplayAlert("✅ Transaction Updated",
                $"Account: {_editingTransaction.AccountName}\nType: {transactionType}\nAmount: {amount}\nDate: {_editingTransaction.Date:dd MMM yyyy}",
                "OK");

            await Navigation.PopAsync();
        }
        else
        {
            // Add mode: save transaction and clear fields
            var transaction = new Transaction
            {
                Date = selectedDate,
                AccountName = accountEntry.Text,
                Debit = transactionType == "Debit" ? amount : 0,
                Credit = transactionType == "Credit" ? amount : 0,
                Remarks = remarksEntry.Text ?? string.Empty
            };

            await _service.SaveTransactionAsync(transaction);

            await DisplayAlert("✅ Transaction Added",
                $"Account: {transaction.AccountName}\nType: {transactionType}\nAmount: {amount}\nDate: {transaction.Date:dd MMM yyyy}",
                "OK");

            // Clear input fields for next entry
            accountEntry.Text = string.Empty;
            amountEntry.Text = string.Empty;
            remarksEntry.Text = string.Empty;
            DebitRadio.IsChecked = false;
            CreditRadio.IsChecked = false;
            datePicker.Date = DateTime.Today;
            accountEntry.Focus();
        }
    }
}
