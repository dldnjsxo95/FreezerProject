using System.Collections.Generic;
using UnityEngine;

namespace Futuregen
{
    /// <summary>
    /// 내레이션 데이터 목록.
    /// </summary>
    public sealed class NarrationRepositoryValue : MonoBehaviour
    {
        [SerializeField] private List<Narration> _dataList = new List<Narration>();

        public void Upload(Narration data)
        {
            _dataList.Add(data);
        }

        public Narration Find(int index)
        {
            return _dataList[index];
        }

        public int Length()
        {
            return _dataList.Count;
        }
    }

    /// <summary>
    /// 내레이션 데이터 저장소.
    /// </summary>
    public sealed class NarrationRepository : MonoBehaviour
    {
        private Dictionary<string, NarrationRepositoryValue> _dataList = new Dictionary<string, NarrationRepositoryValue>();

        public void Create(string key)
        {
            if (!_dataList.ContainsKey(key))
            {
                NarrationRepositoryValue valueObj = new GameObject(key).AddComponent<NarrationRepositoryValue>();
                valueObj.transform.parent = transform;

                _dataList.Add(key, valueObj);
            }
        }

        public void Add(string key, Narration value)
        {
            _dataList[key].Upload(value);
        }

        public Narration Find(string key, int index)
        {
            return _dataList[key].Find(index);
        }

        public int Length(string key)
        {
            return _dataList[key].Length();
        }
    }
}
