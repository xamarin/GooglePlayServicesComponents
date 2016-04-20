
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;
using Android.Gms.Analytics;

namespace Analytics
{
    public class EcommerceFragment : Fragment
    {
        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView (inflater, container, savedInstanceState);

            var view = inflater.Inflate (Resource.Layout.ecommerce, container, false);

            setUniqueOrderId (view);
            calculate (view);

            setupAutoCalculate (view, Resource.Id.item1Quantity);
            setupAutoCalculate (view, Resource.Id.item1Price);
            setupAutoCalculate (view, Resource.Id.item2Quantity);
            setupAutoCalculate (view, Resource.Id.item2Price);

            view.FindViewById<Button> (Resource.Id.ecommerceSend).Click += (sender, e) => {
                try {
                    sendDataToTwoTrackers (new HitBuilders.TransactionBuilder ()
                        .SetTransactionId (getOrderId ())
                        .SetAffiliation (getStoreName ())
                        .SetRevenue (getTotalOrder ())
                        .SetTax (getTotalTax ())
                        .SetShipping (getShippingCost ())
                        .SetCurrencyCode ("USD")
                        .Build ());

                    sendDataToTwoTrackers (new HitBuilders.ItemBuilder ()
                        .SetTransactionId (getOrderId ())
                        .SetName (getItemName (1))
                        .SetSku (getItemSku (1))
                        .SetCategory (getItemCategory (1))
                        .SetPrice (getItemPrice (View, 1))
                        .SetQuantity (getItemQuantity (View, 1))
                        .SetCurrencyCode ("USD")
                        .Build ());

                    sendDataToTwoTrackers (new HitBuilders.ItemBuilder ()
                        .SetTransactionId (getOrderId ())
                        .SetName (getItemName (2))
                        .SetSku (getItemSku (2))
                        .SetCategory (getItemCategory (2))
                        .SetPrice (getItemPrice (View, 2))
                        .SetQuantity (getItemQuantity (View, 2))
                        .SetCurrencyCode ("USD")
                        .Build ());
                } catch (Exception ex) {
                    Toast.MakeText (Activity, ex.Message, ToastLength.Short).Show ();
                }

                setUniqueOrderId (View);
            };

            view.FindViewById<Button> (Resource.Id.ecommerceDispatch).Click += (sender, e) => {
                GoogleAnalytics.GetInstance (Activity.BaseContext).DispatchLocalHits ();
            };

            return view;
        }

        void sendDataToTwoTrackers (IDictionary<string, string> param)
        {
            var appTracker = AnalyticsSampleApp.GetTracker (AnalyticsSampleApp.TrackerName.AppTracker);
            var ecommerceTracker = AnalyticsSampleApp.GetTracker (AnalyticsSampleApp.TrackerName.EcommerceTracker);

            appTracker.Send (param);
            ecommerceTracker.Send (param);
        }

        double calculate (View view)
        {
            var item1Total = getItemQuantity (view, 1) * getItemPrice (view, 1);
            view.FindViewById<TextView> (Resource.Id.item1Total).Text = item1Total.ToString ();

            var item2Total = getItemQuantity (view, 2) * getItemPrice (view, 2);
            view.FindViewById<TextView> (Resource.Id.item2Total).Text = item2Total.ToString ();

            var itemTotal = item1Total + item2Total;
            view.FindViewById<TextView> (Resource.Id.itemTotal).Text = itemTotal.ToString ();

            return itemTotal;
        }

        void setUniqueOrderId (View view)
        {
            var orderIdButton = view.FindViewById<EditText> (Resource.Id.orderId);

            var epochNow = DateTime.UtcNow - new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            orderIdButton.Text = GetString (Resource.String.orderId) + ((int)epochNow.TotalMilliseconds);
        }

        void setupAutoCalculate (View view, int editTextId)
        {
            var editText = view.FindViewById<EditText> (editTextId);
            editText.KeyPress += (sender, e) => {
                calculate (View);
            };
        }

        string getStoreName ()
        {
            var storeName = View.FindViewById<EditText> (Resource.Id.storeName).Text;
            if (string.IsNullOrWhiteSpace (storeName))
                return null;
            return storeName;
        }

        string getOrderId ()
        {
            var orderId = View.FindViewById<EditText> (Resource.Id.orderId).Text;                
            if (string.IsNullOrWhiteSpace (orderId))
                throw new Exception (GetString (Resource.String.orderIdWarning));           
            return orderId;
        }

        double getTotalOrder ()
        {
            var total = View.FindViewById<TextView> (Resource.Id.itemTotal).Text;
            double v = 0;
            double.TryParse (total, out v);
            return v;
        }

        double getTotalTax ()
        {
            var tax = View.FindViewById<EditText> (Resource.Id.totalTax).Text;
            double v = 0;
            double.TryParse (tax, out v);
            return v;
        }

        double getShippingCost ()
        {
            var shipping = View.FindViewById<EditText> (Resource.Id.shippingCost).Text;
            double v = 0;
            double.TryParse (shipping, out v);
            return v;
        }

        string getItemName (int index)
        {
            var buttonId = index == 1 ? Resource.Id.item1Name : Resource.Id.item2Name;
            var name = View.FindViewById<EditText> (buttonId).Text;
            if (string.IsNullOrWhiteSpace (name))
                return null;
            return name;
        }

        string getItemCategory (int index)
        {
            var buttonId = index == 1 ? Resource.Id.item1Category : Resource.Id.item2Category;
            var name = View.FindViewById<EditText> (buttonId).Text;
            if (string.IsNullOrWhiteSpace (name))
                return null;
            return name;
        }

        string getItemSku (int index)
        {
            var buttonId = index == 1 ? Resource.Id.item1Sku : Resource.Id.item2Sku;
            var sku = View.FindViewById<EditText> (buttonId).Text;
            if (string.IsNullOrWhiteSpace (sku)) {
                var warningId = index == 1 ? Resource.String.item1SkuWarning : Resource.String.item2SkuWarning;
                throw new Exception (GetString (warningId));
            }
            return sku;
        }

        long getItemQuantity (View view, int index)
        {
            var buttonId = index == 1 ? Resource.Id.item1Quantity : Resource.Id.item2Quantity;
            var quantity = view.FindViewById<EditText> (buttonId).Text;
            long v = 0;
            long.TryParse (quantity, out v);
            return v;
        }

        double getItemPrice (View view, int index)
        {
            var buttonId = index == 1 ? Resource.Id.item1Price : Resource.Id.item2Price;
            var price = view.FindViewById<EditText> (buttonId).Text;
            double v = 0;
            double.TryParse (price, out v);
            return v;
        }
    }
}

