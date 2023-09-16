using System;
using System.Collections.Generic;
using UnityEngine;

namespace CountMasters.Data.Levels
{
    [CreateAssetMenu(menuName = "LevelsData", fileName = "LevelsData")]
    public class LevelsData : ScriptableObject
    {
        public List<LevelData> levels;
        
        [Serializable]
        public struct LevelData
        {
            public GameObject prefab;
        }
    }
}