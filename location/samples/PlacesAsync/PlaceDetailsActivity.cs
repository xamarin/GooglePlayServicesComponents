using System;
using Android.App;
using Android.Gms.Location.Places;
using Android.Gms.Common.Apis;
using System.Linq;
using Android.Widget;
using System.Text;
using System.Threading.Tasks;

namespace PlacesAsync
{
    [Activity]
    public class PlaceDetailsActivity : Android.Support.V4.App.FragmentActivity, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
    {
        GoogleApiClient googleApiClient;
        TextView textTitle;
        TextView textDetails;
        TextView textInfo;

        protected override void OnCreate (Android.OS.Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.PlaceDetails);

            textTitle = FindViewById<TextView> (Resource.Id.textTitle);
            textDetails = FindViewById<TextView> (Resource.Id.textDetails);
            textInfo = FindViewById<TextView> (Resource.Id.textInfo);

            googleApiClient = new GoogleApiClient.Builder (this)
                .AddConnectionCallbacks (this)
                .AddOnConnectionFailedListener (this)
                .AddApi (PlacesClass.GEO_DATA_API)
                .Build ();
        }

        protected async override void OnStart ()
        {
            base.OnStart ();

            googleApiClient.Connect ();
        }

        protected override void OnStop ()
        {
            googleApiClient.Disconnect ();

            base.OnStop ();
        }

        async Task GetDetails ()
        {
            if (googleApiClient.IsConnected) {

                var placeId = Intent.GetStringExtra ("PLACE_ID");

                var places = await PlacesClass.GeoDataApi.GetPlaceByIdAsync (googleApiClient, placeId);

                if (!places.Status.IsSuccess || !places.Any ()) {
                    Toast.MakeText (this, "Failed to load Place details", ToastLength.Short).Show ();
                    Finish ();
                    return;
                }

                var place = places.FirstOrDefault ();

                textTitle.Text = place.NameFormatted.ToString ();
                textDetails.Text = place.AddressFormatted.ToString ();

                var info = new StringBuilder ();
                info.AppendLine (new string ('$', place.PriceLevel < 0 ? 0 : place.PriceLevel));
                info.AppendLine (new string ('*', (int)place.Rating));
                info.AppendLine ();
                info.AppendLine (place.PhoneNumberFormatted.ToString ());
                info.AppendLine (place.WebsiteUri.ToString ());
                textInfo.Text = info.ToString ();
            }
        }


        public void OnConnectionFailed (Android.Gms.Common.ConnectionResult result)
        {
            Toast.MakeText (this, "Failed to Connect to Google Services", ToastLength.Long).Show ();
        }

        public void OnConnected (Android.OS.Bundle connectionHint)
        {
            GetDetails ();
        }

        public void OnConnectionSuspended (int cause)
        {
            Toast.MakeText (this, "Google Services Connection Suspended", ToastLength.Long).Show ();
        }
    }
}

