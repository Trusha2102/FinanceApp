using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FinanceApp.Models;

public class Transaction : INotifyPropertyChanged
{
    private int _id;
    private string? _accountName;
    private decimal _debit;
    private decimal _credit;
    private string? _remarks;
    private DateTime? _date;

    public int Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    public string? AccountName
    {
        get => _accountName;
        set => SetProperty(ref _accountName, value);
    }

    public decimal Debit
    {
        get => _debit;
        set => SetProperty(ref _debit, value);
    }

    public decimal Credit
    {
        get => _credit;
        set => SetProperty(ref _credit, value);
    }

    public string? Remarks
    {
        get => _remarks;
        set => SetProperty(ref _remarks, value);
    }

    public DateTime? Date
    {
        get => _date;
        set => SetProperty(ref _date, value);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void SetProperty<T>(ref T backingField, T value, [CallerMemberName] string? propertyName = null)
    {
        if (!Equals(backingField, value))
        {
            backingField = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
