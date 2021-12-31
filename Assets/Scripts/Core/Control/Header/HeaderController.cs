using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Futuregen
{
    public sealed class HeaderController : InteractionController
    {
        public InteractableObject[] _checkCircle;

        protected override void OnEnable()
        {
            base.OnEnable();

            RegisterInteractionEvent(EventID.E_S4_00, IntroHeaderEvent);
            RegisterInteractionEvent(EventID.E_S4_01, () => Header1(1));
            RegisterInteractionEvent(EventID.E_S4_02, () => Header2(1));
            RegisterInteractionEvent(EventID.E_S4_03, () => Header1(2));
            RegisterInteractionEvent(EventID.E_S4_04, () => Header2(2));
            RegisterInteractionEvent(EventID.E_S4_05, () => Header1(3));
            RegisterInteractionEvent(EventID.E_S4_06, () => Header2(3));
            RegisterInteractionEvent(EventID.E_S4_07, () => Header1(4));
            RegisterInteractionEvent(EventID.E_S4_08, () => Header2(4));
            RegisterInteractionEvent(EventID.E_S4_09, () => Header1(5));
            RegisterInteractionEvent(EventID.E_S4_10, () => Header2(5));
            RegisterInteractionEvent(EventID.E_S4_11, () => Header1(6));
            RegisterInteractionEvent(EventID.E_S4_12, () => Header2(6));
            RegisterInteractionEvent(EventID.E_S4_13, IntroHeaderEvent);
        }

        protected override void UpdateCameraLocation(int subContentIndex)
        {
            switch (subContentIndex)
            {
                case 4:
                    CameraManager.Instance.SetCameraLookTarget(CameraTargetID.Header);
                    break;
                default:
                    break;
            }
        }

        protected override void ResetController()
        {
            base.ResetController();
        }

        public override void RunInteraction(int index)
        {
            if (!CheckInteractable(index))
            {
                return;
            }

            switch (EventManager.Instance.CurrentEventID)
            {
                case EventID.E_S4_01:
                case EventID.E_S4_03:
                case EventID.E_S4_05:
                case EventID.E_S4_07:
                case EventID.E_S4_09:
                case EventID.E_S4_11:
                    if (_interactableObjects[index].GetComponent<HeaderDirtyChoice>()._dirtyType == 0)
                    {
                        if (!ToolGrabber.Instance.CurrentSelectedTool.Equals(Tool.Tool7))
                        {
                            DialogEventArgs dialogEventArgs = new DialogEventArgs(DialogType.MessageBox)
                            {
                                Title = "알림",
                                Message = "리크테스터기를 사용하세요.",
                            };
                            DialogManager.Instance.GenerateDialog(dialogEventArgs);

                            return;
                        }
                    }
                    else
                    {
                        DialogEventArgs dialogEventArgs = new DialogEventArgs(DialogType.MessageBox)
                        {
                            Title = "알림",
                            Message = "이 부위는 부식 불량 위치입니다.",
                        };
                        DialogManager.Instance.GenerateDialog(dialogEventArgs);

                        return;
                    }
                    break;
                case EventID.E_S4_02:
                case EventID.E_S4_04:
                case EventID.E_S4_06:
                case EventID.E_S4_08:
                case EventID.E_S4_10:
                case EventID.E_S4_12:
                    if (_interactableObjects[index].GetComponent<HeaderDirtyChoice>()._dirtyType != 1)
                    {
                        if (!ToolGrabber.Instance.CurrentSelectedTool.Equals(Tool.Tool7))
                        {
                            DialogEventArgs dialogEventArgs = new DialogEventArgs(DialogType.MessageBox)
                            {
                                Title = "알림",
                                Message = "이 부위는 누기 불량 위치입니다.",
                            };
                            DialogManager.Instance.GenerateDialog(dialogEventArgs);

                            return;
                        }
                    }
                    break;
                default:
                    break;
            }

            DoneInteraction(index);
        }

        private IEnumerator IntroHeaderEvent()
        {
            SetHighlightObject(true, 0);

            while (true)
            {
                yield return null;
            }
        }

        private IEnumerator Header1(int partIndex)
        {
            switch (partIndex)
            {
                case 1:
                    SetCompleteTarget(7, 9);
                    SetHighlightObject(true, 7, 8, 9);
                    break;
                case 2:
                    SetCompleteTarget(10);
                    SetHighlightObject(true, 10, 11, 12);
                    break;
                case 3:
                    SetCompleteTarget(14, 15);
                    SetHighlightObject(true, 13, 14, 15);
                    break;
                case 4:
                    SetCompleteTarget(16);
                    SetHighlightObject(true, 16, 17, 18);
                    break;
                case 5:
                    SetCompleteTarget(20);
                    SetHighlightObject(true, 19, 20, 21);
                    break;
                case 6:
                    SetCompleteTarget(24);
                    SetHighlightObject(true, 22, 23, 24);
                    break;
                default:
                    break;
            }
            

            yield return new WaitUntil(() => _interactionCheckList.ContainsValue(false) == false);
        }

        private IEnumerator Header2(int partIndex)
        {
            switch (partIndex)
            {
                case 1:
                    SetCompleteTarget(8);
                    SetHighlightObject(true, 7, 8, 9);
                    break;
                case 2:
                    SetCompleteTarget(11, 12);
                    SetHighlightObject(true, 10, 11, 12);
                    break;
                case 3:
                    SetCompleteTarget(13);
                    SetHighlightObject(true, 13, 14, 15);
                    break;
                case 4:
                    SetCompleteTarget(17, 18);
                    SetHighlightObject(true, 16, 17, 18);
                    break;
                case 5:
                    SetCompleteTarget(19, 21);
                    SetHighlightObject(true, 19, 20, 21);
                    break;
                case 6:
                    SetCompleteTarget(22, 23);
                    SetHighlightObject(true, 22, 23, 24);
                    break;
                default:
                    break;
            }


            yield return new WaitUntil(() => _interactionCheckList.ContainsValue(false) == false);
        }
    }
}
