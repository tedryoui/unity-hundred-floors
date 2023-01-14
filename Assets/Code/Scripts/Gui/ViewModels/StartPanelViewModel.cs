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

        public void Pause()
        {
            _view.gameObject.SetActive(true);
            Time.timeScale = 0.0f;
            GameCore.GetPlayer.enabled = false;
        }

        public void Resume()
        {
            _view.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
            GameCore.GetPlayer.enabled = true;
        }
    }
}