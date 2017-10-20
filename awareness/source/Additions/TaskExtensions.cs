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
		public Task<DetectedActivityResponse> GetDetectedActivityAsync ()
		{
			return DetectedActivity.AsAsync<DetectedActivityResponse>();
		}

		public Task<HeadphoneStateResponse> GetHeadphoneStateAsync ()
		{
			return HeadphoneState.AsAsync<HeadphoneStateResponse>();
		}

		public Task<LocationResponse> GetLocationAsync ()
		{
			return Location.AsAsync<LocationResponse>();
		}

		public Task<PlacesResponse> GetPlacesAsync()
		{
			return Places.AsAsync<PlacesResponse>();
		}

		public Task<WeatherResponse> GetWeatherAsync()
		{
			return Weather.AsAsync<WeatherResponse>();
		}

		public Task<BeaconStateResponse> GetBeaconStateAsync(State.BeaconStateTypeFilter[] filter)
		{
			return GetBeaconState(filter).AsAsync<BeaconStateResponse>();
		}
	}
}
