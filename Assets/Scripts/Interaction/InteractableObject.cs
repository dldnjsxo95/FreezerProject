using UnityEngine;
using UnityEngine.Events;

namespace Futuregen
{
    /// <summary>
    /// 상호작용 오브젝트의 동작이나 강조 효과를 위해 사용한다.
    /// </summary>
    public sealed class InteractableObject : MonoBehaviour, IInteraction
    {
        /// <summary>
        /// 컨트롤러가 지정한 오브젝트의 index.
        /// </summary>
        [SerializeField] [ReadOnly] private int _index;
        /// <summary>
        /// 상호작용 후에도 강조 효과를 유지.
        /// </summary>
        [SerializeField] [ReadOnly] bool _alwaysHighlight = false;
        /// <summary>
        /// 상호작용 입력 방식.
        /// </summary>
        [SerializeField] private InteractionType _type;
        /// <summary>
        /// 회전 축.
        /// </summary>
        [Header("Wheel Type Settings")]
        [SerializeField] private RotateAxis _axis;
        /// <summary>
        /// 한 번에 회전할 각도.
        /// </summary>
        [SerializeField] [Range(1.0f, 10.0f)] private float _rotationAngle = 2.0f;
        /// <summary>
        /// 최소 회전 값.
        /// </summary>        
        [SerializeField] [Range(-360.0f, 360.0f)] private float _minAngle = 0.0f;
        /// <summary>
        /// 최대 회전 값.
        /// </summary>
        [SerializeField] [Range(-360.0f, 360.0f)] private float _maxAngle = 0.0f;
        /// <summary>
        /// 상호작용이 완료될 회전 값.
        /// </summary>
        [SerializeField] [Range(-360.0f, 360.0f)] private float _completeAngle = 0.0f;
        /// <summary>
        /// 오브젝트의 상호작용 시, 발생시킬 이벤트.
        /// </summary>
        [Header("Event")]
        [SerializeField] private UnityEvent<float> OnValueChanged;

        /// <summary>
        /// 오브젝트를 관리할 컨트롤러.
        /// </summary>
        private InteractionController _interactionController;
        /// <summary>
        /// 강조 효과는 필요한 경우에만 사용.
        /// </summary>
        private Highlighter _highlighter;
        /// <summary>
        /// 초기 오브젝트 회전 값.
        /// </summary>
        private Vector3 _origianlRotation;        

        public void Initialize(InteractionController interactionController, int index)
        {
            _highlighter = GetComponent<Highlighter>();

            _interactionController = interactionController;
            _index = index;

            _origianlRotation = transform.localEulerAngles;
        }

        public void OnHighlight(bool isOn, bool always = false)
        {
            if (_highlighter == null)
            {
                _highlighter = GetComponent<Highlighter>();
            }
            if (_highlighter != null)
            {
                _alwaysHighlight = always;
                _highlighter.SetHighlight(isOn);
            }
        }

        public void OnClick()
        {
            if (_type.Equals(InteractionType.Click))
            {
                if (_highlighter != null)
                {
                    _highlighter.SetHighlight(_alwaysHighlight);
                }

                _interactionController.RunInteraction(_index);

                OnValueChanged?.Invoke(0.0f);
            }
        }

        public void OnWheel(float value)
        {
            Vector3 axis = Vector3.zero;
            float rotation = 0.0f;

            switch (_axis)
            {
                case RotateAxis.X:
                    axis = Vector3.right;
                    rotation = transform.localEulerAngles.x;
                    break;
                case RotateAxis.Y:
                    axis = Vector3.up;
                    rotation = transform.localEulerAngles.y;
                    break;
                case RotateAxis.Z:
                    axis = Vector3.forward;
                    rotation = transform.localEulerAngles.z;
                    break;
            }

            if (rotation > 180.0f)
            {
                rotation -= 360.0f;
            }

            if (rotation >= _minAngle && rotation <= _maxAngle)
            {
                transform.Rotate(axis, value * _rotationAngle);
            }

            if (rotation < _minAngle)
            {
                rotation = _minAngle;
                transform.localEulerAngles = _origianlRotation + (axis * rotation);
            }
            if (rotation > _maxAngle)
            {
                rotation = _maxAngle;
                transform.localEulerAngles = _origianlRotation + (axis * rotation);
            }

            if (rotation > _completeAngle - _rotationAngle && rotation < _completeAngle + _rotationAngle)
            {
                _interactionController.RunInteraction(_index);
            }

            float normalize = rotation / (Mathf.Abs(_minAngle) + Mathf.Abs(_maxAngle));
            OnValueChanged?.Invoke(normalize);
        }

        public void OnReset()
        {
            OnHighlight(false);
            transform.localEulerAngles = _origianlRotation;
        }

        public void OnEnter()
        {
            if (_type.Equals(InteractionType.Wheel))
            {
                InputManager.Instance.OnWheel += OnWheel;
            }
        }

        public void OnExit()
        {
            if (_type.Equals(InteractionType.Wheel))
            {
                InputManager.Instance.OnWheel -= OnWheel;
            }
        }
    }
}
