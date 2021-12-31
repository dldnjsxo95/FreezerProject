using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Futuregen
{
    [RequireComponent(typeof(SceneLoader))]
    public sealed class ScreenFader : MonoBehaviour
    {
        [SerializeField] [Range(0.0f, 1.0f)] private float _fadeTime = 1.0f;
        [SerializeField] private GameObject _fadePrefabs;
                
        private CanvasGroup _canvasGroup;

        public void StartFade(bool fadeOut, UnityAction doneAction = null)
        {
            if (_canvasGroup == null)
            {
                GameObject obj = Instantiate(_fadePrefabs, transform);
                _canvasGroup = obj.GetComponent<CanvasGroup>();

                obj.SetActive(false);
            }

            StopAllCoroutines();
            StartCoroutine(Fade(fadeOut, doneAction));
        }

        private IEnumerator Fade(bool fadeOut, UnityAction doneAction)
        {
            float start = fadeOut ? 1.0f : 0.0f;
            float end = fadeOut ? 0.0f : 1.0f;

            _canvasGroup.alpha = start;
            _canvasGroup.gameObject.SetActive(true);

            float elapsed = 0.0f;

            while (elapsed < _fadeTime)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / _fadeTime);

                _canvasGroup.alpha = Mathf.Lerp(start, end, t);

                yield return null;
            }

            _canvasGroup.alpha = end;
            _canvasGroup.gameObject.SetActive(!fadeOut);

            if (doneAction != null)
            {
                doneAction.Invoke();
            }
        }
    }
}
