using System;
using Code.Scripts.Gui;
using Code.Scripts.Services;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Scripts.Core
{
    public class GameCore : MonoBehaviour
    {
        private static GameCore s_Core = null;

        // Core Mono`s
        [SerializeField] private Player.Player _player;
        [SerializeField] private GuiHandler _guiHandler;
        [SerializeField] private BattleService _battleService;

        // Utility objects
        [SerializeField] private Settings _settings;
        [SerializeField] private Level _level;
        private InputControls _controls;
        
        // Static properties
        public static Level GetLevel => s_Core._level;
        public static Player.Player GetPlayer => s_Core._player;
        public static BattleService GetBattleService => s_Core._battleService;
        public static GuiHandler GetGuiHandler => s_Core._guiHandler;
        public static InputControls GetInput => s_Core._controls;

        // On level created callback
        public static Action<Level> OnLevelInstantiated;

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
            // Initialize startup settings
            _settings.Initialize();
            
            // Make core object dont destroyable on load in case if
            // other GameCore is not been founded before
            FixCoreObjects();
            
            _controls = new InputControls();
            OnLevelInstantiated += ProvideLevelInstantiation;
            
            s_Core = this;
        }

        private void Start()
        {
            GameState.CrrGameState.ActiveStateValue = GameState.GameStateValue.Paused;
        }

        private void FixCoreObjects()
        {
            if (s_Core == null)
            {
                DontDestroyOnLoad(gameObject);
                DontDestroyOnLoad(_player.transform.parent.gameObject);
                DontDestroyOnLoad(_guiHandler.transform.parent.gameObject);
            }
        }
        
        private void ProvideLevelInstantiation(Level obj)
        {
            _level = obj;
        }
    }
}