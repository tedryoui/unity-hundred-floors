using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Code.Scripts.Core
{
    public abstract class Entity : MonoBehaviour
    {
        protected SphereCollider Collider;
        protected State.State CrrState = null;
        
        public abstract Group Group { get; }
        public float ColliderRadius => (Collider != null) ? Collider.radius : 0.0f;

        protected virtual void Awake()
        {
            GameState.CrrGameState.OnStateChanged += OnGameStateChanged;
            Collider = GetComponent<SphereCollider>();
            
            Group.Initialize(transform);
        }

        protected virtual void Update()
        {
            CrrState?.Process();
            Group.Update();
        }

        public void ChangeState(State.State state)
        {
            CrrState = state;
        }

        public void WarpAt(Vector3 pos)
        {
            transform.position = pos;
            Group.Formation.Form(Group);
            
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