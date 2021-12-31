using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Futuregen
{
    /// <summary>
    /// 콘텐츠 진행을 컨트롤하고, 해당 과정에 대한 데이터를 보유한다.
    /// </summary>
    public sealed class ContentManager : MonoSingleton<ContentManager>
    {
        // Inspector에서 실시간 확인 용도.
        [SerializeField] [ReadOnly] private int _mainContentIndex;
        [SerializeField] [ReadOnly] private int _stepIndex;
        [SerializeField] [ReadOnly] private int _subContentIndex;
        [SerializeField] [ReadOnly] private bool _isTraining;

        /// <summary>
        /// 현재 MainContent index.
        /// </summary>
        public int MainContentIndex
        {
            get => _mainContentIndex;
            private set => _mainContentIndex = value;
        }

        /// <summary>
        /// 현재 SubConent index.
        /// </summary>
        public int SubContentIndex
        {
            get => _subContentIndex;
            private set
            {
                if (value < 0 || value >= RepositoryHandler.Instance.ContentRepository.SubLength(MainContentIndex))
                {
                    Debug.LogWarning($"<color=lime>SubContent 범위를 벗어났습니다. (Index: {value})</color>");
                    return;
                }
                _subContentIndex = value;

                CurrentSubContent = RepositoryHandler.Instance.ContentRepository.FindSub(MainContentIndex, _subContentIndex);
                OnSubContentChanged?.Invoke(_subContentIndex);
            }
        }
        /// <summary>
        /// 현재 SubContent 데이터.
        /// </summary>
        public SubContent CurrentSubContent { get; private set; }

        /// <summary>
        /// 현재 Step index.
        /// </summary>
        public int StepIndex
        {
            get => _stepIndex;
            private set
            {
                if (value < 0 || value >= RepositoryHandler.Instance.StepRepository.Length(CurrentSubContent.ID))
                {
                    Debug.LogWarning($"<color=lime>Step 범위를 벗어났습니다. (Index: {value})</color>");
                    return;
                }
                _stepIndex = value;

                CurrentStep = RepositoryHandler.Instance.StepRepository.Find(CurrentSubContent.ID, _stepIndex);
                OnStepChanged?.Invoke(_stepIndex);
            }
        }
        /// <summary>
        /// 현재 Step 데이터.
        /// </summary>
        public Step CurrentStep { get; private set; }

        /// <summary>
        /// 현재 학습 상태.
        /// </summary>
        public bool IsTraining
        {
            get => _isTraining;
            private set => _isTraining = value;
        }

        /// <summary>
        /// SubContent 변경 이벤트.
        /// <see cref="IContentListener.OnSubContentChanged(int)"/>
        /// </summary>
        public UnityAction<int> OnSubContentChanged;
        /// <summary>
        /// Step 변경 이벤트.
        /// <see cref="IContentListener.OnStepChanged(int)"/>
        /// </summary>
        public UnityAction<int> OnStepChanged;

        private void Awake()
        {
            MainContentIndex = Convert.ToInt32(TransitionManager.Instance.CurrentScene);
        }

        private IEnumerator Start()
        {
            IsTraining = RepositoryHandler.Instance.ContentRepository.Find(MainContentIndex).IsTraining;
            string trainingState = IsTraining ? "실습" : "학습";
            Debug.Log($"<color=lime>MainContent {trainingState} 시작. (Index: {MainContentIndex})</color>");

            // 다른 클래스 초기화가 끝나고 콘텐츠가 시작되야 하므로 한 프레임 대기한다.
            yield return new WaitForEndOfFrame();
            StartSubContent(TransitionManager.Instance.SubContentStartIndex);
        }

        private void OnDestroy()
        {
            OnSubContentChanged = null;
            OnStepChanged = null;
        }

        /// <summary>
        /// 선택한 SubContent 시작.
        /// </summary>
        /// <param name="subContentIndex">시작할 SubContent index.</param>
        /// <param name="stepIndex">값이 없을 경우 처음 Step부터 시작.</param>
        public void StartSubContent(int subContentIndex, int stepIndex = 0)
        {
            SubContentIndex = subContentIndex;

            // 두 데이터가 다른 값이면 제대로 된 콘텐츠 범위가 아님.
            if (SubContentIndex != subContentIndex)
            {
                // 더 이상 진행할 SubContent가 없다면 퀴즈로 넘어간다.
                if (subContentIndex >= RepositoryHandler.Instance.ContentRepository.SubLength(MainContentIndex))
                {
                    // TODO: Main Scene은 바로 SceneLoad.
                    // TODO: EventManager로 제어하는 방식으로 생각중...                    
                    Debug.Log($"<color=lime>모든 SubContent 종료.</color>");
                }
                return;
            }
            Debug.Log($"<color=lime>SubContent 시작. (ID: {CurrentSubContent.ID}, Index: {SubContentIndex})</color>");

            StartStep(stepIndex);
        }

        /// <summary>
        /// 이전 Step으로 이동.
        /// </summary>
        public void StartPrevStep()
        {
            // 처음 Step인 경우.
            if (StepIndex <= 0)
            {
                // 이전 SubContent가 있는 경우.
                if (SubContentIndex - 1 >= 0)
                {
                    // 이전 SubContent의 마지막 Step으로 이동한다.
                    SubContent prevSubContent = RepositoryHandler.Instance.ContentRepository.FindSub(MainContentIndex, SubContentIndex - 1);
                    int lastStepIndex = RepositoryHandler.Instance.StepRepository.Length(prevSubContent.ID) - 1;

                    StartSubContent(SubContentIndex - 1, lastStepIndex);
                }
                return;
            }

            StartStep(StepIndex - 1);
        }

        /// <summary>
        /// 다음 Step으로 이동.
        /// </summary>
        public void StartNextStep()
        {
            // 마지막 Step인 경우.
            if (StepIndex + 1 >= RepositoryHandler.Instance.StepRepository.Length(CurrentSubContent.ID))
            {
                // 다음 SubContent의 처음 Step으로 이동한다.
                StartSubContent(SubContentIndex + 1);
                return;
            }

            StartStep(StepIndex + 1);
        }

        /// <summary>
        /// 선택한 Step 시작.
        /// </summary>
        /// <param name="stepIndex">시작할 Step index.</param>
        private void StartStep(int stepIndex)
        {
            // 모든 프로세스 정지.
            StopAllCoroutines();
            EventManager.Instance.StopAllCoroutines();

            // 선택한 Step으로 데이터를 변경하고 시작.
            StepIndex = stepIndex;
            StartCoroutine(StepRoutine(CurrentStep));
        }

        /// <summary>
        /// Step 진행 프로세스.
        /// </summary>
        /// <param name="step">현재 Step 데이터.</param>
        /// <returns></returns>
        private IEnumerator StepRoutine(Step step)
        {
            Debug.Log($"<color=lime>Step 시작. (Index: {StepIndex})</color>");

            // 이벤트 실행.
            EventManager.Instance.InvokeEvent(step.EventId);

            // 등록된 이벤트가 모두 종료될 때까지 대기한다.
            while (!EventManager.Instance.CheckEventEnd())
            {
                yield return null;
            }
            Debug.Log($"<color=lime>Step 종료. (Index: {StepIndex})</color>");

            // 이벤트가 아예 없는 경우는 내레이션 재생이 끝나면 바로 다음 Step으로 넘어간다.
            if (!EventManager.Instance.CurrentEventID.Equals(EventID.NONE))
            {
                StartNextStep();
            }
        }
    }
}
