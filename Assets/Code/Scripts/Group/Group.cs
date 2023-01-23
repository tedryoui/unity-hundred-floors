using System;
using System.Collections.Generic;
using System.Linq;
using Code.Scripts.Units;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Scripts.Core
{
    [Serializable]
    public abstract class Group
    {
        protected List<GroupUnit> _units = new();
        public int Points { get; protected set; }
        [SerializeField] protected int _unitsLimit;
        [SerializeField] protected float _unitSpeed;
        [HideInInspector] public Transform _groupPosition;
        [SerializeField] public Transform _groupTransform;
        
        public abstract Formation Formation { get; }

        public Action OnUpdate;
        public Action OnChange;

        public List<GroupUnit> Units => _units;
        public abstract float GroupSize { get; }

        public virtual void Initialize(Transform parentTransform)
        {
            _groupPosition = parentTransform;
        }

        public virtual void Update()
        {
            // Reform our group
            Formation.Form(this);
            
            // Notify units about update
            OnUpdate?.Invoke();
        }
        
        public bool TryAdd(Transform unitTransform, Unit settings)
        {
            // Check for group fullness
            if (_units.Count >= _unitsLimit)
            {
                ReactGroupOverflow();
                return false;
            }
            else
            {
                // Add unit to the list
                Add(unitTransform, settings);
                Formation.Form(this);

                // Group changed callback
                OnChange?.Invoke();
                return true;
            }
        }


        private void Add(Transform unitTransform, Unit settings)
        {
            // Prepare scene object
            unitTransform.parent = _groupTransform;
            
            // Build new group unit from object and scriptableObject
            var unit = new GroupUnit(settings, unitTransform)
            {
                Speed = _unitSpeed
            };
            OnUpdate += unit.Update;
            
            // Add unit to the list and change points count
            Points += unit.Preset.points;
            _units.Add(unit);
            
            SortUnitsList();
        }

        public void Remove(int points)
        {
            // Get unit with same point count
            var unit = _units.FirstOrDefault(x => x.Preset.points.Equals(points));
            
            // Check for NullReference
            if (unit != null)
            {
                // Decrease points
                Points -= unit.Preset.points;
                
                // Unsub from group events
                OnUpdate -= unit.Update;
                
                // Release object to pool and from group list
                GameCore.GetLevel.GetPool.ReleaseEntity(unit.Preset.prefab.name, unit.Transform.gameObject);
                _units.Remove(unit);

                OnChange?.Invoke();
                
                SortUnitsList();
            }
        }
        
        protected abstract void SortUnitsList();
        
        protected abstract void ReactGroupOverflow();
    }
}