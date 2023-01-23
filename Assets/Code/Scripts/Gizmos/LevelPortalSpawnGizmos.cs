using System;
using UnityEngine;

namespace Code.Scripts.Gizmos
{
    public class LevelPortalSpawnGizmos : MonoBehaviour
    {
        [SerializeField] private float _spawnRadius;
        [SerializeField] private Mesh _portalWireMesh;
        [SerializeField] private Vector3 _portalMeshScaleFactor;
        [SerializeField] private Vector3 _portalDisplayPosition;
        
        private void OnDrawGizmosSelected()
        {
            UnityEngine.Gizmos.color = Color.blue;
            UnityEngine.Gizmos.DrawWireMesh(_portalWireMesh, 0, _portalDisplayPosition, Quaternion.identity, _portalMeshScaleFactor);
            
            UnityEngine.Gizmos.color = Color.green;
            UnityEngine.Gizmos.DrawWireSphere(transform.position, _spawnRadius);
        }
    }
}