using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Cutscenes;
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
                Destroy(gameObject);
        }

        private void OnDestroy()
        {
            onSpotDead?.Invoke();
        }
    }
}