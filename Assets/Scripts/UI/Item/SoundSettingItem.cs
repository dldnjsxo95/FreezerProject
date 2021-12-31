using UnityEngine;
using UnityEngine.UI;

namespace Futuregen
{
    public sealed class SoundSettingItem : MonoBehaviour
    {
        [SerializeField] private Slider _volume;
        [SerializeField] private Text _percentage;

        public void SetData(float volume)
        {
            _volume.SetValueWithoutNotify(volume);
            _percentage.text = string.Format("{0:0}%", volume);
        }
    }
}
