
// Google Addon feed with GPS in it:
//      https://dl-ssl.google.com/android/repository/addon.xml

static class __GpsConsts {
    public const string Url  = "https://dl-ssl.google.com/android/repository/google_m2repository_r29.zip";
    public const string Version = "9.2.0";
    public const string Sha1sum = "ae24bde9c8f732f4d13b72e70802be8c97dcfddf";
    public const string WearVersion = "1.4.0";
}

static class __FbConsts
{
    public const string Url = __GpsConsts.Url;
    public const string Sha1sum = __GpsConsts.Sha1sum;
    public const string Version = __GpsConsts.Version;
}