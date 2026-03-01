using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using DayCare.Models;

namespace DayCare.ViewModels;

public partial class PickupDropoffViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _childName = string.Empty;

    [ObservableProperty]
    private DateTime _selectedDate = DateTime.Today;

    [ObservableProperty]
    private TimeSpan _selectedTime = DateTime.Now.TimeOfDay;

    [ObservableProperty]
    private bool _isDropOff = true;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    [ObservableProperty]
    private bool _hasStatusMessage;

    public ObservableCollection<PickupDropoffRecord> History { get; } = new();

    public PickupDropoffViewModel()
    {
        Title = "Pick Up / Drop Off";
    }

    [RelayCommand]
    private async Task LogPickupDropoffAsync()
    {
        if (IsBusy) return;
        if (string.IsNullOrWhiteSpace(ChildName))
        {
            StatusMessage = "Please enter a child name.";
            HasStatusMessage = true;
            return;
        }

        try
        {
            IsBusy = true;
            HasStatusMessage = false;

            // Simulate API call
            await Task.Delay(500);

            var record = new PickupDropoffRecord
            {
                ChildName = ChildName,
                DateTime = SelectedDate.Date + SelectedTime,
                IsDropOff = IsDropOff
            };

            History.Insert(0, record);

            StatusMessage = IsDropOff
                ? $"{ChildName} dropped off at {record.DateTime:h:mm tt}"
                : $"{ChildName} picked up at {record.DateTime:h:mm tt}";
            HasStatusMessage = true;
        }
        finally
        {
            IsBusy = false;
        }
    }
}

public class PickupDropoffRecord
{
    public string ChildName { get; set; } = string.Empty;
    public DateTime DateTime { get; set; }
    public bool IsDropOff { get; set; }
    public string ActionLabel => IsDropOff ? "Dropped Off" : "Picked Up";
}
