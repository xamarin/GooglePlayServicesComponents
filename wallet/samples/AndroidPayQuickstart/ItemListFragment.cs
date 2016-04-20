using System;
using Android.Support.V4.App;
using Android.Widget;
using Android.Content;
using Android.Views;

namespace AndroidPayQuickstart
{
    public class ItemListFragment : ListFragment
    {

        public override void OnActivityCreated (Android.OS.Bundle savedInstanceState)
        {
            base.OnActivityCreated (savedInstanceState);
        
            var adapter = new ItemAdapter (Activity, Constants.ITEMS_FOR_SALE);
            ListAdapter = adapter;
        }

        public override void OnListItemClick (ListView lValue, View vValue, int position, long id)
        {
            ((AdapterView.IOnItemClickListener) Activity).OnItemClick (lValue, vValue, position, id);
        }

        class ItemAdapter : ArrayAdapter<ItemInfo> 
        {
            LayoutInflater inflater;
            Context context;

            public ItemAdapter (Context context, ItemInfo[] objects) : base (context, Resource.Layout.list_item, Resource.Id.name, objects)
            {                
                inflater = LayoutInflater.From (context);
                this.context = context;
            }

            public override View GetView (int position, View convertView, ViewGroup parent)
            {
                if (convertView == null) {
                    convertView = inflater.Inflate (Resource.Layout.list_item, parent, false);
                }

                var info = GetItem (position);
                var title = convertView.FindViewById<TextView> (Resource.Id.name);
                var price = convertView.FindViewById<TextView> (Resource.Id.price);
                var image = convertView.FindViewById<ImageView> (Resource.Id.image);

                title.Text = info.Name;
                price.Text = Util.FormatPrice (context, info.PriceMicros);
                image.SetImageResource (info.ImageResourceId);

                return convertView;
            }
        }
    }
}

