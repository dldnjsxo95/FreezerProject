using UnityEngine;

namespace Futuregen
{
    /// <summary>
    /// ���α׷��� �ʿ��� ��� JSON ������ ������ �δ��� ���� �ҷ��´�.
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
