using System.Collections;
using UnityEngine;

namespace Futuregen
{
    public sealed class HotWaterTankController : InteractionController
    {
        [SerializeField] private PumpControl _pumpControl;

        protected override void OnEnable()
        {
            base.OnEnable();

            // 열교환기.
            RegisterInteractionEvent(EventID.E_S5_00, HeatExchangerIntroEvent);
            RegisterInteractionEvent(EventID.E_S5_01, () => HeatExchangerDirtyEvent(DirtyType.Corrosion));
            RegisterInteractionEvent(EventID.E_S5_02, () => HeatExchangerDirtyEvent(DirtyType.Aging));
            RegisterInteractionEvent(EventID.E_S5_03, HeatExchangerPressureEvent);
            RegisterInteractionEvent(EventID.E_S5_04, HeatExchangerTemperatureEvent);
            
            // 급탕탱크.
            RegisterInteractionEvent(EventID.E_S6_00, HotWaterTankIntroEvent);
            RegisterInteractionEvent(EventID.E_S6_01, () => HotWaterTankDirtyEvent(DirtyType.Corrosion));
            RegisterInteractionEvent(EventID.E_S6_02, () => HotWaterTankDirtyEvent(DirtyType.Aging));
            RegisterInteractionEvent(EventID.E_S6_03, HotWaterTankTemperatureEvent);

            // 급탕탱크 TC.
            RegisterInteractionEvent(EventID.E_S7_00, HotWaterTankTCIntroEvent);
            RegisterInteractionEvent(EventID.E_S7_01, HotWaterTankTCTemperatureEvent);
            RegisterInteractionEvent(EventID.E_S7_02, HotWaterTankTCTemperatureEvent);

            // 펌프.
            RegisterInteractionEvent(EventID.E_S8_00, PumpIntroEvent);
            RegisterInteractionEvent(EventID.E_S8_01, PumpOverloadEvent);
            RegisterInteractionEvent(EventID.E_S8_02, PumpIntroEvent);
            RegisterInteractionEvent(EventID.E_S8_03, () => PumpDirtyEvent(DirtyType.Corrosion));
            RegisterInteractionEvent(EventID.E_S8_04, () => PumpDirtyEvent(DirtyType.Aging));
            RegisterInteractionEvent(EventID.E_S8_05, () => PumpDirtyEvent(DirtyType.Leak));
        }

        protected override void UpdateCameraLocation(int subContentIndex)
        {
            switch (subContentIndex)
            {
                case 5:
                    CameraManager.Instance.SetCameraLookTarget(CameraTargetID.HeatExchanger);
                    break;
                case 6:
                    CameraManager.Instance.SetCameraLookTarget(CameraTargetID.HotWaterTank);
                    break;
                case 7:
                    CameraManager.Instance.SetCameraLookTarget(CameraTargetID.HotWaterTankTC);
                    break;
                case 8:
                    CameraManager.Instance.SetCameraLookTarget(CameraTargetID.Pump);
                    break;
                default:
                    break;
            }
        }

        protected override void ResetController()
        {
            base.ResetController();

            _pumpControl.SetActiveSelectMenu(false);
        }

        public override void RunInteraction(int index)
        {
            if (!CheckInteractable(index))
            {
                return;
            }

            switch (EventManager.Instance.CurrentEventID)
            {
                case EventID.E_S5_01:
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
                case EventID.E_S5_02:
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
                case EventID.E_S5_04:
                    if (!ToolGrabber.Instance.CurrentSelectedTool.Equals(Tool.Tool0))
                    {
                        DialogEventArgs dialogEventArgs = new DialogEventArgs(DialogType.MessageBox)
                        {
                            Title = "알림",
                            Message = "온도계를 사용하세요."
                        };
                        DialogManager.Instance.GenerateDialog(dialogEventArgs);

                        return;
                    }
                    break;
                case EventID.E_S6_01:
                    if (index == 9 || index == 10)
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
                case EventID.E_S6_02:
                    if (index == 8)
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
                case EventID.E_S6_03:
                    if (!ToolGrabber.Instance.CurrentSelectedTool.Equals(Tool.Tool0))
                    {
                        DialogEventArgs dialogEventArgs = new DialogEventArgs(DialogType.MessageBox)
                        {
                            Title = "알림",
                            Message = "온도계를 사용하세요."
                        };
                        DialogManager.Instance.GenerateDialog(dialogEventArgs);

                        return;
                    }
                    break;
                case EventID.E_S8_03:
                    if (index == 15 || index == 17)
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
                case EventID.E_S8_04:
                    if (index == 14 || index == 16 || index == 18)
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
                case EventID.E_S8_05:
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
                default:
                    break;
            }

            DoneInteraction(index);
        }

        private IEnumerator HeatExchangerIntroEvent()
        {
            SetHighlightObject(true, 0);

            while (true)
            {
                yield return null;
            }
        }

        private IEnumerator HeatExchangerDirtyEvent(DirtyType dirtyType)
        {
            if (dirtyType.Equals(DirtyType.Corrosion))
            {
                SetCompleteTarget(1, 2);
            }
            if (dirtyType.Equals(DirtyType.Aging))
            {
                SetCompleteTarget(3);
            }
            SetHighlightObject(true, 1, 2, 3);

            yield return new WaitUntil(() => _interactionCheckList.ContainsValue(false) == false);
        }

        private IEnumerator HeatExchangerPressureEvent()
        {
            SetHighlightObject(true, 4);

            while (true)
            {
                yield return null;
            }
        }

        private IEnumerator HeatExchangerTemperatureEvent()
        {
            SetCompleteTarget(5, 6);
            SetHighlightObject(true, 5, 6);

            yield return new WaitUntil(() => _interactionCheckList.ContainsValue(false) == false);
        }

        // Hot water tank.
        private IEnumerator HotWaterTankIntroEvent()
        {
            SetHighlightObject(true, 7);

            while (true)
            {
                yield return null;
            }
        }

        private IEnumerator HotWaterTankDirtyEvent(DirtyType dirtyType)
        {
            if (dirtyType.Equals(DirtyType.Corrosion))
            {
                SetCompleteTarget(8);
            }
            if (dirtyType.Equals(DirtyType.Aging))
            {
                SetCompleteTarget(9, 10);
            }
            SetHighlightObject(true, 8, 9, 10);

            yield return new WaitUntil(() => _interactionCheckList.ContainsValue(false) == false);
        }

        private IEnumerator HotWaterTankTemperatureEvent()
        {
            SetCompleteTarget(11);
            SetHighlightObject(true, 11);

            yield return new WaitUntil(() => _interactionCheckList.ContainsValue(false) == false);
        }

        // Hot water tank TC.
        private IEnumerator HotWaterTankTCIntroEvent()
        {
            SetHighlightObject(true, 11);

            while (true)
            {
                yield return null;
            }
        }

        private IEnumerator HotWaterTankTCTemperatureEvent()
        {
            SetHighlightObject(true, 12);

            while (true)
            {
                yield return null;
            }
        }

        // Pump.
        private IEnumerator PumpIntroEvent()
        {
            SetHighlightObject(true, 13);

            while (true)
            {
                yield return null;
            }
        }

        private IEnumerator PumpOverloadEvent()
        {
            _pumpControl.SetActiveSelectMenu(true);

            while (true)
            {
                yield return null;
            }
        }

        private IEnumerator PumpDirtyEvent(DirtyType dirtyType)
        {
            if (dirtyType.Equals(DirtyType.Corrosion))
            {
                SetCompleteTarget(14, 16, 18);
                SetHighlightObject(true, 14, 15, 16, 17, 18);
            }
            if (dirtyType.Equals(DirtyType.Aging))
            {
                SetCompleteTarget(15, 17);
                SetHighlightObject(true, 14, 15, 16, 17, 18);
            }
            if (dirtyType.Equals(DirtyType.Leak))
            {
                SetCompleteTarget(19, 20, 21, 22, 23);
                SetHighlightObject(true, 19, 20, 21, 22, 23);
            }            

            yield return new WaitUntil(() => _interactionCheckList.ContainsValue(false) == false);
        }
    }
}
