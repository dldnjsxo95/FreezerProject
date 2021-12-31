using System;
using System.Collections;

namespace Futuregen
{
    public sealed class InventoryPresenter : BasePresenter, IContentListener
    {
        private void OnEnable()
        {
            ContentManager.Instance.OnStepChanged += OnStepChanged;

            EventManager.Instance.ResisterEvent(EventID.INV_HIGHLIGHT, HighlightShowButtonEvent);
            EventManager.Instance.ResisterEvent(EventID.INV_SHOW, ShowInventoryEvent);

            EventManager.Instance.ResisterEvent(EventID.M_S2_00, ShowInventoryEvent);

            //EventManager.Instance.ResisterEvent(EventID.E_S0_04, ShowInventoryEvent);
            EventManager.Instance.ResisterEvent(EventID.E_S0_06, HighlightShowButtonEvent);
            EventManager.Instance.ResisterEvent(EventID.E_S0_07, HighlightShowButtonEvent);

            EventManager.Instance.ResisterEvent(EventID.E_S1_02, HighlightShowButtonEvent);
            EventManager.Instance.ResisterEvent(EventID.E_S2_01, HighlightShowButtonEvent);
            EventManager.Instance.ResisterEvent(EventID.E_S2_02, HighlightShowButtonEvent);

            EventManager.Instance.ResisterEvent(EventID.E_S4_01, HighlightShowButtonEvent);
            EventManager.Instance.ResisterEvent(EventID.E_S4_03, HighlightShowButtonEvent);
            EventManager.Instance.ResisterEvent(EventID.E_S4_05, HighlightShowButtonEvent);
            EventManager.Instance.ResisterEvent(EventID.E_S4_07, HighlightShowButtonEvent);
            EventManager.Instance.ResisterEvent(EventID.E_S4_09, HighlightShowButtonEvent);
            EventManager.Instance.ResisterEvent(EventID.E_S4_11, HighlightShowButtonEvent);

            EventManager.Instance.ResisterEvent(EventID.E_S5_04, HighlightShowButtonEvent);

            EventManager.Instance.ResisterEvent(EventID.E_S6_03, HighlightShowButtonEvent);

            EventManager.Instance.ResisterEvent(EventID.E_S8_05, HighlightShowButtonEvent);
        }

        protected override void Initialize()
        {
            Tool[] tools = ToolGrabber.Instance.GetInspectionTool(TransitionManager.Instance.CurrentScene);
            if (tools == null)
            {
                return;
            }

            foreach (Tool tool in tools)
            {
                OnUpdateInventory(Convert.ToInt32(tool));
            }
        }

        public void OnSubContentChanged(int subContentIndex)
        {
            // 사용하지 않음.
        }

        public void OnStepChanged(int stepIndex)
        {
            SetActivePanel(false);
            ((InventoryPanel)_panel).SetHighlightShowButton(false);
        }

        /// <summary>
        /// 점검도구 인벤토리 갱신.
        /// </summary>
        /// <param name="toolItemIndex"></param>
        public void OnUpdateInventory(int toolItemIndex)
        {
            ((InventoryPanel)_panel).UpdateItem(toolItemIndex);
        }

        /// <summary>
        /// 점검도구 선택.
        /// </summary>
        /// <param name="toolItemIndex"></param>
        public void OnSelectToolItem(int toolItemIndex)
        {
            ToolGrabber.Instance.SetGrabTool((Tool)toolItemIndex);
            SetActivePanel(false);
        }

        /// <summary>
        /// 인벤토리 열기 이벤트.
        /// </summary>
        /// <returns></returns>
        private IEnumerator ShowInventoryEvent()
        {
            SetActivePanel(true);
            yield return null;
        }

        /// <summary>
        /// 인벤토리 닫기 이벤트.
        /// </summary>
        /// <returns></returns>
        private IEnumerator HideInventoryEvent()
        {
            SetActivePanel(false);
            yield return null;
        }

        /// <summary>
        /// 인벤토리 열기 버튼 강조 이벤트.
        /// </summary>
        /// <returns></returns>
        private IEnumerator HighlightShowButtonEvent()
        {
            ((InventoryPanel)_panel).SetHighlightShowButton(true);
            yield return null;
        }
    }
}
