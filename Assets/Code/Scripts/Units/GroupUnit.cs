using System;
using UnityEngine;

namespace Code.Scripts.Units
{
    [Serializable]
    public class GroupUnit
    {
        public enum State { Stay, Chase, Battle }
        
        public Transform objectTransform;
        public Unit settings;
        public float speed;
        public State state = State.Chase;
        
        //
        // Chase settings
        //
        private Vector3 _chaseTarget;
        
        //
        // Battle settings
        //
        private Transform _battleTargetTransform;
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
                    Chase(_chaseTarget);
                    break;
                case State.Battle:
                    Battle();
                    break;
            }
        }

        private void Battle()
        {
            var dist = Vector3.Distance(objectTransform.position, _battleTargetTransform.position);
            var triggerRadius = settings.triggerRadius + _battleTriggerRadius;

            if (dist > triggerRadius)
                ChaseUnit();
            else
                Attack();
        }

        private void ChaseUnit()
        {
            Chase(_battleTargetTransform.position);
        }

        private void Attack()
        {
            Debug.Log("Attacking!");
        }

        private void Chase(Vector3 targetPosition)
        {
            var position = objectTransform.position;
            var additionalSpeed = Vector3.Distance(position, targetPosition);
            var smoothSpeed = additionalSpeed + speed;

            var movePosition = 
                Vector3.MoveTowards(position, targetPosition, smoothSpeed * Time.deltaTime);
            objectTransform.position = movePosition;
        }

        public void SetBattleState(Transform unitObjectTransform, float triggerRadius)
        {
            _battleTargetTransform = unitObjectTransform;
            _battleTriggerRadius = triggerRadius;
            state = State.Battle;
        }

        public void RemoveBattleState()
        {
            _battleTargetTransform = null;
            _battleTriggerRadius = 0.0f;
            state = State.Chase;
        }
    }
}