using System;
using System.Threading.Tasks;
using FFImageLoading;
using FFImageLoading.Svg.Platform;
using UIKit;
using Vulcanova.Core.Layout;
using Vulcanova.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

// This doesn't work unless the underlying viewmodel is specif ied
[assembly: ExportRenderer(typeof(SvgTabbedPage<HomeTabbedPageViewModel>), typeof(SvgTabbedPageRenderer))]
namespace Vulcanova.iOS
{
    // https://github.com/xamarin/xamarin-forms-samples/blob/main/Navigation/TabbedPageSVGIcons/TabbedPageSVGIcons.iOS/MyTabbedPageRenderer.cs
    public class SvgTabbedPageRenderer : TabbedRenderer
    {
        protected override async Task<Tuple<UIImage, UIImage>> GetIcon(Page page)
        {
            UIImage imageIcon;

            if (page.IconImageSource is FileImageSource fileImage && fileImage.File.Contains(".svg"))
            {
                // Load SVG from file
                var uiImage = await ImageService.Instance.LoadFile(fileImage.File)
                    .WithCustomDataResolver(new SvgDataResolver(10, 10))
                    .AsUIImageAsync();
                imageIcon = uiImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            }
            else
            {
                // Load Xamarin.Forms supported image
                IImageSourceHandler sourceHandler = null;
                switch (page.IconImageSource)
                {
                    case UriImageSource _:
                        sourceHandler = new ImageLoaderSourceHandler();
                        break;
                    case FileImageSource _:
                        sourceHandler = new FileImageSourceHandler();
                        break;
                    case StreamImageSource _:
                        sourceHandler = new StreamImagesourceHandler();
                        break;
                    case FontImageSource _:
                        sourceHandler = new FontImageSourceHandler();
                        break;
                }

                var uiImage = await sourceHandler.LoadImageAsync(page.IconImageSource);
                imageIcon = uiImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
            }

            return new Tuple<UIImage, UIImage>(imageIcon, null);
        }
    }
}