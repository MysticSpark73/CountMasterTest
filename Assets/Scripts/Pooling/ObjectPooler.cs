using System.Collections.Generic;
using System.Linq;
using CountMasters.Core;
using UnityEngine;

namespace CountMasters.Pooling
{
    public class ObjectPooler : MonoBehaviour, IInitable
    {
        [SerializeField] private List<PoolableObject> _objectsToPool;
        
        private Dictionary<ObjectPool, Queue<IPoollable>> _objectPools;

        public void Init()
        {
            GeneratePools();
        }

        public IPoollable SpawnFromPool(ObjectPool key, Vector3 position, Transform container = null)
        {
            if (!_objectPools.ContainsKey(key))
            {
                Debug.LogError($"Dictionary does not contain key '{key}!'");
                return null;
            }
            var obj = _objectPools[key].Dequeue();
            obj.SetPosition(position, container);
            obj.OnSpawnedFromPooled();
            return obj;
        }

        public void ReturnToPool(ObjectPool key, IPoollable obj)
        {
            obj.SetActive(false);
            obj.OnReturnToPool();
            if (!_objectPools.ContainsKey(key))
            {
                Debug.LogError($"Dictionary does not contain key '{key}!'");
                return;
            }
            _objectPools[key].Enqueue(obj);
            var containerTransform = _objectsToPool.FirstOrDefault(o => o.key == obj.pooledKey).container;
            if (containerTransform == null) return;
            obj.SetPosition(containerTransform.position, containerTransform);
        }

        private void GeneratePools()
        {
            _objectPools = new Dictionary<ObjectPool, Queue<IPoollable>>();
            foreach (var obj in _objectsToPool)
            {
                _objectPools.AddSafe(obj.key, new Queue<IPoollable>());
                for (int i = 0; i < obj.poolSize; i++)
                {
                    var pooledObject = Instantiate(obj.prefab, obj.container);
                    var poolable = pooledObject.GetComponent<IPoollable>();
                    pooledObject.SetActive(false);
                    pooledObject.gameObject.name = $"{obj.key} {i}";
                    if (poolable == null)
                    {
                        Debug.LogError($"[ObjectPooler][GeneratePools] object with key {obj.key} doesn't contain IPoollable component! Moving to the next pool.");
                        break;
                    }
                    poolable.pooledKey = obj.key;
                    poolable.Init();
                    _objectPools[obj.key].Enqueue(poolable);
                }
            }
        }
        
        [System.Serializable]
        private struct PoolableObject
        {
            public ObjectPool key;
            public GameObject prefab;
            public Transform container;
            public int poolSize;
        }

    }
}