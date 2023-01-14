using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.Serialization;

namespace Code.Scripts.Gui.Views
{
    public class StickView : MonoBehaviour
    {
        [FormerlySerializedAs("movableStick")] 
        public GameObject movableStickRoot;
        
        public FollowingOnScreenStick movableStick;
        
        public GameObject staticStick;

    }
}