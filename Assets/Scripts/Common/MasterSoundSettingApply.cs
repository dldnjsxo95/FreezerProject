using UnityEngine;

namespace Futuregen
{
    /// <summary>
    /// AudioListener가 있는 카메라에 추가하면, 전체 볼륨을 설정 값에 맞게 조절한다.
    /// </summary>
    [RequireComponent(typeof(AudioListener))]
    public sealed class MasterSoundSettingApply : SettingApply
    {
        protected override void OnSettingValueChanged()
        {
            AudioListener.volume = PlayerPrefs.GetFloat(_prefsKey.ToString(), 100) * 0.01f;
        }
    }
}
