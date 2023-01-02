using System;
using UnityEngine;

namespace Code.Scripts.Units
{
    [Serializable]
    public class SceneUnit
    {
        public Transform transform;
        private Vector3 _placement;
        public int level;

        public Vector3 Placement
        {
            get => _placement;
            set
            {
                _placement = value;
                transform.position = _placement;
            }
        }

        public void Update()
        {
        }
    }
}