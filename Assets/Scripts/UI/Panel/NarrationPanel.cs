using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Futuregen
{
    [RequireComponent(typeof(NarrationPresenter))]
    public sealed class NarrationPanel : BasePanel
    {
        [Header("Narration")]
        [SerializeField] private CanvasGroup _narrationPanel;
        [SerializeField] private Text _narrationText;
        [SerializeField] private AudioSource _narrationPlayer;

        [Header("Function Buttons")]
        [SerializeField] [Range(0.0f, 2.0f)] private float _buttonMoveSpeed = 1.6f;
        [SerializeField] [Range(0.0f, 10.0f)] private float _buttonMoveDistance = 4.0f;
        [SerializeField] private UIHighlighter _prevStepButton;
        [SerializeField] private UIHighlighter _nextStepButton;
        [SerializeField] private GameObject _showButton;
        [SerializeField] private GameObject _hideButton;

        private RectTransform _rectTrans;
        private float _showY = -4.0f;
        private float _hideY = -154.0f;

        private RectTransform _prevStepRectTrans;
        private float _prevStepStartX = -132.0f;

        private RectTransform _nextStepRectTrans;
        private float _nextStepStartX = -48.0f;

        private float _buttonAnimationTime = 0.0f;
        private bool _playAnimation = true;

        public bool PlayAnimation
        {
            set
            {
                _playAnimation = value;

                _prevStepButton.IsActive = _playAnimation;
                _nextStepButton.IsActive = _playAnimation;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _rectTrans = _content.GetComponent<RectTransform>();

            _prevStepButton.SetHighlight(true);
            _prevStepRectTrans = _prevStepButton.GetComponent<RectTransform>();

            _nextStepButton.SetHighlight(true);
            _nextStepRectTrans = _nextStepButton.GetComponent<RectTransform>();

            _showButton.SetActive(false);
            _hideButton.SetActive(true);
        }

        private void FixedUpdate()
        {
            if (!_playAnimation)
            {
                return;
            }

            _buttonAnimationTime = Mathf.PingPong(Time.time * _buttonMoveSpeed, 1.0f);

            ButtonAnimation(true);
            ButtonAnimation(false);
        }

        public override void OnShow()
        {
            StartCoroutine(ShowHideAnimation(true));
        }

        public override void OnHide()
        {
            StartCoroutine(ShowHideAnimation(false));
        }

        public override bool IsActive()
        {
            return _narrationPanel.blocksRaycasts;
        }

        protected override IEnumerator ShowHideAnimation(bool show)
        {
            _canvasGroup.blocksRaycasts = false;

            _narrationPanel.blocksRaycasts = show;
            _showButton.SetActive(!show);
            _hideButton.SetActive(show);

            Vector2 startPos = new Vector2(_rectTrans.anchoredPosition.x, show ? _hideY : _showY);
            Vector2 endPos = new Vector2(_rectTrans.anchoredPosition.x, show ? _showY : _hideY);

            float startAlpha = show ? 0.0f : 1.0f;
            float endAlpha = show ? 1.0f : 0.0f;

            float elapsed = 0.0f;

            while (elapsed < _showHideTime)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / _showHideTime);

                _rectTrans.anchoredPosition = Vector2.Lerp(startPos, endPos, Easing.OutQuad(t));
                _narrationPanel.alpha = Mathf.Lerp(startAlpha, endAlpha, Easing.OutQuad(t));

                yield return null;
            }

            _canvasGroup.blocksRaycasts = true;
        }

        public void SetNarration(Narration data)
        {
            _narrationText.text = data.Text;

            if (data.Clip != null)
            {
                _narrationPlayer.clip = data.Clip;
                _narrationPlayer.Play();
            }
        }

        public void SetStepControllButton(bool prevActive, bool nextActive)
        {
            _prevStepButton.gameObject.SetActive(prevActive);
            _nextStepButton.gameObject.SetActive(nextActive);
        }

        private void ButtonAnimation(bool left)
        {
            RectTransform targetRectTrans = left ? _prevStepRectTrans : _nextStepRectTrans;

            float startX = left ? _prevStepStartX : _nextStepStartX;
            float endX = left ? startX - _buttonMoveDistance : _nextStepStartX + _buttonMoveDistance;

            targetRectTrans.anchoredPosition = new Vector2(Mathf.Lerp(startX, endX, Easing.InQuad(_buttonAnimationTime)), targetRectTrans.anchoredPosition.y);
        }
    }
}
