using UnityEngine;

namespace Futuregen
{
    /// <summary>
    /// AudioSource가 있는 오브젝트에 추가하고, 사운드 종류를 선택하면 볼륨을 설정 값에 맞게 조절한다.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public sealed class SoundSettingApply : SettingApply
    {
        [SerializeField] private AudioSource _audioSource;

        private void OnValidate()
        {
            if (_audioSource == null)
            {
                _audioSource = GetComponent<AudioSource>();
            }
        }

        protected override void OnSettingValueChanged()
        {
            _audioSource.volume = PlayerPrefs.GetFloat(_prefsKey.ToString(), 100) * 0.01f;
        }
    }
}
