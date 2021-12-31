namespace Futuregen
{
    public class HelpPresenter : BasePresenter
    {
        protected override void Initialize()
        {
            ((HelpPanel)_panel).ResetPage();
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

        /// <summary>
        /// HelpItem 클릭.
        /// </summary>
        /// <param name="pageIndex"></param>
        public void OnClickPage(int pageIndex)
        {
            ((HelpPanel)_panel).SetPage(pageIndex);
        }

        /// <summary>
        /// 이전 버튼 클릭.
        /// </summary>
        public void OnClickPrevPage()
        {
            ((HelpPanel)_panel).PrevPage();
        }

        /// <summary>
        /// 다음 버튼 클릭.
        /// </summary>
        public void OnClickNextPage()
        {
            ((HelpPanel)_panel).NextPage();
        }
    }
}
