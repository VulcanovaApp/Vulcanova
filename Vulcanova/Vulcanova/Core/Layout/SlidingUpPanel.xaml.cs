using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using Xamarin.Essentials;
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
                .Subscribe(val => SlidingPanel.TranslationY = Math.Clamp(val, Sheet.Padding.Bottom, SlidingPanel.Height));

            this.PanGestureRecognizer.Events().PanUpdated
                .Where(a => a.StatusType is GestureStatus.Canceled or GestureStatus.Completed)
                .Select(_ => SlidingPanel.TranslationY)
                .WithLatestFrom(startPosition)
                .Subscribe(values =>
                {
                    var (end, start) = values;
                    Open = end <= start;
                });

            this.TapGestureRecognizer.Events().Tapped
                .Subscribe(_ => Open = false);

            var layout = this.Events().LayoutChanged;

            layout.Subscribe(_ =>
            {
                var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
                var height = mainDisplayInfo.Height / mainDisplayInfo.Density;
                Sheet.HeightRequest = height / 2 + Handle.Height + Sheet.Padding.Top + 40;
            });

            this.WhenAnyValue(x => x.Open)
                .CombineLatest(layout)
                .Subscribe(values =>
                {
                    var open = values.First;

                    if (open)
                    {
                        SlidingPanel.TranslateTo(0, Sheet.Padding.Bottom, 150);
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