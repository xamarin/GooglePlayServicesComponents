using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Gms.Extensions;

namespace Android.Gms.Nearby.Connection
{
    public partial class ConnectionsClient
    {
        public Task AcceptConnectionAsync (String p0, PayloadCallback p1)
        {
            return AcceptConnection (p0, p1).AsAsync ();
        }

        public Task CancelPayloadAsync (long p0)
        {
            return CancelPayload (p0).AsAsync ();
        }

        public Task RejectConnectionAsync (String p0)
        {
            return RejectConnection (p0).AsAsync ();
        }

        public Task RequestConnectionAsync (String p0, String p1, ConnectionLifecycleCallback p2)
        {
            return RequestConnection (p0, p1, p2).AsAsync ();
        }

        public Task SendPayloadAsync (String p0, Payload p1)
        {
            return SendPayload (p0, p1).AsAsync ();
        }

        public Task SendPayloadAsync (IList<String> p0, Payload p1)
        {
            return SendPayload (p0, p1).AsAsync ();
        }

        public Task StartAdvertisingAsync (String p0, String p1, ConnectionLifecycleCallback p2, AdvertisingOptions p3)
        {
            return StartAdvertising (p0, p1, p2, p3).AsAsync ();
        }

        public Task StartDiscoveryAsync (String p0, EndpointDiscoveryCallback p1, DiscoveryOptions p2)
        {
            return StartDiscovery (p0, p1, p2).AsAsync ();
        }
    }
}

namespace Android.Gms.Nearby.Messages
{
    public partial class MessagesClient
    {
        public Task PublishAsync (Message p0)
        {
            return Publish (p0).AsAsync ();
        }

        public Task PublishAsync (Message p0, PublishOptions p1)
        {
            return Publish (p0, p1).AsAsync ();
        }

        public Task RegisterStatusCallbackAsync (StatusCallback p0)
        {
            return RegisterStatusCallback (p0).AsAsync ();
        }

        public Task SubscribeAsync (PendingIntent p0)
        {
            return Subscribe (p0).AsAsync ();
        }

        public Task SubscribeAsync (PendingIntent p0, SubscribeOptions p1)
        {
            return Subscribe (p0, p1).AsAsync ();
        }

        public Task SubscribeAsync (MessageListener p0)
        {
            return Subscribe (p0).AsAsync ();
        }

        public Task SubscribeAsync (MessageListener p0, SubscribeOptions p1)
        {
            return Subscribe (p0, p1).AsAsync ();
        }

        public Task UnpublishAsync (Message p0)
        {
            return Unpublish (p0).AsAsync ();
        }

        public Task UnregisterStatusCallbackAsync (StatusCallback p0)
        {
            return UnregisterStatusCallback (p0).AsAsync ();
        }

        public Task UnsubscribeAsync (PendingIntent p0)
        {
            return Unsubscribe (p0).AsAsync ();
        }

        public Task UnsubscribeAsync (MessageListener p0)
        {
            return Unsubscribe (p0).AsAsync ();
        }
    }

}
