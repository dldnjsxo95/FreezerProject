using UnityEngine;
using UnityEngine.Events;

namespace Futuregen
{
    public sealed class SettingsPresenter : BasePresenter
    {
        public static UnityAction OnSettingValueChanged;

        protected override void Initialize()
        {
            // 사운드 설정 로드.
            ((SettingsPanel)_panel).SetMasterVolume(PlayerPrefs.GetFloat(SoundSettingPrefsKey.Master.ToString(), 100));
            ((SettingsPanel)_panel).SetBGMVolume(PlayerPrefs.GetFloat(SoundSettingPrefsKey.BGM.ToString(), 100));
            ((SettingsPanel)_panel).SetSFXVolume(PlayerPrefs.GetFloat(SoundSettingPrefsKey.SFX.ToString(), 100));
            ((SettingsPanel)_panel).SetNarrationVolume(PlayerPrefs.GetFloat(SoundSettingPrefsKey.Narration.ToString(), 100));
            // 사운드 설정 이벤트 호출.
            OnSettingValueChanged?.Invoke();

            // 그래픽 설정 로드.
            int qualityIndex = PlayerPrefs.GetInt("Quality", 0);

            ((SettingsPanel)_panel).SetQualitySetting(qualityIndex);
            QualitySettings.SetQualityLevel(qualityIndex);

            // 화면모드 로드.
            bool fullScreen = PlayerPrefs.GetInt("ScreenMode", Screen.fullScreen ? 1 : 0) != 0;
            ((SettingsPanel)_panel).SetFullScreenMode(fullScreen);

            // 해상도 로드.
#if UNITY_STANDALONE_WIN
            int width = PlayerPrefs.GetInt("ResWidth", 1600);
            int height = PlayerPrefs.GetInt("ResHeight", 900);
#elif UNITY_WEBGL
            int width = PlayerPrefs.GetInt("ResWidth", 1280);
            int height = PlayerPrefs.GetInt("ResHeight", 720);
#endif
            int resolutionIndex = 0;
            if (width == 1280 && height == 720)
            {
                resolutionIndex = 0;
            }
            else if (width == 1600 && height == 900)
            {
                resolutionIndex = 1;
            }
            else if (width == 1920 && height == 1080)
            {
                resolutionIndex = 2;
            }
            else if (width == 2560 && height == 1440)
            {
                resolutionIndex = 3;
            }
            ((SettingsPanel)_panel).SetResolution(resolutionIndex);

            Screen.SetResolution(width, height, fullScreen);
        }

        protected override void ShowPanel()
        {
            EventManager.Instance.InvokeUniqueEvent(EventID.NA_ANI_STOP);
            base.ShowPanel();
        }

        protected override void HidePanel()
        {
            EventManager.Instance.InvokeUniqueEvent(EventID.NA_ANI_PLAY);
            base.HidePanel();
        }

        /// <summary>
        /// 전체 볼륨 조절.
        /// </summary>
        /// <param name="volume"></param>
        public void OnChangedMasterVolume(float volume)
        {
            PlayerPrefs.SetFloat(SoundSettingPrefsKey.Master.ToString(), volume);
            PlayerPrefs.Save();

            ((SettingsPanel)_panel).SetMasterVolume(volume);

            OnSettingValueChanged?.Invoke();
        }

        /// <summary>
        /// 배경음 볼륨 조절.
        /// </summary>
        /// <param name="volume"></param>
        public void OnChangedBGMVolume(float volume)
        {
            PlayerPrefs.SetFloat(SoundSettingPrefsKey.BGM.ToString(), volume);
            PlayerPrefs.Save();

            ((SettingsPanel)_panel).SetBGMVolume(volume);

            OnSettingValueChanged?.Invoke();
        }

        /// <summary>
        /// 효과음 볼륨 조절.
        /// </summary>
        /// <param name="volume"></param>
        public void OnChangedSFXVolume(float volume)
        {
            PlayerPrefs.SetFloat(SoundSettingPrefsKey.SFX.ToString(), volume);
            PlayerPrefs.Save();

            ((SettingsPanel)_panel).SetSFXVolume(volume);

            OnSettingValueChanged?.Invoke();
        }

        /// <summary>
        /// 내레이션 볼륨 조절.
        /// </summary>
        /// <param name="volume"></param>
        public void OnChangedNarrationVolume(float volume)
        {
            PlayerPrefs.SetFloat(SoundSettingPrefsKey.Narration.ToString(), volume);
            PlayerPrefs.Save();

            ((SettingsPanel)_panel).SetNarrationVolume(volume);

            OnSettingValueChanged?.Invoke();
        }

        /// <summary>
        /// 그래픽 설정 변경.
        /// </summary>
        /// <param name="index"></param>
        public void OnChangedQualitySetting(int index)
        {
            PlayerPrefs.SetInt("Quality", index);
            PlayerPrefs.Save();

            QualitySettings.SetQualityLevel(index);
        }

        /// <summary>
        /// 전체화면, 창화면 전환.
        /// </summary>
        /// <param name="index"></param>
        public void OnChangedScreenMode(int index)
        {
            bool isFullScreen = index != 0;

            PlayerPrefs.SetInt("ScreenMode", isFullScreen ? 1 : 0);
            PlayerPrefs.Save();

            Screen.fullScreen = isFullScreen;
        }

        /// <summary>
        /// 해상도 변경.
        /// </summary>
        /// <param name="index"></param>
        public void OnChangedResolution(int index)
        {
            int width, height;
            switch (index)
            {
                case 0:
                    width = 1280; height = 720;
                    break;
                case 1:
                    width = 1600; height = 900;
                    break;
                case 2:
                    width = 1920; height = 1080;
                    break;
                case 3:
                    width = 2560; height = 1440;
                    break;
                default:
                    Debug.LogError("Index와 일치하는 해상도가 없습니다.");
                    return;
            }

            PlayerPrefs.SetInt("ResWidth", width);
            PlayerPrefs.SetInt("ResHeight", height);
            PlayerPrefs.Save();

            Screen.SetResolution(width, height, Screen.fullScreen);
        }

        /// <summary>
        /// 변경사항 확인 버튼 클릭.
        /// </summary>
        public void OnSaveChange()
        {
            DialogEventArgs dialogEventArgs = new DialogEventArgs(DialogType.QuestionBox)
            {
                Title = "알림",
                Message = "변경사항을 저장하시겠습니까?",
                FirstButtonEvent = () => SetActivePanel(false),
            };
            DialogManager.Instance.GenerateDialog(dialogEventArgs);
        }
    }
}
