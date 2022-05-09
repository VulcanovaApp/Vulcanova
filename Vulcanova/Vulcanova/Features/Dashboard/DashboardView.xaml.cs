using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Features.Dashboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardView
    {
        public DashboardView()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, vm => vm.Title, v => v.Title);
                this.OneWayBind(ViewModel, vm => vm, v => v.TimetableListEntry.BindingContext);
                this.OneWayBind(ViewModel, vm => vm, v => v.GradesListEntry.BindingContext);
                this.OneWayBind(ViewModel, vm => vm, v => v.ExamsListEntry.BindingContext);
                this.OneWayBind(ViewModel, vm => vm.LuckyNumber, v => v.LuckyNumberEntry.Text);
            });
        }
    }
}