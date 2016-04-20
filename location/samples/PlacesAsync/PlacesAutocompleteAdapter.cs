using System;
using System.Linq;
using Android.Widget;
using Android.Content;
using Android.Gms.Common.Apis;
using Android.Gms.Maps.Model;
using Android.Gms.Location.Places;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Runtime;
using Android.Views;

namespace PlacesAsync
{
    public class PlacesAutocompleteAdapter : BaseAdapter<PlaceInfo>, IFilterable
    {
        public PlacesAutocompleteAdapter (Context context, GoogleApiClient googleApiClient,
            LatLngBounds bounds, AutocompleteFilter filter)
        {
            this.context = context;
            this.googleApiClient = googleApiClient;
            this.bounds = bounds;
            this.autoCompleteFilter = filter;
        }


        internal Context context;
        internal GoogleApiClient googleApiClient;
        internal LatLngBounds bounds;
        internal AutocompleteFilter autoCompleteFilter;
        internal List<PlaceInfo> resultList = new List<PlaceInfo> ();

        PlacesFilter filter;

        public Filter Filter {
            get { 
                if (filter == null)
                    filter = new PlacesFilter { Adapter = this };
                return filter; 
            }
        }

        public override long GetItemId (int position)
        {
            return position;   
        }

        public override int Count {
            get { return resultList.Count; }
        }

        public override Android.Views.View GetView (int position, View convertView, ViewGroup parent)
        {
            var view = convertView ??
                LayoutInflater.From (context).Inflate (Android.Resource.Layout.SimpleListItem1, parent, false);
                
            view.FindViewById<TextView> (Android.Resource.Id.Text1).Text = this [position].Description;

            return view;
        }

        public override PlaceInfo this[int index] {
            get { return resultList [index]; }
        }

    }

    public class PlacesFilter : Filter
    {
        public PlacesAutocompleteAdapter Adapter { get; set; }

        #region implemented abstract members of Filter
        protected override FilterResults PerformFiltering (Java.Lang.ICharSequence constraint)
        {
            var results = new Filter.FilterResults ();
            // Skip the autocomplete query if no constraints are given.
            if (constraint != null) {
                // Query the autocomplete API for the (constraint) search string.
                Adapter.resultList = getAutocomplete (constraint).Result.ToList ();
                if (Adapter.resultList != null) {
                    // The API successfully returned results.
                    results.Values = new Java.Util.ArrayList (Adapter.resultList);
                    results.Count = Adapter.resultList.Count;
                }
            }
            return results;
        }

        protected override void PublishResults (Java.Lang.ICharSequence constraint, FilterResults results)
        {
            // If the API returned at least one result, update the data.
            if (results != null && results.Count > 0)
                Adapter.NotifyDataSetChanged ();
            else
                Adapter.NotifyDataSetInvalidated ();
        }
        #endregion
         
        async Task<List<PlaceInfo>> getAutocomplete (Java.Lang.ICharSequence constraint) 
        {
            if (Adapter.googleApiClient.IsConnected) {
                
                // Submit the query to the autocomplete API and await the results when the query completes.
                var autocompletePredictions = await PlacesClass.GeoDataApi.GetAutocompletePredictionsAsync (
                    Adapter.googleApiClient, constraint.ToString (), Adapter.bounds, Adapter.autoCompleteFilter);

                // Confirm that the query completed successfully, otherwise return null
                if (!autocompletePredictions.Status.IsSuccess) {                    
                    autocompletePredictions.Release ();
                    return new List<PlaceInfo> ();
                }

                // Copy the results into our own list, because we can't hold onto the buffer
                var list = autocompletePredictions.Select (p => new PlaceInfo { PlaceId = p.PlaceId, Description = p.Description })
                    .ToList ();

                // Release the buffer now that all data has been copied.
                autocompletePredictions.Release();

                return list;
            }

            return new List<PlaceInfo> ();
        }
    }

    public class PlaceInfo
    {
        public string PlaceId { get;set; }
        public string Description { get;set; }

        public override string ToString ()
        {
            return Description;
        }
    }
}

