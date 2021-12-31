using System;
using UnityEngine;

namespace Futuregen
{
    public class Interactable : MonoBehaviour, IInteraction
    {
        public void Initialize(InteractionController interactionController, int index)
        {
            throw new NotImplementedException();
        }

        public void OnClick()
        {
            throw new NotImplementedException();
        }

        public void OnHighlight(bool isOn, bool always = false)
        {
            throw new NotImplementedException();
        }

        public void OnWheel(float value)
        {
            throw new NotImplementedException();
        }
    }
}
