using System;
using UnityEngine;

namespace Code.Scripts.Core
{
    public class GameCore : MonoBehaviour
    {
        public static GameCore Instance;

        [SerializeField] private Player _player;
        public static Player GetPlayer => Instance._player;

        private void Awake()
        {
            Instance = this;
        }

        [RuntimeInitializeOnLoadMethod]
        private static void Setup()
        {
            Application.targetFrameRate = 60;
        }
    }
}