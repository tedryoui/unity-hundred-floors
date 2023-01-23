using Code.Scripts.Core;
using Code.Scripts.Core.Player;
using Code.Scripts.Units;
using UnityEngine;

namespace Code.Scripts.Bakers
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