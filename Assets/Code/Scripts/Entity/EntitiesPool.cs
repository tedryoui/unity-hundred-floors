using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Scripts.Core
{
    [Serializable]
    public class EntitiesPool
    {
        [Serializable]
        public struct EntityPool
        {
            public readonly string Name => prefab.name;
            public GameObject prefab;
            public int size;
        }

        private Transform _poolTransform;
        [SerializeField] private List<EntityPool> _poolsDescriptions;
        private Dictionary<string, Queue<GameObject>> _pools;

        public void Initialize()
        {
            _pools = new Dictionary<string, Queue<GameObject>>();

            GameObject pool = new GameObject("Entity Pool");
            pool.transform.parent = GameCore.GetLevel.transform;
            _poolTransform = pool.transform;
            
            FillPool();
        }

        private void FillPool()
        {
            foreach (var description in _poolsDescriptions)
            {
                Queue<GameObject> pool = new Queue<GameObject>();

                for (int i = 0; i < description.size; i++)
                {
                    GameObject obj = Object.Instantiate(description.prefab, Vector3.zero, Quaternion.identity, _poolTransform);
                    obj.SetActive(false);
                    pool.Enqueue(obj);
                }

                _pools.Add(description.Name, pool);
            }
        }

        public GameObject GetEntity(string type)
        {
            var pool = _pools[type];

            if (pool != null)
            {
                // Since GameObject is references type, we gonna get null if
                // there are no such element in the row and some element otherwise
                var obj = pool.FirstOrDefault(x => !x.activeInHierarchy);

                // Enable object to observe Entity status (released or not)
                if (obj != null)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }
            
            return null;
        }

        public void ReleaseEntity(string type, GameObject obj)
        {
            var pool = _pools[type];

            if (pool != null)
            {
                obj.SetActive(false);
                obj.transform.parent = _poolTransform;
                
                pool.Enqueue(obj);
            }
        }
    }
}