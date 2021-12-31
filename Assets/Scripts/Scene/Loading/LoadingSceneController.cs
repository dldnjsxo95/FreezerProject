using UnityEngine;

namespace Futuregen
{
    public sealed class LoadingSceneController : MonoBehaviour
    {
        private void Start()
        {
            SceneLoader.Instance.LoadNextScene();
        }
    }
}
