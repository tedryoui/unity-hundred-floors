using System;
using Code.Scripts.Units;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Code.Scripts.Core
{
    public class Player : MonoBehaviour
    {
        public enum State {Stay, Move, Battle, Release}
        
        [SerializeField] private int _colliderAdditionalRadius;
        [SerializeField] private Group _group;
        private SphereCollider _collider;
        [HideInInspector] public State state;
        public Group Group => _group;

        private void Awake()
        {
            _collider = GetComponent<SphereCollider>();
            
            _group.Initialize(transform);
            _group.OnChange += UpdateCollider;
        }

        private void Update()
        {
            switch (state)
            {
                
            }
            
            TestInput();
            _group.UpdateGroup();
        }

        private void UpdateCollider()
        {
            var orbitOffset = Group.OrbitOffset;
            var orbitsAmount = Group.OrbitsAmount + 1;

            _collider.radius = orbitOffset * orbitsAmount + _colliderAdditionalRadius;
        }

        private void TestInput()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                _group.Merge();
            }
        }
    }
}