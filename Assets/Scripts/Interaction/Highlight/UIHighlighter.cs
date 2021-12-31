using UnityEngine;
using UnityEngine.UI;

namespace Futuregen
{
    public sealed class UIHighlighter : MonoBehaviour
    {
        [SerializeField]
        private Material _highlightMat;
        [SerializeField]
        private Color _highlightColor = Color.yellow;
        [SerializeField]
        [Range(1f, 5f)]
        private float _colorTweenSpeed = 1.0f;

        private Image _originUiImage;

        private GameObject _highlightUIObject;
        private Image _highlightUiImage;

        public bool IsActive { get; set; }

        private void Initialize()
        {
            _originUiImage = GetComponent<Image>();

            _highlightUIObject = new GameObject("UIHighlighter");
            _highlightUIObject.transform.SetParent(transform);
            _highlightUIObject.transform.localPosition = Vector3.zero;
            _highlightUIObject.transform.localScale = Vector3.one;

            _highlightUiImage = _highlightUIObject.AddComponent<Image>();
            _highlightUiImage.sprite = _originUiImage.sprite;
            _highlightUiImage.material = Instantiate(_highlightMat);
            _highlightUiImage.material.mainTexture = _originUiImage.mainTexture;
            _highlightUiImage.SetNativeSize();

            _highlightColor.a = 1f;
        }

        public void SetHighlight(bool active)
        {
            if (_highlightUIObject == null)
            {
                Initialize();
            }

            IsActive = active;
            _highlightUIObject.SetActive(active);
        }

        private void FixedUpdate()
        {
            if (IsActive == false)
            {
                return;
            }

            _highlightColor.a = Mathf.PingPong(Time.time * _colorTweenSpeed, 1f);
            _highlightUiImage.material.color = _highlightColor;
        }
    }
}
