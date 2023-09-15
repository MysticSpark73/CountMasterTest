using CountMasters.Game.Crowd.Mob;
using UnityEngine;

namespace CountMasters.Game.Level.Obstacles
{
    [RequireComponent(typeof(Collider))]
    public class Pit : Obstacle
    {
        private Collider _collider;
        public override void Init()
        {
            _collider = GetComponent<Collider>();
        }
        
        public override void Reset()
        {
            _collider.enabled = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            var mob = other.GetComponent<Mob>();
            if (mob == null) return;
            LevelEvents.PitTrigger?.Invoke(mob);
        }
    }
}