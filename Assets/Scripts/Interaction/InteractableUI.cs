using UnityEngine;

namespace Futuregen
{
    /// <summary>
    /// 상호작용 UI의 동작이나 강조 효과를 위해 사용한다.
    /// </summary>
    public sealed class InteractableUI : MonoBehaviour, IInteraction
    {
        /// <summary>
        /// 컨트롤러가 지정한 UI의 index.
        /// </summary>
        [SerializeField] [ReadOnly] private int _index;

        /// <summary>
        /// UI를 관리할 컨트롤러.
        /// </summary>
        private InteractionController _interactionController;
        /// <summary>
        /// 강조 효과는 필요한 경우에만 사용.
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
