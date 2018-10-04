
// Google Addon feed with GPS in it:
//      https://dl-ssl.google.com/android/repository/addon.xml

static class __GpsConsts {
    public const string Url  = "https://dl-ssl.google.com/android/repository/google_m2repository_gms_v8_rc41_wear_2a3.zip";
    public const string Sha1sum = "12d2355caafea06987c8323fa53c3ecc61477a35";
    public const string Version = "10.0.0";
    public const string WearVersion = "1.4.0";
    public const string PackageName = "Xamarin.GooglePlayServices";
}

static class __FbConsts
{
    public const string PackageName = __GpsConsts.PackageName;
    public const string Url = __GpsConsts.Url;
    public const string Sha1sum = __GpsConsts.Sha1sum;
    public const string Version = __GpsConsts.Version;
}
