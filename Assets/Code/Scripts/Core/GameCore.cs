using System;
using UnityEngine;

namespace Code.Scripts.Core
{
    public class GameCore : MonoBehaviour
    {
        public static GameCore Instance;

        [SerializeField] private Player _player;
        public static Player GetPlayer => Instance._player;

        public GameObject smoke;
        
        private void Awake()
        {
            Instance = this;
        }
    }
}