using System;
using UnityEngine;

namespace Futuregen
{
    /// <summary>
    /// 실습 결과 관련 JSON 파일을 읽어서 저장소에 저장.
    /// </summary>
    [Serializable]
    public sealed class PracticeResultDataLoader
    {
        [SerializeField] private TextAsset _practiceResultAsset;

        /// <summary>
        /// 데이터 로드.
        /// </summary>
        public void Load()
        {
            // 실습 결과 항목 데이터 로드.
            if (_practiceResultAsset != null)
            {
                PracticeResult[] practiceResults = JsonUtilityHelper.FromJsonArray<PracticeResult[]>(_practiceResultAsset.text);
                RepositoryHandler.Instance.CreateTrainingResultRepository(practiceResults);

                Debug.Log("PracticeResult 데이터 로드.");
            }
        }
    }
}
