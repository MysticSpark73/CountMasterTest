using CountMasters.Core;
using UnityEngine;

namespace CountMasters.Pooling
{
    public interface IPoollable : IInitable
    {
        ObjectPool pooledKey { get; set; }
        
        void OnSpawnedFromPooled();

        void OnReturnToPool();

        void SetPosition(Vector3 pos, Transform container = null);

        void SetActive(bool value);

    }
}