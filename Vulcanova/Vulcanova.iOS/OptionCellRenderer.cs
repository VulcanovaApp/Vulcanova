using UIKit;
using Vulcanova.Core.Layout.Controls;
using Vulcanova.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(OptionCell), typeof(OptionCellRenderer))]
namespace Vulcanova.iOS
{
    public class OptionCellRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var optionCell = (OptionCell) item;

            var nativeCell = (NativeOptionCell) reusableCell;

            if (nativeCell == null)
            {
                nativeCell = new NativeOptionCell(item.GetType().FullName, optionCell);
            }
            else
            {
                nativeCell.UpdateCell(optionCell);
            }

            return nativeCell;
        }
    }
}