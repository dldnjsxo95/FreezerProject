using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Futuregen
{
    [RequireComponent(typeof(HelpPresenter))]
    public sealed class HelpPanel : BasePanel
    {
        [Header("Help Pages")]
        [SerializeField] private RectTransform _helpPanel;
        [SerializeField] [Range(0.0f, 1.0f)] private float _horizontalMoveTime = 0.4f;
        [SerializeField] private HelpItem[] _items;

        [Header("Move Dummy Objects")]
        [SerializeField] private RectTransform _dummyLeftHide;
        [SerializeField] private RectTransform _dummyLeft;
        [SerializeField] private RectTransform _dummyCenter;
        [SerializeField] private RectTransform _dummyRight;
        [SerializeField] private RectTransform _dummyRightHide;

        [Header("Function Buttons")]
        [SerializeField] private Button _prevButton;
        [SerializeField] private Button _nextButton;

        private Vector2 _showPos = Vector2.zero;
        private Vector2 _hidePos = new Vector2(-834.0f, 446.0f);
        private int _currnetPage;

        //Kwon - 추가된 필드
        [SerializeField] private List<Button> _helpCatalogItems;
        private Button currentCatalogItem;

        public override void OnShow()
        {
            ResetPage();
            StartCoroutine(ShowHideAnimation(true));
        }

        public override void OnHide()
        {
            StartCoroutine(ShowHideAnimation(false));
        }

        protected override IEnumerator ShowHideAnimation(bool show)
        {
            _canvasGroup.blocksRaycasts = false;

            // 도움말 열릴 때.
            if (show)
            {
                base.OnShow();
            }

            float startAlpha = show ? 0.0f : 1.0f;
            float endAlpha = show ? 1.0f : 0.0f;

            Vector2 startPos = show ? _hidePos : _showPos;
            Vector2 endPos = show ? _showPos : _hidePos;

            Vector3 startScale = show ? Vector3.zero : Vector3.one;
            Vector3 endScale = show ? Vector3.one : Vector3.zero;

            float elapsed = 0.0f;

            while (elapsed < _showHideTime)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / _showHideTime);

                _canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, Easing.OutQuad(t));

                _helpPanel.anchoredPosition = Vector2.Lerp(startPos, endPos, Easing.OutQuad(t));
                _helpPanel.localScale = Vector3.Lerp(startScale, endScale, Easing.OutQuad(t));
                yield return null;
            }

            // 도움말 닫힐 떄.
            if (!show)
            {
                base.OnHide();
            }

            _canvasGroup.blocksRaycasts = true;
        }

        /// <summary>
        /// 첫 페이지로 초기화.
        /// </summary>
        public void ResetPage()
        {
            _currnetPage = 0;
            SelectCatalogItem(_currnetPage);
            for (int i = 0; i < _items.Length; i++)
            {
                if (i == 0)
                {
                    _items[i].RectTrans.anchoredPosition3D = _dummyCenter.anchoredPosition3D;
                    _items[i].RectTrans.localRotation = _dummyCenter.localRotation;
                    _items[i].RectTrans.localScale = _dummyCenter.localScale;
                }
                else if (i == 1)
                {
                    _items[i].RectTrans.anchoredPosition3D = _dummyRight.anchoredPosition3D;
                    _items[i].RectTrans.localRotation = _dummyRight.localRotation;
                    _items[i].RectTrans.localScale = _dummyRight.localScale;
                }
                else
                {
                    _items[i].RectTrans.anchoredPosition3D = _dummyRightHide.anchoredPosition3D;
                    _items[i].RectTrans.localRotation = _dummyRightHide.localRotation;
                    _items[i].RectTrans.localScale = _dummyRightHide.localScale;
                }
            }

            _prevButton.gameObject.SetActive(false);
        }

        //Kwon - 변경된 함수
        public void SetPage(int pageIndex)
        {
            SelectCatalogItem(pageIndex);

            StartCoroutine(ShorcutPageRoutine(pageIndex));
        }

        //Kwon - 추가된 함수
        private void SelectCatalogItem(int index)
        {
            if (currentCatalogItem != null)
            {
                currentCatalogItem.interactable = true;
            }

            currentCatalogItem = _helpCatalogItems[index];
            _helpCatalogItems[index].interactable = false;
        }

        //Kwon - 추가된 함수
        private IEnumerator ShorcutPageRoutine(int pageIndex)
        {
            int dir = 0;
            int moveAmount = 0;
            if (_currnetPage > pageIndex)
            {
                dir = -1;
                moveAmount = _currnetPage - pageIndex;
            }
            else if (_currnetPage == pageIndex)
            {
                yield break;
            }
            else if (_currnetPage < pageIndex)
            {
                dir = 1;
                moveAmount = pageIndex - _currnetPage;
            }

            float moveTime = _horizontalMoveTime / moveAmount;

            while (moveAmount > 0)
            {
                _currnetPage += dir;
                moveAmount--;
                yield return StartCoroutine(MovePage(moveTime));

            }
        }

        public void PrevPage()
        {
            _currnetPage--;

            if (_currnetPage < 0)
            {
                _currnetPage = 0;
                return;
            }

            SelectCatalogItem(_currnetPage);
            StartCoroutine(MovePage(_horizontalMoveTime));
        }

        public void NextPage()
        {
            _currnetPage++;

            if (_currnetPage >= _items.Length)
            {
                _currnetPage = _items.Length - 1;
                return;
            }

            SelectCatalogItem(_currnetPage);
            StartCoroutine(MovePage(_horizontalMoveTime));
        }

        //Kwon - 변경된 함수 -_horizontalMoveTime 필드를 그대로 쓰는게아닌 인자값을 받아서 처리하도록 변경
        private IEnumerator MovePage(float movetime)
        {
            _prevButton.gameObject.SetActive(false);
            _nextButton.gameObject.SetActive(false);

            _canvasGroup.blocksRaycasts = false;

            // 시작 포지션 설정.
            for (int i = -2; i < 3; i++)
            {
                if (IsExist(_currnetPage + i))
                {
                    _items[_currnetPage + i].SetLerpStartTransform();
                }
            }

            // 이동 시작.
            float elapsed = 0.0f;

            while (elapsed < movetime)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / movetime);

                if (IsExist(_currnetPage - 2))
                {
                    _items[_currnetPage - 2].Lerp(_dummyLeftHide, t);
                }
                if (IsExist(_currnetPage - 1))
                {
                    _items[_currnetPage - 1]?.Lerp(_dummyLeft, t);
                }
                if (IsExist(_currnetPage + 0))
                {
                    _items[_currnetPage + 0]?.Lerp(_dummyCenter, t);
                }
                if (IsExist(_currnetPage + 1))
                {
                    _items[_currnetPage + 1]?.Lerp(_dummyRight, t);
                }
                if (IsExist(_currnetPage + 2))
                {
                    _items[_currnetPage + 2].Lerp(_dummyRightHide, t);
                }

                yield return null;
            }

            _prevButton.gameObject.SetActive(_currnetPage != 0);
            _nextButton.gameObject.SetActive(_currnetPage != _items.Length - 1);

            _canvasGroup.blocksRaycasts = true;
        }

        private bool IsExist(int pageIndex)
        {
            return pageIndex >= 0 && pageIndex < _items.Length;
        }
    }
}
