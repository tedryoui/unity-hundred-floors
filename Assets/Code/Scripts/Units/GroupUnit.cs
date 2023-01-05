using System;
using UnityEngine;

namespace Code.Scripts.Units
{
    [Serializable]
    public class GroupUnit
    {
        public Transform objectTransform;
        private Vector3 _targetPosition;
        public int level;
        public float speed;

        public Vector3 TargetPosition
        {
            get => _targetPosition;
            set => _targetPosition = value;
        }

        public void Update()
        {
            SmoothChase();
        }

        private void SmoothChase()
        {
            var position = objectTransform.position;
            var additionalSpeed = Vector3.Distance(position, TargetPosition);
            var smoothSpeed = additionalSpeed + speed;
            
            var movePosition = Vector3.MoveTowards(position, TargetPosition, smoothSpeed * Time.deltaTime);
            objectTransform.position = movePosition;
        }
    }
}