using System;
using System.Threading.Tasks;
using Android.Gms.Common.Apis;
using Android.Runtime;

namespace Android.Gms.Nearby.Connection
{
    public static partial class IConnectionsExtensions
    {
        public static async Task<Statuses> AcceptConnectionRequestAsync (this IConnections api, GoogleApiClient apiClient, string remoteEndpointId, byte[] payload, IConnectionsMessageListener messageListener)
        {
            return (await api.AcceptConnectionRequest (apiClient, remoteEndpointId, payload, messageListener)).JavaCast<Statuses> ();
        }
        public static async Task<Statuses> RejectConnectionRequestAsync (this IConnections api, GoogleApiClient apiClient, string remoteEndpointId)
        {
            return (await api.RejectConnectionRequest (apiClient, remoteEndpointId)).JavaCast<Statuses> ();
        }
        public static async Task<Statuses> SendConnectionRequestAsync (this IConnections api, GoogleApiClient apiClient, string name, string remoteEndpointId, byte[] payload, IConnectionsConnectionResponseCallback connectionResponseCallback, IConnectionsMessageListener messageListener)
        {
            return (await api.SendConnectionRequest (apiClient, name, remoteEndpointId, payload, connectionResponseCallback, messageListener)).JavaCast<Statuses> ();
        }
        public static async Task<IConnectionsStartAdvertisingResult> StartAdvertisingAsync (this IConnections api, GoogleApiClient apiClient, string name, Android.Gms.Nearby.Connection.AppMetadata appMetadata, long durationMillis, IConnectionsConnectionRequestListener connectionRequestListener)
        {
            return (await api.StartAdvertising (apiClient, name, appMetadata, durationMillis, connectionRequestListener)).JavaCast<IConnectionsStartAdvertisingResult> ();
        }
        public static async Task<Statuses> StartDiscoveryAsync (this IConnections api, GoogleApiClient apiClient, string serviceId, long durationMillis, IConnectionsEndpointDiscoveryListener listener)
        {
            return (await api.StartDiscovery (apiClient, serviceId, durationMillis, listener)).JavaCast<Statuses> ();
        }
    }
}

namespace Android.Gms.Nearby.Messages
{
    public static partial class IMessagesExtensions
    {
        public static async Task<Statuses> GetPermissionStatusAsync (this IMessages api, GoogleApiClient client)
        {
            return (await api.GetPermissionStatus (client)).JavaCast<Statuses> ();
        }
        public static async Task<Statuses> PublishAsync (this IMessages api, GoogleApiClient client, Android.Gms.Nearby.Messages.Message message)
        {
            return (await api.Publish (client, message)).JavaCast<Statuses> ();
        }
        public static async Task<Statuses> PublishAsync (this IMessages api, GoogleApiClient client, Android.Gms.Nearby.Messages.Message message, Android.Gms.Nearby.Messages.PublishOptions options)
        {
            return (await api.Publish (client, message, options)).JavaCast<Statuses> ();
        }
        [Obsolete]
        public static async Task<Statuses> PublishAsync (this IMessages api, GoogleApiClient client, Android.Gms.Nearby.Messages.Message message, Android.Gms.Nearby.Messages.Strategy strategy)
        {
            return (await api.Publish (client, message, strategy)).JavaCast<Statuses> ();
        }
        public static async Task<Statuses> RegisterStatusCallbackAsync (this IMessages api, GoogleApiClient client, Android.Gms.Nearby.Messages.StatusCallback statusCallback)
        {
            return (await api.RegisterStatusCallback (client, statusCallback)).JavaCast<Statuses> ();
        }
        public static async Task<Statuses> SubscribeAsync (this IMessages api, GoogleApiClient client, Android.Gms.Nearby.Messages.MessageListener listener)
        {
            return (await api.Subscribe (client, listener)).JavaCast<Statuses> ();
        }
        [Obsolete]
        public static async Task<Statuses> SubscribeAsync (this IMessages api, GoogleApiClient client, Android.Gms.Nearby.Messages.MessageListener listener, Android.Gms.Nearby.Messages.Strategy strategy)
        {
            return (await api.Subscribe (client, listener, strategy)).JavaCast<Statuses> ();
        }
        [Obsolete]
        public static async Task<Statuses> SubscribeAsync (this IMessages api, GoogleApiClient client, Android.Gms.Nearby.Messages.MessageListener listener, Android.Gms.Nearby.Messages.Strategy strategy, Android.Gms.Nearby.Messages.MessageFilter filter)
        {
            return (await api.Subscribe (client, listener, strategy, filter)).JavaCast<Statuses> ();
        }
        public static async Task<Statuses> SubscribeAsync (this IMessages api, GoogleApiClient client, Android.Gms.Nearby.Messages.MessageListener listener, Android.Gms.Nearby.Messages.SubscribeOptions options)
        {
            return (await api.Subscribe (client, listener, options)).JavaCast<Statuses> ();
        }
        public static async Task<Statuses> UnpublishAsync (this IMessages api, GoogleApiClient client, Android.Gms.Nearby.Messages.Message message)
        {
            return (await api.Unpublish (client, message)).JavaCast<Statuses> ();
        }
        public static async Task<Statuses> UnregisterStatusCallbackAsync (this IMessages api, GoogleApiClient client, Android.Gms.Nearby.Messages.StatusCallback statusCallback)
        {
            return (await api.UnregisterStatusCallback (client, statusCallback)).JavaCast<Statuses> ();
        }
        public static async Task<Statuses> UnsubscribeAsync (this IMessages api, GoogleApiClient client, Android.Gms.Nearby.Messages.MessageListener listener)
        {
            return (await api.Unsubscribe (client, listener)).JavaCast<Statuses> ();
        }
        public static async Task<Statuses> SubscribeAsync (this IMessages api, GoogleApiClient client, Android.App.PendingIntent pendingIntent)
        {
            return (await api.Subscribe (client, pendingIntent)).JavaCast<Statuses> ();
        }
        public static async Task<Statuses> SubscribeAsync (this IMessages api, GoogleApiClient client, Android.App.PendingIntent pendingIntent, SubscribeOptions options)
        {
            return (await api.Subscribe (client, pendingIntent, options)).JavaCast<Statuses> ();
        }
        public static async Task<Statuses> UnsubscribeAsync (this IMessages api, GoogleApiClient client, Android.App.PendingIntent pendingIntent)
        {
            return (await api.Unsubscribe (client, pendingIntent)).JavaCast<Statuses> ();
        }
    }
}


