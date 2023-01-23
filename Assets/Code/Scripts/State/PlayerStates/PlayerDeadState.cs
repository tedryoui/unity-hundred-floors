using Code.Scripts.Core;
using Code.Scripts.Core.Player;
using Code.Scripts.State.GenericStates;

namespace Code.Scripts.State.PlayerStates
{
    public class PlayerDeadState : DefaultDeadState<Player>
    {
        public PlayerDeadState(Player actor) : base(actor)
        {
            _actor = actor;
        }
        
        public override void Process()
        {
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