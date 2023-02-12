using System;
using UnityEngine;

namespace Code.Scripts.Gizmos
{
    public class PortalGizmos : MonoBehaviour
    {
        [SerializeField] private float _playerTriggerRadius = 15;
        
        private void OnDrawGizmosSelected()
        {
            UnityEngine.Gizmos.color = Color.white;
            UnityEngine.Gizmos.DrawWireSphere(transform.position, _playerTriggerRadius);
        }
    }
}