using FinanceApp.Views;

namespace FinanceApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Optional: Register routes if you want to navigate programmatically
        Routing.RegisterRoute(nameof(AddTransactionPage), typeof(AddTransactionPage));
        //Routing.RegisterRoute(nameof(TransactionListPage), typeof(TransactionListPage));
        //Routing.RegisterRoute(nameof(TrialBalancePage), typeof(TrialBalancePage));
    }
}
