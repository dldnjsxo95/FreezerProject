using UnityEngine;

namespace Futuregen
{
    public sealed class HelpItem : MonoBehaviour
    {
        private RectTransform _rectTrans;
        private Vector3 _startPosition;
        private Quaternion _startRotation;
        private Vector3 _startScale;

        public RectTransform RectTrans
        {
            get
            {
                if (_rectTrans == null)
                {
                    _rectTrans = GetComponent<RectTransform>();
                }
                return _rectTrans;
            }
            set => _rectTrans = value;
        }

        public void SetLerpStartTransform()
        {
            _startPosition = RectTrans.anchoredPosition3D;
            _startRotation = RectTrans.localRotation;
            _startScale = RectTrans.localScale;
        }

        public void Lerp(RectTransform end, float t)
        {
            float easing = Easing.InOutQuad(t);

            RectTrans.anchoredPosition3D = Vector3.Lerp(_startPosition, end.anchoredPosition3D, easing);
            RectTrans.localRotation = Quaternion.Lerp(_startRotation, end.localRotation, easing);
            RectTrans.localScale = Vector3.Lerp(_startScale, end.localScale, easing);
        }
    }
}
