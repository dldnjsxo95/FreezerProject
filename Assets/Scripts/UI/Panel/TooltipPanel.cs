using UnityEngine;
using UnityEngine.Events;

namespace Futuregen
{
    [RequireComponent(typeof(TooltipPresenter))]
    public sealed class TooltipPanel : BasePanel
    {
        [Header("Tooltip")]
        [SerializeField] private TooltipItem[] _items;
        [SerializeField] [Range(0.0f, 1.0f)] private float _showInterval = 0.5f;

        [Header("Tooltip Popup")]
        [SerializeField] private GameObject _tooltipPopupContainer;
        [SerializeField] private TooltipPopupItem[] _tooltipPopupItems;

        private int _activeTooltipCount = 0;

        public void ShowTooltip(Tooltip data, UnityAction call)
        {
            TooltipItem item = _items[_activeTooltipCount++];
            item.SetData(data, call);
            item.Show(_showHideTime, _showInterval);
        }

        public void HideAllTooltip()
        {
            foreach (TooltipItem item in _items)
            {
                item.Hide(_showHideTime);
            }
            _activeTooltipCount = 0;
        }

        public bool ShowTooltipPopup(Tooltip data)
        {
            // 세부 내용이 없는 팝업의 경우.
            if (string.IsNullOrEmpty(data.DetailMessage))
            {
                return false;
            }

            foreach (TooltipPopupItem item in _tooltipPopupItems)
            {
                if (item.Index == data.Index)
                {
                    item.Show(data);
                }
                else
                {
                    item.Hide();
                }
            }
            _tooltipPopupContainer.SetActive(true);

            return true;
        }

        public void HideTooltipPopup()
        {
            _tooltipPopupContainer.SetActive(false);
        }
    }
}
