using Code.Scripts.Core;
using Code.Scripts.Services;
using Code.Scripts.Units;
using UnityEngine;

namespace Code.Scripts.Helpers
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
                GroupHelpers.InstantiateToGroup(group, _unitToInstantiate, spotPosition);
            
            Destroy(gameObject);
        }
    }
}