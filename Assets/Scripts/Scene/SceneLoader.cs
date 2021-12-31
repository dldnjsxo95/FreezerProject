using UnityEngine;
using UnityEngine.SceneManagement;

namespace Futuregen
{
    public sealed class SceneLoader : MonoSingleton<SceneLoader>
    {
        private ScreenFader _screenFader;

        private void Awake()
        {
            TransitionManager.Instance.CurrentScene = GetSceneType(SceneManager.GetActiveScene().name);

            _screenFader = GetComponent<ScreenFader>();
            _screenFader.StartFade(true);
        }

        /// <summary>
        /// 사용할 때, 커스텀해서 사용.
        /// </summary>
        private string GetSceneName(SceneType scene)
        {
            string sceneName = string.Empty;
            switch (scene)
            {
                case SceneType.Home:
                    sceneName = "00.Home";
                    break;
                case SceneType.Main:
                    sceneName = "01.Main";
                    break;
                case SceneType.EquipmentInspection:
                    sceneName = "02.EquipmentInspection";
                    break;
                case SceneType.DailyInspection:
                    sceneName = "03.DailyInspection";
                    break;
                case SceneType.Loading:
                    sceneName = "99.Loading";
                    break;
                case SceneType.None:
                default:
                    Debug.LogWarning("올바른 SceneType이 아닙니다.");
                    break;
            }

            return sceneName;
        }

        /// <summary>
        /// 사용할 때, 커스텀해서 사용.
        /// </summary>
        private SceneType GetSceneType(string sceneName)
        {
            SceneType scene = SceneType.None;
            switch (sceneName)
            {
                case "00.Home":
                    scene = SceneType.Home;
                    break;
                case "01.Main":
                    scene = SceneType.Main;
                    break;
                case "02.EquipmentInspection":
                    scene = SceneType.EquipmentInspection;
                    break;
                case "03.DailyInspection":
                    scene = SceneType.DailyInspection;
                    break;
                case "99.Loading":
                    scene = SceneType.Loading;
                    break;
                default:
                    Debug.LogWarning("올바른 SceneName이 아닙니다.");
                    scene = SceneType.EquipmentInspection;
                    break;
            }

            return scene;
        }

        public void LoadLoadingScene(SceneType scene, int subContentStartIndex = 0)
        {
            string sceneName = GetSceneName(SceneType.Loading);

            TransitionManager.Instance.NextScene = scene;
            TransitionManager.Instance.SubContentStartIndex = subContentStartIndex;

            LoadScene(sceneName);
        }

        public void LoadNextScene()
        {
            string sceneName = GetSceneName(TransitionManager.Instance.NextScene);

            TransitionManager.Instance.NextScene = SceneType.None;
            LoadScene(sceneName);
        }

        private void LoadScene(string sceneName)
        {
            _screenFader.StartFade(false, () => SceneManager.LoadScene(sceneName));
            Debug.Log(sceneName + " Scene 로드");
        }
    }
}
