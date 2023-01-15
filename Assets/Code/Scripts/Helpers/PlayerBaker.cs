using Code.Scripts.Core;
using Code.Scripts.Units;
using UnityEngine;

namespace Code.Scripts.Helpers
{
    public class PlayerBaker : MonoBehaviour
    {
        [SerializeField] private Player _player;

        [SerializeField] private Unit _unitToInstantiate;
        [SerializeField] private GameObject _unitCell;
        [SerializeField] private int _amount;
        private void Start()
        {
            var group = _player.Group;
            var spotPosition = _player.transform.position;
            
            for (int i = 0; i < _amount; i++)
            {
                GameObject cell = Instantiate(_unitCell, Vector3.zero, Quaternion.identity, group._groupTransform);
                group.GroupService.Add(cell.transform, _unitToInstantiate);
                
                // GroupHelpers.InstantiateToGroup(group, _unitToInstantiate, spotPosition);
            }
            
            Destroy(gameObject);
        }
    }
}