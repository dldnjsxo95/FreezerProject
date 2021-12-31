using UnityEngine;
using UnityEngine.UI;

namespace Futuregen
{
    public sealed class AlphaIgnoreUI : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        }
    }
}
