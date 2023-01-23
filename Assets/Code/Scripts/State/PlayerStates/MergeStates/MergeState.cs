using System.Collections.Generic;
using System.Linq;
using Code.Scripts.Core;
using Code.Scripts.Core.Player;
using Code.Scripts.Units;

namespace Code.Scripts.State.PlayerStates.MergeStates
{
    public abstract class MergeState : State
    {
        protected Player _actor;

        protected Core.Group Group => _actor.Group; 

        protected List<GroupUnit> Units => Group.Units;

        protected MergeState(Player actor)
        {
            _actor = actor;
        }
        
        public override void Process()
        {
            var unitGroups = Units.GroupBy(x => x.Preset)
                .Select(x => new {x.Key, GroupUnits = x.ToList<GroupUnit>()})
                .OrderBy(x => x.Key);

            foreach (var group in unitGroups)
                TryMerge(group.Key, group.GroupUnits);

            _actor.ChangeState(_actor.MoveState);
        }

        protected abstract void TryMerge(Unit unitsPreset, List<GroupUnit> groupUnits);
    }
}