using UnityEngine;

namespace Futuregen
{
    /// <summary>
    /// ������ ĳ���� �� �� ����.
    /// </summary>
    public sealed class HoldCharacterTransform : MonoBehaviour
    {
        private void Update()
        {
            transform.position = transform.parent.position + Vector3.down * 1.5f;
            transform.eulerAngles = Vector3.up * transform.parent.localEulerAngles.y;
        }
    }
}
