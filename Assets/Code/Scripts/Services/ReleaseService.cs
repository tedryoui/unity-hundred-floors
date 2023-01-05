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
        [SerializeField] private LayerMask _playerMask;

        [SerializeField] private Vector3 _playerOverlapPosition;
        [SerializeField] private float _playerOverlapRadius;

        [HideInInspector] public bool isReleasable = false;

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
                var unitLevel = units[i].level;
                var unitObject = cachedObjects[i];
                
                bool result = player.Group.Add(unitObject.transform, unitLevel);

                if (!result)
                    Object.Destroy(unitObject);
            }
        }

        private bool IsPlayerTriggering()
        {
            Collider[] playerCollider = new Collider[1];
            int amount = Physics.OverlapSphereNonAlloc(_playerOverlapPosition, _playerOverlapRadius, playerCollider,
                _playerMask);

            return amount == 1;
        }
    }
}