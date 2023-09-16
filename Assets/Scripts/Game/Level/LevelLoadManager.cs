using System.Threading.Tasks;
using CountMasters.Core;
using CountMasters.Data.Levels;
using UnityEngine;

namespace CountMasters.Game.Level
{
    public class LevelLoadManager : IInitable
    {
        private LevelsData _levelsData;
        private Level _currentLevel;
        private int _currentLevelIndex = 0;

        public async Task<Level> LoadLevel(int index, Transform container)
        {
            if(_levelsData == null) await CacheLevelsData();
            var data = _levelsData.levels[index];
            var obj = Object.Instantiate(data.prefab, container.position, Quaternion.identity, container);
            var level = obj.GetComponent<Level>();
            if (level == null) return null;
            obj.transform.position = level.GetLevelSpawnPoint();
            _currentLevelIndex = index;
            return level;
        }

        public async Task<Level> LoadNextLevel(Transform container)
        {
            if (_levelsData == null) CacheLevelsData();
            _currentLevelIndex = _currentLevelIndex + 1 > _levelsData.levels.Count - 1 ? 0 : _currentLevelIndex + 1;
            return await LoadLevel(_currentLevelIndex, container);
        }

        public async Task<Level> LoadCurrentLevel(Transform container)
        {
            return await LoadLevel(_currentLevelIndex, container);
        }

        public async void Init(params object[] args)
        {
            await CacheLevelsData();
        }

        private async Task CacheLevelsData()
        {
            var request = Resources.LoadAsync<LevelsData>(Parameters.path_resources_levels_data);
            await request;
            _levelsData = request.asset as LevelsData;
        }
    }
}