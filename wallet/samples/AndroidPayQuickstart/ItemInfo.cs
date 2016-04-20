using System;

namespace AndroidPayQuickstart
{
    public class ItemInfo
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        // Micros are used for prices to avoid rounding errors when converting between currencies
        public long PriceMicros { get; private set; }
        // The estimated tax used to calculate the estimated total price for a Masked Wallet request.
        public long EstimatedTaxMicros { get; private set; }
        // The estimated shipping price used with a Masked Wallet request.
        public long EstimatedShippingPriceMicros { get; private set; }
        // Actual tax and shipping price that should be calculated based on the shipping address
        // received in a MaskedWallet and used when fetching a Full Wallet.
        public long TaxMicros { get; private set; }
        public long ShippingPriceMicros { get; private set; }
        public string CurrencyCode { get; private set; }
        public string SellerData { get; private set; }
        public int ImageResourceId { get; private set; }

        public ItemInfo (string name, string description, long price, long shippingPrice,
            string currencyCode, string sellerData, int imageResourceId) {
            Name = name;
            Description = description;
            PriceMicros = price;
            EstimatedTaxMicros = (int) (price * 0.10);
            TaxMicros = (int) (price * 0.10);
            // put in an estimated shipping price
            EstimatedShippingPriceMicros = 10000000L;
            ShippingPriceMicros = shippingPrice;
            CurrencyCode = currencyCode;
            SellerData = sellerData;
            ImageResourceId = imageResourceId;
        }

        public override string ToString ()
        {
            return Name;
        }

        public long TotalPrice {
            get { return PriceMicros + TaxMicros + ShippingPriceMicros; }
        }
    }
}

