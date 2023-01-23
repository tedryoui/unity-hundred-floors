using UnityEngine;

namespace Code.Scripts.State.GenericStates
{
    public class DefaultDeadState<T> : State
        where T : MonoBehaviour
    {
        protected T _actor;
        
        public DefaultDeadState(T actor)
        {
            _actor = actor;
        }
        
        public override void Process()
        {
            Object.Destroy(_actor.gameObject);
        }
    }
}