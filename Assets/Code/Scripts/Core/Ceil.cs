using System;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Scripts.Core
{
    public class Ceil : MonoBehaviour
    {
        [SerializeField] private LayerMask _playerMask;

        [SerializeField] private Vector3 _playerOverlapPosition;
        [SerializeField] private float _playerOverlapRadius;

        private bool isReleasable = false;
        private void Update()
        {
            if(isReleasable)
                CheckForPlayer();
        }

        private void CheckForPlayer()
        {
            Collider[] playerCollider = new Collider[1];
            int amount = Physics.OverlapSphereNonAlloc(_playerOverlapPosition, _playerOverlapRadius, playerCollider,
                _playerMask);

            if (amount == 1)
            {
                ReleaseUnits();
            }
        }

        private void ReleaseUnits()
        {
            Debug.Log("Releasing!");
        }

        public void TriggerReleasability()
        {
            isReleasable = true;
        }
    }
}