using System;
using Code.Scripts.Core;
using Code.Scripts.Helpers;
using Code.Scripts.Services;
using Code.Scripts.Units;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Scripts.Mocks
{
    public class BattleMock : MonoBehaviour
    {
        public Player player;
        public Spot spot;
        public Spot spot2;

        public Unit level1Unit;

        private void Start()
        {
            InitializePlayerUnits();
            InitializeSpotUnits();
            InitializeSpot2Units();
        }
        
        [SerializeField] private int _spotCount;

        private void InitializeSpotUnits()
        {
            for (int i = 0; i < _spotCount; i++)
            {
                Vector3 spawnPos = new Vector3(
                    Random.Range(0.0f, 10.0f),
                    0.0f,
                    Random.Range(0.0f, 10.0f));
                
                GroupHelpers.InstantiateToGroup(spot.Group, level1Unit, spawnPos);
            }
        }
        
        private void InitializeSpot2Units()
        {
            for (int i = 0; i < _spotCount; i++)
            {
                Vector3 spawnPos = new Vector3(
                    Random.Range(0.0f, 10.0f),
                    0.0f,
                    Random.Range(0.0f, 10.0f));
                
                GroupHelpers.InstantiateToGroup(spot2.Group, level1Unit, spawnPos);
            }
        }

        [SerializeField] private int _playerCount;

        private void InitializePlayerUnits()
        {
            for (int i = 0; i < _playerCount; i++)
            {
                Vector3 spawnPos = new Vector3(
                    Random.Range(0.0f, 10.0f),
                    0.0f,
                    Random.Range(0.0f, 10.0f));
                
                GroupHelpers.InstantiateToGroup(player.Group, level1Unit, spawnPos);
            }
        }
    }
}