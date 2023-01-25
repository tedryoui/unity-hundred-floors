using System;
using System.Collections.Generic;
using Code.Scripts.Core.Player;
using Code.Scripts.Services;
using Code.Scripts.States;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;
using Code.Scripts.Units;

namespace Code.Scripts.Core
{
    public class Cell : Entity, IInteractable
    {
        [SerializeField] private bool isReleasedByDefault = false;
        
        [SerializeField] private Transform _groupTransform;
        [SerializeField] private List<Unit> _cellUnits;

        private State _releasableState;
        private State _unreleasableState;
        

        [SerializeField] private StaticGroup _group;
        public override Group Group => _group;

        protected override void Awake()
        {
            base.Awake();
            
            _releasableState = new ReleasableState(this);
            _unreleasableState = new UnreleasableState();

            if (isReleasedByDefault)
                CrrState = _releasableState;
            else
                CrrState = _unreleasableState;
        }

        private void Start()
        {
            FillCell();
        }

        protected override void Update()
        {
        }

        private void FillCell()
        {
            foreach (var unit in _cellUnits)
            {
                GameObject obj = GameCore.GetLevel.GetPool.GetEntity(unit.prefab.name);
                obj.transform.parent = _groupTransform;

                _group.TryAdd(obj.transform, unit);
            }
        }

        public void Release() => CrrState = _releasableState;
        
        public void Accept(IInteractor interactor)
        {
            if (interactor is PlayerInteractor && CrrState is ReleasableState)
                CrrState.Process();
        }
    }

    internal class ReleasableState : State
    {
        private Cell _actor;

        public ReleasableState(Cell actor)
        {
            _actor = actor;
        }
        
        public override void Process()
        {
            var group = _actor.Group;
            var units = group.Units;

            foreach (var unit in units)
            {
                var preset = unit.Preset;
                var obj = unit.Transform;

                GameCore.GetPlayer.Group.TryAdd(obj, preset);
            }
            
            units.Clear();
            Object.Destroy(_actor.gameObject);
        }
    }

    internal class UnreleasableState : State
    {
        public override void Process()
        {
        }
    }
}