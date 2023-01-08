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
        public enum State {Free, Stay, Battle, Move, Dead}
        
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
                    Destroy(transform.parent.gameObject);
                    break;
                case State.Free:
                    WaitForBattle();
                    _group.Update();
                    break;
                case State.Battle:
                    _group.Update();
                    break;
            }
        }

        private void WaitForBattle()
        {
            Collider[] playerCollider = new Collider[1];
            float radius = _group.FormationService.OrbitsAmount * _group.FormationService.OrbitOffset;

            var size = Physics.OverlapSphereNonAlloc(transform.position, radius, playerCollider, _playerMask);

            if (size == 1)
            {
                var player = playerCollider[0].GetComponent<Player>();
                player.BattleService.InvokeBattle(this);
            }
        }

        private void OnDestroy()
        {
            onSpotDead?.Invoke();
        }
    }
}