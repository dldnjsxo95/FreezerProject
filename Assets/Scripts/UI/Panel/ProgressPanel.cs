using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Futuregen
{
    [RequireComponent(typeof(ProgressPresenter))]
    public sealed class ProgressPanel : BasePanel
    {
        [Header("Progress")]
        [SerializeField] private CanvasGroup _progressPanel;
        [SerializeField] private Text _title;
        [SerializeField] private Text _percentage;

        [Header("Short Cut")]
        [SerializeField] [Range(0.0f, 1.0f)] private float _horizontalMoveTime = 1.0f;
        [SerializeField] private RectTransform _itemContainer;
        [SerializeField] private int _itemViewCount;
        [SerializeField] private List<ProgressItem> _items = new List<ProgressItem>();

        [Header("Function Buttons")]
        [SerializeField] private GameObject _prevButton;
        [SerializeField] private GameObject _nextButton;
        [SerializeField] private Button _showButton;

        private RectTransform _rectTrans;
        private float _showY = 10.0f;
        private float _hideY = 58.0f;
        private GridLayoutGroup _gridLayoutGroup;
        private int _firstViewIndex;        

        protected override void Awake()
        {
            base.Awake();
            _rectTrans = _content.GetComponent<RectTransform>();
            _gridLayoutGroup = _itemContainer.GetComponent<GridLayoutGroup>();

            for (int i = 0; i < _itemContainer.childCount; i++)
            {
                Destroy(_itemContainer.GetChild(i).gameObject);
            }

            _showButton.image.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            _showButton.interactable = false;
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
            return !_showButton.interactable;
        }

        protected override IEnumerator ShowHideAnimation(bool show)
        {
            _canvasGroup.blocksRaycasts = false;

            Vector2 startPos = new Vector2(_rectTrans.anchoredPosition.x, show ? _hideY : _showY);
            Vector2 endPos = new Vector2(_rectTrans.anchoredPosition.x, show ? _showY : _hideY);

            float startAlpha = show ? 0.0f : 1.0f;
            float endAlpha = show ? 1.0f : 0.0f;

            Color color = _showButton.image.color;

            float elapsed = 0.0f;

            while (elapsed < _showHideTime)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / _showHideTime);

                _rectTrans.anchoredPosition = Vector2.Lerp(startPos, endPos, Easing.OutQuad(t));
                _progressPanel.alpha = Mathf.Lerp(startAlpha, endAlpha, Easing.OutQuad(t));

                color.a = Mathf.Lerp(1.0f - startAlpha, 1.0f - endAlpha, Easing.OutQuad(t));
                _showButton.image.color = color;

                yield return null;
            }

            _showButton.interactable = !show;

            _canvasGroup.blocksRaycasts = true;
        }

        public void SetProgressTitle(MainContent data)
        {
            _title.text = data.Title;
        }

        public void CreateProgressItem(ProgressItem item)
        {
            item.transform.SetParent(_itemContainer, false);
            _items.Add(item);
        }

        public void UpdateProgress(int percentage, int subContentIndex)
        {
            _percentage.text = percentage.ToString() + "%";

            for (int i = 0; i < _items.Count; i++)
            {
                _items[i].SelectItem(i == subContentIndex);
            }

            SetItemPosition(subContentIndex);
            UpdateButton();
        }

        private void SetItemPosition(int subContentIndex)
        {
            // 화면에 보여지는 첫 Item 인덱스를 설정한다.
            _firstViewIndex = subContentIndex;
            if (_firstViewIndex + _itemViewCount >= _items.Count)
            {
                _firstViewIndex = _items.Count - _itemViewCount;

                // 총 보여지는 개수보다 Item 개수가 적은 것 방지.
                if (_firstViewIndex < 0)
                {
                    _firstViewIndex = 0;
                }
            }

            StartCoroutine(MoveItem());
        }

        public void ShowPrevItem()
        {
            if (_firstViewIndex <= 0)
            {
                return;
            }
            _firstViewIndex--;

            StartCoroutine(MoveItem());
        }

        public void ShowNextItem()
        {
            if (_firstViewIndex + _itemViewCount >= _items.Count)
            {
                return;
            }
            _firstViewIndex++;

            StartCoroutine(MoveItem());
        }

        private IEnumerator MoveItem()
        {
            _canvasGroup.blocksRaycasts = false;

            float start = _itemContainer.anchoredPosition.x;
            float end = -_firstViewIndex * _gridLayoutGroup.cellSize.x;

            float elapsed = 0.0f;

            while (elapsed < _horizontalMoveTime)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / _horizontalMoveTime);

                _itemContainer.anchoredPosition = Vector3.Lerp(Vector3.right * start, Vector3.right * end, Easing.InOutQuad(t));
                yield return null;
            }

            _itemContainer.anchoredPosition = Vector3.right * end;
            _canvasGroup.blocksRaycasts = true;

            UpdateButton();
        }

        private void UpdateButton()
        {
            _prevButton.SetActive(_firstViewIndex > 0);
            _nextButton.SetActive(_firstViewIndex + _itemViewCount < _items.Count);
        }
    }
}
