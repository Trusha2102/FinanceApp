# FinanceApp Documentation

## Overview

FinanceApp is a simple cross-platform personal finance app built using **.NET MAUI**. It allows users to record financial transactions (debit/credit) and view a trial balance report. The app is designed to be clean, simple, and intuitive for daily financial tracking.

---

## Features

### 1. Transaction Entry

**Screen Description:**

* Allows users to add financial transactions.
* Fields include:

  * **Date**: Date of transaction.
  * **Account Name**: Name of the account involved.
  * **Debit or Credit Amount**: Input either debit or credit.
  * **Remarks**: Optional notes.

**Functionality:**

* Validates required fields.
* Users can save transactions for future reference.

*Screenshot Placeholder: [Attach Screenshot]*

---

### 2. Transaction List

**Screen Description:**

* Displays all transactions entered by the user.
* Provides options to:

  * **Edit**: Modify transaction details.
  * **Delete**: Remove unwanted transactions.

*Screenshot Placeholder: [Attach Screenshot]*

---

### 3. Trial Balance

**Screen Description:**

* Calculates total debit and credit per account.
* Indicates whether the trial balance is balanced.

**Functionality:**

* Automatically sums up debit and credit amounts.
* Provides a visual confirmation of balance status.

*Screenshot Placeholder: [Attach Screenshot]*

---

## Project Structure

```
FinanceApp/
├── Models/
│   └── Transaction.cs
├── Services/
│   └── TransactionService.cs
├── Views/
│   ├── AddTransactionPage.xaml
│   ├── AddTransactionPage.xaml.cs
│   ├── TransactionListPage.xaml
│   ├── TransactionListPage.xaml.cs
│   ├── TrialBalancePage.xaml
│   └── TrialBalancePage.xaml.cs
├── App.xaml
├── App.xaml.cs
└── FinanceApp.sln
```

---

## Setup Instructions

1. Clone the repository:

```bash
git clone https://github.com/Trusha2102/FinanceApp.git
```

2. Open `FinanceApp.sln` in Visual Studio 2022+.
3. Build and run on Windows, Android, or iOS.
4. Start adding transactions and viewing the trial balance.

---

## Future Enhancements

* Persist transactions using SQLite.
* Add graphical charts for income and expense tracking.
* Implement categories for better account management.
* Enable export/import functionality (CSV/Excel).
