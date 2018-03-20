using System;
using System.Threading.Tasks;
using Java.Interop;
using Android.Gms.Common.Apis;
using Android.Gms.Awareness.Fence;
using Android.Gms.Awareness.Snapshot;
using Android.Gms.Awareness.State;

namespace Android.Gms.Awareness
{
    public static class IFenceApiExtensions
    {
        [Obsolete]
        public static async Task<IFenceQueryResult> QueryFencesAsync (this IFenceApi api, GoogleApiClient client, FenceQueryRequest fenceQueryRequest)
        {
            return (await api.QueryFences (client, fenceQueryRequest)).JavaCast<IFenceQueryResult> ();
        }
        [Obsolete]
        public static async Task<Statuses> UpdateFencesAsync (this IFenceApi api, GoogleApiClient client, IFenceUpdateRequest fenceUpdateRequest)
        {
            return (await api.UpdateFences (client, fenceUpdateRequest)).JavaCast<Statuses> ();
        }
    }

    public static class ISnapshotApiExtensions
    {
        [Obsolete]
        public static async Task<IBeaconStateResult> GetBeaconStateAsync (this ISnapshotApi api, GoogleApiClient client, BeaconStateTypeFilter [] beaconTypes)
        {
            return (await api.GetBeaconState (client)).JavaCast<IBeaconStateResult> ();
        }
        [Obsolete]
        public static async Task<IBeaconStateResult> GetBeaconStateAsync (this ISnapshotApi api, GoogleApiClient client, System.Collections.Generic.ICollection<BeaconStateTypeFilter> beaconTypes)
        {
            return (await api.GetBeaconState (client)).JavaCast<IBeaconStateResult> ();
        }
        [Obsolete]
        public static async Task<IDetectedActivityResult> GetDetectedActivityAsync (this ISnapshotApi api, GoogleApiClient client)
        {
            return (await api.GetDetectedActivity (client)).JavaCast<IDetectedActivityResult> ();
        }
        [Obsolete]
        public static async Task<IHeadphoneStateResult> GetHeadphoneStateAsync (this ISnapshotApi api, GoogleApiClient client)
        {
            return (await api.GetHeadphoneState (client)).JavaCast<IHeadphoneStateResult> ();
        }
        [Obsolete]
        public static async Task<ILocationResult> GetLocationAsync (this ISnapshotApi api, GoogleApiClient client)
        {
            return (await api.GetLocation (client)).JavaCast<ILocationResult> ();
        }
        [Obsolete]
        public static async Task<IPlacesResult> GetPlacesAsync (this ISnapshotApi api, GoogleApiClient client)
        {
            return (await api.GetPlaces (client)).JavaCast<IPlacesResult> ();
        }
        [Obsolete]
        public static async Task<IWeatherResult> GetWeatherAsync (this ISnapshotApi api, GoogleApiClient client)
        {
            return (await api.GetWeather (client)).JavaCast<IWeatherResult> ();
        }
    }
}



