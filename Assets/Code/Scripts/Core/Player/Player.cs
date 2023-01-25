using System.Collections.Generic;
using Code.Scripts.States;
using Code.Scripts.States.PlayerStates;
using Code.Scripts.States.PlayerStates.MergeStates;
using Code.Scripts.Stats;
using Code.Scripts.Units;
using UnityEngine;

namespace Code.Scripts.Core.Player
{
    public class Player : Entity
    {
        [Space(10)]
        [SerializeField] private PlayerInteractor _interactor = new();
        
        [Space(10)]
        [SerializeField] private PlayerCameraService _cameraService;
        
        [Space(10)]
        [SerializeField] private DynamicGroup _group;

        [Space(10)] 
        [SerializeField] private PlayerStats _stats;
        
        [Space(10)]
        public float unitSpeedDifference = 2;
        public float playerColliderMultiplier = 4.0f;

        public State MoveState { get; private set; }
        public State DeadState { get; private set; }
        public MergeState MergeState { get; private set;}
        
        public override Group Group => _group;

        [field: SerializeField] public VulnerableStat SpeedStat { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            
            _cameraService.Initialize(this);

            MoveState = new ControllableMoveState(this);
            DeadState = new PlayerDeadState(this);
            MergeState = new PairMergeState(this);
            
            CrrState = MoveState;

            Group.OnChange += ChangeCollider;
            Group.OnChange += ChangeUnitsSpeed;
        }

        protected override void Update()
        {
            ChangeUnitsSpeed();
            base.Update();
            
            _cameraService.Update();
            _interactor.CheckForInteractions();
        }
        
        private void ChangeCollider()
        {
            Collider.radius = Group.GroupSize * playerColliderMultiplier;
        }

        private void ChangeUnitsSpeed()
        {
            List<GroupUnit> units = Group.Units;
            float newSpeed = (SpeedStat.GetValue * Group.GroupSize) - unitSpeedDifference;

            foreach (var groupUnit in units)
                groupUnit.Speed = newSpeed;
        }
    }
}