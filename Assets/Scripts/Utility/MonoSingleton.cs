using UnityEngine;

namespace Futuregen
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance = null;
        private static readonly object _lockObject = new object();
        //private static bool _shuttingDown = false;

        protected static bool _dontDestroyOnLoad = false;

        public static T Instance
        {
            get
            {
                // Scene 이동 시, 초기화가 안되는 문제 때문에 주석 처리함.
                //if (_shuttingDown)
                //{
                //    Debug.LogWarning("Singleton instance [" + typeof(T) + "] already destroy.");
                //    return null;
                //}

                lock (_lockObject)
                {
                    if (_instance == null)
                    {
                        _instance = (T)FindObjectOfType(typeof(T));
                        if (_instance == null)
                        {
                            GameObject gameObject = new GameObject(typeof(T).ToString());
                            _instance = gameObject.AddComponent<T>();
                        }

                        if (_dontDestroyOnLoad)
                        {
                            DontDestroyOnLoad(_instance);
                        }
                    }
                }
                return _instance;
            }
        }

        //private void OnDestroy()
        //{
        //    _shuttingDown = true;
        //}

        //private void OnApplicationQuit()
        //{
        //    _shuttingDown = true;
        //}
    }
}
