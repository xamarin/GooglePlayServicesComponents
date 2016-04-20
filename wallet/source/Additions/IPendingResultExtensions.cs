using System;
using System.Threading.Tasks;
using Android.Gms.Common.Apis;
using Android.Runtime;

namespace Android.Gms.Wallet
{
    public static partial class IPaymentsExtensions
    {
        public static async Task<BooleanResult> IsReadyToPayAsync (this IPayments api, GoogleApiClient googleApiClient)
        {
            return (await api.IsReadyToPay (googleApiClient)).JavaCast<BooleanResult> ();
        }
    }
}
