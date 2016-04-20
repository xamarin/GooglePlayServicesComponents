//using System;
//using Android.Runtime;
//using Java.Util.Concurrent;
//using Android.OS;
//using System.Threading.Tasks;
//using System.Runtime.CompilerServices;
//
//namespace Android.Gms.Common.Apis
//{
//    public class ResultCallback<TResult> : Java.Lang.Object, IResultCallback where TResult : class, IResult
//    {
//        public ResultCallback (Action<TResult> handler)
//        {
//            OnResultHandler = handler;
//        }
//
//        public Action<TResult> OnResultHandler { get; private set; }
//
//        public void OnResult (Java.Lang.Object result)
//        {
//            var h = OnResultHandler;
//            if (h != null)
//                h(result.JavaCast<TResult> ());
//        }
//    }
//
//    public class AwaitableResultCallback<TResult> : Java.Lang.Object, IResultCallback where TResult : class, IResult
//    {
//        public AwaitableResultCallback ()
//        {
//            taskCompletionSource = new TaskCompletionSource<TResult> ();
//        }
//
//        TaskCompletionSource<TResult> taskCompletionSource;
//
//        public void OnResult (Java.Lang.Object result)
//        {
//            var r = result.JavaCast<TResult> ();
//
//            taskCompletionSource.SetResult (r);
//        }
//
//        public Task<TResult> AwaitAsync ()
//        {
//            return taskCompletionSource.Task;
//        }
//
//        public TaskAwaiter<TResult> GetAwaiter ()
//        {
//            return taskCompletionSource.Task.GetAwaiter ();
//        }
//    }
//
//    public static class IPendingResultExtensions
//    {        
//        public static void SetResultCallback<TResult> (this IPendingResult pr, Action<TResult> callback) where TResult : class, IResult
//        {
//            pr.SetResultCallback (new ResultCallback<TResult> (callback));
//        }
//
//        public static Task<TResult> AsAsync<TResult> (this IPendingResult pr) where TResult : class, IResult
//        {
//            var rc = new AwaitableResultCallback<TResult> ();
//
//            pr.SetResultCallback (rc);
//
//            return rc.AwaitAsync ();
//        }
//
//        public static async Task AsAsync (this IPendingResult pr)
//        {
//            var rc = new AwaitableResultCallback<Statuses> ();
//
//            pr.SetResultCallback (rc);
//
//            await rc.AwaitAsync ();
//        }
//
//        public static TaskAwaiter<TResult> GetAwaiter<TResult>(this IPendingResult pr) where TResult : class, IResult
//        { 
//            var rc = new AwaitableResultCallback<TResult> ();
//
//            pr.SetResultCallback (rc);
//
//            return rc.GetAwaiter ();             
//        }
//
//        public static TaskAwaiter<IResult> GetAwaiter (this IPendingResult pr)
//        { 
//            var rc = new AwaitableResultCallback<IResult> ();
//
//            pr.SetResultCallback (rc);
//
//            return rc.GetAwaiter ();
//        }
//    }
//
//    public partial class GoogleApiClientBuilder
//    {
//        public GoogleApiClientBuilder AddConnectionCallbacks (Action<Bundle> connectedCallback, Action<int> connectionSuspendedCallback)
//        {
//            return AddConnectionCallbacks (new IGoogleApiClientConnectionCallbacksImpl (connectedCallback, connectionSuspendedCallback));
//        }
//
//        public GoogleApiClientBuilder AddOnConnectionFailedListener (Action<Android.Gms.Common.ConnectionResult> callback)
//        {
//            return AddOnConnectionFailedListener (new IGoogleApiClientOnConnectionFailedListenerImpl (callback));
//        }
//
//        public GoogleApiClientBuilder EnableAutoManage (Android.Support.V4.App.FragmentActivity fragmentActivity, int clientId, Action<Android.Gms.Common.ConnectionResult> unresolvedConnectionFailedHandler)
//        {
//            return EnableAutoManage (fragmentActivity, clientId, new IGoogleApiClientOnConnectionFailedListenerImpl (unresolvedConnectionFailedHandler));
//        }
//    }
//
//    internal class IGoogleApiClientConnectionCallbacksImpl : Java.Lang.Object, IGoogleApiClientConnectionCallbacks
//    {
//        public IGoogleApiClientConnectionCallbacksImpl (Action<Bundle> onConnectedHandler, Action<int> onConnectionSuspendedHandler)
//        {
//            OnConnectedHandler = onConnectedHandler;
//            OnConnectionSuspendedHandler = onConnectionSuspendedHandler;
//        }
//
//        public Action<Bundle> OnConnectedHandler { get; private set; }
//        public Action<int> OnConnectionSuspendedHandler { get; private set; }
//
//        public void OnConnected (Bundle bundle)
//        {
//            var h = OnConnectedHandler;
//            if (h != null)
//                h (bundle);
//        }
//
//        public void OnConnectionSuspended (int cause)
//        {
//            var h = OnConnectionSuspendedHandler;
//            if (h != null)
//                h (cause);
//        }
//    }
//
//    internal class IGoogleApiClientOnConnectionFailedListenerImpl : Java.Lang.Object, IGoogleApiClientOnConnectionFailedListener
//    {
//        public IGoogleApiClientOnConnectionFailedListenerImpl (Action<Android.Gms.Common.ConnectionResult> handler)
//        {
//            OnConnectionFailedHandler = handler;
//        }
//
//        public Action<Android.Gms.Common.ConnectionResult> OnConnectionFailedHandler { get; private set; }
//
//        public void OnConnectionFailed (Android.Gms.Common.ConnectionResult result)
//        {
//            var h = OnConnectionFailedHandler;
//            if (h != null)
//                h (result);
//        }
//    }
//}
