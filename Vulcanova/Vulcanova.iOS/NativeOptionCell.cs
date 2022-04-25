using System.ComponentModel;
using UIKit;
using Vulcanova.Core.Layout.Controls;
using Xamarin.Forms;

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
            BackgroundColor = UIColor.FromRGBA(0, 0, 0, 255);

            OptionCell.PropertyChanged -= CellOnPropertyChanged;
            cell.PropertyChanged += CellOnPropertyChanged;

            OptionCell = cell;
        }

        private void CellOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            TextLabel.Text = OptionCell.Text;
            Accessory = OptionCell.Selected ? UITableViewCellAccessory.Checkmark : UITableViewCellAccessory.None;
        }
    }
}