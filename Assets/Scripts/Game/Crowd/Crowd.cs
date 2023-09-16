using CountMasters.Core;
using CountMasters.Game.Level;
using CountMasters.Input;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CountMasters.Game.Crowd
{
    public class Crowd : MonoBehaviour, IInitable
    {
        [SerializeField] private CrowdType _crowdType;
        [SerializeField] private Transform _mobsContainer;
        [SerializeField] private Transform _spawnTransform;
        [SerializeField] private Image _cloudImage;
        [SerializeField] private Image _arrowImage;
        [SerializeField] private TextMeshProUGUI _countLabel;
        [SerializeField] private int _initialMobs;

        public int MobsCount => _mobController.MobsCount;
        public int InitialMobs => _initialMobs;
        
        private CrowdMoveController _crowdMoveController;
        private CrowdMobController _mobController;
        private CrowdUIController _uiController;
        
        public void Init(params object[] args)
        {
            _crowdMoveController = new CrowdMoveController(transform);
            _mobController = new CrowdMobController(this);
            _uiController = new CrowdUIController(_cloudImage, _arrowImage, _countLabel);
            _mobController.SetType(_crowdType);
            _uiController.SetType(_crowdType);
            _crowdMoveController.Init();
            _mobController.Init();
            _uiController.Init();
            SubscribeToPlayerEvents();
        }

        private void Update()
        {
            if (!_crowdMoveController.IsMoving) return;
            _crowdMoveController.Move();
            _mobController.CheckDistance(transform.position);
        }

        public void AddMob(Mob.Mob mob) => _mobController.AddMob(mob);

        public void AddMob(Mob.Mob[] mobs) => _mobController.AddMob(mobs);

        public void RemoveMob(Mob.Mob mob) => _mobController.RemoveMob(mob);

        public void KillMob() => _mobController.KillMob();

        public void OnMobsCountChanged()
        {
            _uiController.UpdateCountText(MobsCount);
        }

        public bool IsContainsMob(Mob.Mob mob) => _mobController.IsContainsMob(mob);

        public Transform GetMobsContainer() => _mobsContainer;

        public Vector3 GetSpawnPoint() => _spawnTransform.position;

        public void SetPosition(Vector3 pos) => transform.position = pos;

        public void Kill()
        {
            gameObject.SetActive(false);
            LevelEvents.CrowdKilled?.Invoke(_crowdType);
        }

        private void OnApplicationQuit()
        {
            UnsubscribeFromPlayerEvents();
        }

        private void SubscribeToPlayerEvents()
        {
            if (_crowdType != CrowdType.PlayerCrowd) return;
            InputEvents.CursorMoved += OnCursorMoved;
            GameStateEvents.GameStateChanged += OnGameStateChanged;
        }

        private void UnsubscribeFromPlayerEvents()
        {
            if (_crowdType != CrowdType.PlayerCrowd) return;
            InputEvents.CursorMoved -= OnCursorMoved;
            GameStateEvents.GameStateChanged -= OnGameStateChanged;
        }

        public void OnMobFall(Mob.Mob mob)
        {
            _mobController.RemoveMob(mob);
            _mobController.OnMobFall(mob);
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