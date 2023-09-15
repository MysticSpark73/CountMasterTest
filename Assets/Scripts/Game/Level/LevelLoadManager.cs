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
        
        public Level LoadLevel(int index, Transform container)
        {
            if(_levelsData == null) CacheLevelsData();
            var data = _levelsData.levels[index];
            Object.Instantiate(data.prefab, data.rootScript.GetLevelSpawnPoint(),
                Quaternion.identity, container);
            _currentLevelIndex = index;
            return data.rootScript;
        }

        public Level LoadNextLevel(Transform container)
        {
            if (_levelsData == null) CacheLevelsData();
            _currentLevelIndex = _currentLevelIndex + 1 > _levelsData.levels.Count - 1 ? 0 : _currentLevelIndex + 1;
            return LoadLevel(_currentLevelIndex, container);
        }

        public Level LoadCurrentLevel(Transform container)
        {
            return LoadLevel(_currentLevelIndex, container);
        }

        public void Init()
        {
            CacheLevelsData();
        }

        private void CacheLevelsData()
        {
            _levelsData = Resources.Load<LevelsData>(Parameters.path_resources_levels_data);
        }
    }
}