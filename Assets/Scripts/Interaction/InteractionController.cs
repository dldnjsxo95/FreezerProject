using System;
using System.Collections.Generic;
using UnityEngine;

namespace Futuregen
{
    public abstract class InteractionController : MonoBehaviour, IContentListener
    {
        [SerializeField] protected List<GameObject> _interactableObjects;

        protected List<IInteraction> _interactables = new List<IInteraction>();
        protected Dictionary<int, bool> _interactionCheckList = new Dictionary<int, bool>(); //상호작용 완료 체크( 한스텝에 여러 상호작용이 있을수도있기때문에 여러개로만듬)
        protected List<int> _interactionList = new List<int>(); //상호작용 완료 체크( 한스텝에 여러 상호작용이 있을수도있기때문에 여러개로만듬)

        protected virtual void Start()
        {
            Initialize();
        }

        protected virtual void OnEnable()
        {
            ContentManager.Instance.OnSubContentChanged += OnSubContentChanged;
            ContentManager.Instance.OnStepChanged += OnStepChanged;
        }

        public  void OnSubContentChanged(int subContentIndex)
        {
            UpdateCameraLocation(subContentIndex);
        }

        public void OnStepChanged(int stepIndex)
        {
            ResetController();
        }

        protected abstract void UpdateCameraLocation(int subContentIndex);

        protected virtual void ResetController()
        {
            foreach (IInteraction interactable in _interactables)
            {
                interactable.OnHighlight(false);
                //interactable.OnReset();
            }
            _interactionCheckList.Clear();
            _interactionList.Clear();
        }

        protected virtual void Initialize()
        {
            for (int i = 0; i < _interactableObjects.Count; ++i)
            {
                IInteraction interactable = _interactableObjects[i].GetComponent<IInteraction>();
                interactable.Initialize(this, i);

                _interactables.Add(interactable);
            }
        }

        protected void RegisterInteractionEvent(EventID eventId, StepEvent fun)
        {
            EventManager.Instance.ResisterEvent(eventId, fun);
        }

        protected void SetHighlightObject(bool always, params int[] indices)
        {
            //실습이면 하이라이트를 하지않는다.
            if (ContentManager.Instance.IsTraining)
            {
                return;
            }

            //인수로 받은 인덱스 만 켜고 나머진 다 꺼진다.
            for (int i = 0; i < _interactableObjects.Count; ++i)
            {
                _interactables[i].OnHighlight(false);
            }

            foreach (int index in indices)
            {
                _interactables[index].OnHighlight(true, always);
                _interactionList.Add(index);
            }

            //SetInteractionObject(true, true, 1, 2, 3);
        }

        protected void SetCompleteTarget(params int[] indices)
        {
            foreach (int index in indices)
            {
                _interactionCheckList.Add(index, false);
            }
        }

        protected void SetInteractionObject(bool isHighlight, bool alwaysHighlight, bool useInteraction, params int[] indices)
        {
            // 모든 강조 효과 비활성화.
            foreach (IInteraction interactable in _interactables)
            {
                interactable.OnHighlight(false);
            }

            foreach (int index in indices)
            {
                _interactables[index].OnHighlight(isHighlight, alwaysHighlight);

                if (useInteraction)
                {
                    _interactionCheckList.Add(index, false);
                }
            }
        }

        public abstract void RunInteraction(int index);

        protected bool CheckInteractable(int index)
        {
            bool result = false;
            foreach (int saveIndex in _interactionList)
            {
                if (saveIndex == index)
                {
                    result = true;
                }
            }
            return result;
        }

        protected void DoneInteraction(int index)
        {
            if (_interactionCheckList.ContainsKey(index))
            {
                _interactionCheckList[index] = true;
            }
        }
    }
}
