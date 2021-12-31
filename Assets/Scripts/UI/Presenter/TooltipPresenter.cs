namespace Futuregen
{
    public sealed class TooltipPresenter : BasePresenter, IContentListener
    {
        private void OnEnable()
        {
            ContentManager.Instance.OnStepChanged += OnStepChanged;
        }

        protected override void Initialize()
        {
            // 사용하지 않음.
        }

        public void OnSubContentChanged(int subContentIndex)
        {
            // 사용하지 않음.
        }

        public void OnStepChanged(int stepIndex)
        {
            ((TooltipPanel)_panel).HideAllTooltip();

            int[] tooltipIndices = ContentManager.Instance.CurrentStep.TooltipIndex;
            if (tooltipIndices == null)
            {
                return;
            }

            for (int i = 0; i < tooltipIndices.Length; i++)
            {
                Tooltip tooltip = RepositoryHandler.Instance.TooltipRepository.Find(tooltipIndices[i]);
                ((TooltipPanel)_panel).ShowTooltip(tooltip, () => OnOpenTooltipPopup(tooltip));
            }
        }

        /// <summary>
        /// 툴팁 클릭.
        /// </summary>
        /// <param name="tooltipData"></param>
        public void OnOpenTooltipPopup(Tooltip tooltipData)
        {
            if (((TooltipPanel)_panel).ShowTooltipPopup(tooltipData))
            {
                EventManager.Instance.InvokeUniqueEvent(EventID.NA_ANI_STOP);
            }
        }

        /// <summary>
        /// 툴팁 닫기 버튼 클릭.
        /// </summary>
        public void OnCloseTooltipPopup()
        {
            EventManager.Instance.InvokeUniqueEvent(EventID.NA_ANI_PLAY);
            ((TooltipPanel)_panel).HideTooltipPopup();
        }
    }
}
