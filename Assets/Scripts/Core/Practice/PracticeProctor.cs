using System;
using UnityEngine;

namespace Futuregen
{
    /// <summary>
    /// 실습 평가 항목 데이터를 계산하는 클래스.
    /// </summary>
    [Serializable]
    public sealed class PracticeProctor
    {
        #region RecordPlayTime
        private int _capturedStartTime = 0;

        public void StartRecordTrainingPlayTime()
        {
            _capturedStartTime = (int)Time.realtimeSinceStartup;
            Debug.Log($"{PracticeManager.Instance.CurrentTraining.Name} 항목 플레이타임 측정 시작 : {0}");
        }

        public void EndRecordTrainingPlayTime()
        {
            var trainingItem = PracticeManager.Instance.CurrentTraining;
            trainingItem.PlayTime = (int)Time.realtimeSinceStartup - _capturedStartTime;
            Debug.Log($"{trainingItem.Name} 항목 플레이타임 측정 종료 : {trainingItem.PlayTime}");
        }
        #endregion

        #region CheckTraining
        public void CheckTrainingScore(int detailedTrainingItemIndex, int score)
        {
            var trainingItem = PracticeManager.Instance.CurrentTraining;
            trainingItem.DetailedPracticeResults[detailedTrainingItemIndex].Score += score;

            Debug.Log($"{trainingItem.Name} 항목 채점 : {score}점 / {trainingItem.DetailedPracticeResults.Length * score}점");
        }

        public void CheckDetailedTrainingItemResult(int detailedTrainingItemIndex)
        {
            var trainingItem = PracticeManager.Instance.CurrentTraining;
            trainingItem.DetailedPracticeResults[detailedTrainingItemIndex].IsSuccess = true;

            Debug.Log($"{trainingItem.Name} 항목 통과 여부 : {true}");
        }
        #endregion
    }
}
