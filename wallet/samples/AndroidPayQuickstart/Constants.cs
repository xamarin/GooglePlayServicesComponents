using System;
using Android.Gms.Wallet;

namespace AndroidPayQuickstart
{
    public class Constants
    {
        // Environment to use when creating an instance of Wallet.WalletOptions
        public const int WALLET_ENVIRONMENT = WalletConstants.EnvironmentSandbox;

        public const string MERCHANT_NAME = "Awesome Bike Store";

        // Intent extra keys
        public const string EXTRA_ITEM_ID = "EXTRA_ITEM_ID";
        public const string EXTRA_MASKED_WALLET = "EXTRA_MASKED_WALLET";
        public const string EXTRA_FULL_WALLET = "EXTRA_FULL_WALLET";

        public const string CURRENCY_CODE_USD = "USD";

        // values to use with KEY_DESCRIPTION
        public const string DESCRIPTION_LINE_ITEM_SHIPPING = "Shipping";
        public const string DESCRIPTION_LINE_ITEM_TAX = "Tax";

        /**
     * Sample list of items for sale. The list would normally be fetched from
     * the merchant's servers.
     */
        public static readonly ItemInfo [] ITEMS_FOR_SALE = {
            new ItemInfo ("Simple Bike", "Features", 300000000, 9990000, CURRENCY_CODE_USD,
                "seller data 0", Resource.Drawable.bike000),
            new ItemInfo ("Adjustable Bike", "More features", 400000000, 9990000, CURRENCY_CODE_USD,
                "seller data 1", Resource.Drawable.bike001),
            new ItemInfo ("Conference Bike", "Even more features", 600000000, 9990000,
                CURRENCY_CODE_USD, "seller data 2", Resource.Drawable.bike002)
        };

        // To change promotion item, change the item here and also corresponding text/image
        // in fragment_promo_address_lookup.xml layout.
        public const int PROMOTION_ITEM = 2;

    }
}

