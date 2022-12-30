using System;
using UnityEngine;

namespace Code.Scripts.Gizmos
{
    public class UnitGroupGizmos : MonoBehaviour
    {
        public Transform playerPosition;
        public int _baseOrbitUnitAmount;

        private void OnDrawGizmosSelected()
        {
            var basePosition = playerPosition.position;
            var orbitUnitsAmount = _baseOrbitUnitAmount;
            var angleIncrement = 720.0f / orbitUnitsAmount;

            for (int i = 0; i < orbitUnitsAmount; i++)
            {
                var angle = (angleIncrement * Mathf.PI / 360.0f) * i;

                var x = Mathf.Cos(angle);
                var z = Mathf.Sin(angle);

                var vector = basePosition + new Vector3(x, 0, z) * 5.0f;
                
                UnityEngine.Gizmos.color = Color.blue;
                UnityEngine.Gizmos.DrawLine(basePosition, vector);
            }
        }
    }
}