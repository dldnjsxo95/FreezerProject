using System.Collections;
using UnityEngine;

namespace Futuregen
{
    public sealed class NarrationPresenter : BasePresenter, IContentListener
    {
        [SerializeField] [Range(0.0f, 2.0f)] private float _narrationWaitTime = 0.0f;

        private void OnEnable()
        {
            ContentManager.Instance.OnStepChanged += OnStepChanged;

            EventManager.Instance.ResisterEvent(EventID.NA_ANI_PLAY, PlayButtonAnimationEvnet);
            EventManager.Instance.ResisterEvent(EventID.NA_ANI_STOP, PauseButtonAnimationEvent);
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
            // 내레이션 재생.
            Narration data = RepositoryHandler.Instance.NarrationRepository.Find(ContentManager.Instance.CurrentSubContent.ID, stepIndex);
            ((NarrationPanel)_panel).SetNarration(data);

            float playTime = data.Clip == null ? 0.0f : data.Clip.length;
            EventManager.Instance.SetNarrationPlayTime(playTime + _narrationWaitTime);

            // 버튼 상태 변경.
            int currentStepIndex = ContentManager.Instance.StepIndex;
            int lastStepIndex = RepositoryHandler.Instance.StepRepository.Length(ContentManager.Instance.CurrentSubContent.ID) - 1;

            int currentSubContentIndex = ContentManager.Instance.SubContentIndex;
            int lastSubContentIndex = RepositoryHandler.Instance.ContentRepository.SubLength(ContentManager.Instance.MainContentIndex) - 1;

            ((NarrationPanel)_panel).SetStepControllButton(
                currentSubContentIndex != 0 || currentStepIndex != 0,
                currentSubContentIndex != lastSubContentIndex || currentStepIndex != lastStepIndex + 1);
        }

        /// <summary>
        /// 버튼 애니메이션 실행 이벤트.
        /// </summary>
        /// <returns></returns>
        private IEnumerator PlayButtonAnimationEvnet()
        {
            ((NarrationPanel)_panel).PlayAnimation = true;
            yield return null;
        }

        /// <summary>
        /// 버튼 애니메이션 일시정지 이벤트.
        /// </summary>
        /// <returns></returns>
        private IEnumerator PauseButtonAnimationEvent()
        {
            ((NarrationPanel)_panel).PlayAnimation = false;
            yield return null;
        }
    }
}
