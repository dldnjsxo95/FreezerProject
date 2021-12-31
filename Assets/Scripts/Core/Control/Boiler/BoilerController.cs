using System.Collections;
using UnityEngine;

namespace Futuregen
{
    public sealed class BoilerController : InteractionController
    {
        [SerializeField] private GameObject _paintingObj;
        [SerializeField] private BoilerBodyControl _body;
        [SerializeField] private SteamValveControl _steamValve;
        [SerializeField] private BurnerBlowerFanControl _burnerBlowerFan;
        [SerializeField] private BurnerAirDamper _burnerAirDamper;

        protected override void OnEnable()
        {
            base.OnEnable();

            // 보일러.
            RegisterInteractionEvent(EventID.E_S0_00, BoilerIntroEvent);
            RegisterInteractionEvent(EventID.E_S0_01, HighlightPaintingPart);
            RegisterInteractionEvent(EventID.E_S0_02, HighlightCorrosionPart);
            RegisterInteractionEvent(EventID.E_S0_03, HighlightBody2);
            RegisterInteractionEvent(EventID.E_S0_04, BoilerBlisterEvent);
            RegisterInteractionEvent(EventID.E_S0_05, BoilerIntroEvent);
            RegisterInteractionEvent(EventID.E_S0_06, HighlightLineEvent);
            RegisterInteractionEvent(EventID.E_S0_07, HighlightBodyElectricEvent);

            // 주증기 벨브.
            RegisterInteractionEvent(EventID.E_S1_00, SteamValve);
            RegisterInteractionEvent(EventID.E_S1_01, SteamValveCover);
            RegisterInteractionEvent(EventID.E_S1_02, HighlightStreamValveLeakEvent);
            RegisterInteractionEvent(EventID.E_S1_03, HighlightStreamValveEvent);

            // 송풍기.
            RegisterInteractionEvent(EventID.E_S2_00, Air1);
            RegisterInteractionEvent(EventID.E_S2_01, Air2);
            RegisterInteractionEvent(EventID.E_S2_02, Air3);
            RegisterInteractionEvent(EventID.E_S2_03, Air4);

            // 댐퍼.
            RegisterInteractionEvent(EventID.E_S3_00, Damper1);
            RegisterInteractionEvent(EventID.E_S3_01, Damper2);
        }

        protected override void UpdateCameraLocation(int subContentIndex)
        {
            switch (subContentIndex)
            {
                case 0:
                    CameraManager.Instance.SetCameraLookTarget(CameraTargetID.BoilerFront);
                    break;
                case 1:
                    CameraManager.Instance.SetCameraLookTarget(CameraTargetID.SteamValve);
                    break;
                case 2:
                    CameraManager.Instance.SetCameraLookTarget(CameraTargetID.BurnerBlowerFan);
                    break;
                case 3:
                    CameraManager.Instance.SetCameraLookTarget(CameraTargetID.BurnerAirDamper);
                    break;
                default:
                    break;
            }
        }

        protected override void ResetController()
        {
            base.ResetController();

            _paintingObj.SetActive(false);
            _burnerBlowerFan.SetActiveSelectMenu(false);
            _burnerAirDamper.ResetMove();

            ((InteractableObject)_interactables[6]).OnReset();
        }

        public override void RunInteraction(int index)
        {
            if (!CheckInteractable(index))
            {
                return;
            }

            switch (EventManager.Instance.CurrentEventID)
            {
                case EventID.E_S0_01:
                    CheckCorrectPaintingPart(index);
                    break;
                case EventID.E_S0_02:
                    CheckCorrectCorrosionPart(index);
                    break;
                case EventID.E_S0_04:
                    if (!ToolGrabber.Instance.CurrentSelectedTool.Equals(Tool.Tool0))
                    {
                        DialogEventArgs dialogEventArgs = new DialogEventArgs(DialogType.MessageBox)
                        {
                            Title = "알림",
                            Message = "블리스터 검사를 위해 초음파 검사기를 사용하세요.",
                        };
                        DialogManager.Instance.GenerateDialog(dialogEventArgs);

                        return;
                    }
                    break;
                case EventID.E_S0_06:
                    if (!ToolGrabber.Instance.CurrentSelectedTool.Equals(Tool.Tool2))
                    {
                        DialogEventArgs dialogEventArgs = new DialogEventArgs(DialogType.MessageBox)
                        {
                            Title = "알림",
                            Message = "멀티미더를 사용하세요.",
                        };
                        DialogManager.Instance.GenerateDialog(dialogEventArgs);

                        return;
                    }
                    break;
                case EventID.E_S0_07:
                    if (!ToolGrabber.Instance.CurrentSelectedTool.Equals(Tool.Tool2))
                    {
                        DialogEventArgs dialogEventArgs = new DialogEventArgs(DialogType.MessageBox)
                        {
                            Title = "알림",
                            Message = "멀티미더를 사용하세요.",
                        };
                        DialogManager.Instance.GenerateDialog(dialogEventArgs);

                        return;
                    }
                    break;
                case EventID.E_S1_02:
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
                    break;
                case EventID.E_S2_01:
                    if (!ToolGrabber.Instance.CurrentSelectedTool.Equals(Tool.Tool9))
                    {
                        DialogEventArgs dialogEventArgs = new DialogEventArgs(DialogType.MessageBox)
                        {
                            Title = "알림",
                            Message = "소음 측정기를 사용하세요.",
                        };
                        DialogManager.Instance.GenerateDialog(dialogEventArgs);

                        return;
                    }
                    break;
                case EventID.E_S2_02:
                    if (!ToolGrabber.Instance.CurrentSelectedTool.Equals(Tool.Tool8))
                    {
                        DialogEventArgs dialogEventArgs = new DialogEventArgs(DialogType.MessageBox)
                        {
                            Title = "알림",
                            Message = "진동 측정기를 사용하세요.",
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

        private IEnumerator BoilerIntroEvent()
        {
            SetInteractionObject(true, true, false, 0);

            while (true)
            {
                yield return null;
            }
        }

        public IEnumerator HighlightPaintingPart()
        {
            _paintingObj.SetActive(true);
                        
            SetCompleteTarget(3);
            SetHighlightObject(true, 1, 2, 3);

            yield return new WaitUntil(() => _interactionCheckList.ContainsValue(false) == false);
        }

        private void CheckCorrectPaintingPart(int partIndex)
        {
            if (partIndex == 1 || partIndex == 2)
            {
                DialogEventArgs dialogEventArgs = new DialogEventArgs(DialogType.MessageBox)
                {
                    Title = "알림",
                    Message = "이 부위는 부식 불량 위치입니다.",
                };
                DialogManager.Instance.GenerateDialog(dialogEventArgs);
            }
        }

        public IEnumerator HighlightCorrosionPart()
        {
            _paintingObj.SetActive(true);

            SetCompleteTarget(1, 2);
            SetHighlightObject(false, 1, 2, 3);

            yield return new WaitUntil(() => _interactionCheckList.ContainsValue(false) == false);
        }

        private void CheckCorrectCorrosionPart(int partIndex)
        {
            if (partIndex == 3)
            {
                DialogEventArgs dialogEventArgs = new DialogEventArgs(DialogType.MessageBox)
                {
                    Title = "알림",
                    Message = "이 부위는 도장 불량 위치입니다.",
                };
                DialogManager.Instance.GenerateDialog(dialogEventArgs);
            }
        }

        public IEnumerator HighlightBody2()
        {
            SetHighlightObject(false, 4);

            while (true)
            {
                yield return null;
            }
        }

        public IEnumerator BoilerBlisterEvent()
        {
            EventManager.Instance.InvokeUniqueEvent(EventID.INV_SHOW);

            SetCompleteTarget(5);
            SetHighlightObject(true, 5);

            yield return new WaitUntil(() => _interactionCheckList.ContainsValue(false) == false);
        }

        public IEnumerator HighlightLineEvent()
        {
            SetCompleteTarget(6);
            SetHighlightObject(true, 6);

            yield return new WaitUntil(() => _interactionCheckList.ContainsValue(false) == false);
        }

        public IEnumerator HighlightBodyElectricEvent()
        {
            _interactionCheckList.Add(5, false);

            SetHighlightObject(false, 5);

            yield return new WaitUntil(() => _interactionCheckList.ContainsValue(false) == false);
        }

        private IEnumerator SteamValve()
        {
            SetHighlightObject(true, 12);

            while (true)
            {
                yield return null;
            }
        }

        public IEnumerator SteamValveCover()
        {
            SetCompleteTarget(7);
            SetHighlightObject(false, 7);

            yield return new WaitUntil(() => _interactionCheckList.ContainsValue(false) == false);
        }

        public IEnumerator HighlightStreamValveLeakEvent()
        {
            SetCompleteTarget(8);
            SetHighlightObject(true, 8);

            yield return new WaitUntil(() => _interactionCheckList.ContainsValue(false) == false);
        }

        public IEnumerator HighlightStreamValveEvent()
        {
            SetCompleteTarget(9);
            SetHighlightObject(false, 9);
            ((InteractableObject)_interactables[9]).transform.localEulerAngles = Vector3.right * 100.0f;

            yield return new WaitUntil(() => _interactionCheckList.ContainsValue(false) == false);
        }

        public IEnumerator Air1()
        {
            SetHighlightObject(true, 10);

            while (true)
            {
                yield return null;
            }
        }

        public IEnumerator Air2()
        {
            SetCompleteTarget(10);
            SetHighlightObject(true, 10);

            yield return new WaitUntil(() => _interactionCheckList.ContainsValue(false) == false);
        }

        public IEnumerator Air3()
        {
            SetCompleteTarget(10);
            SetHighlightObject(true, 10);

            yield return new WaitUntil(() => _interactionCheckList.ContainsValue(false) == false);
        }

        public IEnumerator Air4()
        {
            _burnerBlowerFan.SetActiveSelectMenu(true);

            while (true)
            {
                yield return null;
            }
        }

        public IEnumerator Damper1()
        {
            SetHighlightObject(true, 11);

            while (true)
            {
                yield return null;
            }
        }

        public IEnumerator Damper2()
        {
            _burnerAirDamper.SetDamperMove();
            while (true)
            {
                yield return null;
            }
        }

    }
}
