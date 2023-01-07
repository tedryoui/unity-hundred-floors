using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Cutscenes;
using Code.Scripts.Services;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Scripts.Core
{
    public class Spot : MonoBehaviour
    {
        public enum State {Wait, Stay, Battle, Move, Dead}
        
        [SerializeField] private LayerMask _playerMask;
        [SerializeField] private Group _group;
        
        [Space]
        [SerializeField] private bool isDead;
        [SerializeField] private UnityEvent onSpotDead;

        [HideInInspector] public State state;
        
        public Group Group => _group;

        private void Awake()
        {
            _group.Initialize(transform);
        }

        private void Update()
        {
            switch (state)
            {
                case State.Dead:
                    Destroy(gameObject);
                    break;
                case State.Wait:
                    WaitForBattle();
                    break;
            }
            
            _group.UpdateGroup();
        }

        private void WaitForBattle()
        {
            Collider[] _playerCollider = new Collider[1];
            float radius = _group.OrbitsAmount * _group.OrbitOffset;

            var size = Physics.OverlapSphereNonAlloc(transform.position, radius, _playerCollider, _playerMask);

            if (size == 1)
            {
                var player = _playerCollider[0].GetComponent<Player>();
                StartCoroutine(BattleService.StartBattle(player, this));
            }
        }

        private void OnDestroy()
        {
            onSpotDead?.Invoke();
        }
    }
}