using UnityEngine;

namespace Futuregen
{
    public sealed class PracticeResultPresenter : BasePresenter
    {
        [SerializeField] private GameObject _itemPrefab;

        protected override void Initialize()
        {
            PracticeResultRepository repository = RepositoryHandler.Instance.PracticeResultRepository;
            bool oddEven = false;

            for (int i = 0; i < repository.Length(); i++)
            {
                PracticeResult practiceResult = repository.Find(i);

                PracticeResultItem item = Instantiate(_itemPrefab).GetComponent<PracticeResultItem>();
                int resultIndex = i;
                item.SetData(oddEven, practiceResult, () => OnClickDetailedResult(resultIndex));

                ((PracticeResultPanel)_panel).CreateResultItem(item);

                oddEven = !oddEven;
            }

            ((PracticeResultPanel)_panel).HideDetailedResult();
        }

        protected override void ShowPanel()
        {
            base.ShowPanel();

            PracticeResultRepository repository = RepositoryHandler.Instance.PracticeResultRepository;
            int totalScore = 0;

            for (int i = 0; i < repository.Length(); i++)
            {
                PracticeResult practiceResult = repository.Find(i);
                ((PracticeResultPanel)_panel).UpdateResult(i, practiceResult);

                //totalScore += practiceResult.GetScroe();
            }

            string mainContentTitle = RepositoryHandler.Instance.ContentRepository.Find(ContentManager.Instance.MainContentIndex).Title;
            ((PracticeResultPanel)_panel).UpdateTotalScore(mainContentTitle, totalScore);

            ((PracticeResultPanel)_panel).ShowHideResult(true);
            ((PracticeResultPanel)_panel).HideDetailedResult();
        }

        /// <summary>
        /// 상세보기 버튼 클릭.
        /// </summary>
        /// <param name="resultIndex"></param>
        public void OnClickDetailedResult(int resultIndex)
        {
            PracticeResultRepository repository = RepositoryHandler.Instance.PracticeResultRepository;
            PracticeResult practiceResult = repository.Find(resultIndex);

            ((PracticeResultPanel)_panel).ShowHideResult(false);
            ((PracticeResultPanel)_panel).ShowDetailedResult(practiceResult.DetailedPracticeResults);
        }

        /// <summary>
        /// 실습 다시하기 버튼 클릭.
        /// </summary>
        public void OnContinuePractice()
        {

        }

        /// <summary>
        /// 상세 결과 뒤로가기 버튼 클릭.
        /// </summary>
        public void OnCloseDetailedResult()
        {
            ((PracticeResultPanel)_panel).ShowHideResult(true);
            ((PracticeResultPanel)_panel).HideDetailedResult();
        }

        /// <summary>
        /// 실습 완료 버튼 클릭.
        /// </summary>
        public void OnCompeleteResult()
        {

        }
    }
}
