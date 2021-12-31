using System.Collections.Generic;
using UnityEngine;

namespace Futuregen
{
    /// <summary>
    /// Step 데이터 목록.
    /// </summary>
    public sealed class StepRepositoryValue : MonoBehaviour
    {
        [SerializeField] private Step[] _dataList;

        public void Upload(Step[] dataList)
        {
            _dataList = dataList;
        }

        public Step Find(int index)
        {
            return _dataList[index];
        }

        public int Length()
        {
            return _dataList.Length;
        }
    }

    /// <summary>
    /// Step 데이터 저장소.
    /// </summary>
    public sealed class StepRepository : MonoBehaviour
    {
        private Dictionary<string, StepRepositoryValue> _dataList = new Dictionary<string, StepRepositoryValue>();

        public void Create(string key, Step[] value)
        {
            StepRepositoryValue valueObj = new GameObject(key).AddComponent<StepRepositoryValue>();
            valueObj.transform.parent = transform;
            valueObj.Upload(value);

            _dataList.Add(key, valueObj);
        }

        public Step Find(string key, int index)
        {
            return _dataList[key].Find(index);
        }

        public int Length(string key)
        {
            return _dataList[key].Length();
        }
    }
}
