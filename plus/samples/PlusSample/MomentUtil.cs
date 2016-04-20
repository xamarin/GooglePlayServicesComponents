using System;
using System.Collections.Generic;
using System.Linq;
using Android.Gms.Plus.Model.Moments;

namespace PlusSample
{
    public class MomentUtil
    {
        /**
     * A mapping of moment type to target URL.
     */
        public static Dictionary<string, string> MOMENT_TYPES;

        /**
     * A list of moment target types.
     */
        public static List<string> MOMENT_LIST;
        public static string[] ACTIONS;

        static MomentUtil ()
        {
            MOMENT_TYPES = new Dictionary<string, string> (9);
            MOMENT_TYPES.Add ("AddActivity", "https://developers.google.com/+/plugins/snippet/examples/thing");
            MOMENT_TYPES.Add ("BuyActivity", "https://developers.google.com/+/plugins/snippet/examples/a-book");
            MOMENT_TYPES.Add ("CheckInActivity", "https://developers.google.com/+/plugins/snippet/examples/place");
            MOMENT_TYPES.Add ("CommentActivity", "https://developers.google.com/+/plugins/snippet/examples/blog-entry");
            MOMENT_TYPES.Add ("CreateActivity", "https://developers.google.com/+/plugins/snippet/examples/photo");
            MOMENT_TYPES.Add ("ListenActivity", "https://developers.google.com/+/plugins/snippet/examples/song");
            MOMENT_TYPES.Add ("ReserveActivity", "https://developers.google.com/+/plugins/snippet/examples/restaurant");
            MOMENT_TYPES.Add ("ReviewActivity", "https://developers.google.com/+/plugins/snippet/examples/widget");

            MOMENT_LIST = new List<string> (MomentUtil.MOMENT_TYPES.Keys);
            MOMENT_LIST.Sort ();

            ACTIONS = MOMENT_TYPES.Keys.ToArray ();
            int count = ACTIONS.Length;
            for (int i = 0; i < count; i++)
                ACTIONS[i] = "http://schemas.google.com/" + ACTIONS[i];
        }

        /**
     * Generates the "result" JSON object for select moments.
     *
     * @param momentType The type of the moment.
     */
        public static IItemScope GetResultFor (String momentType) 
        {
            if (momentType.Equals ("CommentActivity"))
                return GetCommentActivityResult ();
            if (momentType.Equals("ReserveActivity"))
                return GetReserveActivityResult();
            if (momentType.Equals("ReviewActivity"))
                return GetReviewActivityResult();
            return null;
        }

        /**
     * Generates the "result" JSON object for CommentActivity moment.
     */
        static IItemScope GetCommentActivityResult() 
        {
            return new ItemScopeBuilder ()
                .SetType("http://schema.org/Comment")
                .SetUrl("https://developers.google.com/+/plugins/snippet/examples/blog-entry#comment-1")
                .SetName("This is amazing!")
                .SetText("I can't wait to use it on my site!")
                .Build();
        }

        /**
     * Generates the "result" JSON object for ReserveActivity moment.
     */
        static IItemScope GetReserveActivityResult() 
        {
            return new ItemScopeBuilder ()
                .SetType("http://schemas.google.com/Reservation")
                .SetStartDate("2012-06-28T19:00:00-08:00")
                .SetAttendeeCount(3)
                .Build();
        }

        /**
     * Generates the "result" JSON object for ReviewActivity moment.
     */
        static IItemScope GetReviewActivityResult() 
        {
            var rating = new ItemScopeBuilder ()
                .SetType("http://schema.org/Rating")
                .SetRatingValue("100")
                .SetBestRating("100")
                .SetWorstRating("0")
                .Build();

            return new ItemScopeBuilder ()
                .SetType("http://schema.org/Review")
                .SetName("A Humble Review of Widget")
                .SetUrl("https://developers.google.com/+/plugins/snippet/examples/review")
                .SetText("It is amazingly effective")
                .SetReviewRating(rating)
                .Build();
        }
    }
}

