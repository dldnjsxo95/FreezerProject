using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Futuregen
{
    [RequireComponent(typeof(SettingsPresenter))]
    public sealed class SettingsPanel : BasePanel
    {
        [Header("Settings")]
        [SerializeField] private RectTransform _settingsPanel;

        [Header("Sound Settings")]
        [SerializeField] private SoundSettingItem _master;
        [SerializeField] private SoundSettingItem _bgm;
        [SerializeField] private SoundSettingItem _sfx;
        [SerializeField] private SoundSettingItem _narration;

        [Header("Function Settings")]
        [SerializeField] private ToggleGroupEvent _quality;
        [SerializeField] private ToggleGroupEvent _fullscreen;
        [SerializeField] private Dropdown _resolution;

        private Vector2 _showPos = new Vector2(1.5f, -2.0f);
        private Vector2 _hidePos = new Vector2(-834.0f, 446.0f);

        public override void OnShow()
        {
            StartCoroutine(ShowHideAnimation(true));
        }

        public override void OnHide()
        {
            StartCoroutine(ShowHideAnimation(false));
        }

        protected override IEnumerator ShowHideAnimation(bool show)
        {
            _canvasGroup.blocksRaycasts = false;

            // 환경설정 열릴 때.
            if (show)
            {
                base.OnShow();
            }

            float startAlpha = show ? 0.0f : 1.0f;
            float endAlpha = show ? 1.0f : 0.0f;

            Vector2 startPos = show ? _hidePos : _showPos;
            Vector2 endPos = show ? _showPos : _hidePos;

            Vector3 startScale = show ? Vector3.zero : Vector3.one;
            Vector3 endScale = show ? Vector3.one : Vector3.zero;

            float elapsed = 0.0f;

            while (elapsed < _showHideTime)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / _showHideTime);

                _canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, Easing.OutQuad(t));

                _settingsPanel.anchoredPosition = Vector2.Lerp(startPos, endPos, Easing.OutQuad(t));
                _settingsPanel.localScale = Vector3.Lerp(startScale, endScale, Easing.OutQuad(t));
                yield return null;
            }

            // 환경설정 닫힐 떄.
            if (!show)
            {
                base.OnHide();
            }

            _canvasGroup.blocksRaycasts = true;
        }

        public void SetMasterVolume(float volume)
        {
            _master.SetData(volume);
        }

        public void SetBGMVolume(float volume)
        {
            _bgm.SetData(volume);
        }

        public void SetSFXVolume(float volume)
        {
            _sfx.SetData(volume);
        }

        public void SetNarrationVolume(float volume)
        {
            _narration.SetData(volume);
        }

        public void SetQualitySetting(int index)
        {
            _quality.SetActiveToggle(index);
        }

        public void SetFullScreenMode(bool isFullScreen)
        {
            _fullscreen.SetActiveToggle(isFullScreen ? 1 : 0);
        }

        public void SetResolution(int index)
        {
            _resolution.SetValueWithoutNotify(index);
        }
    }
}
