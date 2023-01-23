using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Code.Scripts.Gui.Views
{
    public class StickView : MonoBehaviour
    {
        [Header("Dynamic Stick Images")]
        public Image dynamicRaycastAreaImage;
        public Image dynamicStickImage;
        public Image dynamicBackgroundImage;
        
        [Space(20)]
        [Header("Static Stick Images")]
        public Image staticStickImage;
        public Image staticBackgroundImage;
    }
}