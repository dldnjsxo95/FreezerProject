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
                    Title = "�˸�",
                    Message = "�� ������ �ν� �ҷ� ��ġ�Դϴ�.",
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
                    Title = "�˸�",
                    Message = "�� ������ ���� �ҷ� ��ġ�Դϴ�.",
                };
                DialogManager.Instance.GenerateDialog(dialogEventArgs);
            }
        }
    }
}
