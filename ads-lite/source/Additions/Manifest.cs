using System;
using Android.App;
using Android.Content;
using Android.Runtime;

[assembly: UsesPermission (Android.Manifest.Permission.Internet)]
[assembly: UsesPermission (Android.Manifest.Permission.AccessNetworkState)]

namespace Android.Gms.Ads
{
//    <activity
//    android:name="com.google.android.gms.ads.AdActivity"
//        android:configChanges="keyboard|keyboardHidden|orientation|screenLayout|uiMode|screenSize|smallestScreenSize"
//        android:theme="@android:style/Theme.Translucent" />
    [Activity (
        Name="com.google.android.gms.ads.AdActivity",
        ConfigurationChanges = Android.Content.PM.ConfigChanges.Keyboard
            | Android.Content.PM.ConfigChanges.KeyboardHidden
            | Android.Content.PM.ConfigChanges.Orientation
            | Android.Content.PM.ConfigChanges.ScreenLayout
            | Android.Content.PM.ConfigChanges.UiMode
            | Android.Content.PM.ConfigChanges.ScreenSize
            | Android.Content.PM.ConfigChanges.SmallestScreenSize,
        Theme = "@android:style/Theme.Translucent")]
    partial class AdActivity { }
}

namespace Android.Gms.Ads.Purchase
{
//    <activity android:name="com.google.android.gms.ads.purchase.InAppPurchaseActivity"
//        android:theme="@style/Theme.IAPTheme"/>
    [Activity (
        Name="com.google.android.gms.ads.purchase.InAppPurchaseActivity",
        Theme="@style/Theme.IAPTheme")]
    partial class InAppPurchaseActivity { }
}

