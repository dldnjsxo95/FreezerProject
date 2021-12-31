using UnityEngine;

namespace Futuregen
{
    /// <summary>
    /// 데이터를 이용해서 UI 제어 및 이벤트 관리.
    /// </summary>
    public abstract class BasePresenter : MonoBehaviour
    {
        protected BasePanel _panel;

        protected virtual void Awake()
        {
            _panel = GetComponent<BasePanel>();
        }

        protected virtual void Start()
        {
            Initialize();
        }

        /// <summary>
        /// 패널에 필요한 데이터 초기화에 사용.
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// 패널을 활성화할 때, 갱신할 데이터가 있으면 오버라이드해서 사용.
        /// </summary>
        protected virtual void ShowPanel()
        {
            _panel.OnShow();
        }

        /// <summary>
        /// 패널을 비활성화할 때, 갱신할 데이터가 있으면 오버라이드해서 사용.
        /// </summary>
        protected virtual void HidePanel()
        {
            _panel.OnHide();
        }

        /// <summary>
        /// 패널 활성화, 비활성화.
        /// </summary>
        /// <param name="value"></param>
        public void SetActivePanel(bool value)
        {
            if (_panel.IsActive() == value)
            {
                return;
            }

            if (value)
            {
                ShowPanel();
            }
            else
            {
                HidePanel();
            }
        }
    }
}