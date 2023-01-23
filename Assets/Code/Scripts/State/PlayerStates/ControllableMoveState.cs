using Code.Scripts.Core;
using Code.Scripts.Core.Player;
using UnityEngine;

namespace Code.Scripts.State.PlayerStates
{
    public class ControllableMoveState : State
    {
        private Player _actor; 
        
        public ControllableMoveState(Player actor)
        {
            _actor = actor;
        }
        
        public override void Process()
        {
            var delta = GameCore.GetInput.Stick.Direction.ReadValue<Vector2>();
            var direction = new Vector3(delta.x, 0.0f, delta.y);
            var speed = _actor.speed * _actor.Group.GroupSize;

            _actor.transform.position += direction * (speed * Time.deltaTime);
        }
    }
}