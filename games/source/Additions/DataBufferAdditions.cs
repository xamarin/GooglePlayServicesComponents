using System;
using System.Linq;
using System.Collections.Generic;

namespace Android.Gms.Games
{
    public partial class GameBuffer : IEnumerable<IGame>
    {
        public IEnumerator<IGame> GetEnumerator()
        {
            return this.ToEnumerable<IGame> ().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public partial class PlayerBuffer : IEnumerable<IPlayer>
    {
        public IEnumerator<IPlayer> GetEnumerator()
        {
            return this.ToEnumerable<IPlayer> ().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
    
namespace Android.Gms.Games.Achievement
{
    public partial class AchievementBuffer : IEnumerable<IAchievement>
    {
        public IEnumerator<IAchievement> GetEnumerator()
        {
            return this.ToEnumerable<IAchievement> ().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

namespace Android.Gms.Games.AppContent
{
    public partial class AppContentSectionBuffer : IEnumerable<IAppContentSection>
    {
        public IEnumerator<IAppContentSection> GetEnumerator()
        {
            return this.ToEnumerable<IAppContentSection> ().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

namespace Android.Gms.Games.Event
{
    public partial class EventBuffer : IEnumerable<IEvent>
    {
        public IEnumerator<IEvent> GetEnumerator()
        {
            return this.ToEnumerable<IEvent> ().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

namespace Android.Gms.Games.LeaderBoard
{
    public partial class LeaderboardBuffer : IEnumerable<ILeaderboard>
    {
        public IEnumerator<ILeaderboard> GetEnumerator()
        {
            return this.ToEnumerable<ILeaderboard> ().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public partial class LeaderboardScoreBuffer : IEnumerable<ILeaderboardScore>
    {
        public IEnumerator<ILeaderboardScore> GetEnumerator()
        {
            return this.ToEnumerable<ILeaderboardScore> ().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

namespace Android.Gms.Games.MultiPlayer
{
    public partial class ParticipantBuffer : IEnumerable<IParticipant>
    {
        public IEnumerator<IParticipant> GetEnumerator()
        {
            return this.ToEnumerable<IParticipant> ().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public partial class InvitationBuffer : IEnumerable<IInvitation>
    {
        public IEnumerator<IInvitation> GetEnumerator()
        {
            return this.ToEnumerable<IInvitation> ().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

namespace Android.Gms.Games.MultiPlayer.RealTime
{
    public partial class RoomBuffer : IEnumerable<IRoom>
    {
        public IEnumerator<IRoom> GetEnumerator()
        {
            return this.ToEnumerable<IRoom> ().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

namespace Android.Gms.Games.MultiPlayer.TurnBased
{
    public partial class TurnBasedMatchBuffer : IEnumerable<ITurnBasedMatch>
    {
        public IEnumerator<ITurnBasedMatch> GetEnumerator()
        {
            return this.ToEnumerable<ITurnBasedMatch> ().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

namespace Android.Gms.Games.Request
{
    public partial class GameRequestSummaryBuffer : IEnumerable<IGameRequestSummary>
    {
        public IEnumerator<IGameRequestSummary> GetEnumerator()
        {
            return this.ToEnumerable<IGameRequestSummary> ().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public partial class GameRequestBuffer : IEnumerable<IGameRequest>
    {
        public IEnumerator<IGameRequest> GetEnumerator()
        {
            return this.ToEnumerable<IGameRequest> ().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

namespace Android.Gms.Games.Snapshot
{
    public partial class SnapshotMetadataBuffer : IEnumerable<ISnapshotMetadata>
    {
        public IEnumerator<ISnapshotMetadata> GetEnumerator()
        {
            return this.ToEnumerable<ISnapshotMetadata> ().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

namespace Android.Gms.Games.Stats
{
    public partial class PlayerStatsBuffer : IEnumerable<IPlayerStats>
    {
        public IEnumerator<IPlayerStats> GetEnumerator()
        {
            return this.ToEnumerable<IPlayerStats> ().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

namespace Android.Gms.Games.Quest
{
    public partial class MilestoneBuffer : IEnumerable<IMilestone>
    {
        public IEnumerator<IMilestone> GetEnumerator()
        {
            return this.ToEnumerable<IMilestone> ().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public partial class QuestBuffer : IEnumerable<IQuest>
    {
        public IEnumerator<IQuest> GetEnumerator()
        {
            return this.ToEnumerable<IQuest> ().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

namespace Android.Gms.Games.Video
{
    public partial class VideoBuffer : IEnumerable<IVideo>
    {
        public IEnumerator<IVideo> GetEnumerator()
        {
            return this.ToEnumerable<IVideo> ().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
