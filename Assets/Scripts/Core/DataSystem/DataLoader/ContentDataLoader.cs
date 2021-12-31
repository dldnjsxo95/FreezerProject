using System;
using UnityEngine;

namespace Futuregen
{
    /// <summary>
    /// 콘텐츠 진행 관련 JSON 파일을 읽어서 저장소에 저장.
    /// </summary>
    [Serializable]
    public sealed class ContentDataLoader
    {
        [SerializeField] private TextAsset _contentAsset;
        [SerializeField] private TextAsset[] _stepAssets;

        /// <summary>
        /// 데이터 로드.
        /// </summary>
        public void Load()
        {
            // Main, Sub Content 데이터 로드.
            if (_contentAsset != null)
            {
                MainContent[] mainContents = JsonUtilityHelper.FromJsonArray<MainContent[]>(_contentAsset.text);
                RepositoryHandler.Instance.CreateContentRepository(mainContents);

                Debug.Log("Content 데이터 로드.");
            }

            // Step 데이터 로드.
            if (_stepAssets != null)
            {
                foreach (TextAsset stepAsset in _stepAssets)
                {
                    Step[] steps = JsonUtilityHelper.FromJsonArray<Step[]>(stepAsset.text);
                    RepositoryHandler.Instance.CreateStepRepository(stepAsset.name, steps);

                    foreach (Step step in steps)
                    {
                        // 내레이션 데이터 로드.
                        Narration data = new Narration(step.Narration, step.NarrationClipPath);
                        RepositoryHandler.Instance.CreateNarrationRepository(stepAsset.name, data);
                    }
                }

                Debug.Log("Step 데이터 로드.");
            }
        }
    }
}
