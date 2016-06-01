using System;
using Android.Content;

namespace Firebase.Provider
{
    // <provider
    //    android:authorities="${applicationId}.firebaseinitprovider"
    //    android:name=".provider.FirebaseInitProvider"
    //    android:exported="false"
    //    android:initOrder="100" />
    [ContentProvider (new [] { "${applicationId}.firebaseinitprovider" },
        Name="com.google.firebase.provider.FirebaseInitProvider",
        Exported=true, 
        InitOrder=100)]
    public partial class FirebaseInitProvider
    {
    }
}

