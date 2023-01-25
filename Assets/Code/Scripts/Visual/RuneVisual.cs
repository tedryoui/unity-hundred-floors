using UnityEngine;

namespace Code.Scripts.Visual
{
    public class RuneVisual : MonoBehaviour
    {
        [SerializeField] private float _upAndDownDiapozone;
        [SerializeField] private float _upAndDownSpeed;
        [SerializeField] private AnimationCurve _upAndDownAnimationCurve;
        private float _upAndDownTime;
        private Vector3 _upAndDownTop;
        private Vector3 _upAndDownDown;
        
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private AnimationCurve _rotationAnimationCurve;
        private float _rotationTime;
        
        private void Awake()
        {
            _upAndDownTime = 0.0f;
            _rotationTime = 0.0f;

            _upAndDownTop = transform.position + Vector3.up * _upAndDownDiapozone;
            _upAndDownDown = transform.position + Vector3.down * _upAndDownDiapozone;
        }

        private void Update()
        {
            transform.position = Vector3.Lerp(_upAndDownDown, _upAndDownTop,
                _upAndDownAnimationCurve.Evaluate(_upAndDownTime));
            
            _upAndDownTime += Time.deltaTime * _upAndDownSpeed;

            if (_upAndDownTime >= 10000) _upAndDownTime = 0.0f;
            
            transform.rotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(0, 180, 0), 
                _rotationAnimationCurve.Evaluate(_rotationTime));
            
            _rotationTime += Time.deltaTime * _rotationSpeed;

            if (_rotationTime >= 10000) _rotationTime = 0.0f;
        }
    }
}