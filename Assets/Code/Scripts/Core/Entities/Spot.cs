using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Core.Player;
using Code.Scripts.Cutscenes;
using Code.Scripts.Services;
using Code.Scripts.State.GenericStates;
using Code.Scripts.State.SpotStates;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Scripts.Core
{
    public class Spot : Entity, IInteractable, ITaskable
    {
        public float spotColliderMultiplier = 4.0f;
        
        [SerializeField] private StaticGroup _group;
        public override Group Group => _group;
        
        public State.State MoveState { get; private set; }
        public State.State DeadState { get; private set; }

        public UnityEvent OnSpotDead;
        public Action<ITaskable> OnTaskProgressed { get; set; }

        protected override void Awake()
        {
            base.Awake();

            MoveState = new DisabledMoveState();
            DeadState = new SpotDeadState(this);
            
            CrrState = MoveState;

            Group.OnChange += ChangeCollider;
        }

        public void Accept(IInteractor interactor)
        {
            if (interactor is PlayerInteractor && CrrState is not SpotDeadState)
            {
                GameCore.GetBattleService.InvokeBattle(this);
            }
        }
        
        private void ChangeCollider()
        {
            Collider.radius = Group.GroupSize * spotColliderMultiplier;
        }
    }
}