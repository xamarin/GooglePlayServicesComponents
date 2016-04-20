using System;
using Android.Support.V4.App;
using Android.Widget;

namespace AndroidPayQuickstart
{
    public class CartDetailFragment : Fragment
    {
        private int mItemId;

        public override void OnCreate (Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);
            mItemId = Activity.Intent.GetIntExtra (Constants.EXTRA_ITEM_ID, 0);
        }

        public override Android.Views.View OnCreateView (Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Android.OS.Bundle savedInstanceState)
        {            
            var view = inflater.Inflate (Resource.Layout.fragment_cart_detail, container, false);

            var itemInfo = Constants.ITEMS_FOR_SALE[mItemId];

            var itemName = view.FindViewById<TextView> (Resource.Id.text_item_name);
            itemName.Text = itemInfo.Name;

            var itemImage = Resources.GetDrawable (itemInfo.ImageResourceId);
            int imageSize = Resources.GetDimensionPixelSize (Resource.Dimension.image_thumbnail_size);
            int actualWidth = itemImage.IntrinsicWidth;
            int actualHeight = itemImage.IntrinsicHeight;
            int scaledHeight = imageSize;
            int scaledWidth = (int) (((float) actualWidth / actualHeight) * scaledHeight);
            itemImage.SetBounds (0, 0, scaledWidth, scaledHeight);
            itemName.SetCompoundDrawables (itemImage, null, null, null);

            var itemPrice = view.FindViewById<TextView> (Resource.Id.text_item_price);
            itemPrice.Text = Util.FormatPrice (Activity, itemInfo.PriceMicros);
            var shippingCost = view.FindViewById<TextView> (Resource.Id.text_shipping_price);
            var tax = view.FindViewById<TextView> (Resource.Id.text_tax_price);
            var total = view.FindViewById<TextView> (Resource.Id.text_total_price);
            if ((mItemId == Constants.PROMOTION_ITEM) && ((BikestoreApplication)this.Activity.Application).IsAddressValidForPromo) {
                shippingCost.Text = Util.FormatPrice (Activity, 0L);
            } else {
                shippingCost.Text = Util.FormatPrice (Activity, itemInfo.ShippingPriceMicros);
            }

            tax.Text = Util.FormatPrice (Activity, itemInfo.TaxMicros);
            total.Text = Util.FormatPrice (Activity, itemInfo.TotalPrice);

            return view;
        }
    }
}

