using System;
using System.Runtime.Serialization;
using Code.Scripts.States;
using Code.Scripts.Stats;
using UnityEngine;

namespace Code.Scripts.Core
{
    public abstract class Entity : MonoBehaviour
    {
        protected SphereCollider Collider;
        protected State CrrState = null;
        
        public abstract Group Group { get; }
        
        [field: SerializeField] public InvulnerableStat PointsStat { get; private set; }
        
        public float ColliderRadius => (Collider != null) ? Collider.radius : 0.0f;

        protected virtual void Awake()
        {
            GameState.CrrGameState.OnStateChanged += OnGameStateChanged;
            Collider = GetComponent<SphereCollider>();
            
            Group.Initialize(this, transform);
        }

        protected virtual void Update()
        {
            CrrState?.Process();
            Group.Update();
        }

        public void ChangeState(State state)
        {
            CrrState = state;
        }

        public void WarpAt(Vector3 pos)
        {
            // Make Y coord of Pos equals to zero, sience wy don`t need
            // to move our player by Y coord
            transform.position = pos;
            
            // Reform Warped player`s formation
            Group.Formation.Form(Group);
            
            // Warp player`s units
            foreach (var groupServiceUnit in Group.Units)
                groupServiceUnit.WarpToTarget();
        }
        
        private void OnGameStateChanged(GameState.GameStateValue state)
        {
            enabled = state == GameState.GameStateValue.Gameplay;
        }

        private void OnDestroy()
        {
            GameState.CrrGameState.OnStateChanged -= OnGameStateChanged;
        }
    }
}