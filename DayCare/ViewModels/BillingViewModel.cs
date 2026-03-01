using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace DayCare.ViewModels;

public partial class BillingViewModel : BaseViewModel
{
    [ObservableProperty]
    private decimal _totalBalance;

    [ObservableProperty]
    private decimal _amountDue;

    [ObservableProperty]
    private string _paymentStatus = string.Empty;

    [ObservableProperty]
    private bool _hasPaymentStatus;

    public ObservableCollection<Invoice> Invoices { get; } = new();

    public BillingViewModel()
    {
        Title = "Billing";
        LoadSampleInvoices();
    }

    private void LoadSampleInvoices()
    {
        Invoices.Add(new Invoice
        {
            InvoiceNumber = "INV-001",
            Description = "Weekly Care - March Week 1",
            Amount = 250.00m,
            DueDate = new DateTime(2026, 3, 7),
            IsPaid = false
        });
        Invoices.Add(new Invoice
        {
            InvoiceNumber = "INV-002",
            Description = "Weekly Care - February Week 4",
            Amount = 250.00m,
            DueDate = new DateTime(2026, 2, 28),
            IsPaid = true
        });

        AmountDue = Invoices.Where(i => !i.IsPaid).Sum(i => i.Amount);
        TotalBalance = Invoices.Sum(i => i.Amount);
    }

    [RelayCommand]
    private async Task PayInvoiceAsync(Invoice invoice)
    {
        if (IsBusy || invoice.IsPaid) return;

        try
        {
            IsBusy = true;
            HasPaymentStatus = false;

            // Simulate payment processing
            await Task.Delay(1000);

            invoice.IsPaid = true;
            AmountDue = Invoices.Where(i => !i.IsPaid).Sum(i => i.Amount);

            PaymentStatus = $"Payment of ${invoice.Amount:F2} processed successfully!";
            HasPaymentStatus = true;
        }
        finally
        {
            IsBusy = false;
        }
    }
}

public partial class Invoice : ObservableObject
{
    public string InvoiceNumber { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }

    [ObservableProperty]
    private bool _isPaid;

    public string StatusLabel => IsPaid ? "Paid" : "Due";
}
