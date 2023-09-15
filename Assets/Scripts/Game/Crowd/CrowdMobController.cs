using System.Collections.Generic;
using CountMasters.Core;
using DG.Tweening;
using UnityEngine;

namespace CountMasters.Game.Crowd
{
    public class CrowdMobController : IInitable
    {
        private List<Mob.Mob> _mobs;
        private Transform _selfTransform;
        private CrowdType _crowdType;

        private readonly float _distance = .15f;
        private readonly float _radius = .5f;


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
            _mobs.Add(mob);
            FormatMobs();
            
            
            
            /*mob.SetPosition(new Vector3(Mathf.Sin(_mobs.Count) * .05f * _mobs.Count,
                mob.transform.position.y,
                Mathf.Cos(_mobs.Count) * .05f * _mobs.Count));*/
        }

        public void RemoveMob(Mob.Mob mob)
        {
            if (_mobs == null) return;
            _mobs.Remove(mob);
        }

        private void FormatMobs()
        {
            for (int i = 0; i < _mobs.Count; i++)
            {
                Vector3 newPos = new Vector3(
                    _distance * Mathf.Sqrt(i) * Mathf.Cos(i * _radius),
                    _mobs[i].transform.position.y,
                    _distance * Mathf.Sqrt(i) * Mathf.Sin(i * _radius));
                _mobs[i].transform.DOLocalMove(newPos, .5f);
            }
        }

        public void Init()
        {
            _mobs = new List<Mob.Mob>();
        }
    }
}