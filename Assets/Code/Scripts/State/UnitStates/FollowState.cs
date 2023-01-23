using System;
using Code.Scripts.Units;

namespace Code.Scripts.State.UnitStates
{
    public class FollowState : State
    {
        private readonly GroupUnit _actor;

        public FollowState(GroupUnit actor)
        {
            this._actor = actor;
        }
        
        public override void Process()
        {
            if(_actor.Transform.gameObject.activeInHierarchy)
            {
                var distance = _actor.GetNavMeshAgent.remainingDistance;
                _actor.GetNavMeshAgent.speed = _actor.Speed + Math.Clamp(distance, 0, 100);
                
                _actor.GetNavMeshAgent.SetDestination(_actor.targetPosition);
            }
        }
    }
}