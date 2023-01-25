using Code.Scripts.Stats;
using UnityEngine;

namespace Code.Scripts.Core.Interactables
{
    public class YellowRune : BuffInteractable
    {
        protected override Stat GetFinalStat => GameCore.GetPlayer.PointsStat;
    }
}