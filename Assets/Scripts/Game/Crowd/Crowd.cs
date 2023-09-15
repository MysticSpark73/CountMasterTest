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
            SubscribeToPlayerEvents();
        }

        private void Update()
        {
            if (!_crowdMoveController.IsMoving) return;
            _crowdMoveController.Move();
        }

        public void AddMob(Mob.Mob mob) => _mobController.AddMob(mob);

        public Transform GetMobsContainer() => _mobsContainer;

        private void OnApplicationQuit()
        {
            UnsubscribeFromPlayerEvents();
        }

        private void SubscribeToPlayerEvents()
        {
            if (_crowdType != CrowdType.Player) return;
            InputEvents.CursorMoved += OnCursorMoved;
            GameStateEvents.GameStateChanged += OnGameStateChanged;
        }

        private void UnsubscribeFromPlayerEvents()
        {
            if (_crowdType != CrowdType.Player) return;
            InputEvents.CursorMoved -= OnCursorMoved;
            GameStateEvents.GameStateChanged -= OnGameStateChanged;
        }

        private void OnCursorMoved(bool isTouchDown, Vector2 cursorPos)
        {
            _crowdMoveController.CursorMoved(isTouchDown, cursorPos);
        }

        private void OnGameStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.Playing:
                    _crowdMoveController.SetMoving(true);
                    break;
                default:
                    _crowdMoveController.SetMoving(false);
                    break;
            }
        }
    }
}