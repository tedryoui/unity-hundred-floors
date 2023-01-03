using UnityEngine;

namespace Code.Scripts.Gizmos
{
    public class CeilGizmos : MonoBehaviour
    {
        public bool isVisible;
        [SerializeField] private Vector3 _playerOverlapPosition;
        [SerializeField] private float _playerOverlapRadius;

        private void OnDrawGizmosSelected()
        {
            if (!isVisible) return;

            UnityEngine.Gizmos.color = Color.green;
            UnityEngine.Gizmos.DrawWireSphere(_playerOverlapPosition, _playerOverlapRadius);
        }
    }
}