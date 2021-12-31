using UnityEngine;

namespace Futuregen
{
    /// <summary>
    /// AudioSource�� �ִ� ������Ʈ�� �߰��ϰ�, ���� ������ �����ϸ� ������ ���� ���� �°� �����Ѵ�.
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
