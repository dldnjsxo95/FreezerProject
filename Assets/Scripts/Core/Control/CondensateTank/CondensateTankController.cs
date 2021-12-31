using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Futuregen
{
    public class CondensateTankController : InteractionController
    {
        [SerializeField] private GameObject _guideArrow;

        protected override void OnEnable()
        {
            base.OnEnable();

            RegisterInteractionEvent(EventID.E_S9_00, CondensateTankIntroEvent);
            RegisterInteractionEvent(EventID.E_S9_01, () => CondensateTankDirtyEvent(DirtyType.Corrosion));
            RegisterInteractionEvent(EventID.E_S9_02, () => CondensateTankDirtyEvent(DirtyType.Leak));
            RegisterInteractionEvent(EventID.E_S9_03, () => CondensateTankDirtyEvent(DirtyType.Aging));
        }

        protected override void UpdateCameraLocation(int subContentIndex)
        {
            switch (subContentIndex)
            {
                case 9:
                    CameraManager.Instance.SetCameraLookTarget(CameraTargetID.CondensateTank);
                    break;
                default:
                    break;
            }
        }

        protected override void ResetController()
        {
            base.ResetController();

            _guideArrow.SetActive(false);
        }

        public override void RunInteraction(int index)
        {
            if (!CheckInteractable(index))
            {
                return;
            }

            switch (EventManager.Instance.CurrentEventID)
            {
                case EventID.E_S9_01:
                    if (index == 3)
                    {
                        DialogEventArgs dialogEventArgs = new DialogEventArgs(DialogType.MessageBox)
                        {
                            Title = "알림",
                            Message = "이 부위는 노후화 위치입니다."
                        };
                        DialogManager.Instance.GenerateDialog(dialogEventArgs);

                        return;
                    }
                    break;
                case EventID.E_S9_02:
                    if (!ToolGrabber.Instance.CurrentSelectedTool.Equals(Tool.Tool7))
                    {
                        DialogEventArgs dialogEventArgs = new DialogEventArgs(DialogType.MessageBox)
                        {
                            Title = "알림",
                            Message = "리크테스터기를 사용하세요."
                        };
                        DialogManager.Instance.GenerateDialog(dialogEventArgs);

                        return;
                    }
                    break;
                case EventID.E_S9_03:
                    Debug.Log(index);
                    if (index == 1 || index == 2)
                    {
                        DialogEventArgs dialogEventArgs = new DialogEventArgs(DialogType.MessageBox)
                        {
                            Title = "알림",
                            Message = "이 부위는 부식 불량 위치입니다."
                        };
                        DialogManager.Instance.GenerateDialog(dialogEventArgs);

                        return;
                    }
                    break;                
                default:
                    break;
            }

            DoneInteraction(index);
        }

        private IEnumerator CondensateTankIntroEvent()
        {
            SetHighlightObject(true, 0);

            while (true)
            {
                yield return null;
            }
        }

        private IEnumerator CondensateTankDirtyEvent(DirtyType dirtyType)
        {
            if (dirtyType.Equals(DirtyType.Corrosion))
            {
                _guideArrow.SetActive(true);

                SetCompleteTarget(1, 2);
                SetHighlightObject(true, 1, 2, 3);
            }
            if (dirtyType.Equals(DirtyType.Aging))
            {
                _guideArrow.SetActive(true);

                SetCompleteTarget(3);
                SetHighlightObject(true, 1, 2, 3);
            }
            if (dirtyType.Equals(DirtyType.Leak))
            {
                SetCompleteTarget(4, 5);
                SetHighlightObject(true, 4, 5);
            }

            yield return new WaitUntil(() => _interactionCheckList.ContainsValue(false) == false);
        }
    }
}
