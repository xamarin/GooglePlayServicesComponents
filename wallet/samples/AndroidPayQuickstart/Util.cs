using System;
using Android.Gms.Wallet;
using Android.Content;

namespace AndroidPayQuickstart
{
    public static class Util
    {
        /**
     * Formats the payment descriptions in a {@code MaskedWallet} for display.
     *
     * @param maskedWallet The wallet that contains the payment descriptions.
     * @return The payment descriptions in a format suitable for display to the user.
     */
        public static string FormatPaymentDescriptions (MaskedWallet maskedWallet) 
        {
            return string.Join ("\n", maskedWallet.GetPaymentDescriptions ());
        }

        /**
     * Formats the address for display.
     *
     * @param context The context to get String resources from.
     * @param address The {@link Address} to format.
     * @return The address in a format suitable for display to the user.
     */
        public static string FormatAddress (Context context, Address address) 
        {
            // different locales may need different address formats, which would be handled in
            // R.string.address_format
            var address2 = string.IsNullOrEmpty (address.Address2) ? address.Address2 : address.Address2 + "\n";
            var address3 = string.IsNullOrEmpty (address.Address3) ? address.Address3 : address.Address3 + "\n";


            return context.GetString (Resource.String.address_format, address.Name,
                                    address.Address1, address2, address3, address.City, address.State, address.PostalCode);
        }

        /**
     * Formats a price for display.
     *
     * @param context The context to get String resources from.
     * @param priceMicros The price to display, in micros.
     * @return The given price in a format suitable for display to the user.
     */
        public static string FormatPrice (Context context, long priceMicros) 
        {
            return context.GetString (Resource.String.price_format, priceMicros / 1000000d);
        }
    }
}

