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
            // ������� ����.
        }

        public void OnSubContentChanged(int subContentIndex)
        {
            // ������� ����.
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
                Title = result ? "�˸�" : "���",
                Message = result ? "�����Դϴ�.\n���� �н����� �̵��մϴ�." : "�����Դϴ�.\n�ٽ� �������ּ���.",
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

            string title = "�������";
            if (nextScene.Equals(SceneType.EquipmentInspection))
            {
                title = "�������� " + title;
            }
            if (nextScene.Equals(SceneType.DailyInspection))
            {
                title = "�ϻ����� " + title;
            }

            ((PreCheckPanel)_panel).ShowToolPanel(title);

            while (true)
            {
                yield return null;
            }
        }
    }
}
