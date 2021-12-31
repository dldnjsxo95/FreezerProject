using System;

namespace Futuregen
{
    /// <summary>
    /// �ǽ� ��� ���� ������.
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
    /// �ǽ� ��� ���� ������.
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
