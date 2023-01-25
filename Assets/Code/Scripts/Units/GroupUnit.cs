using System;
using Code.Scripts.States;
using Code.Scripts.States.UnitStates;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Scripts.Units
{
    [Serializable]
    public class GroupUnit
    {
        public Transform Transform { get; private set; }
        public Unit Preset { get; private set; }

        private State CrrState = null;
        public State FollowState { get; private set; }
        
        private NavMeshAgent _navMeshAgent;
        public NavMeshAgent GetNavMeshAgent => _navMeshAgent ??= Transform.GetComponent<NavMeshAgent>();

        private float speed;
        public float Speed {
            get => speed;
            set => speed = value;
        }

        public Vector3 targetPosition;

        public GroupUnit(Unit preset, Transform unitObject)
        {
            this.Preset = preset;
            this.Transform = unitObject;
            
            Transform.gameObject.SetActive(false);
            Transform.gameObject.SetActive(true);

            FollowState = new FollowState(this);

            CrrState = FollowState;
        }

        public void Update()
        {
            if (ReferenceEquals(Transform, null)) 
                return;

            CrrState.Process();
        }

        public void WarpToTarget()
        {
            GetNavMeshAgent.Warp(targetPosition);
        }
    }
}