using System;
using Code.Scripts.Services;
using UnityEngine;

namespace Code.Scripts.Core
{
    public class GameCore : MonoBehaviour
    {
        public static GameCore s_Core;

        [SerializeField] private Player _player;
        [SerializeField] private GuiHandler _guiHandler;
        [SerializeField] private InputControls _controls;
        public static Player GetPlayer => s_Core._player;
        public static GuiHandler GetGuiHandler => s_Core._guiHandler;
        public static InputControls GetInput => s_Core._controls;

        private void OnEnable()
        {
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        private void Awake()
        {
            _controls = new InputControls();
            
            s_Core = this;
            
            _guiHandler.PauseGame();
        }

        [RuntimeInitializeOnLoadMethod]
        private static void Setup()
        {
            Application.targetFrameRate = 60;
        }
    }
}