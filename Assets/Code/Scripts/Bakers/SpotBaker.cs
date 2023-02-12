using Code.Scripts.Core;
using Code.Scripts.Units;
using UnityEngine;

namespace Code.Scripts.Bakers
{
    public class SpotBaker : MonoBehaviour
    {
        [SerializeField] private Spot _spot;

        [SerializeField] private Unit _unitToInstantiate;
        [SerializeField] private int _amount;
        private void Start()
        {
            var group = _spot.Group;
            var spotPosition = _spot.transform.position;
            
            for (int i = 0; i < _amount; i++)
            {
                string unitName = _unitToInstantiate.prefab.name;
                GameObject obj = GameCore.GetLevel.GetPool.GetEntity(unitName);

                if (obj != null)
                {
                    group.TryAdd(obj.transform, _unitToInstantiate);
                }
            }
            
            Destroy(gameObject);
        }
    }
}