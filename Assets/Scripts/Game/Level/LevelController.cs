using CountMasters.Game.Crowd.Mob;
using CountMasters.Pooling;
using UnityEngine;

namespace CountMasters.Game.Level
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private ObjectPooler _pooler;
        [SerializeField] private Crowd.Crowd _crowd;

        private void Awake()
        {
            _pooler.Init();
            _crowd.Init();
            for (int i = 0; i < 50; i++)
            {
                var mob = _pooler.SpawnFromPool(ObjectPool.Mob, Vector3.zero, _crowd.GetMobsContainer()) as Mob;
                if (mob != null)
                {
                    _crowd.AddMob(mob);
                }
            }
            //GameStateManager.SetGameState(GameState.Playing);
        }
    }
}