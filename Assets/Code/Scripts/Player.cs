using System;
using UnityEngine;

namespace Code.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private UnitGroup _group;

        public GameObject unitRef;
        
        private void Awake()
        {
            _group.Add(unitRef.transform, 1);
        }
    }
}