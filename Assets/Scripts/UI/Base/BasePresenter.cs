using UnityEngine;

namespace Futuregen
{
    /// <summary>
    /// �����͸� �̿��ؼ� UI ���� �� �̺�Ʈ ����.
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
        /// �гο� �ʿ��� ������ �ʱ�ȭ�� ���.
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// �г��� Ȱ��ȭ�� ��, ������ �����Ͱ� ������ �������̵��ؼ� ���.
        /// </summary>
        protected virtual void ShowPanel()
        {
            _panel.OnShow();
        }

        /// <summary>
        /// �г��� ��Ȱ��ȭ�� ��, ������ �����Ͱ� ������ �������̵��ؼ� ���.
        /// </summary>
        protected virtual void HidePanel()
        {
            _panel.OnHide();
        }

        /// <summary>
        /// �г� Ȱ��ȭ, ��Ȱ��ȭ.
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