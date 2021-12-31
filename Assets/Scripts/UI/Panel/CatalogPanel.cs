using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Futuregen
{
    [RequireComponent(typeof(CatalogPresenter))]
    public sealed class CatalogPanel : BasePanel
    {
        [Header("Catalog")]
        [SerializeField] private RectTransform _catalogPaenl;
        [SerializeField] private Text[] _mainTitles;
        [SerializeField] private CatalogItem[] _items;

        private Vector2 _showPos = new Vector2(1.5f, -2.0f);
        private Vector2 _hidePos = new Vector2(-834.0f, 446.0f);

        public override void OnShow()
        {
            StartCoroutine(ShowHideAnimation(true));
        }

        public override void OnHide()
        {
            StartCoroutine(ShowHideAnimation(false));
        }

        protected override IEnumerator ShowHideAnimation(bool show)
        {
            _canvasGroup.blocksRaycasts = false;

            // 전체목록 열릴 때.
            if (show)
            {
                base.OnShow();
            }

            float startAlpha = show ? 0.0f : 1.0f;
            float endAlpha = show ? 1.0f : 0.0f;

            Vector2 startPos = show ? _hidePos : _showPos;
            Vector2 endPos = show ? _showPos : _hidePos;

            Vector3 startScale = show ? Vector3.zero : Vector3.one;
            Vector3 endScale = show ? Vector3.one : Vector3.zero;

            float elapsed = 0.0f;

            while (elapsed < _showHideTime)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / _showHideTime);

                _canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, Easing.OutQuad(t));

                _catalogPaenl.anchoredPosition = Vector2.Lerp(startPos, endPos, Easing.OutQuad(t));
                _catalogPaenl.localScale = Vector3.Lerp(startScale, endScale, Easing.OutQuad(t));
                yield return null;
            }

            // 전체목록 닫힐 떄.
            if (!show)
            {
                base.OnHide();
            }

            _canvasGroup.blocksRaycasts = true;
        }

        public void SetMainTitle(int index, MainContent data)
        {
            if (index >= _mainTitles.Length)
            {
                Debug.LogError("MainTitle 개수가 부족합니다.");
                return;
            }

            _mainTitles[index].text = data.Title;
        }

        public void SetButtonIndex(SubContent data, int itemIndex, int mainIndex, int subIndex, UnityAction call)
        {
            if (itemIndex >= _items.Length)
            {
                Debug.LogError("CatalogItem 개수가 부족합니다.");
                return;
            }

            _items[itemIndex].SetData(data, mainIndex, subIndex, call);
        }

        public void SelectCurrentContent(int mainIndex, int subIndex)
        {
            foreach (CatalogItem item in _items)
            {
                item.SelectItem(item.MainIndex == mainIndex && item.SubIndex == subIndex);
            }
        }
    }
}
