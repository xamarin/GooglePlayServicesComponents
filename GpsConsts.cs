
// Google Addon feed with GPS in it:
//      https://dl-ssl.google.com/android/repository/addon.xml

static class __GpsConsts {
    public const string Url  = "https://dl-ssl.google.com/android/repository/google_m2repository_r35.zip";
    public const string Sha1sum = "209d1f3b4c5db966044436b793fb8fbd77a7eaf1";
    public const string Version = "9.6.1";
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
