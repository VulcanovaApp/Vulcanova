using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Vulcanova.Extensions;
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

        public static readonly BindableProperty SelectedTextColorProperty =
            BindableProperty.Create(nameof(SelectedTextColor), typeof(Color), typeof(CalendarDateCell), Color.White);

        public Color SelectedTextColor
        {
            get => (Color) GetValue(SelectedTextColorProperty);
            set => SetValue(SelectedTextColorProperty, value);
        }

        public static readonly BindableProperty SecondaryTextColorProperty =
            BindableProperty.Create(nameof(SecondaryTextColor), typeof(Color), typeof(CalendarDateCell), Color.White);

        public Color SecondaryTextColor
        {
            get => (Color) GetValue(SecondaryTextColorProperty);
            set => SetValue(SecondaryTextColorProperty, value);
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
            UpdateCalendarGrid();
            UpdateIndicators(null, SelectedDate);
        }

        private void SetupHeader()
        {
            // 3.01.2022 is on Monday
            var date = new DateTime(2022, 1, 3);
            CalendarGrid.RowDefinitions.Add(new RowDefinition {Height = GridLength.Auto});

            for (var day = 0; day < 7; day++)
            {
                var text = date.ToString("ddd", CultureInfo.CurrentCulture).ToUpperInvariant()[..1];
                var label = new Label
                {
                    Text = text,
                    HorizontalTextAlignment = TextAlignment.Center
                };
                label.FontSize = Device.GetNamedSize(NamedSize.Small, label);

                CalendarGrid.Children.Add(label, day, 0);

                date = date.AddDays(1);
            }
        }

        private void UpdateCalendarGrid()
        {
            var firstDayOfMonth = new DateTime(SelectedDate.Year, SelectedDate.Month, 1);
            var lastDayOfMonth = new DateTime(SelectedDate.Year, SelectedDate.Month,
                DateTime.DaysInMonth(SelectedDate.Year, SelectedDate.Month));

            var currentDay = firstDayOfMonth.LastMonday();
            var endDay = lastDayOfMonth.NextSunday();

            var daysToDraw = (endDay - currentDay).TotalDays + 1;

            var cnt = Math.Max(daysToDraw, _dateCells.Count);

            _dateCells.Clear();

            var children = CalendarGrid.Children
                .OrderBy(Grid.GetRow)
                .ThenBy(Grid.GetColumn)
                .Skip(7) // skip weekday names header
                .ToArray();

            // date cells start on 2nd row (1st is weekday names)
            var weekRow = 1;

            for (var i = 0; i < cnt; i++)
            {
                var dayColumn = i % 7;

                CalendarDateCell cell = null;
                if (children.Length > i)
                {
                    cell = (CalendarDateCell) children[i];
                }

                if (i >= daysToDraw)
                {
                    if (cell != null)
                    {
                        CalendarGrid.Children.Remove(cell);
                    }
                }
                else
                {
                    if (CalendarGrid.RowDefinitions.Count < weekRow + 1)
                    {
                        CalendarGrid.RowDefinitions.Add(new RowDefinition {Height = 32});
                    }

                    if (cell == null)
                    {
                        cell = CreateCellForDate(currentDay);
                        CalendarGrid.Children.Add(cell, dayColumn, weekRow);
                    }
                    else
                    {
                        UpdateCellForDate(cell, currentDay);
                        cell.Selected = false;
                    }

                    _dateCells.Add(currentDay, cell);
                }

                if ((i + 1) % 7 == 0)
                {
                    weekRow++;
                }

                currentDay = currentDay.AddDays(1);
            }
        }

        private CalendarDateCell CreateCellForDate(DateTime date)
        {
            var cell = new CalendarDateCell
            {
                Day = date.Day,
                TapCommand = new Command(() => SelectedDate = date),
                Secondary = date.Month != SelectedDate.Month
            };

            var colorBinding = new Binding
            {
                Source = this,
                Path = nameof(SelectedColor)
            };

            cell.SetBinding(CalendarDateCell.SelectedColorProperty, colorBinding);

            var textColorBinding = new Binding
            {
                Source = this,
                Path = nameof(SelectedTextColor)
            };

            cell.SetBinding(CalendarDateCell.SelectedTextColorProperty, textColorBinding);

            var secondaryColorBinding = new Binding
            {
                Source = this,
                Path = nameof(SecondaryTextColor)
            };

            cell.SetBinding(CalendarDateCell.SecondaryTextColorProperty, secondaryColorBinding);

            return cell;
        }

        private void UpdateCellForDate(CalendarDateCell cell, DateTime date)
        {
            cell.Day = date.Day;
            cell.TapCommand = new Command(() => SelectedDate = date);
            cell.Secondary = date.Month != SelectedDate.Month;
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
            var oldDate = (DateTime) oldValue;
            var newDate = (DateTime) newValue;

            if (oldDate.Month != newDate.Month)
            {
                calendar.UpdateCalendarGrid();
                calendar.UpdateIndicators(null, newDate);

                return;
            }

            calendar.UpdateIndicators(oldDate, newDate);
        }
    }
}