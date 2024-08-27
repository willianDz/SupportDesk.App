using SupportDesk.App.Extensions;

namespace SupportDesk.App.Controls;

public partial class DateRange : ContentView
{
    #region Properties

    public List<DateRangeFilter> DateRangeFilters { get; set; }
    public DateRangeFilter SelectedDateRangeFilter { get; set; }

    #endregion


    #region Bindables Properties

    public static readonly BindableProperty StartDateProperty = BindableProperty.Create(
       nameof(StartDate),
       typeof(DateTime),
       typeof(DateRange),
       DateTime.Today,
       BindingMode.TwoWay,
       null,
       SetStartDate);

    public DateTime StartDate
    {
        get => (DateTime)this.GetValue(StartDateProperty);
        set => this.SetValue(StartDateProperty, value);
    }

    private static void SetStartDate(BindableObject bindable, object oldValue, object newValue)
    {
        (bindable as DateRange).dpStartDate.Date = (DateTime)newValue;
    }


    public static readonly BindableProperty EndDateProperty = BindableProperty.Create(
       nameof(EndDate),
       typeof(DateTime),
       typeof(DateRange),
       DateTime.Today,
       BindingMode.TwoWay,
       null,
       SetEndDate);

    public DateTime EndDate
    {
        get => (DateTime)this.GetValue(EndDateProperty);
        set => this.SetValue(EndDateProperty, value);
    }

    private static void SetEndDate(BindableObject bindable, object oldValue, object newValue)
    {
        (bindable as DateRange).dpEndDate.Date = (DateTime)newValue;
    }

    #endregion

    public DateRange()
    {
        InitializeComponent();
        InitializePickerDateRangeFilters();
        FilterByCustomRange(false);
    }

    private void InitializePickerDateRangeFilters()
    {
        DateRangeFilters = DateExtensions.DateRangeFilters;
        pickerDateRangeFilters.Items.Clear();

        foreach (DateRangeFilter item in DateRangeFilters)
        {
            pickerDateRangeFilters.Items.Add(item.Description);
        }

        pickerDateRangeFilters.SelectedIndex = 0;
    }

    private void startDate_DateSelected(object sender, DateChangedEventArgs e)
    {
        if (e.NewDate.Year <= 1900)
        {
            dpStartDate.Date = SelectedDateRangeFilter.FromDate;
        }
        else
        {
            StartDate = e.NewDate;
        }
    }

    private void endDate_DateSelected(object sender, DateChangedEventArgs e)
    {
        if (e.NewDate.Year <= 1900)
        {
            dpEndDate.Date = SelectedDateRangeFilter.ToDate;
        }
        else
        {
            EndDate = e.NewDate;
        }
    }

    private void pickerDateRangeFilters_SelectedIndexChanged(object sender, EventArgs e)
    {
        string filterName = pickerDateRangeFilters.Items[pickerDateRangeFilters.SelectedIndex];
        SelectedDateRangeFilter = DateRangeFilters.FirstOrDefault(x => x.Description == filterName);

        if (SelectedDateRangeFilter.Type == DateRangeFilterTypes.CustomRange)
        {
            dpStartDate.Date = DateTime.Today;
            dpEndDate.Date = DateTime.Today;

            FilterByCustomRange(true);
        }
        else
        {
            dpStartDate.Date = SelectedDateRangeFilter.FromDate;
            dpEndDate.Date = SelectedDateRangeFilter.ToDate;

            FilterByCustomRange(false);
        }
    }

    private void BtnHideCustomRangePickers_Clicked(object sender, EventArgs e)
    {
        FilterByCustomRange(false);
        pickerDateRangeFilters.SelectedIndex = 0;
    }

    private void FilterByCustomRange(bool filterByCustomRange)
    {
        if (filterByCustomRange)
        {
            ContainerCustomRange.IsVisible = true;
            ContainerFilters.IsVisible = false;
        }
        else
        {
            ContainerCustomRange.IsVisible = false;
            ContainerFilters.IsVisible = true;
        }
    }
}