using System;
using CountMasters.Game.Crowd;
using CountMasters.Game.Crowd.Mob;
using CountMasters.Game.Level.Obstacles;

namespace CountMasters.Game.Level
{
    public static class LevelEvents
    {
        public static Action<GateOperation, int> GateTrigger;
        public static Action<Mob> PitTrigger;
        public static Action<Mob> MobDied;
        public static Action LevelFinish;
        public static Action<CrowdType> CrowdKilled;
        public static Action NextLevelRequested;
        public static Action RestartRequested;
    }
}