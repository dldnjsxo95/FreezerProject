using UnityEngine;

namespace Futuregen
{
    public sealed class ProgressPresenter : BasePresenter, IContentListener
    {
        [SerializeField] private GameObject _itemPrefab;

        private int _subContentTotalCount = 0;

        private void OnEnable()
        {
            ContentManager.Instance.OnSubContentChanged += OnSubContentChanged;
        }

        protected override void Initialize()
        {
            // 메인 콘텐츠 타이틀 설정.
            ContentRepository repository = RepositoryHandler.Instance.ContentRepository;
            MainContent mainContent = repository.Find(ContentManager.Instance.MainContentIndex);

            ((ProgressPanel)_panel).SetProgressTitle(mainContent);

            // Item 생성.
            SubContent[] subContents = mainContent.SubContents;
            _subContentTotalCount = subContents.Length;

            bool oddEven = true;

            for (int i = 0; i < _subContentTotalCount; i++)
            {
                SubContent subContent = subContents[i];

                ProgressItem item = Instantiate(_itemPrefab).GetComponent<ProgressItem>();
                int subContentIndex = i;
                item.SetData(oddEven, subContent, () => OnClickShortCut(subContentIndex), i == _subContentTotalCount - 1);

                ((ProgressPanel)_panel).CreateProgressItem(item);

                oddEven = !oddEven;
            }
        }

        public void OnSubContentChanged(int subContentIndex)
        {
            int percentage = (int)((float)subContentIndex / _subContentTotalCount * 100.0f);
            ((ProgressPanel)_panel).UpdateProgress(percentage, subContentIndex);
        }

        public void OnStepChanged(int stepIndex)
        {
            // 사용하지 않음.
        }

        /// <summary>
        /// ShortCutItem 클릭.
        /// </summary>
        /// <param name="subContentIndex"></param>
        public void OnClickShortCut(int subContentIndex)
        {
            if (ContentManager.Instance.SubContentIndex == subContentIndex)
            {
                return;
            }

            DialogEventArgs dialogEventArgs = new DialogEventArgs(DialogType.QuestionBox)
            {
                Title = "단계 이동",
                Message = "이동 하시겠습니까?",
                FirstButtonEvent = () => ContentManager.Instance.StartSubContent(subContentIndex),
            };
            DialogManager.Instance.GenerateDialog(dialogEventArgs);
        }

        /// <summary>
        /// 이전 버튼 클릭.
        /// </summary>
        public void OnPrevShortCut()
        {
            ((ProgressPanel)_panel).ShowPrevItem();
        }

        /// <summary>
        /// 다음 버튼 클릭.
        /// </summary>
        public void OnNextShortCut()
        {
            ((ProgressPanel)_panel).ShowNextItem();
        }
    }
}
