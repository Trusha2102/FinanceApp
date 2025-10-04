using FinanceApp.Models;
using FinanceApp.Services;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceApp.Views;

public partial class TrialBalancePage : ContentPage
{
    private readonly TransactionService _service;

    public class AccountBalance
    {
        public string AccountName { get; set; } = string.Empty;
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
    }

    public List<AccountBalance> AccountBalances { get; set; } = new();

    public decimal TotalDebit { get; set; }
    public decimal TotalCredit { get; set; }

    public TrialBalancePage()
    {
        InitializeComponent();
        _service = App.TransactionService;

        CalculateBalances();
        BindingContext = this;
    }

    private void CalculateBalances()
    {
        var grouped = new Dictionary<string, (decimal Debit, decimal Credit)>();

        foreach (var t in _service.GetAllTransactions())
        {
            if (!grouped.ContainsKey(t.AccountName))
                grouped[t.AccountName] = (0, 0);

            var curr = grouped[t.AccountName];
            curr.Debit += t.Debit;
            curr.Credit += t.Credit;
            grouped[t.AccountName] = curr;
        }

        AccountBalances = grouped.Select(kvp => new AccountBalance
        {
            AccountName = kvp.Key,
            TotalDebit = kvp.Value.Debit,
            TotalCredit = kvp.Value.Credit
        }).ToList();

        TotalDebit = AccountBalances.Sum(a => a.TotalDebit);
        TotalCredit = AccountBalances.Sum(a => a.TotalCredit);

        // Show balanced/unbalanced status
        decimal currentBalance = TotalDebit - TotalCredit;
        if (currentBalance == 0)
            BalanceStatusLabel.Text = "✅ Balanced";
        else
            BalanceStatusLabel.Text = $"⚠️ Unbalanced! Current Balance: {currentBalance:C}";
    }
}
