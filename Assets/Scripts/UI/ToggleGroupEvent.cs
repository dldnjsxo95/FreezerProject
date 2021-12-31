using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Futuregen
{
    [RequireComponent(typeof(ToggleGroup))]
    public sealed class ToggleGroupEvent : MonoBehaviour
    {
        [SerializeField] private Toggle[] _toggles;
        [SerializeField] private UnityEvent<int> OnActiveToggleChanged;

        private ToggleGroup _toggleGroup;

        private void Awake()
        {
            _toggleGroup = GetComponent<ToggleGroup>();

            foreach (Toggle toggle in _toggles)
            {
                toggle.group = _toggleGroup;
                toggle.onValueChanged.AddListener(OnValueChanged);
            }
        }

        private void OnDestroy()
        {
            foreach (Toggle toggle in _toggles)
            {
                toggle.onValueChanged.RemoveListener(OnValueChanged);
                toggle.group = null;
            }
        }

        private void OnValueChanged(bool isOn)
        {
            if (isOn)
            {
                // Hierarchy에서 Toggle에 할당할 인덱스에 맞게 오브젝트를 정렬해야 함.
                Toggle activeToggle = _toggleGroup.ActiveToggles().FirstOrDefault();
                int index = activeToggle.transform.GetSiblingIndex();

                OnActiveToggleChanged?.Invoke(index);
            }
        }

        public void SetActiveToggle(int index)
        {
            for (int i = 0; i < _toggles.Length; i++)
            {
                _toggles[i].SetIsOnWithoutNotify(i == index);
            }
        }
    }
}
