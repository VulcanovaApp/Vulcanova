using System;
using System.Linq;
using Vulcanova.Core.Layout;
using Vulcanova.Core.Layout.Controls;
using Xamarin.Forms;

namespace Vulcanova.Features.Settings.Controls;

// TableSection is a sealed class and therefore cannot be inherited from.
// This hack wires up all the required event handlers and sets up base TableSection
// in a form of a single choice picker.
public sealed class OptionPickerBuilder
{
    private readonly TableSection _tableSection = new();

    private Action<int> _handler;
    private int _selectedIndex;

    public OptionPickerBuilder WithOption(string option)
    {
        var cell = new OptionCell
        {
            Text = option
        };

        cell.SetAppThemeColor(OptionCell.TextColorProperty, ThemeUtility.GetColor("LightPrimaryTextColor"),
            ThemeUtility.GetColor("DarkPrimaryTextColor"));

        _tableSection.Add(cell);

        return this;
    }

    public OptionPickerBuilder WithSelectedIndex(int index)
    {
        _selectedIndex = index;

        return this;
    }

    public OptionPickerBuilder WithSelectionHandler(Action<int> handler)
    {
        _handler = handler;
        return this;
    }

    public TableSection Build()
    {
        foreach (var (cell, i) in _tableSection
                     .Cast<OptionCell>()
                     .Select((o, i) => (o, i)))
        {
            cell.Selected = _selectedIndex == i;
            cell.Tapped += (_, _) =>
            {
                foreach (var (cell2, i2) in _tableSection
                             .Cast<OptionCell>()
                             .Select((o, i2) => (o, i2)))
                {
                    cell2.Selected = i2 == i;
                }

                _handler?.Invoke(i);
            };
        }

        return _tableSection;
    }
}