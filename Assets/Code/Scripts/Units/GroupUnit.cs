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

        private CapsuleCollider _collider;
        private Rigidbody _rb;

        public Rigidbody GetRb
        {
            get
            {
                if (ReferenceEquals(_rb, null)) _rb = objectTransform.GetComponent<Rigidbody>();

                return _rb;
            }
        }

        public CapsuleCollider GetCollider
        {
            get
            {
                if (ReferenceEquals(_collider, null)) _collider = objectTransform.GetComponent<CapsuleCollider>();

                return _collider;
            }
        }
        
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
            if (_battleTargetTransform == null)
            {
                state = State.Chase;
                return;
            }
            
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
            GetRb.velocity = Vector3.zero;
        }

        private void Chase(Vector3 targetPosition)
        {
            var position = objectTransform.position;
            var additionalSpeed = Vector3.Distance(position, targetPosition);
            var smoothSpeed = additionalSpeed * speed;

            GetRb.velocity = (targetPosition - position) * smoothSpeed;
        }

        public void SetBattleState(Transform unitObjectTransform, float triggerRadius)
        {
            _battleTargetTransform = unitObjectTransform;
            _battleTriggerRadius = triggerRadius;
            GetCollider.isTrigger = false;
            state = State.Battle;
        }

        public void RemoveBattleState()
        {
            _battleTargetTransform = null;
            _battleTriggerRadius = 0.0f;
            GetCollider.isTrigger = true;
            state = State.Chase;
        }
    }
}