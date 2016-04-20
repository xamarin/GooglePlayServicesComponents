using System;
using Android.Widget;
using Android.Support.V4.App;
using Android.Content;
using Android.App;

namespace AndroidPayQuickstart
{
    [Activity (Label = "Android Pay", MainLauncher = true, Icon = "@drawable/icon")]
    public class ItemListActivity : BikestoreFragmentActivity, AdapterView.IOnItemClickListener
    {
        bool mIsDualFrame = false;
        ListView mItemList;
        ItemDetailsFragment mDetailsFragment;

        protected override void OnCreate (Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);
        
            SetContentView(Resource.Layout.activity_item_list);

            mItemList = FindViewById<ListView> (Android.Resource.Id.List);
            mDetailsFragment = (ItemDetailsFragment) SupportFragmentManager.FindFragmentById (Resource.Id.item_details);
            mIsDualFrame = mDetailsFragment != null;
            if (mIsDualFrame) {
                mItemList.ChoiceMode = ChoiceMode.Single;
                mDetailsFragment.ItemId = 0;
            }
        }

        protected override void OnResume ()
        {
            base.OnResume ();
            ActivityCompat.InvalidateOptionsMenu (this);
        }

        protected override Android.Support.V4.App.Fragment ResultTargetFragment {
            get { return SupportFragmentManager.FindFragmentById (Resource.Id.promo_fragment); }
        }

        public void OnItemClick (AdapterView parent, Android.Views.View view, int position, long id)
        {
            if (mIsDualFrame) {
                mDetailsFragment.ItemId = position;
            } else {
                var intent = new Intent(this, typeof (ItemDetailsActivity));
                intent.PutExtra (Constants.EXTRA_ITEM_ID, position);

                StartActivity (intent);
            }
        }
    }
}

