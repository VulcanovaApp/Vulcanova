using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Core.Layout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [ContentProperty("Contents")]
    public partial class SlidingUpPanel
    {
        public IList<View> Contents => ContentWrapper.Children;
        
        public static readonly BindableProperty OpenProperty = BindableProperty.Create(
            nameof(Open), typeof(bool), typeof(SlidingUpPanel), false);
 
        public bool Open
        {
            get => (bool)GetValue(OpenProperty);
            set => SetValue(OpenProperty, value); 
        }

        public SlidingUpPanel()
        {
            InitializeComponent();

            var startPosition = PanGestureRecognizer.Events().PanUpdated
                .Where(a => a.StatusType == GestureStatus.Started)
                .Select(_ => SlidingPanel.TranslationY);

            this.PanGestureRecognizer.Events().PanUpdated
                .Where(a => a.StatusType == GestureStatus.Running)
                .WithLatestFrom(startPosition)
                .Select(values => values.Second + values.First.TotalY)
                .Subscribe(val => SlidingPanel.TranslationY = val);

            this.PanGestureRecognizer.Events().PanUpdated
                .Where(a => a.StatusType is GestureStatus.Canceled or GestureStatus.Completed)
                .Select(_ => SlidingPanel.TranslationY)
                .WithLatestFrom(startPosition)
                .Subscribe(values =>
                {
                    var (end, start) = values;
                    Open = end < start;
                });

            var layout = this.Events().LayoutChanged;

            this.WhenAnyValue(x => x.Open)
                .CombineLatest(layout)
                .Subscribe(values =>
                {
                    var open = values.First;

                    if (open)
                    {
                        SlidingPanel.TranslateTo(0, 8, 150);
                        Backdrop.FadeTo(0.2);
                    }
                    else
                    {
                        SlidingPanel.TranslateTo(0, SlidingPanel.Height, 150);
                        Backdrop.FadeTo(0);
                    }
                });
        }
    }
}