using System;
using Code.Scripts.Core;
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

        public Unit level1Unit;

        private void Start()
        {
            InitializePlayerUnits();
            InitializeSpotUnits();
        }

        private void InitializeSpotUnits()
        {
            for (int i = 0; i < 10; i++)
            {
                Vector3 spawnPos = new Vector3(
                    Random.Range(0.0f, 10.0f),
                    0.0f,
                    Random.Range(0.0f, 10.0f));
                
                GroupHelpers.InstantiateToGroup(level1Unit, spawnPos, spot.Group);
            }
        }

        private void InitializePlayerUnits()
        {
            for (int i = 0; i < 10; i++)
            {
                Vector3 spawnPos = new Vector3(
                    Random.Range(0.0f, 10.0f),
                    0.0f,
                    Random.Range(0.0f, 10.0f));
                
                GroupHelpers.InstantiateToGroup(level1Unit, spawnPos, player.Group);
            }
        }
    }
}