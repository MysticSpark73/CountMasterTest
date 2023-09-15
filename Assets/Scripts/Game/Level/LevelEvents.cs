using System;
using CountMasters.Game.Crowd.Mob;
using CountMasters.Game.Level.Obstacles;

namespace CountMasters.Game.Level
{
    public static class LevelEvents
    {
        public static Action<GateOperation, int> GateTrigger;
        public static Action<Mob> PitTrigger;
        public static Action<Mob> MobDied;
    }
}