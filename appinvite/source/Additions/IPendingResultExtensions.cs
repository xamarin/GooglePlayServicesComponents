using System;
using System.Threading.Tasks;
using Android.Runtime;
using Android.Gms.Common.Apis;

namespace Android.Gms.AppInvite 
{
    public static partial class IAppInviteApiExtensions 
    {
        public static async Task<Statuses> ConvertInvitationAsync (this IAppInviteApi api, GoogleApiClient client, string invitationId) {
            return (await api.ConvertInvitation (client, invitationId)).JavaCast<Statuses> ();
        }

        public static async Task<Statuses> UpdateInvitationOnInstallAsync (this IAppInviteApi api, GoogleApiClient client, string invitationId) {
            return (await api.UpdateInvitationOnInstall (client, invitationId)).JavaCast<Statuses> ();
        }
    }
}


