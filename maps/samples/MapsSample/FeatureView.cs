using System;
using Android.Widget;
using Android.Content;
using Android.Views;

namespace MapsSample
{
    public class FeatureView : FrameLayout
    {
        public FeatureView (Context context)
            : base (context)
        {
            var layoutInflater = LayoutInflater.From (context);

            layoutInflater.Inflate (Resource.Layout.feature, this);
        }

        public void SetTitleId (int titleId)
        {
            FindViewById<TextView> (Resource.Id.title).SetText (titleId);
        }

        public void SetDescriptionId (int descriptionId)
        {
            FindViewById<TextView> (Resource.Id.description).SetText (descriptionId);
        }
    }
}

