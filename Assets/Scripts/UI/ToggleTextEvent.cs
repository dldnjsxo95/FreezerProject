using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Futuregen
{
    [RequireComponent(typeof(Toggle))]
    public class ToggleTextEvent : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private Color _onColor;
        [SerializeField] private Color _offColor;

        private Toggle _toggle;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
        }

        private void OnEnable()
        {
            OnValueChanged(_toggle.isOn);
        }

        public void OnValueChanged(bool value)
        {
            _text.color = value ? _onColor : _offColor;
        }

        public void OnPointerEnter(BaseEventData eventData)
        {
            if (_toggle.isOn)
            {
                return;
            }
            _text.color = _onColor;
        }

        public void OnPointerExit(BaseEventData eventData)
        {
            if (_toggle.isOn)
            {
                return;
            }
            _text.color = _offColor;
        }
    }
}
