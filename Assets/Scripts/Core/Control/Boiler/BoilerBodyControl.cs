using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Futuregen
{
    public sealed class BoilerBodyControl : MonoBehaviour
    {
        private void CheckCorrectPaintingPart(int partIndex)
        {
            if (partIndex == 1 || partIndex == 2)
            {
                DialogEventArgs dialogEventArgs = new DialogEventArgs(DialogType.MessageBox)
                {
                    Title = "알림",
                    Message = "이 부위는 부식 불량 위치입니다.",
                };
                DialogManager.Instance.GenerateDialog(dialogEventArgs);
            }
        }

        private void CheckCorrectCorrosionPart(int partIndex)
        {
            if (partIndex == 3)
            {
                DialogEventArgs dialogEventArgs = new DialogEventArgs(DialogType.MessageBox)
                {
                    Title = "알림",
                    Message = "이 부위는 도장 불량 위치입니다.",
                };
                DialogManager.Instance.GenerateDialog(dialogEventArgs);
            }
        }
    }
}
