using System.Collections;
using UnityEngine;

namespace Futuregen
{
    public sealed class PreCheckPresenter : BasePresenter, IContentListener
    {
        private void OnEnable()
        {
            ContentManager.Instance.OnStepChanged += OnStepChanged;

            EventManager.Instance.ResisterEvent(EventID.M_S0_00, DrawingGuideMarkerEvent);
            EventManager.Instance.ResisterEvent(EventID.M_S2_00, SelectToolEvent);
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
            ((PreCheckPanel)_panel).OnHide();
        }

        public void OnSelectEducation(int educationIndex)
        {
            TransitionManager.Instance.NextScene = (SceneType)educationIndex;
        }

        public void OnCheckSelectTool()
        {
            SceneType nextScene = TransitionManager.Instance.NextScene;
            Tool[] correctToolList = ToolGrabber.Instance.GetInspectionTool(nextScene);
            bool result = ((PreCheckPanel)_panel).CheckCorrectTool(correctToolList);

            DialogEventArgs dialogEventArgs = new DialogEventArgs(DialogType.MessageBox)
            {
                Title = result ? "알림" : "경고",
                Message = result ? "정답입니다.\n다음 학습으로 이동합니다." : "오답입니다.\n다시 선택해주세요.",
                FirstButtonEvent = () =>
                {
                    if (result)
                    {
                        if (nextScene.Equals(SceneType.EquipmentInspection))
                        {
                            SceneLoader.Instance.LoadLoadingScene(SceneType.EquipmentInspection);
                        }
                        if (nextScene.Equals(SceneType.DailyInspection))
                        {
                            SceneLoader.Instance.LoadLoadingScene(SceneType.DailyInspection);
                        }
                    }
                },
            };
            DialogManager.Instance.GenerateDialog(dialogEventArgs, !result);
        }

        private IEnumerator DrawingGuideMarkerEvent()
        {
            ((PreCheckPanel)_panel).ShowDrawingPanel();

            while (true)
            {                
                yield return null;
            }
        }

        private IEnumerator SelectToolEvent()
        {
            SceneType nextScene = TransitionManager.Instance.NextScene;
            if (nextScene.Equals(SceneType.None))
            {
                TransitionManager.Instance.NextScene = SceneType.EquipmentInspection;
            }

            string title = "도구목록";
            if (nextScene.Equals(SceneType.EquipmentInspection))
            {
                title = "설비점검 " + title;
            }
            if (nextScene.Equals(SceneType.DailyInspection))
            {
                title = "일상점검 " + title;
            }

            ((PreCheckPanel)_panel).ShowToolPanel(title);

            while (true)
            {
                yield return null;
            }
        }
    }
}
