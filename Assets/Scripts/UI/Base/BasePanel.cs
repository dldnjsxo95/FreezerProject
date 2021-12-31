using System.Collections;
using UnityEngine;

namespace Futuregen
{
    /// <summary>
    /// UI 관련 보여지는 부분을 관리.
    /// </summary>
    public abstract class BasePanel : MonoBehaviour
    {
        [Header("Common")]
        [SerializeField] protected bool runOnHidePanel = true;
        [SerializeField] protected GameObject _content;
        [SerializeField] [Range(0.0f, 1.0f)] protected float _showHideTime = 0.6f;

        protected CanvasGroup _canvasGroup;

        protected virtual void Awake()
        {
            if (runOnHidePanel)
            {
                _content.SetActive(false);
            }
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        /// <summary>
        /// 애니메이션이 필요하면 오버라이드해서 사용.
        /// </summary>
        public virtual void OnShow()
        {
            _content.SetActive(true);
        }

        /// <summary>
        /// 애니메이션이 필요하면 오버라이드해서 사용.
        /// </summary>
        public virtual void OnHide()
        {
            _content.SetActive(false);
        }

        /// <summary>
        /// UI가 활성화 상태인지 판단. 필요하면 오버라이드해서 사용.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsActive()
        {
             return _content.activeSelf;
        }

        /// <summary>
        /// 애니메이션은 여기에 구현해서 사용.
        /// </summary>
        /// <param name="show">열리면 true, 닫히면 false.</param>
        /// <returns></returns>
        protected virtual IEnumerator ShowHideAnimation(bool show)
        {
            yield return null;
        }
    }
}
