using UnityEngine;

namespace Futuregen
{
    /// <summary>
    /// 프로그램에 필요한 모든 JSON 파일을 각각의 로더를 통해 불러온다.
    /// </summary>
    public sealed class DataLoadManager : MonoBehaviour
    {
        [SerializeField] private ContentDataLoader _contentDataLoader;
        [SerializeField] private TooltipDataLoader _tooltipDataLoader;
        [SerializeField] private PracticeResultDataLoader _practiceResultDataLoader;

        private void Awake()
        {
            _contentDataLoader.Load();
            _tooltipDataLoader.Load();
            _practiceResultDataLoader.Load();
        }
    }
}
