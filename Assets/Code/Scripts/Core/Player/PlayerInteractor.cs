using System;
using UnityEngine;

namespace Code.Scripts.Core.Player
{
    [Serializable]
    public class PlayerInteractor : IInteractor
    {
        [SerializeField] private LayerMask _interactablesLayerMask;
        [SerializeField] private int _maxInteractionsPerFrame = 5;

        private Vector3 GetPlayerPosition => GameCore.GetPlayer.transform.position;
        private float GetPlayerRadius => GameCore.GetPlayer.ColliderRadius;
        
        public void CheckForInteractions()
        {
            // Overlaping 5 closest objects consuming to the
            // interactables layer mask
            Collider[] colliders = new Collider[_maxInteractionsPerFrame];
            Physics.OverlapSphereNonAlloc(GetPlayerPosition, GetPlayerRadius, colliders, _interactablesLayerMask);

            foreach (var collider in colliders)
            {
                if (collider != null)
                {
                    // Getting IInteractable interface of an object
                    var interactable = collider.GetComponent<IInteractable>();
                    
                    if(interactable != null)
                    {
                        // Invoking interaction
                        InteractWith(interactable);
                    }
                }
            }
        }

        public void InteractWith(IInteractable interactable)
        {
            interactable.Accept(this);
        }
    }
}