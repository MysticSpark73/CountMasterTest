using System.Collections.Generic;
using CountMasters.Core;
using DG.Tweening;
using UnityEngine;

namespace CountMasters.Game.Crowd
{
    public class CrowdMobController : IInitable
    {
        public int MobsCount => _mobs.Count;
        
        private List<Mob.Mob> _mobs;
        private Transform _selfTransform;
        private CrowdType _crowdType;
        private Crowd _crowd;

        private readonly float _distance = .15f;
        private readonly float _radius = .5f;


        public CrowdMobController(Crowd crowd)
        {
            _crowd = crowd;
        }
        
        public void Init(params object[] args)
        {
            _mobs = new List<Mob.Mob>();
        }

        public void SetType(CrowdType crowdType) => _crowdType = crowdType;

        public void AddMob(Mob.Mob mob)
        {
            if (_mobs == null) return;
            mob.SetCrowdType(_crowdType);
            mob.SetActive(true);
            _mobs.Add(mob);
            FormatMobs();
        }

        public void AddMob(Mob.Mob[] mobs)
        {
            if (mobs == null || mobs.Length == 0) return;
            for (int i = 0; i < mobs.Length; i++)
            {
                mobs[i].SetCrowdType(_crowdType);
                mobs[i].SetActive(true);
                mobs[i].SetIsRun();
                _mobs.Add(mobs[i]);
            }
            FormatMobs();
        }

        public void RemoveMob(Mob.Mob mob)
        {
            if (_mobs == null) return;
            _mobs.Remove(mob);
        }
        
        public void KillMob()
        {
            if (_mobs == null || _mobs.Count == 0) return;
            var mob = _mobs[^1];
            RemoveMob(mob);
            mob.Kill();
        }

        public void CheckDistance(Vector3 target)
        {
            var mobsOutOfBounds = _mobs.FindAll(m => m.IsBeyondMaxDistance(target));
            foreach (var mob in mobsOutOfBounds)
            {
                RemoveMob(mob);
                mob.Kill();
            }
        }

        public void OnMobFall(Mob.Mob mob) => mob.Fall();

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
    }
}