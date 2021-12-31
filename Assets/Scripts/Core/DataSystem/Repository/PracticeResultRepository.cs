using UnityEngine;

namespace Futuregen
{
    /// <summary>
    /// �ǽ� �Ʒ� ��� ������ �����.
    /// </summary>
    public sealed class PracticeResultRepository : MonoBehaviour
    {
        [SerializeField] private PracticeResult[] _dataList;

        public void Create(PracticeResult[] dataList)
        {
            _dataList = dataList;
        }

        public PracticeResult Find(int index)
        {
            return _dataList[index];
        }

        public PracticeResult Find(TrainingItemType type)
        {
            return _dataList[(int)type];
        }

        public int Length()
        {
            return _dataList.Length;
        }
    }
}
