using System;
using UnityEngine;

namespace Code.Scripts.Gizmos
{
    public class PlayerBoundsGizmos : MonoBehaviour
    {
        public Transform position;
        public float orbitOffset;
        public int orbitsAmount;
        public float additionalDistance;

        private void OnDrawGizmosSelected()
        {
            UnityEngine.Gizmos.color = Color.magenta;
            
            UnityEngine.Gizmos.DrawWireSphere(transform.position + position.position, orbitOffset * orbitsAmount + additionalDistance);
        }
    }
}