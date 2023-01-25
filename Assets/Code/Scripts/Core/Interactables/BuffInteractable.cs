using Code.Scripts.Core.Player;
using Code.Scripts.Stats;
using Code.Scripts.Stats.Buffs;
using UnityEngine;

namespace Code.Scripts.Core.Interactables
{
    public abstract class BuffInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private Buff _buff;

        protected abstract Stat GetFinalStat { get; }
        
        public void Accept(IInteractor interactor)
        {
            if (interactor is PlayerInteractor) 
            {
                GetFinalStat.AddStatBuff(_buff);
                Destroy(gameObject);
            }
        }
    }
}