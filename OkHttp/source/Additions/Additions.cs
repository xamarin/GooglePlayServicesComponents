using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;
using Square.OkIO;
using static Okhttp3.Internal.Http2.Http2Connection;

namespace Okhttp3
{
    // Metadata.xml XPath interface reference: path="/api/package[@name='okhttp3']/interface[@name='WebSocket']"
    [Register("okhttp3/WebSocket", "", "Okhttp3.IWebSocketInvoker")]
    public partial interface IWebSocket : IJavaObject
    {

        // Metadata.xml XPath method reference: path="/api/package[@name='okhttp3']/interface[@name='WebSocket']/method[@name='cancel' and count(parameter)=0]"
        [Register("cancel", "()V", "GetCancelHandler:Okhttp3.IWebSocketInvoker, OkHttp")]
        void Cancel();

        // Metadata.xml XPath method reference: path="/api/package[@name='okhttp3']/interface[@name='WebSocket']/method[@name='close' and count(parameter)=2 and parameter[1][@type='int'] and parameter[2][@type='java.lang.String']]"
        [Register("close", "(ILjava/lang/String;)Z", "GetClose_ILjava_lang_String_Handler:Okhttp3.IWebSocketInvoker, OkHttp")]
        bool Close(int p0, string p1);

        // Metadata.xml XPath method reference: path="/api/package[@name='okhttp3']/interface[@name='WebSocket']/method[@name='queueSize' and count(parameter)=0]"
        [Register("queueSize", "()J", "GetQueueSizeHandler:Okhttp3.IWebSocketInvoker, OkHttp")]
        long QueueSize();

        // Metadata.xml XPath method reference: path="/api/package[@name='okhttp3']/interface[@name='WebSocket']/method[@name='request' and count(parameter)=0]"
        [Register("request", "()Lokhttp3/Request;", "GetRequestHandler:Okhttp3.IWebSocketInvoker, OkHttp")]
        global::Okhttp3.Request Request();

        // Metadata.xml XPath method reference: path="/api/package[@name='okhttp3']/interface[@name='WebSocket']/method[@name='send' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
        [Register("send", "(Ljava/lang/String;)Z", "GetSend_Ljava_lang_String_Handler:Okhttp3.IWebSocketInvoker, OkHttp")]
        bool Send(string p0);

        // Metadata.xml XPath method reference: path="/api/package[@name='okhttp3']/interface[@name='WebSocket']/method[@name='send' and count(parameter)=1 and parameter[1][@type='okio.ByteString']]"
        [Register("send", "(Lokio/ByteString;)Z", "GetSend_Lokio_ByteString_Handler:Okhttp3.IWebSocketInvoker, OkHttp")]
        bool Send(global::Square.OkIO.ByteString p0);
    }
}

namespace Okhttp3.Internal.Http2
{
    public partial class Http2Connection
    {
        // Metadata.xml XPath class reference: path="/api/package[@name='okhttp3.internal.http2']/class[@name='Http2Connection.PingRunnable']"
        [global::Android.Runtime.Register("okhttp3/internal/http2/Http2Connection$PingRunnable", DoNotGenerateAcw = true)]
        public sealed partial class PingRunnable : global::Okhttp3.Internal.NamedRunnable
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


            protected override void Execute()
            {
                throw new NotImplementedException();
            }
        }
    }
}
