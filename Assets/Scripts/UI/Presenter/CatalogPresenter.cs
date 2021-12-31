namespace Futuregen
{
    public sealed class CatalogPresenter : BasePresenter, IContentListener
    {
        private void OnEnable()
        {
            ContentManager.Instance.OnSubContentChanged += OnSubContentChanged;
        }

        protected override void Initialize()
        {
            ContentRepository repository = RepositoryHandler.Instance.ContentRepository;
            int buttonIndex = 0;

            for (int i = 0; i < repository.Length(); i++)
            {
                // 메인 콘텐츠 이름 설정.
                MainContent mainContent = repository.Find(i);
                ((CatalogPanel)_panel).SetMainTitle(i, mainContent);

                // 서브 콘텐츠 데이터 설정.
                for (int j = 0; j < repository.SubLength(i); j++)
                {
                    SubContent subContent = repository.FindSub(i, j);
                    int mainContentIndex = i;
                    int subContentIndex = j;

                    ((CatalogPanel)_panel).SetButtonIndex(subContent, buttonIndex, mainContentIndex, subContentIndex, () => OnClickCatalog(mainContentIndex, subContentIndex));

                    buttonIndex++;
                }
            }
        }

        protected override void ShowPanel()
        {
            EventManager.Instance.InvokeUniqueEvent(EventID.NA_ANI_STOP);
            base.ShowPanel();
        }

        protected override void HidePanel()
        {
            EventManager.Instance.InvokeUniqueEvent(EventID.NA_ANI_PLAY);
            base.HidePanel();
        }

        public void OnSubContentChanged(int subContentIndex)
        {
            ((CatalogPanel)_panel).SelectCurrentContent(ContentManager.Instance.MainContentIndex, subContentIndex);
        }

        public void OnStepChanged(int stepIndex)
        {
            // 사용하지 않음.
        }

        /// <summary>
        /// CatalogItem 클릭.
        /// </summary>
        /// <param name="mainContentIndex"></param>
        /// <param name="subContentIndex"></param>
        public void OnClickCatalog(int mainContentIndex, int subContentIndex)
        {
            if (ContentManager.Instance.MainContentIndex == mainContentIndex && ContentManager.Instance.SubContentIndex == subContentIndex)
            {
                return;
            }

            DialogEventArgs dialogEventArgs = new DialogEventArgs(DialogType.QuestionBox)
            {
                Title = "단계 이동",
                Message = "이동 하시겠습니까?",
                FirstButtonEvent = () => SceneLoader.Instance.LoadLoadingScene((SceneType)mainContentIndex, subContentIndex)
            };
            DialogManager.Instance.GenerateDialog(dialogEventArgs);
        }
    }
}
