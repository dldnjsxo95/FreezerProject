using UnityEngine;

namespace Futuregen
{
    /// <summary>
    /// PlayerPrefs에 저장된 각 사운드의 설정 값을 종류에 따라 구분하고 적용한다.
    /// </summary>
    public abstract class SettingApply : MonoBehaviour
    {
        [SerializeField] protected SoundSettingPrefsKey _prefsKey;

        protected void Awake()
        {
            if (_prefsKey == SoundSettingPrefsKey.None)
            {
                Debug.LogWarning(name + "의 PlayerPrefs key가 설정되지 않았습니다.");
            }
        }

        protected void OnEnable()
        {
            if (_prefsKey != SoundSettingPrefsKey.None)
            {
                SettingsPresenter.OnSettingValueChanged += OnSettingValueChanged;
                OnSettingValueChanged();
            }
        }

        protected void OnDisable()
        {
            if (_prefsKey != SoundSettingPrefsKey.None)
            {
                SettingsPresenter.OnSettingValueChanged -= OnSettingValueChanged;
            }
        }

        /// <summary>
        /// 설정 값이 변경될 때마다 호출.
        /// </summary>
        protected abstract void OnSettingValueChanged();
    }
}
