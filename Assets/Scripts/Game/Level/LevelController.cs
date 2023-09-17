using System;
using CountMasters.Data.Saving;
using CountMasters.Game.Crowd;
using CountMasters.Game.Crowd.Mob;
using CountMasters.Game.Level.Obstacles;
using CountMasters.Pooling;
using CountMasters.UI.Dialogs;
using UnityEngine;

namespace CountMasters.Game.Level
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private DialogsManager _dialogsManager;
        [SerializeField] private ObjectPooler _pooler;
        [SerializeField] private Crowd.Crowd _crowd;
        [SerializeField] private Transform _levelContainer;
        [SerializeField] private UnityEngine.Camera _camera;

        private LevelLoadManager _loadManager;
        private SavesManager _savesManager;
        private Level _currentLevel;
        
        public Mob PoolMob()
        {
            return _pooler.SpawnFromPool(ObjectPool.Mob, _crowd.GetSpawnPoint(), _crowd.GetMobsContainer()) as Mob;
        }

        private async void Awake()
        {
            GameStateManager.SetGameState(GameState.Loading);
            _loadManager = new LevelLoadManager();
            _savesManager = new SavesManager();
            
            _pooler.Init();
            _crowd.Init();
            _loadManager.Init();
            _savesManager.ReadCoins();
            
            LevelEvents.GateTrigger += OnGateTriggered;
            LevelEvents.PitTrigger += OnPitTriggered;
            LevelEvents.MobDied += OnMobDied;
            LevelEvents.LevelFinish += OnLevelFinish;
            LevelEvents.CrowdKilled += OnCrowdKilled;
            LevelEvents.NextLevelRequested += OnNextLevelRequested;
            LevelEvents.RestartRequested += OnRestartRequested;

            _currentLevel = await _loadManager.LoadCurrentLevel(_levelContainer);
            _currentLevel.Init(_pooler);
            var levelColor = Parameters.GetRandomLevelColor();
            _currentLevel.SetLevelColor(levelColor);
            _camera.backgroundColor = levelColor;
            _crowd.SetPosition(_currentLevel.GetCrowdSpawnPoint());
            FillCrowd();
            
            _dialogsManager.ShowDialog<LevelUI>();
        }

        private void FillCrowd()
        {
            Mob[] mobs = new Mob[_crowd.InitialMobs];
            for (int i = 0; i < _crowd.InitialMobs; i++)
            {
                var mob = PoolMob();
                if (mob != null)
                {
                    mobs[i] = mob;
                }
            }
            _crowd.AddMob(mobs);
        }

        private void OnApplicationQuit()
        {
            LevelEvents.GateTrigger -= OnGateTriggered;
            LevelEvents.PitTrigger -= OnPitTriggered;
            LevelEvents.MobDied -= OnMobDied;
            LevelEvents.LevelFinish -= OnLevelFinish;
            LevelEvents.CrowdKilled -= OnCrowdKilled;
            LevelEvents.NextLevelRequested -= OnNextLevelRequested;
            LevelEvents.RestartRequested -= OnRestartRequested;
            _savesManager.KillSave();
        }

        #region Operations
        
        private void ApplyMultiplication(int value)
        {
            int mobsInCrowd = _crowd.MobsCount;
            int result = mobsInCrowd * value;
            Mob mob;
            Mob[] mobsToAdd = new Mob[result - mobsInCrowd];
            for (int i = 0; i < result - mobsInCrowd; i++)
            {
                mob = PoolMob();
                if (mob == null)
                {
                    Debug.LogError($"[LevelController][ApplyMultiplication] Pooler returned a null! Seems like queue is empty!");
                    return;
                }
                mobsToAdd[i] = mob;
            }
            _crowd.AddMob(mobsToAdd);
        }
        
        private void ApplyDivision(int value)
        {
            //prevent world from collapsing
            if (value == 0) return;
            int mobsInCrowd = _crowd.MobsCount;
            int result = Mathf.RoundToInt(mobsInCrowd / value);
            for (int i = 0; i < mobsInCrowd - result; i++)
            {
                _crowd.KillMob();
            }
        }
        
        private void ApplyAddition(int value)
        {
            Mob[] mobs = new Mob[value];
            Mob mob;
            for (int i = 0; i < value; i++)
            {
                mob = PoolMob();
                if (mob == null)
                {
                    Debug.LogError($"[LevelController][ApplyAddition] Pooler returned a null! Seems like queue is empty!");
                    return;
                }
                mobs[i] = mob;
            }
            _crowd.AddMob(mobs);
        }
        
        private void ApplySubtraction(int value)
        {
            for (int i = 0; i < value; i++)
            {
                if (_crowd.MobsCount == 0) return;
                _crowd.KillMob();
            }
        }
        
        #endregion

        private void OnGateTriggered(GateOperation operation, int value)
        {
            Action<int> action = operation switch
            {
                GateOperation.Multiplication => ApplyMultiplication,
                GateOperation.Division => ApplyDivision,
                GateOperation.Addition => ApplyAddition,
                GateOperation.Subtraction => ApplySubtraction,
                _ => (_) => {}
            };
            action.Invoke(value);
        }

        private void OnPitTriggered(Mob mob) => _crowd.OnMobFall(mob);

        private void OnMobDied(Mob mob)
        {
            if (_crowd.IsContainsMob(mob))
            {
                _crowd.RemoveMob(mob);
            }

            _currentLevel.TryRemoveMob(mob);
            _pooler.ReturnToPool(mob.pooledKey, mob);
        }

        private void OnLevelFinish()
        {
            GameStateManager.SetGameState(GameState.Win);
            Parameters.AddCoins(100);
            _savesManager.SaveCoins();
            _dialogsManager.ShowDialog<WinDialog>();
        }

        private void OnCrowdKilled(CrowdType crowdType)
        {
            if (crowdType == CrowdType.PlayerCrowd)
            {
                GameStateManager.SetGameState(GameState.Lose);
                _dialogsManager.ShowDialog<LoseDialog>();
            }
        }

        private async void OnNextLevelRequested()
        {
            _currentLevel.Destroy();
            _currentLevel = await _loadManager.LoadNextLevel(_levelContainer);
            _currentLevel.Init(_pooler);
            _currentLevel.SetLevelColor(Parameters.GetRandomLevelColor());
            _crowd.SetPosition(_currentLevel.GetCrowdSpawnPoint());
            _crowd.Kill(true);
            FillCrowd();
            _crowd.Reset();
            var levelColor = Parameters.GetRandomLevelColor();
            _currentLevel.SetLevelColor(levelColor);
            _camera.backgroundColor = levelColor;
            var dialog = _dialogsManager.GetDialog<LevelUI>();
            if (dialog == null) return;
            dialog.ShowTapToContinue();
        }

        private void OnRestartRequested()
        {
            _currentLevel.Reset();
            _crowd.SetPosition(_currentLevel.GetCrowdSpawnPoint());
            _crowd.Kill(true);
            FillCrowd();
            _crowd.Reset();
            var levelColor = Parameters.GetRandomLevelColor();
            _currentLevel.SetLevelColor(levelColor);
            _camera.backgroundColor = levelColor;
            var dialog = _dialogsManager.GetDialog<LevelUI>();
            if (dialog == null) return;
            dialog.ShowTapToContinue();
        }
    }
}