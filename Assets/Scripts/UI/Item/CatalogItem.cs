using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Futuregen
{
    public sealed class CatalogItem : MonoBehaviour
    {
        [SerializeField] private Text _title;
        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _hoverColor;
        [SerializeField] private EventTrigger _eventTrigger;

        private Selectable _selectable;

        public int MainIndex { get; private set; }
        public int SubIndex { get; private set; }

        public void SetData(SubContent data, int mainIndex, int subIndex, UnityAction call)
        {
            _title.text = data.Title;

            MainIndex = mainIndex;
            SubIndex = subIndex;

            if (_selectable == null)
            {
                _selectable = _title.GetComponent<Selectable>();

                ColorBlock colors = _selectable.colors;
                colors.normalColor = _normalColor;
                colors.highlightedColor = _hoverColor;
                colors.pressedColor = _hoverColor;
                colors.selectedColor = _hoverColor;
                colors.disabledColor = _hoverColor;

                _selectable.colors = colors;
            }

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((eventData) => { call.Invoke(); });

            _eventTrigger.triggers.Add(entry);
        }

        public void SelectItem(bool selected)
        {
            _selectable.interactable = !selected;
        }
    }
}
