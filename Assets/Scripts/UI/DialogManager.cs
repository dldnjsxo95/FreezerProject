using System;
using UnityEngine;
using UnityEngine.Events;

namespace Futuregen
{
    public class DialogEventArgs : EventArgs
    {
        public DialogEventArgs(DialogType dialogType)
        {
            Type = dialogType;

            switch (dialogType)
            {
                case DialogType.QuestionBox:
                    FirstButtonText = "예";
                    SecondButtonText = "아니오";
                    break;
                case DialogType.MessageBox:
                    FirstButtonText = "확인";
                    break;
                default:
                    break;
            }
        }

        public DialogType Type { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string FirstButtonText { get; set; }
        public string SecondButtonText { get; set; }
        public UnityAction FirstButtonEvent { get; set; }
        public UnityAction SecondButtonEvent { get; set; }
    }

    public class DialogManager : MonoSingleton<DialogManager>
    {
        [SerializeField]
        private DialogPanel _dialogPanel;

        [SerializeField]
        private Sprite[] _dialogBackSprites;
        [SerializeField]
        private Sprite[] _dialogButtonSprites;

        public void GenerateDialog(DialogEventArgs dialogEventArgs, bool isWarning = false)
        {
            EventManager.Instance.InvokeUniqueEvent(EventID.NA_ANI_STOP);

            dialogEventArgs.FirstButtonEvent += () => EventManager.Instance.InvokeUniqueEvent(EventID.NA_ANI_PLAY);
            dialogEventArgs.SecondButtonEvent += () => EventManager.Instance.InvokeUniqueEvent(EventID.NA_ANI_PLAY);

            _dialogPanel.UpdateData(dialogEventArgs);

            Sprite back = isWarning ? _dialogBackSprites[0] : _dialogBackSprites[1];
            Sprite button = isWarning ? _dialogButtonSprites[0] : _dialogButtonSprites[1];

            _dialogPanel.SetDialogTexture(back, button);

            _dialogPanel.OnShow();
        }
    }
}
