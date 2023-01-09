using System;
using System.Collections.Generic;
using Code.Scripts.Core;
using Code.Scripts.Units;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Scripts.Services
{
    [Serializable]
    public class ReleaseService
    {
        private Cell _cell;
        
        [SerializeField] private LayerMask _playerMask;
        [SerializeField] private float _overlapRaius;

        public bool isReleasable = false;

        public void Initialize(Cell cell)
        {
            _cell = cell;
        }
        
        public bool Release(ref Player player, ref List<GameObject> cachedObjects, ref List<Unit> units)
        {
            bool isPlayerTriggered = IsPlayerTriggering();

            if (isReleasable && isPlayerTriggered)
            {
                TransferUnits(ref player, ref cachedObjects, ref units);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void TransferUnits(ref Player player, ref List<GameObject> cachedObjects, ref List<Unit> units)
        {
            for (var i = 0; i < cachedObjects.Count; i++)
            {
                var unitLevel = units[i];
                var unitObject = cachedObjects[i];
                
                bool result = player.Group.GroupService.Add(unitObject.transform, unitLevel);

                if (!result)
                    Object.Destroy(unitObject);
            }
        }

        private bool IsPlayerTriggering()
        {
            Collider[] playerCollider = new Collider[1];
            int amount = Physics.OverlapSphereNonAlloc(_cell.transform.position, _overlapRaius, playerCollider,
                _playerMask);

            return amount == 1;
        }
    }
}