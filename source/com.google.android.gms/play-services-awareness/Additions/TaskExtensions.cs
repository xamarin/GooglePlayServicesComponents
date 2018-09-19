using System;
using System.Threading.Tasks;
using Android.Gms.Awareness.Fence;
using Android.Gms.Awareness.Snapshot;
using Android.Gms.Extensions;

namespace Android.Gms.Awareness
{
	public partial class FenceClient
	{
		public Task<FenceQueryResponse> QueryFencesAsync(FenceQueryRequest fenceQueryRequest)
		{
			return QueryFences(fenceQueryRequest).AsAsync<FenceQueryResponse>();
		}

		public Task UpdateFencesAsync(IFenceUpdateRequest fenceUpdateRequest)
		{
			return UpdateFences(fenceUpdateRequest).AsAsync();
		}
	}

	public partial class SnapshotClient
	{
        [Obsolete]
        public Android.Gms.Tasks.Task DetectedActivity { get { return GetDetectedActivity (); } }
		public Task<DetectedActivityResponse> GetDetectedActivityAsync ()
		{
			return GetDetectedActivity().AsAsync<DetectedActivityResponse>();
		}

        [Obsolete]
        public Android.Gms.Tasks.Task HeadphoneState { get { return GetHeadphoneState (); } }
		public Task<HeadphoneStateResponse> GetHeadphoneStateAsync ()
		{
			return GetHeadphoneState().AsAsync<HeadphoneStateResponse>();
		}

        [Obsolete]
        public Android.Gms.Tasks.Task Location { get { return GetLocation (); } }
		public Task<LocationResponse> GetLocationAsync ()
		{
			return GetLocation().AsAsync<LocationResponse>();
		}

        [Obsolete]
        public Android.Gms.Tasks.Task Places { get { return GetPlaces (); } }
        public Task<PlacesResponse> GetPlacesAsync()
		{
			return GetPlaces().AsAsync<PlacesResponse>();
		}

        [Obsolete]
        public Android.Gms.Tasks.Task Weather { get { return GetWeather (); } }
		public Task<WeatherResponse> GetWeatherAsync()
		{
			return GetWeather().AsAsync<WeatherResponse>();
		}

		public Task<BeaconStateResponse> GetBeaconStateAsync(State.BeaconStateTypeFilter[] filter)
		{
			return GetBeaconState(filter).AsAsync<BeaconStateResponse>();
		}

        [Obsolete]
        public Android.Gms.Tasks.Task TimeIntervals { get { return GetTimeIntervals (); } }
        public Task<Android.Gms.Awareness.Snapshot.TimeIntervalsResponse> GetTimeIntervalsAsync ()
        {
            return GetTimeIntervals ().AsAsync<Android.Gms.Awareness.Snapshot.TimeIntervalsResponse>();
        }
	}
}
