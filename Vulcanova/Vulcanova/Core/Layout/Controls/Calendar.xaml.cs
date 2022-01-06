using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Core.Layout.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Calendar : ContentView
    {
        public static readonly BindableProperty SelectedDateProperty =
            BindableProperty.Create(nameof(SelectedDate), typeof(DateTime), typeof(Calendar), DateTime.Now);

        public DateTime SelectedDate
        {
            get => (DateTime) GetValue(SelectedDateProperty);
            set => SetValue(SelectedDateProperty, value);
        }

        private readonly Dictionary<DateTime, Label> _dateLabels = new();

        public Calendar()
        {
            InitializeComponent();

            SetupLayout();
        }

        private void SetupLayout()
        {
            SetupHeader();
            SetupCalendarGrid();
            UpdateIndicators();
        }

        private void SetupHeader()
        {
            // 3.01.2022 is on Monday
            var date = new DateTime(2022, 1, 3);
            CalendarGrid.RowDefinitions.Add(new RowDefinition {Height = GridLength.Star});

            for (var day = 0; day < 7; day++)
            {
                CalendarGrid.Children.Add(new Label {Text = date.ToString("ddd", CultureInfo.CurrentCulture)}, day, 0);
                date = date.AddDays(1);
            }
        }

        private void SetupCalendarGrid()
        {
            var firstDayOfMonth = new DateTime(SelectedDate.Year, SelectedDate.Month, 1);
            var currentDay = firstDayOfMonth.Subtract(TimeSpan.FromDays((int) firstDayOfMonth.DayOfWeek - 1));
            var lastDayOfMonth = new DateTime(SelectedDate.Year, SelectedDate.Month,
                DateTime.DaysInMonth(SelectedDate.Year, SelectedDate.Month));

            var weekRow = 1;

            while (currentDay <= lastDayOfMonth)
            {
                CalendarGrid.RowDefinitions.Add(new RowDefinition {Height = GridLength.Star});

                for (var day = 0; day < 7; day++)
                {
                    var label = new Label {Text = currentDay.Day.ToString()};
                    CalendarGrid.Children.Add(label, day, weekRow);

                    _dateLabels.Add(currentDay, label);

                    currentDay = currentDay.AddDays(1);
                }

                weekRow++;
            }
        }

        private void UpdateIndicators()
        {
            _dateLabels[SelectedDate.Date].BackgroundColor = Color.Red;
        }
    }
}