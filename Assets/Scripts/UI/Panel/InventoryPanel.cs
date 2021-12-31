using System.Collections;
using UnityEngine;

namespace Futuregen
{
    [RequireComponent(typeof(InventoryPresenter))]
    public sealed class InventoryPanel : BasePanel
    {
        [Header("Inventory")]
        [SerializeField] private CanvasGroup _inventoryPanel;
        [SerializeField] private InventoryItem[] _items;
        [SerializeField] [Range(0.0f, 1.0f)] private float _itemAnimationTime = 0.4f;

        [Header("Function Buttons")]
        [SerializeField] private UIHighlighter _showButton;
        [SerializeField] private GameObject _hideButton;

        private RectTransform _rectTrans;
        private float _showX = -11.0f;
        private float _hideX = -352.0f;

        protected override void Awake()
        {
            base.Awake();
            _rectTrans = _content.GetComponent<RectTransform>();

            _inventoryPanel.alpha = 0.0f;
            _inventoryPanel.blocksRaycasts = false;

            _showButton.gameObject.SetActive(true);
            _hideButton.SetActive(false);
        }

        public override void OnShow()
        {
            StartCoroutine(ShowHideAnimation(true));
        }

        public override void OnHide()
        {
            StartCoroutine(ShowHideAnimation(false));
        }

        public override bool IsActive()
        {
            return _inventoryPanel.blocksRaycasts;
        }

        protected override IEnumerator ShowHideAnimation(bool show)
        {
            _canvasGroup.blocksRaycasts = false;

            _inventoryPanel.blocksRaycasts = show;
            _showButton.gameObject.SetActive(!show);
            _hideButton.SetActive(show);

            Vector2 startPos = new Vector2(show ? _hideX : _showX, _rectTrans.anchoredPosition.y);
            Vector2 endPos = new Vector2(show ? _showX : _hideX, _rectTrans.anchoredPosition.y);

            float startAlpha = show ? 0.0f : 1.0f;
            float endAlpha = show ? 1.0f : 0.0f;

            float elapsed = 0.0f;

            while (elapsed < _showHideTime)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / _showHideTime);

                _rectTrans.anchoredPosition = Vector2.Lerp(startPos, endPos, Easing.OutQuad(t));
                _inventoryPanel.alpha = Mathf.Lerp(startAlpha, endAlpha, Easing.OutQuad(t));

                yield return null;
            }

            _canvasGroup.blocksRaycasts = true;
        }

        public void UpdateItem(int itemIndex)
        {
            InventoryItem item = _items[itemIndex];

            if (item.IsActive)
            {
                item.Hide(_itemAnimationTime);
            }
            else
            {
                item.transform.SetSiblingIndex(_items.Length - 1);
                item.Show(_itemAnimationTime);
            }
        }

        public void SetHighlightShowButton(bool active)
        {
            _showButton.SetHighlight(active);
        }
    }
}
