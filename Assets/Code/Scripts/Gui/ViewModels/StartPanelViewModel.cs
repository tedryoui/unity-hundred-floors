using System;
using Code.Scripts.Core;
using Code.Scripts.Gui.Views;
using UnityEngine;

namespace Code.Scripts.Gui.ViewModels
{
    [Serializable]
    public class StartPanelViewModel
    {
        [SerializeField] private StartPanelView _view;

        public void Resume()
        {
            _view.gameObject.SetActive(false);
            GameState.CrrGameState.ActiveStateValue = GameState.GameStateValue.Gameplay;
        }
    }
}