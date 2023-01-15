using System;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Scripts.Units
{
    [Serializable]
    public class GroupUnit
    {
        public enum State { Stay, Chase, Battle }
        
        public Transform objectTransform;
        public Unit unit;
        public State state = State.Chase;
        private NavMeshAgent _navMeshAgent;

        public NavMeshAgent GetNavMeshAgent => _navMeshAgent ??= objectTransform.GetComponent<NavMeshAgent>();
        public float Speed {
            get => GetNavMeshAgent.speed;
            set => GetNavMeshAgent.speed = value;
        }

        private Vector3 _chaseTarget;
        private Transform _battleTarget;
        private float _battleTriggerRadius;

        public Vector3 TargetPosition
        {
            get => _chaseTarget;
            set => _chaseTarget = value;
        }

        public void Update()
        {
            switch (state)
            {
                case State.Chase:
                    GetNavMeshAgent.SetDestination(TargetPosition);
                    break;
                case State.Battle:
                    Attack();
                    break;
            }
        }

        private void Attack()
        {
            if (_battleTarget == null)
            {
                RemoveBattleState();
            }
            else
            {
                GetNavMeshAgent.SetDestination(_battleTarget.position);
            }
        }

        public void SetBattleState(Transform unitObjectTransform, float triggerRadius)
        {
            _battleTarget = unitObjectTransform;
            GetNavMeshAgent.stoppingDistance = triggerRadius;
            state = State.Battle;
        }

        public void RemoveBattleState()
        {
            _battleTarget = null;
            GetNavMeshAgent.stoppingDistance = 0.0f;
            state = State.Chase;
        }
    }
}