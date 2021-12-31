using UnityEngine;

namespace Futuregen
{
    /// <summary>
    /// Tooltip 데이터 저장소.
    /// </summary>
    public sealed class TooltipRepository : MonoBehaviour
    {
        [SerializeField] private Tooltip[] _dataList;

        public void Create(Tooltip[] dataList)
        {
            _dataList = dataList;
        }

        public Tooltip Find(int index)
        {
            return _dataList[index];
        }

        public int Length()
        {
            return _dataList.Length;
        }
    }
}
