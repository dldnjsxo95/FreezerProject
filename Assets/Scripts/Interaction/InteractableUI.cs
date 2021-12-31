using UnityEngine;

namespace Futuregen
{
    /// <summary>
    /// ��ȣ�ۿ� UI�� �����̳� ���� ȿ���� ���� ����Ѵ�.
    /// </summary>
    public sealed class InteractableUI : MonoBehaviour, IInteraction
    {
        /// <summary>
        /// ��Ʈ�ѷ��� ������ UI�� index.
        /// </summary>
        [SerializeField] [ReadOnly] private int _index;

        /// <summary>
        /// UI�� ������ ��Ʈ�ѷ�.
        /// </summary>
        private InteractionController _interactionController;
        /// <summary>
        /// ���� ȿ���� �ʿ��� ��쿡�� ���.
        /// </summary>
        private UIHighlighter _highlighter;

        public void Initialize(InteractionController interactionController, int index)
        {
            _highlighter = GetComponent<UIHighlighter>();

            _interactionController = interactionController;
            _index = index;
        }

        public void OnHighlight(bool isOn, bool always = false)
        {
            if (_highlighter != null)
            {
                _highlighter.SetHighlight(isOn);
            }
        }

        public void OnInteraction()
        {
            OnHighlight(false);
            _interactionController.RunInteraction(_index);
        }

        public void OnClick()
        {

        }

        public void OnWheel(float value)
        {
            
        }
    }
}
