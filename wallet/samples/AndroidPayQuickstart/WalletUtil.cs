using System;
using Android.Gms.Wallet;
using System.Collections.Generic;
using Java.Math;

namespace AndroidPayQuickstart
{
    public class WalletUtil
    {
        static readonly BigDecimal MICROS = new BigDecimal(1000000d);

        /**
     * Creates a MaskedWalletRequest for direct merchant integration (no payment processor)
     *
     * @param itemInfo {@link com.google.android.gms.samples.wallet.ItemInfo} containing details
     *                 of an item.
     * @return {@link MaskedWalletRequest} instance
     */
        public static MaskedWalletRequest CreateMaskedWalletRequest (ItemInfo itemInfo) 
        {
            return CreateMaskedWalletRequest (itemInfo, null);
        }

        /**
     * Creates a MaskedWalletRequest for processing payments with Stripe
     *
     * @param itemInfo {@link com.google.android.gms.samples.wallet.ItemInfo} containing details
     *                 of an item.
     * @param parameters {@link PaymentMethodTokenizationParameters} object containing details
     *                   for payment processing with Stripe.
     * @return {@link MaskedWalletRequest} instance
     */
        public static MaskedWalletRequest CreateStripeMaskedWalletRequest (ItemInfo itemInfo, PaymentMethodTokenizationParameters parameters) 
        {
            return CreateMaskedWalletRequest (itemInfo, parameters);
        }

        private static MaskedWalletRequest CreateMaskedWalletRequest (ItemInfo itemInfo, PaymentMethodTokenizationParameters parameters) {
            // Build a List of all line items
            var lineItems = buildLineItems (itemInfo, true);

            // Calculate the cart total by iterating over the line items.
            var cartTotal = calculateCartTotal(lineItems);

            var builder = MaskedWalletRequest.NewBuilder()
                .SetMerchantName (Constants.MERCHANT_NAME)
                .SetPhoneNumberRequired (true)
                .SetShippingAddressRequired (true)
                .SetCurrencyCode (Constants.CURRENCY_CODE_USD)
                .SetEstimatedTotalPrice (cartTotal)
                // Create a Cart with the current line items. Provide all the information
                // available up to this point with estimates for shipping and tax included.
                .SetCart (Cart.NewBuilder ()
                    .SetCurrencyCode (Constants.CURRENCY_CODE_USD)
                    .SetTotalPrice (cartTotal)
                    .SetLineItems (lineItems)
                    .Build ());

            if (parameters != null)
                builder.SetPaymentMethodTokenizationParameters (parameters);

            return builder.Build ();
        }

        /**
     * Build a list of line items based on the {@link ItemInfo} and a boolean that indicates
     * whether to use estimated values of tax and shipping for setting up the
     * {@link MaskedWalletRequest} or actual values in the case of a {@link FullWalletRequest}
     *
     * @param itemInfo {@link com.google.android.gms.samples.wallet.ItemInfo} used for building the
     *                 {@link com.google.android.gms.wallet.LineItem} list.
     * @param isEstimate {@code boolean} that indicates whether to use estimated values for
     *                   shipping and tax values.
     * @return list of line items
     */
        private static List<LineItem> buildLineItems (ItemInfo itemInfo, bool isEstimate) 
        {
            var list = new List<LineItem> ();
            var itemPrice = toDollars(itemInfo.PriceMicros);

            list.Add (LineItem.NewBuilder()
                .SetCurrencyCode (Constants.CURRENCY_CODE_USD)
                .SetDescription (itemInfo.Name)
                .SetQuantity ("1")
                .SetUnitPrice (itemPrice)
                .SetTotalPrice (itemPrice)
                .Build ());

            var shippingPrice = toDollars (
                isEstimate ? itemInfo.EstimatedShippingPriceMicros : itemInfo.ShippingPriceMicros);

            list.Add (LineItem.NewBuilder ()
                .SetCurrencyCode (Constants.CURRENCY_CODE_USD)
                .SetDescription (Constants.DESCRIPTION_LINE_ITEM_SHIPPING)
                .SetRole (LineItem.Role.Shipping)
                .SetTotalPrice (shippingPrice)
                .Build ());

            String tax = toDollars(
                isEstimate ? itemInfo.EstimatedTaxMicros : itemInfo.TaxMicros);

            list.Add (LineItem.NewBuilder ()
                .SetCurrencyCode (Constants.CURRENCY_CODE_USD)
                .SetDescription (Constants.DESCRIPTION_LINE_ITEM_TAX)
                .SetRole (LineItem.Role.Tax)
                .SetTotalPrice (tax)
                .Build ());

            return list;
        }

        /**
     *
     * @param lineItems List of {@link com.google.android.gms.wallet.LineItem} used for calculating
     *                  the cart total.
     * @return cart total.
     */
        private static String calculateCartTotal(List<LineItem> lineItems) 
        {
            BigDecimal cartTotal = BigDecimal.Zero;

            // Calculate the total price by adding up each of the line items
            foreach (var lineItem in lineItems) {
                BigDecimal lineItemTotal = lineItem.TotalPrice == null ?
                    new BigDecimal(lineItem.UnitPrice)
                    .Multiply (new BigDecimal(lineItem.Quantity)) :
                    new BigDecimal(lineItem.TotalPrice);

                cartTotal = cartTotal.Add (lineItemTotal);
            }

            return cartTotal.SetScale (2, RoundingMode.HalfEven).ToString ();
        }

        /**
     *
     * @param itemInfo {@link com.google.android.gms.samples.wallet.ItemInfo} to use for creating
     *                 the {@link com.google.android.gms.wallet.FullWalletRequest}
     * @param googleTransactionId
     * @return {@link FullWalletRequest} instance
     */
        public static FullWalletRequest CreateFullWalletRequest(ItemInfo itemInfo, string googleTransactionId) {

            List<LineItem> lineItems = buildLineItems(itemInfo, false);

            String cartTotal = calculateCartTotal(lineItems);

            return FullWalletRequest.NewBuilder ()
                .SetGoogleTransactionId (googleTransactionId)
                .SetCart (Cart.NewBuilder ()
                    .SetCurrencyCode (Constants.CURRENCY_CODE_USD)
                    .SetTotalPrice (cartTotal)
                    .SetLineItems (lineItems)
                    .Build ())
                .Build ();
        }

        /**
     * @param googleTransactionId
     * @param status from {@link NotifyTransactionStatusRequest.Status} which could either be
     *               {@code NotifyTransactionStatusRequest.Status.SUCCESS} or one of the error codes
     *               from {@link NotifyTransactionStatusRequest.Status.Error}
     * @return {@link NotifyTransactionStatusRequest} instance
     */

        public static NotifyTransactionStatusRequest CreateNotifyTransactionStatusRequest (string googleTransactionId, int status) {
            return NotifyTransactionStatusRequest.NewBuilder ()
                .SetGoogleTransactionId (googleTransactionId)
                .SetStatus (status)
                .Build ();
        }

        /**
     * @param micros Amount micros
     * @return string formatted as "0.00" required by the Instant Buy API.
     */
        static string toDollars (long micros)
        {
            return new BigDecimal (micros).Divide (MICROS)
                .SetScale (2, RoundingMode.HalfEven).ToString ();
        }
    }
}

