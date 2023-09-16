using System.Collections.Generic;
using CountMasters.Core;
using CountMasters.Game.Crowd.Mob;
using CountMasters.Game.Level.Obstacles;
using CountMasters.Pooling;
using UnityEngine;

namespace CountMasters.Game.Level
{
    public class Level : MonoBehaviour, IInitable, IResetable
    {
        [SerializeField] private Transform _levelStartPoint;
        [SerializeField] private Transform _levelEndPoint;
        [SerializeField] private Transform _crowdSpawnPoint;
        [SerializeField] private List<Gate> _gates;
        [SerializeField] private List<Pit> _pits;
        [SerializeField] private List<Crowd.Crowd> _crowds;

        private ObjectPooler _pooler;

        public void Init(params object[] args)
        {
            if (args != null) _pooler = args[0] as ObjectPooler;
            _gates.ForEach(g => g.Init());
            _pits.ForEach(p => p.Init());
            _crowds.ForEach(c => c.Init());
            if (_pooler == null) return;
            foreach (var crowd in _crowds)
            {
                Mob[] mobsToAdd = new Mob[crowd.InitialMobs];
                for (int i = 0; i < crowd.InitialMobs; i++)
                {
                    var mob = _pooler.SpawnFromPool(ObjectPool.Mob, crowd.GetSpawnPoint(),
                        crowd.GetMobsContainer()) as Mob;
                    if (mob != null)
                    {
                        mobsToAdd[i] = mob;
                    }
                }
                crowd.AddMob(mobsToAdd);
            }
        }
        
        public void Reset()
        {
            _gates.ForEach(g => g.Reset());
            _pits.ForEach(p => p.Reset());
        }

        public bool TryRemoveMob(Mob mob)
        {
            foreach (var crowd in _crowds)
            {
                if (crowd.IsContainsMob(mob))
                {
                    crowd.RemoveMob(mob);
                    return true;
                }
            }

            return false;
        }

        public Vector3 GetLevelSpawnPoint() => transform.position;

        public Vector3 GetCrowdSpawnPoint() => _crowdSpawnPoint.position;

        public void SetLevelColor(Color color)
        {
            _gates.ForEach(g => g.SetColor(new Color(color.r, color.g, color.b, .5f)));
        }
    }
}