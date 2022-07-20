using FFImageLoading.Svg.Forms;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using Xamarin.Forms;

namespace Vulcanova.Core.Layout;

// https://github.com/luberda-molinet/FFImageLoading/issues/491#issuecomment-279891483
public class TintedCachedImage : SvgCachedImage
{
    public static BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(TintedCachedImage), Color.Transparent, propertyChanged: UpdateColor);

    public Color TintColor
    {
        get => (Color)GetValue(TintColorProperty);
        set => SetValue(TintColorProperty, value);
    }

    private static void UpdateColor(BindableObject bindable, object oldColor, object newColor)
    {
        var oldcolor = (Color)oldColor;
        var newcolor = (Color)newColor;

        if (oldcolor.Equals(newcolor)) return;

        var view = (TintedCachedImage)bindable;
        var transformations = new System.Collections.Generic.List<ITransformation>
        {
            new TintTransformation((int)(newcolor.R * 255), (int)(newcolor.G * 255), (int)(newcolor.B * 255), (int)(newcolor.A * 255)) {
                EnableSolidColor = true
            }
        };
        view.Transformations = transformations;
    }
}