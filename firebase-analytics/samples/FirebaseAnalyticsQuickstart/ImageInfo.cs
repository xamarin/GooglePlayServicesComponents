using System;
namespace FirebaseAnalyticsQuickstart
{
    public class ImageInfo
    {
        public int Image { get; private set; }
        public int Title { get; private set; }
        public int Id { get; private set; }

        /**
         * Create a new ImageInfo.
         */
        public ImageInfo (int image, int title, int id)
        {
            Image = image;
            Title = title;
            Id = id;
        }
    }
}
