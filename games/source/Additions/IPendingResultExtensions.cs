using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Gms.Common.Apis;
using Android.Runtime;

namespace Android.Gms.Games
{
    public partial class GamesClass
    {
        public async Task<Statuses> SignOutAsync (GoogleApiClient apiClient)
        {
            return (await SignOut (apiClient)).JavaCast<Statuses> ();
        }
    }

    public static partial class IGamesMetadataExtensions
    {
        public static async Task<IGamesMetadataLoadGamesResult> LoadGameAsync (this IGamesMetadata api, GoogleApiClient apiClient)
        {
            return (await api.LoadGame (apiClient)).JavaCast<IGamesMetadataLoadGamesResult> ();
        }
    }

    public static partial class IPlayersExtensions
    {
        public static async Task<IPlayersLoadPlayersResult> LoadConnectedPlayersAsync (this IPlayers api, GoogleApiClient apiClient, bool forceReload)
        {
            return (await api.LoadConnectedPlayers (apiClient, forceReload)).JavaCast<IPlayersLoadPlayersResult> ();
        }
        public static async Task<IPlayersLoadPlayersResult> LoadInvitablePlayersAsync (this IPlayers api, GoogleApiClient apiClient, int pageSize, bool forceReload)
        {
            return (await api.LoadInvitablePlayers (apiClient, pageSize, forceReload)).JavaCast<IPlayersLoadPlayersResult> ();
        }
        public static async Task<IPlayersLoadPlayersResult> LoadMoreInvitablePlayersAsync (this IPlayers api, GoogleApiClient apiClient, int pageSize)
        {
            return (await api.LoadMoreInvitablePlayers (apiClient, pageSize)).JavaCast<IPlayersLoadPlayersResult> ();
        }
        public static async Task<IPlayersLoadPlayersResult> LoadMoreRecentlyPlayedWithPlayersAsync (this IPlayers api, GoogleApiClient apiClient, int pageSize)
        {
            return (await api.LoadMoreRecentlyPlayedWithPlayers (apiClient, pageSize)).JavaCast<IPlayersLoadPlayersResult> ();
        }
        public static async Task<IPlayersLoadPlayersResult> LoadPlayerAsync (this IPlayers api, GoogleApiClient apiClient, string playerId)
        {
            return (await api.LoadPlayer (apiClient, playerId)).JavaCast<IPlayersLoadPlayersResult> ();
        }
        public static async Task<IPlayersLoadPlayersResult> LoadPlayerAsync (this IPlayers api, GoogleApiClient apiClient, string playerId, bool forceReload)
        {
            return (await api.LoadPlayer (apiClient, playerId, forceReload)).JavaCast<IPlayersLoadPlayersResult> ();
        }
        public static async Task<IPlayersLoadPlayersResult> LoadRecentlyPlayedWithPlayersAsync (this IPlayers api, GoogleApiClient apiClient, int pageSize, bool forceReload)
        {
            return (await api.LoadRecentlyPlayedWithPlayers (apiClient, pageSize, forceReload)).JavaCast<IPlayersLoadPlayersResult> ();
        }
    }
}
namespace Android.Gms.Games.Achievement
{
    public static partial class IAchievementsExtensions
    {
        public static async Task<IAchievementsUpdateAchievementResult> IncrementImmediateAsync (this IAchievements api, GoogleApiClient apiClient, string id, int numSteps)
        {
            return (await api.IncrementImmediate (apiClient, id, numSteps)).JavaCast<IAchievementsUpdateAchievementResult> ();
        }
        public static async Task<IAchievementsLoadAchievementsResult> LoadAsync (this IAchievements api, GoogleApiClient apiClient, bool forceReload)
        {
            return (await api.Load (apiClient, forceReload)).JavaCast<IAchievementsLoadAchievementsResult> ();
        }
        public static async Task<IAchievementsUpdateAchievementResult> RevealImmediateAsync (this IAchievements api, GoogleApiClient apiClient, string id)
        {
            return (await api.RevealImmediate (apiClient, id)).JavaCast<IAchievementsUpdateAchievementResult> ();
        }
        public static async Task<IAchievementsUpdateAchievementResult> SetStepsImmediateAsync (this IAchievements api, GoogleApiClient apiClient, string id, int numSteps)
        {
            return (await api.SetStepsImmediate (apiClient, id, numSteps)).JavaCast<IAchievementsUpdateAchievementResult> ();
        }
        public static async Task<IAchievementsUpdateAchievementResult> UnlockImmediateAsync (this IAchievements api, GoogleApiClient apiClient, string id)
        {
            return (await api.UnlockImmediate (apiClient, id)).JavaCast<IAchievementsUpdateAchievementResult> ();
        }
    }
}
namespace Android.Gms.Games.Event
{
    public static partial class IEventsExtensions
    {
        public static async Task<IEventsLoadEventsResult> LoadAsync (this IEvents api, GoogleApiClient apiClient, bool forceReload)
        {
            return (await api.Load (apiClient, forceReload)).JavaCast<IEventsLoadEventsResult> ();
        }
        public static async Task<IEventsLoadEventsResult> LoadByIdsAsync (this IEvents api, GoogleApiClient apiClient, bool forceReload, params string[] eventIds)
        {
            return (await api.LoadByIds (apiClient, forceReload, eventIds)).JavaCast<IEventsLoadEventsResult> ();
        }
    }
}
namespace Android.Gms.Games.LeaderBoard
{
    public static partial class ILeaderboardsExtensions
    {
        public static async Task<ILeaderboardsLoadPlayerScoreResult> LoadCurrentPlayerLeaderboardScoreAsync (this ILeaderboards api, GoogleApiClient apiClient, string leaderboardId, int span, int leaderboardCollection)
        {
            return (await api.LoadCurrentPlayerLeaderboardScore (apiClient, leaderboardId, span, leaderboardCollection)).JavaCast<ILeaderboardsLoadPlayerScoreResult> ();
        }
        public static async Task<ILeaderboardsLeaderboardMetadataResult> LoadLeaderboardMetadataAsync (this ILeaderboards api, GoogleApiClient apiClient, string leaderboardId, bool forceReload)
        {
            return (await api.LoadLeaderboardMetadata (apiClient, leaderboardId, forceReload)).JavaCast<ILeaderboardsLeaderboardMetadataResult> ();
        }
        public static async Task<ILeaderboardsLeaderboardMetadataResult> LoadLeaderboardMetadataAsync (this ILeaderboards api, GoogleApiClient apiClient, bool forceReload)
        {
            return (await api.LoadLeaderboardMetadata (apiClient, forceReload)).JavaCast<ILeaderboardsLeaderboardMetadataResult> ();
        }
        public static async Task<ILeaderboardsLoadScoresResult> LoadMoreScoresAsync (this ILeaderboards api, GoogleApiClient apiClient, LeaderboardScoreBuffer buffer, int maxResults, int pageDirection)
        {
            return (await api.LoadMoreScores (apiClient, buffer, maxResults, pageDirection)).JavaCast<ILeaderboardsLoadScoresResult> ();
        }
        public static async Task<ILeaderboardsLoadScoresResult> LoadPlayerCenteredScoresAsync (this ILeaderboards api, GoogleApiClient apiClient, string leaderboardId, int span, int leaderboardCollection, int maxResults)
        {
            return (await api.LoadPlayerCenteredScores (apiClient, leaderboardId, span, leaderboardCollection, maxResults)).JavaCast<ILeaderboardsLoadScoresResult> ();
        }
        public static async Task<ILeaderboardsLoadScoresResult> LoadPlayerCenteredScoresAsync (this ILeaderboards api, GoogleApiClient apiClient, string leaderboardId, int span, int leaderboardCollection, int maxResults, bool forceReload)
        {
            return (await api.LoadPlayerCenteredScores (apiClient, leaderboardId, span, leaderboardCollection, maxResults, forceReload)).JavaCast<ILeaderboardsLoadScoresResult> ();
        }
        public static async Task<ILeaderboardsLoadScoresResult> LoadTopScoresAsync (this ILeaderboards api, GoogleApiClient apiClient, string leaderboardId, int span, int leaderboardCollection, int maxResults)
        {
            return (await api.LoadTopScores (apiClient, leaderboardId, span, leaderboardCollection, maxResults)).JavaCast<ILeaderboardsLoadScoresResult> ();
        }
        public static async Task<ILeaderboardsLoadScoresResult> LoadTopScoresAsync (this ILeaderboards api, GoogleApiClient apiClient, string leaderboardId, int span, int leaderboardCollection, int maxResults, bool forceReload)
        {
            return (await api.LoadTopScores (apiClient, leaderboardId, span, leaderboardCollection, maxResults, forceReload)).JavaCast<ILeaderboardsLoadScoresResult> ();
        }
        public static async Task<ILeaderboardsSubmitScoreResult> SubmitScoreImmediateAsync (this ILeaderboards api, GoogleApiClient apiClient, string leaderboardId, long score)
        {
            return (await api.SubmitScoreImmediate (apiClient, leaderboardId, score)).JavaCast<ILeaderboardsSubmitScoreResult> ();
        }
        public static async Task<ILeaderboardsSubmitScoreResult> SubmitScoreImmediateAsync (this ILeaderboards api, GoogleApiClient apiClient, string leaderboardId, long score, string scoreTag)
        {
            return (await api.SubmitScoreImmediate (apiClient, leaderboardId, score, scoreTag)).JavaCast<ILeaderboardsSubmitScoreResult> ();
        }
    }
}
namespace Android.Gms.Games.MultiPlayer
{
    public static partial class IInvitationsExtensions
    {
        public static async Task<IInvitationsLoadInvitationsResult> LoadInvitationsAsync (this IInvitations api, GoogleApiClient apiClient)
        {
            return (await api.LoadInvitations (apiClient)).JavaCast<IInvitationsLoadInvitationsResult> ();
        }
        public static async Task<IInvitationsLoadInvitationsResult> LoadInvitationsAsync (this IInvitations api, GoogleApiClient apiClient, int sortOrder)
        {
            return (await api.LoadInvitations (apiClient, sortOrder)).JavaCast<IInvitationsLoadInvitationsResult> ();
        }
    }
}
namespace Android.Gms.Games.MultiPlayer.TurnBased
{
    public static partial class ITurnBasedMultiplayerExtensions
    {
        public static async Task<ITurnBasedMultiplayerInitiateMatchResult> AcceptInvitationAsync (this ITurnBasedMultiplayer api, GoogleApiClient apiClient, string invitationId)
        {
            return (await api.AcceptInvitation (apiClient, invitationId)).JavaCast<ITurnBasedMultiplayerInitiateMatchResult> ();
        }
        public static async Task<ITurnBasedMultiplayerCancelMatchResult> CancelMatchAsync (this ITurnBasedMultiplayer api, GoogleApiClient apiClient, string matchId)
        {
            return (await api.CancelMatch (apiClient, matchId)).JavaCast<ITurnBasedMultiplayerCancelMatchResult> ();
        }
        public static async Task<ITurnBasedMultiplayerInitiateMatchResult> CreateMatchAsync (this ITurnBasedMultiplayer api, GoogleApiClient apiClient, TurnBasedMatchConfig config)
        {
            return (await api.CreateMatch (apiClient, config)).JavaCast<ITurnBasedMultiplayerInitiateMatchResult> ();
        }
        public static async Task<ITurnBasedMultiplayerUpdateMatchResult> FinishMatchAsync (this ITurnBasedMultiplayer api, GoogleApiClient apiClient, string matchId)
        {
            return (await api.FinishMatch (apiClient, matchId)).JavaCast<ITurnBasedMultiplayerUpdateMatchResult> ();
        }
        public static async Task<ITurnBasedMultiplayerUpdateMatchResult> FinishMatchAsync (this ITurnBasedMultiplayer api, GoogleApiClient apiClient, string matchId, byte[] matchData, IList<ParticipantResult> results)
        {
            return (await api.FinishMatch (apiClient, matchId, matchData, results)).JavaCast<ITurnBasedMultiplayerUpdateMatchResult> ();
        }
        public static async Task<ITurnBasedMultiplayerUpdateMatchResult> FinishMatchAsync (this ITurnBasedMultiplayer api, GoogleApiClient apiClient, string matchId, byte[] matchData, params ParticipantResult[] results)
        {
            return (await api.FinishMatch (apiClient, matchId, matchData, results)).JavaCast<ITurnBasedMultiplayerUpdateMatchResult> ();
        }
        public static async Task<ITurnBasedMultiplayerLeaveMatchResult> LeaveMatchAsync (this ITurnBasedMultiplayer api, GoogleApiClient apiClient, string matchId)
        {
            return (await api.LeaveMatch (apiClient, matchId)).JavaCast<ITurnBasedMultiplayerLeaveMatchResult> ();
        }
        public static async Task<ITurnBasedMultiplayerLeaveMatchResult> LeaveMatchDuringTurnAsync (this ITurnBasedMultiplayer api, GoogleApiClient apiClient, string matchId, string pendingParticipantId)
        {
            return (await api.LeaveMatchDuringTurn (apiClient, matchId, pendingParticipantId)).JavaCast<ITurnBasedMultiplayerLeaveMatchResult> ();
        }
        public static async Task<ITurnBasedMultiplayerLoadMatchResult> LoadMatchAsync (this ITurnBasedMultiplayer api, GoogleApiClient apiClient, string matchId)
        {
            return (await api.LoadMatch (apiClient, matchId)).JavaCast<ITurnBasedMultiplayerLoadMatchResult> ();
        }
        public static async Task<ITurnBasedMultiplayerLoadMatchesResult> LoadMatchesByStatusAsync (this ITurnBasedMultiplayer api, GoogleApiClient apiClient, int invitationSortOrder, int[] matchTurnStatuses)
        {
            return (await api.LoadMatchesByStatus (apiClient, invitationSortOrder, matchTurnStatuses)).JavaCast<ITurnBasedMultiplayerLoadMatchesResult> ();
        }
        public static async Task<ITurnBasedMultiplayerLoadMatchesResult> LoadMatchesByStatusAsync (this ITurnBasedMultiplayer api, GoogleApiClient apiClient, int[] matchTurnStatuses)
        {
            return (await api.LoadMatchesByStatus (apiClient, matchTurnStatuses)).JavaCast<ITurnBasedMultiplayerLoadMatchesResult> ();
        }
        public static async Task<ITurnBasedMultiplayerInitiateMatchResult> RematchAsync (this ITurnBasedMultiplayer api, GoogleApiClient apiClient, string matchId)
        {
            return (await api.Rematch (apiClient, matchId)).JavaCast<ITurnBasedMultiplayerInitiateMatchResult> ();
        }
        public static async Task<ITurnBasedMultiplayerUpdateMatchResult> TakeTurnAsync (this ITurnBasedMultiplayer api, GoogleApiClient apiClient, string matchId, byte[] matchData, string pendingParticipantId)
        {
            return (await api.TakeTurn (apiClient, matchId, matchData, pendingParticipantId)).JavaCast<ITurnBasedMultiplayerUpdateMatchResult> ();
        }
        public static async Task<ITurnBasedMultiplayerUpdateMatchResult> TakeTurnAsync (this ITurnBasedMultiplayer api, GoogleApiClient apiClient, string matchId, byte[] matchData, string pendingParticipantId, IList<ParticipantResult> results)
        {
            return (await api.TakeTurn (apiClient, matchId, matchData, pendingParticipantId, results)).JavaCast<ITurnBasedMultiplayerUpdateMatchResult> ();
        }
        public static async Task<ITurnBasedMultiplayerUpdateMatchResult> TakeTurnAsync (this ITurnBasedMultiplayer api, GoogleApiClient apiClient, string matchId, byte[] matchData, string pendingParticipantId, params ParticipantResult[] results)
        {
            return (await api.TakeTurn (apiClient, matchId, matchData, pendingParticipantId, results)).JavaCast<ITurnBasedMultiplayerUpdateMatchResult> ();
        }
    }
}
namespace Android.Gms.Games.Quest
{
    public static partial class IQuestsExtensions
    {
        public static async Task<IQuestsAcceptQuestResult> AcceptAsync (this IQuests api, GoogleApiClient apiClient, string questId)
        {
            return (await api.Accept (apiClient, questId)).JavaCast<IQuestsAcceptQuestResult> ();
        }
        public static async Task<IQuestsClaimMilestoneResult> ClaimAsync (this IQuests api, GoogleApiClient apiClient, string questId, string milestoneId)
        {
            return (await api.Claim (apiClient, questId, milestoneId)).JavaCast<IQuestsClaimMilestoneResult> ();
        }
        public static async Task<IQuestsLoadQuestsResult> LoadAsync (this IQuests api, GoogleApiClient apiClient, int[] questSelectors, int sortOrder, bool forceReload)
        {
            return (await api.Load (apiClient, questSelectors, sortOrder, forceReload)).JavaCast<IQuestsLoadQuestsResult> ();
        }
        public static async Task<IQuestsLoadQuestsResult> LoadByIdsAsync (this IQuests api, GoogleApiClient apiClient, bool forceReload, params string[] questIds)
        {
            return (await api.LoadByIds (apiClient, forceReload, questIds)).JavaCast<IQuestsLoadQuestsResult> ();
        }
    }
}
namespace Android.Gms.Games.Request
{
    public static partial class IRequestsExtensions
    {
        public static async Task<IRequestsUpdateRequestsResult> AcceptRequestAsync (this IRequests api, GoogleApiClient apiClient, string requestId)
        {
            return (await api.AcceptRequest (apiClient, requestId)).JavaCast<IRequestsUpdateRequestsResult> ();
        }
        public static async Task<IRequestsUpdateRequestsResult> AcceptRequestsAsync (this IRequests api, GoogleApiClient apiClient, IList<string> requestIds)
        {
            return (await api.AcceptRequests (apiClient, requestIds)).JavaCast<IRequestsUpdateRequestsResult> ();
        }
        public static async Task<IRequestsUpdateRequestsResult> DismissRequestAsync (this IRequests api, GoogleApiClient apiClient, string requestId)
        {
            return (await api.DismissRequest (apiClient, requestId)).JavaCast<IRequestsUpdateRequestsResult> ();
        }
        public static async Task<IRequestsUpdateRequestsResult> DismissRequestsAsync (this IRequests api, GoogleApiClient apiClient, IList<string> requestIds)
        {
            return (await api.DismissRequests (apiClient, requestIds)).JavaCast<IRequestsUpdateRequestsResult> ();
        }
        public static async Task<IRequestsLoadRequestsResult> LoadRequestsAsync (this IRequests api, GoogleApiClient apiClient, int requestDirection, int types, int sortOrder)
        {
            return (await api.LoadRequests (apiClient, requestDirection, types, sortOrder)).JavaCast<IRequestsLoadRequestsResult> ();
        }
    }
}
namespace Android.Gms.Games.Snapshot
{
    public static partial class ISnapshotsExtensions
    {
        public static async Task<ISnapshotsCommitSnapshotResult> CommitAndCloseAsync (this ISnapshots api, GoogleApiClient apiClient, ISnapshot snapshot, SnapshotMetadataChange metadataChange)
        {
            return (await api.CommitAndClose (apiClient, snapshot, metadataChange)).JavaCast<ISnapshotsCommitSnapshotResult> ();
        }
        public static async Task<ISnapshotsDeleteSnapshotResult> DeleteAsync (this ISnapshots api, GoogleApiClient apiClient, ISnapshotMetadata metadata)
        {
            return (await api.Delete (apiClient, metadata)).JavaCast<ISnapshotsDeleteSnapshotResult> ();
        }
        public static async Task<ISnapshotsLoadSnapshotsResult> LoadAsync (this ISnapshots api, GoogleApiClient apiClient, bool forceReload)
        {
            return (await api.Load (apiClient, forceReload)).JavaCast<ISnapshotsLoadSnapshotsResult> ();
        }
        public static async Task<ISnapshotsOpenSnapshotResult> OpenAsync (this ISnapshots api, GoogleApiClient apiClient, ISnapshotMetadata metadata)
        {
            return (await api.Open (apiClient, metadata)).JavaCast<ISnapshotsOpenSnapshotResult> ();
        }
        public static async Task<ISnapshotsOpenSnapshotResult> OpenAsync (this ISnapshots api, GoogleApiClient apiClient, ISnapshotMetadata metadata, int conflictPolicy)
        {
            return (await api.Open (apiClient, metadata, conflictPolicy)).JavaCast<ISnapshotsOpenSnapshotResult> ();
        }
        public static async Task<ISnapshotsOpenSnapshotResult> OpenAsync (this ISnapshots api, GoogleApiClient apiClient, string fileName, bool createIfNotFound)
        {
            return (await api.Open (apiClient, fileName, createIfNotFound)).JavaCast<ISnapshotsOpenSnapshotResult> ();
        }
        public static async Task<ISnapshotsOpenSnapshotResult> OpenAsync (this ISnapshots api, GoogleApiClient apiClient, string fileName, bool createIfNotFound, int conflictPolicy)
        {
            return (await api.Open (apiClient, fileName, createIfNotFound, conflictPolicy)).JavaCast<ISnapshotsOpenSnapshotResult> ();
        }
        public static async Task<ISnapshotsOpenSnapshotResult> ResolveConflictAsync (this ISnapshots api, GoogleApiClient apiClient, string conflictId, ISnapshot snapshot)
        {
            return (await api.ResolveConflict (apiClient, conflictId, snapshot)).JavaCast<ISnapshotsOpenSnapshotResult> ();
        }
        public static async Task<ISnapshotsOpenSnapshotResult> ResolveConflictAsync (this ISnapshots api, GoogleApiClient apiClient, string conflictId, string snapshotId, SnapshotMetadataChange metadataChange, ISnapshotContents snapshotContents)
        {
            return (await api.ResolveConflict (apiClient, conflictId, snapshotId, metadataChange, snapshotContents)).JavaCast<ISnapshotsOpenSnapshotResult> ();
        }
    }
}

namespace Android.Gms.Games.Stats
{
    public static partial class IStatsExtensions
    {
        public static async Task<IStatsLoadPlayerStatsResult> LoadPlayerStatsAsync (this IStats api, GoogleApiClient apiClient, bool forceReload)
        {
            return (await api.LoadPlayerStats (apiClient, forceReload)).JavaCast<Android.Gms.Games.Stats.IStatsLoadPlayerStatsResult> ();
        }
    }
}