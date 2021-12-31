using UnityEngine;

namespace Futuregen
{
    /// <summary>
    /// Main, Sub Content 데이터 저장소.
    /// </summary>
    public sealed class ContentRepository : MonoBehaviour
    {
        [SerializeField] private MainContent[] _dataList;

        public void Create(MainContent[] dataList)
        {
            _dataList = dataList;
        }

        public MainContent Find(int mainIndex)
        {
            return _dataList[mainIndex];
        }

        public int Length()
        {
            return _dataList.Length;
        }

        public SubContent FindSub(int mainIndex, int subIndex)
        {
            return _dataList[mainIndex].SubContents[subIndex];
        }

        public int SubLength(int mainIndex)
        {
            return _dataList[mainIndex].SubContents.Length;
        }
    }
}
