using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Futuregen
{
    public sealed class LoadingController : MonoBehaviour
    {
        private const string Dot = ".";

        [SerializeField] [Range(1.0f, 5.0f)] private float _loadingTime = 3.0f;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _horizontalBar;
        [SerializeField] private Image _verticalBar;
        [SerializeField] private Text _loadingText;
        [SerializeField] private Text _percentage;

        public void StartLoading(UnityAction call)
        {
            gameObject.SetActive(true);
            StartCoroutine(LoadingProcess(call));
        }

        public void StopLoading()
        {
            StopAllCoroutines();
            gameObject.SetActive(false);
        }

        private IEnumerator LoadingProcess(UnityAction call)
        {
            float elapsed = 0.0f;

            while (elapsed < 1.0f)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed);

                _canvasGroup.alpha = t;
                yield return null;
            }

            elapsed = 0.0f;
            float halfTime = _loadingTime * 0.5f;
            float prevElapsed = elapsed;
            int dotCount = 1;

            while (elapsed < _loadingTime)
            {
                elapsed += Time.deltaTime;
                float horizentalFill = Mathf.Clamp(elapsed / halfTime, 0.0f, 0.99f);
                float verticalFill = Mathf.Clamp01((elapsed - halfTime) / halfTime);

                _horizontalBar.fillAmount = horizentalFill;
                _verticalBar.fillAmount = verticalFill;

                if (prevElapsed + 0.3f < elapsed)
                {
                    prevElapsed = elapsed;

                    dotCount = dotCount < 3 ? dotCount + 1 : 1;
                    string loading = "LOADING";
                    for (int i = 0; i < dotCount; i++)
                    {
                        loading += Dot;
                    }
                    _loadingText.text = loading;
                }               

                int percentage = (int)(elapsed / _loadingTime * 100.0f);
                _percentage.text = percentage.ToString() + "%";

                yield return null;
            }

            elapsed = 1.0f;

            while (elapsed > 0.0f)
            {
                elapsed -= Time.deltaTime;
                float t = Mathf.Clamp01(elapsed);

                _canvasGroup.alpha = t;
                yield return null;
            }

            call?.Invoke();
        }
    }
}
