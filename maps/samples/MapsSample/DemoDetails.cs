using System;

namespace MapsSample
{
    public class DemoDetails : Java.Lang.Object
    {
        public DemoDetails (int titleId, int descriptionId, Type activityType)
        {
            TitleId = titleId;
            DescriptionId = descriptionId;
            ActivityType = activityType;
        }

        public int TitleId { get; private set; }
        public int DescriptionId { get; private set; }
        public Type ActivityType { get; private set; }
    }
}

