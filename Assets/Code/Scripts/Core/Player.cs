using System;
using Cinemachine;
using Code.Scripts.Services;
using Code.Scripts.Units;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Code.Scripts.Core
{
    public class Player : MonoBehaviour
    {
        public enum State {Free, Battle, Dead}
        [HideInInspector] public State state;
        public float speed;
        
        [SerializeField] private Group _group;
        [SerializeField] private BattleService _battleService;
        [SerializeField] private InputService _inputService;
        [SerializeField] private PlayerCameraService _cameraService;

        private SphereCollider _collider;
        
        public Group Group => _group;
        public BattleService BattleService => _battleService;

        private void Awake()
        {
            _collider = GetComponent<SphereCollider>();
            
            _group.Initialize(transform);
            _group.OnChange += UpdateCollider;
            
            _battleService.Initialize(this);
            _inputService.Initialize(this);
            _cameraService.Initialize(this);
        }
        
        private void Update()
        {
            switch (state)
            {
                case State.Dead:
                    return;
                case State.Battle:
                    _group.Update();
                    return;
                case State.Free:
                    {
                        _group.Update();
                        _inputService.Update();
                    }
                    break;
            }
            
            _cameraService.Update();
        }

        private void UpdateCollider()
        {
            var orbitOffset = Group.FormationService.OrbitOffset;
            var orbitsAmount = Group.FormationService.OrbitsAmount + 1;

            _collider.radius = orbitOffset * orbitsAmount;
        }

        public void Kill()
        {
            Destroy(transform.parent.gameObject);
        }
    }
}