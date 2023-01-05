using System;
using System.Collections.Generic;
using Code.Scripts.Services;
using Code.Scripts.Units;
using UnityEngine;
using UnityEngine.Events;

// TODO - UnitReleaseService

namespace Code.Scripts.Core
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Transform _unitsTransform;
        
        [SerializeField] private List<Unit> _cellUnits;
        private List<GameObject> _cachedUnits;

        [SerializeField] private ReleaseService _releaseService;

        private void Awake()
        {
            CacheUnits();
        }

        private void Update()
        {
            TryRelease();
        }

        private void TryRelease()
        {
            bool result = _releaseService.Release(ref _player, ref _cachedUnits, ref _cellUnits);

            if (result)
            {
                //TODO - Some behaviour on released cell
                Destroy(gameObject);
            }
            else
            {
                //TODO - Some behaviour on unreleased cell
            }
        }
        
        private void CacheUnits()
        {
            _cachedUnits = new List<GameObject>();
            
            foreach (var ceilUnit in _cellUnits)
            {
                GameObject unit = Instantiate(ceilUnit.prefab, transform.position, Quaternion.identity, _unitsTransform);
                _cachedUnits.Add(unit);
            }
        }

        public void MakeReleasable() => _releaseService.isReleasable = true;
    }
}