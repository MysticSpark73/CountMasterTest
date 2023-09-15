using System.Collections.Generic;
using CountMasters.Core;
using UnityEngine;

namespace CountMasters.Game.Crowd
{
    public class CrowdMobController : IInitable
    {
        private List<Mob.Mob> _mobs;
        private Transform _selfTransform;
        private CrowdType _crowdType;

        public CrowdMobController(Transform transform)
        {
            _selfTransform = transform;
        }

        public void SetType(CrowdType crowdType) => _crowdType = crowdType;

        public void AddMob(Mob.Mob mob)
        {
            if (_mobs == null) return;
            mob.SetCrowdType(_crowdType);
            mob.SetActive(true);
        }

        public void RemoveMob(Mob.Mob mob)
        {
            if (_mobs == null) return;
        }

        public void Init()
        {
            _mobs = new List<Mob.Mob>();
        }
    }
}