using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Futuregen
{
    public class DialogPanel : BasePanel
    {
        [Header("Dialog")]
        [SerializeField]
        private Text _title;
        [SerializeField]
        private Text _message;
        [SerializeField]
        private Button _firstButton;
        [SerializeField]
        private Button _secondButton;
        [SerializeField]
        private Text _firstButtonText;
        [SerializeField]
        private Text _secondButtonText;

        [SerializeField]
        private Image _dialogBackGround;
        [SerializeField]
        private Image[] _buttonBackGrounds;

        public void UpdateData(DialogEventArgs dialogEventArgs)
        {
            _title.text = dialogEventArgs.Title;
            _message.text = dialogEventArgs.Message;

            switch (dialogEventArgs.Type)
            {
                case DialogType.QuestionBox:
                    _secondButton.gameObject.SetActive(true);
                    break;
                case DialogType.MessageBox:
                    _secondButton.gameObject.SetActive(false);
                    break;
                default:
                    break;
            }

            _firstButtonText.text = dialogEventArgs.FirstButtonText;
            _secondButtonText.text = dialogEventArgs.SecondButtonText;

            _firstButton.onClick.RemoveAllListeners();
            _secondButton.onClick.RemoveAllListeners();

            _firstButton.onClick.AddListener(() => OnHide());
            _secondButton.onClick.AddListener(() => OnHide());

            _firstButton.onClick.AddListener(dialogEventArgs.FirstButtonEvent);

            _secondButton.onClick.AddListener(dialogEventArgs.SecondButtonEvent);
        }

        public void SetDialogTexture(Sprite background, Sprite button)
        {
            _dialogBackGround.sprite = background;

            foreach (var buttonBackground in _buttonBackGrounds)
            {
                buttonBackground.sprite = button;
            }
        }
    }
}
