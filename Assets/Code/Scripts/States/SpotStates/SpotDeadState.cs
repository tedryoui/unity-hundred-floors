using Code.Scripts.Core;
using Code.Scripts.States.GenericStates;

namespace Code.Scripts.States.SpotStates
{
    public class SpotDeadState : DefaultDeadState<Spot>
    {
        public SpotDeadState(Spot actor) : base(actor)
        {
            _actor = actor;
        }

        public override void Process()
        {
            _actor.OnSpotDead?.Invoke();
            _actor.OnTaskProgressed?.Invoke(_actor);
            
            foreach (var unit in _actor.Group.Units)
            {
                var name = unit.Preset.prefab.name;
                var obj = unit.Transform.gameObject;
                
                GameCore.GetLevel.GetPool.ReleaseEntity(name, obj);
            }
            
            base.Process();
        }
    }
}