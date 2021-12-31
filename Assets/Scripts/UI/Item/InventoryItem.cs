using System.Collections;
using UnityEngine;

namespace Futuregen
{
    public sealed class InventoryItem : MonoBehaviour
    {
        [SerializeField] private RectTransform _container;

        private CanvasGroup _canvasGroup;
        private float _hideY;
        private float _currentY;
        private float _currentAlpha;

        public bool IsActive { get; private set; }

        private void Awake()
        {
            _hideY = -_container.sizeDelta.x * 0.12f;
            _currentY = _hideY;

            _container.anchoredPosition = Vector2.up * _currentY;

            _currentAlpha = 0.0f;

            _canvasGroup = _container.GetComponent<CanvasGroup>();
            _canvasGroup.alpha = _currentAlpha;

            gameObject.SetActive(false);
        }

        public void Show(float showTime)
        {
            IsActive = true;

            gameObject.SetActive(true);

            StopAllCoroutines();
            StartCoroutine(ShowHideAnimation(true, showTime));
        }

        public void Hide(float hideTime)
        {
            IsActive = false;

            StopAllCoroutines();
            StartCoroutine(ShowHideAnimation(false, hideTime));
        }

        public IEnumerator ShowHideAnimation(bool show, float time)
        {
            _canvasGroup.blocksRaycasts = false;

            Vector2 startPos = show ? Vector2.up * _currentY : Vector2.zero;
            Vector2 endPos = show ? Vector2.zero : Vector2.up * _hideY;

            float startAlpha = _currentAlpha;
            float endAlpha = show ? 1.0f : 0.0f;

            float elapsed = 0.0f;

            while (elapsed < time)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / time);

                _container.anchoredPosition = Vector2.Lerp(startPos, endPos, Easing.OutQuad(t));
                _canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, Easing.OutQuad(t));

                _currentY = _container.anchoredPosition.y;
                _currentAlpha = _canvasGroup.alpha;

                yield return null;
            }

            if (!show)
            {
                gameObject.SetActive(false);
            }

            _canvasGroup.blocksRaycasts = true;
        }
    }
}
