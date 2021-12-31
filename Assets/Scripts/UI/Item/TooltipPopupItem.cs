using UnityEngine;
using UnityEngine.UI;

namespace Futuregen
{
    public sealed class TooltipPopupItem : MonoBehaviour
    {
        [SerializeField] private int _index;
        [SerializeField] private Text _detailMessage;

        public int Index => _index;

        public void Show(Tooltip data)
        {
            gameObject.SetActive(true);

            if (string.IsNullOrEmpty(data.DetailMessage))
            {
                return;
            }

            _detailMessage.text = data.DetailMessage;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
