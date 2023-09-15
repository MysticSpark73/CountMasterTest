using System.Collections.Generic;
using CountMasters.Core;
using CountMasters.Game.Level.Obstacles;
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

        public void Init()
        {
            _gates.ForEach(g => g.Init());
            _pits.ForEach(p => p.Init());
        }
        
        public void Reset()
        {
            _gates.ForEach(g => g.Reset());
            _pits.ForEach(p => p.Reset());
        }

        public Vector3 GetLevelSpawnPoint() => transform.position;

        public Vector3 GetCrowdSpawnPoint() => _crowdSpawnPoint.position;

        public void SetLevelColor(Color color)
        {
            _gates.ForEach(g => g.SetColor(new Color(color.r, color.g, color.b, .5f)));
        }
    }
}