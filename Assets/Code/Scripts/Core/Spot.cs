using System;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Scripts.Core
{
    public class Spot : MonoBehaviour
    {
        [SerializeField] private bool isDead;
        
        [SerializeField] private UnityEvent onSpotDead;

        private void Update()
        {
            if (isDead)
            {
                onSpotDead?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}