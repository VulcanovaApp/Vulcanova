using System.ComponentModel;
using UIKit;
using Vulcanova.Core.Layout.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Vulcanova.iOS
{
    public class NativeOptionCell : UITableViewCell, INativeElementView
    {
        public OptionCell OptionCell { get; private set; }

        public Element Element => OptionCell;

        public NativeOptionCell(string cellId, OptionCell cell) : base(UITableViewCellStyle.Default, cellId)
        {
            OptionCell = cell;
            
            UpdateCell(cell);
        }
        
        public void UpdateCell(OptionCell cell)
        {
            Accessory = cell.Selected ? UITableViewCellAccessory.Checkmark : UITableViewCellAccessory.None;
            TextLabel.Text = cell.Text;
            BackgroundColor = cell.BackgroundColor.ToUIColor();
            TextLabel.TextColor = cell.TextColor.ToUIColor();

            OptionCell.PropertyChanged -= CellOnPropertyChanged;
            cell.PropertyChanged += CellOnPropertyChanged;

            OptionCell = cell;
        }

        private void CellOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == OptionCell.TextProperty.PropertyName)
            {
                TextLabel.Text = OptionCell.Text;
            }
            else if (e.PropertyName == OptionCell.SelectedProperty.PropertyName)
            {
                Accessory = OptionCell.Selected ? UITableViewCellAccessory.Checkmark : UITableViewCellAccessory.None;
            }
            else if (e.PropertyName == OptionCell.BackgroundColorProperty.PropertyName)
            {
                BackgroundColor = OptionCell.BackgroundColor.ToUIColor();
            }
            else if (e.PropertyName == OptionCell.TextColorProperty.PropertyName)
            {
                TextLabel.TextColor = OptionCell.TextColor.ToUIColor();
            }
        }
    }
}