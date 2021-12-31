using UnityEngine;

namespace Futuregen
{
    /// <summary>
    /// 씬 이동에서 꼭 필요한 데이터를 담아둘 파괴 불가 객체.
    /// </summary>
    public sealed class TransitionManager : MonoSingleton<TransitionManager>
    {
        [SerializeField] [ReadOnly] private SceneType _currentScene = SceneType.None;
        [SerializeField] [ReadOnly] private SceneType _nextScene = SceneType.None;
        [SerializeField] [ReadOnly] private int _subContentStartIndex;

        public SceneType CurrentScene { get => _currentScene; set => _currentScene = value; }
        public SceneType NextScene { get => _nextScene; set => _nextScene = value; }
        public int SubContentStartIndex { get => _subContentStartIndex; set => _subContentStartIndex = value; }

        private void Awake()
        {
            _dontDestroyOnLoad = true;
        }
    }
}
