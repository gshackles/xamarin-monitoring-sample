using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ListView), typeof(CompanySearch.iOS.Renderers.EmptyFooterListViewRenderer))]

namespace CompanySearch.iOS.Renderers
{
    public class EmptyFooterListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
                Control.TableFooterView = new UIView();
        }
    }
}
