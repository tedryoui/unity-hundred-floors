using System;
using UnityEngine;

namespace Code.Scripts.Gizmos
{
    public class UnitGroupGizmos : MonoBehaviour
    {
        public bool isVisible = false;
        public Transform playerPosition;
        public int _baseOrbitUnitAmount;

        private void OnDrawGizmosSelected()
        {
            if (!isVisible) return;
            
            var basePosition = playerPosition.position;
            var orbitUnitsAmount = _baseOrbitUnitAmount;
            var angleIncrement = 360.0f / orbitUnitsAmount;

            for (int i = 0; i < orbitUnitsAmount; i++)
            {
                var angle = Mathf.Deg2Rad * (angleIncrement * i);

                var x = Mathf.Cos(angle);
                var z = Mathf.Sin(angle);

                var vector = basePosition + new Vector3(x, 0, z) * 5.0f;
                
                UnityEngine.Gizmos.color = Color.blue;
                UnityEngine.Gizmos.DrawLine(basePosition, vector);
            }
        }
    }
}