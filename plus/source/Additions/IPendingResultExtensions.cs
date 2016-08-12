using System;
using System.Threading.Tasks;
using Android.Gms.Common.Apis;
using Android.Runtime;
using System.Collections.Generic;

namespace Android.Gms.Plus
{
    public static partial class IAccountExtensions
    {
        [Obsolete ("deprecated")]
        public static async Task<Statuses> RevokeAccessAndDisconnectAsync (this IAccount api, GoogleApiClient googleApiClient)
        {
            return (await api.RevokeAccessAndDisconnect (googleApiClient)).JavaCast<Statuses> ();
        }
    }

    //public static partial class IMomentsExtensions
    //{
    //    public static async Task<IMomentsLoadMomentsResult> LoadAsync (this IMoments api, GoogleApiClient googleApiClient)
    //    {
    //        return (await api.Load (googleApiClient)).JavaCast<IMomentsLoadMomentsResult> ();
    //    }
    //    public static async Task<IMomentsLoadMomentsResult> LoadAsync (this IMoments api, GoogleApiClient googleApiClient, int maxResults, string pageToken, Android.Net.Uri targetUrl, string type, string userId)
    //    {
    //        return (await api.Load (googleApiClient, maxResults, pageToken, targetUrl, type, userId)).JavaCast<IMomentsLoadMomentsResult> ();
    //    }
    //    public static async Task<Statuses> RemoveAsync (this IMoments api, GoogleApiClient googleApiClient, string momentId)
    //    {
    //        return (await api.Remove (googleApiClient, momentId)).JavaCast<Statuses> ();
    //    }
    //    public static async Task<Statuses> WriteAsync (this IMoments api, GoogleApiClient googleApiClient, Android.Gms.Plus.Model.Moments.IMoment moment)
    //    {
    //        return (await api.Write (googleApiClient, moment)).JavaCast<Statuses> ();
    //    }
    //}

    public static partial class IPeopleExtensions
    {
        [Obsolete ("deprecated")]
        public static async Task<IPeopleLoadPeopleResult> LoadAsync (this IPeople api, GoogleApiClient googleApiClient, ICollection<string> personIds)
        {
            return (await api.Load (googleApiClient, personIds)).JavaCast<IPeopleLoadPeopleResult> ();
        }
        [Obsolete ("deprecated")]
        public static async Task<IPeopleLoadPeopleResult> LoadAsync (this IPeople api, GoogleApiClient googleApiClient, params string[] personIds)
        {
            return (await api.Load (googleApiClient, personIds)).JavaCast<IPeopleLoadPeopleResult> ();
        }
        [Obsolete ("deprecated")]
        public static async Task<IPeopleLoadPeopleResult> LoadConnectedAsync (this IPeople api, GoogleApiClient googleApiClient)
        {
            return (await api.LoadConnected (googleApiClient)).JavaCast<IPeopleLoadPeopleResult> ();
        }
        [Obsolete ("deprecated")]
        public static async Task<IPeopleLoadPeopleResult> LoadVisibleAsync (this IPeople api, GoogleApiClient googleApiClient, int orderBy, string pageToken)
        {
            return (await api.LoadVisible (googleApiClient, orderBy, pageToken)).JavaCast<IPeopleLoadPeopleResult> ();
        }
        [Obsolete ("deprecated")]
        public static async Task<IPeopleLoadPeopleResult> LoadVisibleAsync (this IPeople api, GoogleApiClient googleApiClient, string pageToken)
        {
            return (await api.LoadVisible (googleApiClient, pageToken)).JavaCast<IPeopleLoadPeopleResult> ();
        }
    }
}
