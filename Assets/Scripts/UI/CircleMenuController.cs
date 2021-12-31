using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Futuregen
{
    public sealed class CircleMenuController : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _childButtonGroup;
        private float _currentProgress = 0f;
        private bool _isOnPointer;

        [SerializeField]
        [Range(1f, 5f)]
        private float _easingSpeed;

        private void Awake()
        {
            _isOnPointer = false;
            StartCoroutine(ButtonGroupDisappear());
        }

        public void OnPointerEnter()
        {
            if (_isOnPointer == true)
            {
                return;
            }

            _isOnPointer = true;

            StartCoroutine(ButtonGroupAppear());
        }

        public void OnPointerExit()
        {
            if (_isOnPointer == false)
            {
                return;
            }

            _isOnPointer = false;

            StartCoroutine(ButtonGroupDisappear());
        }

        private IEnumerator ButtonGroupAppear()
        {
            float progress = _currentProgress;

            while (progress < 1f)
            {
                progress += Time.deltaTime * _easingSpeed;
                _childButtonGroup.transform.localScale = Vector3.one * Easing.OutQuad(progress);
                _childButtonGroup.alpha = progress;
                _currentProgress = progress;

                if (_isOnPointer == false)
                {
                    StartCoroutine(ButtonGroupDisappear());
                    yield break;
                }

                yield return null;
            }

            progress = 1f;
            _childButtonGroup.transform.localScale = Vector3.one * progress;
            _childButtonGroup.alpha = progress;
            _currentProgress = progress;
        }

        private IEnumerator ButtonGroupDisappear()
        {
            float progress = _currentProgress;

            while (progress > 0f)
            {
                progress -= Time.deltaTime * _easingSpeed;
                _childButtonGroup.transform.localScale = Vector3.one * Easing.OutQuad(progress);
                _childButtonGroup.alpha = progress;
                _currentProgress = progress;

                if (_isOnPointer == true)
                {
                    StartCoroutine(ButtonGroupAppear());
                    yield break;
                }

                yield return null;
            }

            progress = 0f;
            _childButtonGroup.transform.localScale = Vector3.one * progress;
            _childButtonGroup.alpha = progress;
            _currentProgress = progress;
        }

        public void OnClickHome()
        {
            if (TransitionManager.Instance.CurrentScene.Equals(SceneType.Home))
            {
                return;
            }

            DialogEventArgs dialogEventArgs = new DialogEventArgs(DialogType.QuestionBox)
            {
                Title = "알림",
                Message = "홈 화면으로 이동하시겠습니까?",
                FirstButtonEvent = () => SceneLoader.Instance.LoadLoadingScene(SceneType.Home),
            };
            DialogManager.Instance.GenerateDialog(dialogEventArgs);
        }

        public void OnClickExit()
        {
            DialogEventArgs dialogEventArgs = new DialogEventArgs(DialogType.QuestionBox)
            {
                Title = "프로그램 종료",
                Message = "종료 하시겠습니까?",
                FirstButtonEvent = () =>
                {
                    Application.Quit();
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#endif
                }
            };
            DialogManager.Instance.GenerateDialog(dialogEventArgs);
        }
    }
}
