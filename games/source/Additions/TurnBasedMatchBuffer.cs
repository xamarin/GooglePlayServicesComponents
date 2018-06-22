using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Gms.Common.Apis;
using Android.Runtime;

namespace Android.Gms.Games.MultiPlayer.TurnBased 
{
    // Metadata.xml XPath class reference: path="/api/package[@name='com.google.android.gms.games.multiplayer.turnbased']/class[@name='TurnBasedMatchBuffer']"
    public sealed partial class TurnBasedMatchBuffer 
    {
        // Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.gms.games.multiplayer.turnbased']/class[@name='TurnBasedMatchBuffer']/method[@name='get' and count(parameter)=1 and parameter[1][@type='int']]"
        [Register ("get", "(I)Ljava/lang/Object;", "")]
        public /* mc++ override */ unsafe global::Java.Lang.Object Get (int index)
        {
            const string __id = "get.(I)Ljava/lang/Object;";
            try {
                JniArgumentValue* __args = stackalloc JniArgumentValue [1];
                __args [0] = new JniArgumentValue (index);
                var __rm = _members.InstanceMethods.InvokeAbstractObjectMethod (__id, this, __args);
                return global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
            } finally {
            }
        }
    }
}