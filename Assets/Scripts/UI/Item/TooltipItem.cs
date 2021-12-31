using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Futuregen
{
    public sealed class TooltipItem : MonoBehaviour
    {
        [SerializeField] private Text _tooltip;
        [SerializeField] private EventTrigger _eventTrigger;

        private CanvasGroup _canvasGroup;
        private RectTransform _rectTrans;
        private float _showX = 121.5f;
        private float _hideX = 200.0f;
        private float _currentX;
        private float _currentAlpha;

        private void Awake()
        {
            _currentX = _hideX;

            _rectTrans = GetComponent<RectTransform>();
            _rectTrans.anchoredPosition = new Vector2(_currentX, _rectTrans.anchoredPosition.y);

            _currentAlpha = 0.0f;

            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = _currentAlpha;
            _canvasGroup.blocksRaycasts = false;
        }

        public void SetData(Tooltip data, UnityAction call)
        {
            _tooltip.text = data.Message;

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((eventData) => { call.Invoke(); });

            _eventTrigger.triggers.Add(entry);
        }

        public void Show(float showTime, float showInterval)
        {
            StopAllCoroutines();
            StartCoroutine(ShowHideAnimation(true, showTime, showInterval));
        }

        public void Hide(float hideTime)
        {
            StopAllCoroutines();
            StartCoroutine(ShowHideAnimation(false, hideTime));
        }

        private IEnumerator ShowHideAnimation(bool show, float time, float interval = 0.0f)
        {
            yield return new WaitForSeconds(interval);

            _canvasGroup.blocksRaycasts = false;

            Vector2 startPos = new Vector2(_currentX, _rectTrans.anchoredPosition.y);
            Vector2 endPos = new Vector2(show ? _showX : _hideX, _rectTrans.anchoredPosition.y);

            float startAlpha = _currentAlpha;
            float endAlpha = show ? 1.0f : 0.0f;

            float elapsed = 0.0f;

            while (elapsed < time)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / time);

                _rectTrans.anchoredPosition = Vector2.Lerp(startPos, endPos, Easing.OutQuad(t));
                _canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, Easing.OutQuad(t));

                _currentX = _rectTrans.anchoredPosition.x;
                _currentAlpha = _canvasGroup.alpha;

                yield return null;
            }

            _canvasGroup.blocksRaycasts = show;
        }
    }
}
