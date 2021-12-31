using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Futuregen
{
    public sealed class ToolItem : MonoBehaviour
    {
        private RectTransform _checkmark;
        private float _effectOnceTime = 0.1f;
        private float _scaleMax = 1.2f;

        private void Awake()
        {
            Toggle toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(OnValueChanged);

            _checkmark = toggle.graphic.GetComponent<RectTransform>();
        }

        public void OnValueChanged(bool value)
        {
            if (value)
            {
                StartCoroutine(CheckEffect());
            }
        }

        private IEnumerator CheckEffect()
        {
            Vector2 start = _checkmark.localScale * _scaleMax;

            float elapsed = 0f;

            while (elapsed < _effectOnceTime)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / _effectOnceTime);

                _checkmark.localScale = Vector2.Lerp(Vector2.one, start, Easing.OutExpo(t));

                yield return null;
            }

            elapsed = 0f;
            while (elapsed < _effectOnceTime)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / _effectOnceTime);

                _checkmark.localScale = Vector2.Lerp(start, Vector2.one, Easing.InExpo(t));

                yield return null;
            }
        }
    }
}
