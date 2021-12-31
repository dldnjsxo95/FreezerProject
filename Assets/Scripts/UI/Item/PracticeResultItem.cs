using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Futuregen
{
    public sealed class PracticeResultItem : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private Text _name;
        [SerializeField] private Text _playTime;
        [SerializeField] private Text _number;
        [SerializeField] private Text _score;
        [SerializeField] private Button _detailButton;

        public void SetData(bool oddEven, PracticeResult data, UnityAction call)
        {
            _background.enabled = oddEven;

            _name.text = data.Name;
            _playTime.text = TimeSpan.FromSeconds(data.PlayTime).ToString("mm':'ss");

            //_number.text = data.DetailedPracticeResults.Length.ToString();
            //_score.text = "00 / " + data.GetTotalScroe();
            _detailButton.onClick.AddListener(call);
        }

        public void UpdateData(PracticeResult data)
        {
            _playTime.text = TimeSpan.FromSeconds(data.PlayTime).ToString("mm':'ss");
            //_score.text = data.GetScroe() + " / " + data.GetTotalScroe();
        }
    }
}
