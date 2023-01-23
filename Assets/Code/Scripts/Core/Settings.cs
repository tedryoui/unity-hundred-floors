using System;
using UnityEngine;

namespace Code.Scripts.Core
{
    [Serializable]
    public class Settings
    {
        [SerializeField] private int _targetFrameRate;
        
        public void Initialize()
        {
            Application.targetFrameRate = _targetFrameRate;
        }
    }
}