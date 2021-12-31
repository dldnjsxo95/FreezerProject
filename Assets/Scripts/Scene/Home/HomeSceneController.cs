using System.Collections;
using UnityEngine;

namespace Futuregen
{
    public sealed class HomeSceneController : MonoBehaviour
    {
        [Header("Login")]
        [SerializeField] private GameObject _loginPanel;
        [SerializeField] private ScreenFader _screenFader;
        [Header("Loading")]
        [SerializeField] private LoadingController _loadingController;
        [Header("Intro")]
        [SerializeField] private Transform _rotateModel;
        [SerializeField] [Range(0.0f, 1.0f)] private float _rotateSpeed = 0.2f;
        [SerializeField] private CanvasGroup _contentSelectPanel;

        private bool _introRunning = false;

        private void Start()
        {
            _loginPanel.SetActive(true);
            _loadingController.StopLoading();
            _contentSelectPanel.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_introRunning)
            {
                _rotateModel.Rotate(Vector3.up * _rotateSpeed);
            }
        }

        public void OnClickExit()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        public void OnClickLogin()
        {
            _screenFader.StartFade(false, () =>
            {
                _loginPanel.SetActive(false);
                _introRunning = true;

                _screenFader.StartFade(true, () =>
                {
                    _loadingController.StartLoading(() =>
                    {
                        _contentSelectPanel.gameObject.SetActive(true);
                        StartCoroutine(ContentFade());
                    });
                });
            });
        }

        private IEnumerator ContentFade()
        {
            float elapsed = 0.0f;

            while (elapsed < 1.0f)
            {
                elapsed += Time.deltaTime;
                _contentSelectPanel.alpha = elapsed;

                yield return null;
            }
        }

        public void OnClickTraining()
        {
            SceneLoader.Instance.LoadLoadingScene(SceneType.Main);
        }

        public void OnClickPrctice()
        {
            Debug.Log("실습 미구현.");
            //SceneLoader.Instance.LoadLoadingScene(SceneType.Main);
        }
    }
}
