using System;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Android.Content;

namespace AndroidPayQuickstart
{
    public class ItemDetailsFragment : Fragment
    {
        private View mRoot;
        protected int mItemId;

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Android.OS.Bundle savedInstanceState)
        {            
            mRoot = inflater.Inflate (Resource.Layout.fragment_item_details, container, false);
            mItemId = Activity.Intent.GetIntExtra (Constants.EXTRA_ITEM_ID, 2);

            ItemId = mItemId;

            return mRoot;
        }

        /**
     * Updates the item details with the item in {@link Constants#ITEMS_FOR_SALE}
     * at <code>position</code>
     *
     * @param position The index of the item in {@link Constants#ITEMS_FOR_SALE}
     * @see Constants#ITEMS_FOR_SALE
     */
        public int ItemId {
            get { return mItemId; }
            set {
            
                mItemId = value;
                ItemInfo itemInfo = Constants.ITEMS_FOR_SALE [mItemId];

                var itemName = mRoot.FindViewById<TextView> (Resource.Id.text_details_item_name);
                itemName.Text = itemInfo.Name;

                var itemPrice = mRoot.FindViewById<TextView> (Resource.Id.text_details_item_price);
                itemPrice.Text = Util.FormatPrice (Activity, itemInfo.PriceMicros);

                var imageView = mRoot.FindViewById<ImageView> (Resource.Id.image_details_item_image);
                imageView.SetImageResource (itemInfo.ImageResourceId);

                var button = mRoot.FindViewById<Button> (Resource.Id.button_details_button_add);
                button.Click += delegate {
                    var intent = new Intent (Activity, typeof(CheckoutActivity));
                    intent.PutExtra (Constants.EXTRA_ITEM_ID, mItemId);
                    StartActivity (intent);
                };
            }
        }
    }
}

