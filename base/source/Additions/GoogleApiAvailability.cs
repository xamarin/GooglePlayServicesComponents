using System;

namespace Android.Gms.Common
{
    public partial class GoogleApiAvailability
    {
        public override int IsGooglePlayServicesAvailable (Android.Content.Context context)
        {
            return base.IsGooglePlayServicesAvailable (context);
        }

        public override string GetOpenSourceSoftwareLicenseInfo (Android.Content.Context context)
        {
            return base.GetOpenSourceSoftwareLicenseInfo (context);
        }

        public override Android.App.PendingIntent GetErrorResolutionPendingIntent (Android.Content.Context context, int errorCode, int requestCode)
        {
            return base.GetErrorResolutionPendingIntent (context, errorCode, requestCode);
        }
    }
}
