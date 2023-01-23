using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Code.Scripts.Core
{
    public class Level : MonoBehaviour
    {
        public NavMeshData navMeshData;
        public Transform playerSpawnPoint;

        [SerializeField] private EntitiesPool _entitiesPool = new();
        [SerializeField] private Portal _portal;
        private Task _task = new KillSpotsTask();

        [Space(10)] 
        [Header("Level Options")] 
        [SerializeField] private float _levelRadius;
        [SerializeField] private string _nextLevelSceneName;

        public EntitiesPool GetPool => _entitiesPool; 
        
        private void Awake()
        {
            // Replace current level in gameCore object
            GameCore.OnLevelInstantiated(this);
            
            // Update nav mesh data to current scene one
            UpdateNavMesh();
            
            // Register all necessary object into pool
            _entitiesPool.Initialize();
            
            // Inject in ITaskable level task dependency
            _task.Initialize();
            _task.OnComplete += _portal.Release;
            
            // Set portal complete action
            _portal.OnPlayerEntered += GoNextLevel;
        }

        private void Start()
        {
            // Prepare scene objects
            GameCore.GetPlayer.WarpAt(playerSpawnPoint.position);
        }

        private void UpdateNavMesh()
        {
            NavMesh.RemoveAllNavMeshData();
            NavMesh.AddNavMeshData(navMeshData);
        }

        private void GoNextLevel()
        {
            SceneManager.LoadScene(_nextLevelSceneName);
        }
    }
}