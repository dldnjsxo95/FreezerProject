using UnityEngine;

namespace Futuregen
{
    public class InteractableGroup : MonoBehaviour, IInteraction
    {
        [SerializeField] [ReadOnly] private int _index;
        [SerializeField] private InteractableObject[] _objects;

        private InteractionController _interactionController;

        public void Initialize(InteractionController interactionController, int index)
        {
            _interactionController = interactionController;
            _index = index;
        }

        public void OnHighlight(bool isOn, bool always = false)
        {
            foreach (InteractableObject obj in _objects)
            {
                obj.OnHighlight(isOn, always);
            }
        }

        public void OnClick()
        {
            //foreach (InteractableObject obj in _objects)
            //{
            //    obj.OnClick();
            //}
        }

        public void OnWheel(float value)
        {
            //throw new System.NotImplementedException();
        }
    }
}
