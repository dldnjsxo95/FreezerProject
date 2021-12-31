using System;
using UnityEngine;

namespace Futuregen
{
    /// <summary>
    /// 툴팁 관련 JSON 파일을 읽어서 저장소에 저장.
    /// </summary>
    [Serializable]
    public sealed class TooltipDataLoader
    {
        [SerializeField] private TextAsset _tooltipAsset;

        /// <summary>
        /// 데이터 로드.
        /// </summary>
        public void Load()
        {
            // Tooltip 데이터 로드.
            if (_tooltipAsset != null)
            {
                Tooltip[] tooltips = JsonUtilityHelper.FromJsonArray<Tooltip[]>(_tooltipAsset.text);
                RepositoryHandler.Instance.CreateTooltipRepository(tooltips);

                Debug.Log("Tooltip 데이터 로드.");
            }
        }
    }
}
