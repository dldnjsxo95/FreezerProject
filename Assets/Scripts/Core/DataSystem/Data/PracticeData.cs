using System;

namespace Futuregen
{
    /// <summary>
    /// 실습 결과 메인 데이터.
    /// </summary>
    [Serializable]
    public sealed class PracticeResult
    {
        public int Index;
        public string Name;
        public int PlayTime;
        public DetailedPracticeResult[] DetailedPracticeResults;

        public int GetTotalScroe()
        {
            int totalScroe = 0;
            foreach (DetailedPracticeResult result in DetailedPracticeResults)
            {
                totalScroe += result.Score;
            }
            return totalScroe;
        }

        public int GetScroe()
        {
            int scroe = 0;
            foreach (DetailedPracticeResult result in DetailedPracticeResults)
            {
                if (result.IsSuccess)
                {
                    scroe += result.Score;
                }
            }
            return scroe;
        }
    }

    /// <summary>
    /// 실습 결과 세부 데이터.
    /// </summary>
    [Serializable]
    public sealed class DetailedPracticeResult
    {
        public string Name;
        public string Content;
        public bool IsSuccess;
        public int Score;
    }
}
