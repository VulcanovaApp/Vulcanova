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
            BindableProperty.Create(nameof(SelectedDate), typeof(DateTime), typeof(Calendar), DateTime.Now,
                propertyChanged: SelectedDateChanged);

        public DateTime SelectedDate
        {
            get => (DateTime) GetValue(SelectedDateProperty);
            set => SetValue(SelectedDateProperty, value);
        }

        public static readonly BindableProperty SelectedColorProperty =
            BindableProperty.Create(nameof(SelectedColor), typeof(Color), typeof(CalendarDateCell), Color.Red);

        public Color SelectedColor
        {
            get => (Color) GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        private readonly Dictionary<DateTime, CalendarDateCell> _dateCells = new();

        public Calendar()
        {
            InitializeComponent();

            SetupLayout();
        }

        private void SetupLayout()
        {
            SetupHeader();
            SetupCalendarGrid();
            UpdateIndicators(null, SelectedDate);
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
                    var cell = CreateCellForDate(currentDay);

                    CalendarGrid.Children.Add(cell, day, weekRow);

                    _dateCells.Add(currentDay, cell);

                    currentDay = currentDay.AddDays(1);
                }

                weekRow++;
            }
        }

        private CalendarDateCell CreateCellForDate(DateTime date)
        {
            var colorBinding = new Binding
            {
                Source = this,
                Path = nameof(SelectedColor)
            };

            var cell = new CalendarDateCell
            {
                Day = date.Day,
                TapCommand = new Command(() => SelectedDate = date)
            };

            cell.SetBinding(CalendarDateCell.SelectedColorProperty, colorBinding);

            return cell;
        }

        private void UpdateIndicators(DateTime? oldDate, DateTime newDate)
        {
            if (oldDate != null)
            {
                _dateCells[oldDate.Value.Date].Selected = false;
            }

            _dateCells[newDate.Date].Selected = true;
        }

        private static void SelectedDateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var calendar = (Calendar) bindable;
            calendar.UpdateIndicators((DateTime) oldValue, (DateTime) newValue);
        }
    }
}