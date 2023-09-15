using CountMasters.Core;
using CountMasters.Input;
using UnityEngine;

namespace CountMasters.Game.Crowd
{
    public class Crowd : MonoBehaviour, IInitable
    {
        [SerializeField] private CrowdType _crowdType;
        [SerializeField] private Transform _mobsContainer;
        
        private CrowdMoveController _crowdMoveController;
        private CrowdMobController _mobController;

        public void Init()
        {
            _crowdMoveController = new CrowdMoveController(transform);
            _mobController = new CrowdMobController(transform);
            _mobController.SetType(_crowdType);
            _crowdMoveController.Init();
            _mobController.Init();
            if (_crowdType == CrowdType.Player) InputEvents.CursorMoved += OnCursorMoved;
        }

        public void AddMob(Mob.Mob mob) => _mobController.AddMob(mob);

        public Transform GetMobsContainer() => _mobsContainer;

        private void OnApplicationQuit()
        {
            if (_crowdType == CrowdType.Player) InputEvents.CursorMoved -= OnCursorMoved;
        }

        private void OnCursorMoved(bool isTouchDown, Vector2 cursorPos)
        {
            _crowdMoveController.CursorMoved(isTouchDown, cursorPos);
        }
    }
}