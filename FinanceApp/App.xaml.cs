using FinanceApp.Services;

namespace FinanceApp;

public partial class App : Application
{
    // Shared service for all pages
    public static TransactionService TransactionService { get; } = new TransactionService();

    public App()
    {
        InitializeComponent(); // ✅ Works because this matches App.xaml
        MainPage = new AppShell();
    }
}
