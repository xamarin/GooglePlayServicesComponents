using System;
using Android.Runtime;
using Android.App;

namespace Android.Gms.Auth.Api.SignIn.Internal
{
    // PACKAGE: com.google.android.gms.auth

//    <activity android:name=".api.signin.internal.SignInHubActivity"
//        android:theme="@android:style/Theme.Translucent.NoTitleBar"
//        android:excludeFromRecents="true"
//        android:exported="false" />
    [Activity (
        Name="com.google.android.gms.auth.api.signin.internal.SignInHubActivity",
        Theme="@android:style/Theme.Translucent.NoTitleBar",
        ExcludeFromRecents=true,
        Exported=false)]
    partial class SignInHubActivity { }

//        <!--Service handling Google Sign-In user revocation. For apps that do not integrate with
//            Google Sign-In, this service will never be started.-->
//        <service
//        android:name=".api.signin.RevocationBoundService"
//        android:exported="true"
//        android:permission="com.google.android.gms.auth.api.signin.permission.REVOCATION_NOTIFICATION" />
    [Service (
        Name="com.google.android.gms.auth.api.signin.RevocationBoundService",
        Exported=true,
        Permission="com.google.android.gms.auth.api.signin.permission.REVOCATION_NOTIFICATION")]
    partial class RevocationBoundService { }
}

