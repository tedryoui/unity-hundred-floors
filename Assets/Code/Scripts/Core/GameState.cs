using System;

namespace Code.Scripts.Core
{
    public class GameState
    {
        private static GameState _gameState;

        public static GameState CrrGameState => _gameState ??= new GameState();

        public enum GameStateValue {
            Paused,
            Gameplay,
            Battle
        }

        private GameStateValue _activeStateValue;

        public GameStateValue ActiveStateValue
        {
            get => _activeStateValue;
            set
            {
                _activeStateValue = value;
                OnStateChanged?.Invoke(_activeStateValue);
            }
        }
        
        public Action<GameStateValue> OnStateChanged;

        public GameState()
        {
            _activeStateValue = GameStateValue.Gameplay;
        }
    }
}