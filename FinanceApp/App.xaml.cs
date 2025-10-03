using FinanceApp.Views;
using FinanceApp.Services;

namespace FinanceApp;

public partial class App : Application
{
    public App(TransactionService service)
    {
        InitializeComponent();

        // NavigationPage wraps the first page
        MainPage = new NavigationPage(new AddTransactionPage(service));
    }
}
