using System;
using System.Collections.Generic;
using System.Linq;
using Code.Scripts.Services;
using Code.Scripts.Units;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Scripts.Core
{
    [Serializable]
    public class Group
    {
        [HideInInspector] public Transform _parentTransform;
        [SerializeField] public Transform _groupTransform;
        
        [SerializeField] private FormationService _formationService;
        [SerializeField] private MergeService _mergeService;
        [SerializeField] private GroupService _groupService;

        public Action OnUpdate;
        public Action OnChange;

        public FormationService FormationService => _formationService;
        public MergeService MergeService => _mergeService;
        public GroupService GroupService => _groupService;

        public void Initialize(Transform parentTransform)
        {
            _parentTransform = parentTransform;
            
            _groupService.Initialize(this);
            _formationService.Initialize(this);
            _mergeService.Initialize(this);

            OnUpdate += Update;
            OnChange += Update;
        }

        public void Update()
        {
            _formationService.Form(_groupService.units);
            _groupService.UnitsUpdateCallback?.Invoke();
        }
        
        public void Merge()
        {
            _mergeService.Merge(_groupService.units);
            OnChange?.Invoke();
        }
    }
}