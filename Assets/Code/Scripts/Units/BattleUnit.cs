using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Code.Scripts.Units
{
    [Serializable]
    public class BattleUnit
    {
        private GroupUnit _defender;
        private List<GroupUnit> _attackers = new();

        public Action OnUpdate;
        public Action OnReady;

        public BattleUnit(GroupUnit defender)
        {
            OnUpdate += defender.Update;
            _defender = defender;
        }

        public bool IsReady()
        {
            bool isReady = false;

            isReady = _defender.GetNavMeshAgent.velocity == Vector3.zero;
            foreach (var attacker in _attackers)
                isReady = attacker.GetNavMeshAgent.velocity == Vector3.zero;

            return isReady;
        }

        public void AddAttacker(GroupUnit attacker)
        {
            OnUpdate += attacker.Update;
            _attackers.Add(attacker);
        }

        public void Update()
        {
            ResetTargets();
            
            OnUpdate?.Invoke();
        }

        public void ResetTargets()
        {
            _defender.targetPosition = _attackers[0].Transform.position;
            _defender.GetNavMeshAgent.stoppingDistance = 3.0f;

            if (_defender != null) {
                foreach (var attacker in _attackers)
                {
                    attacker.targetPosition = _defender.Transform.position;
                    attacker.GetNavMeshAgent.stoppingDistance = 3.0f;
                }
            }
        }

        public void ResetUnits()
        {
            if (_defender != null)
                _defender.GetNavMeshAgent.stoppingDistance = 0.0f;

            foreach (var attacker in _attackers)
                if (attacker != null)
                    attacker.GetNavMeshAgent.stoppingDistance = 0.0f;
        }
    }
}