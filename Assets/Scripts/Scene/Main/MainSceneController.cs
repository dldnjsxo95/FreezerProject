using System.Collections;
using UnityEngine;

namespace Futuregen
{
    public sealed class MainSceneController : InteractionController
    {
        protected override void OnEnable()
        {
            base.OnEnable();

            RegisterInteractionEvent(EventID.M_S1_00, ShowGuideArrowEvent);
            RegisterInteractionEvent(EventID.M_S1_01, SelectEducationEvent);
        }

        protected override void UpdateCameraLocation(int subContentIndex)
        {

        }

        protected override void ResetController()
        {
            base.ResetController();

            foreach (GameObject obj in _interactableObjects)
            {
                obj.SetActive(false);
            }
        }

        public override void RunInteraction(int index)
        {
            StartCoroutine(InteractionRoutine(index));
        }

        private IEnumerator InteractionRoutine(int index)
        {
            Debug.Log("등장");

            yield return null;

            DoneInteraction(index);
        }

        public IEnumerator ShowGuideArrowEvent()
        {
            if (CameraManager.Instance.IsArriveLocation(CameraTargetID.BoilerFront))
            {
                CameraManager.Instance.SetCameraLookTarget(CameraTargetID.BoilerFrontNear);
            }

            _interactableObjects[0].SetActive(true);

            yield return new WaitUntil(() => CameraManager.Instance.IsArriveLocation(CameraTargetID.BoilerFront));
        }

        public IEnumerator SelectEducationEvent()
        {
            _interactionCheckList.Clear();
            _interactionCheckList.Add(1, false);

            // 기본 학습 설정.
            TransitionManager.Instance.NextScene = SceneType.EquipmentInspection;

            if (!CameraManager.Instance.IsArriveLocation(CameraTargetID.BoilerFront))
            {
                CameraManager.Instance.SetCameraLookTarget(CameraTargetID.BoilerFront);
            }

            _interactableObjects[1].SetActive(true);

            yield return new WaitUntil(() => !_interactionCheckList.ContainsValue(false));
        }
    }
}
