using System;
using Android.Runtime;
using Java.Interop;

namespace Okhttp3.Internal.WS
{
    partial class RealWebSocket : global::Java.Lang.Object, global::Okhttp3.IWebSocket
    {

        bool Okhttp3.IWebSocket.Close(int p0, string p1)
        {
            throw new NotImplementedException();
        }
    }
}

namespace Okhttp3.Internal.Http2
{
    public partial class Http2Connection
    {
        // Metadata.xml XPath class reference: path="/api/package[@name='okhttp3.internal.http2']/class[@name='Http2Connection.PingRunnable']"
        [global::Android.Runtime.Register("okhttp3/internal/http2/Http2Connection$PingRunnable", DoNotGenerateAcw = true)]
        public sealed class PingRunnable : global::Okhttp3.Internal.NamedRunnable
        {

            internal new static readonly JniPeerMembers _members = new XAPeerMembers("okhttp3/internal/http2/Http2Connection$PingRunnable", typeof(PingRunnable));
            internal static new IntPtr class_ref
            {
                get
                {
                    return _members.JniPeerType.PeerReference.Handle;
                }
            }

            public override global::Java.Interop.JniPeerMembers JniPeerMembers
            {
                get { return _members; }
            }

            protected override IntPtr ThresholdClass
            {
                get { return _members.JniPeerType.PeerReference.Handle; }
            }

            protected override global::System.Type ThresholdType
            {
                get { return _members.ManagedPeerType; }
            }

            internal PingRunnable(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }

            // Metadata.xml XPath method reference: path="/api/package[@name='okhttp3.internal.http2']/class[@name='Http2Connection.PingRunnable']/method[@name='execute' and count(parameter)=0]"
            [Register("execute", "()V", "")]
            protected override unsafe void Execute()
            {
                const string __id = "execute.()V";
                try
                {
                    _members.InstanceMethods.InvokeAbstractVoidMethod(__id, this, null);
                }
                finally
                {
                }
            }
        }
    }
}
