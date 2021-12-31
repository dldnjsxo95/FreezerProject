using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Futuregen
{
    public sealed class PracticeManager : MonoSingleton<PracticeManager>
    {
        private PracticeProctor _trainingProctor;

        public PracticeResult CurrentTraining { get; private set; }

        private void Awake()
        {          
            _trainingProctor = new PracticeProctor();
        }

        //InteractionManager 에서 초기화 할때 호출
        public void StartTraining(TrainingItemType type)
        {
            if(ContentManager.Instance.IsTraining == false)
            {
                return;
            }

            CurrentTraining = RepositoryHandler.Instance.PracticeResultRepository.Find(type);
  
            _trainingProctor.StartRecordTrainingPlayTime();
        }

        //InteractionController 에서 상호작용 할 떄 호출
        public void EndTraining()
        {
            if (ContentManager.Instance.IsTraining == false)
            {
                return;
            }

            if (CurrentTraining == null)
            {
                return;
            }

            _trainingProctor.EndRecordTrainingPlayTime();
        }

        //InteractionController 에서 상호작용 할 떄 호출
        //TODO : detailIndex 는 추후 Enum 형식이나 Const 로 매직넘버 없앨수있으면 좋겠다.
        public void CheckTrainingItem(int detailIndex ,int score)
        {
            if (ContentManager.Instance.IsTraining == false)
            {
                return;
            }

            _trainingProctor.CheckDetailedTrainingItemResult(detailIndex);
            _trainingProctor.CheckTrainingScore(detailIndex, score);
        }

    }
}
