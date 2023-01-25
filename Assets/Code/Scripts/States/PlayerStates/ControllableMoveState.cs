using Code.Scripts.Core;
using Code.Scripts.Core.Player;
using UnityEngine;

namespace Code.Scripts.States.PlayerStates
{
    public class ControllableMoveState : State
    {
        private Player _actor;
        private Rigidbody _actorRb;
        
        public ControllableMoveState(Player actor)
        {
            _actor = actor;
            _actorRb = _actor.GetComponent<Rigidbody>();
        }
        
        public override void Process()
        {
            var delta = GameCore.GetInput.Stick.Direction.ReadValue<Vector2>();
            var direction = new Vector3(delta.x, 0.0f, delta.y);
            var speed = _actor.SpeedStat.GetValue * _actor.Group.GroupSize;

            _actorRb.velocity = direction * speed;
        }
    }
}