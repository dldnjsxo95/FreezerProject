using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Futuregen
{
    public sealed class ProgressItem : MonoBehaviour
    {
        [Header("Title")]
        [SerializeField] private Text _title;
        [SerializeField] private Selectable _titleSelctable;
        [Header("Button")]
        [SerializeField] private Button _button;
        [SerializeField] private Sprite _oddSpriteNormal;
        [SerializeField] private Sprite _oddSpriteHover;
        [SerializeField] private Sprite _evenSpriteNormal;
        [SerializeField] private Sprite _evenSpriteHover;
        [Header("Arrow")]
        [SerializeField] private Image _arrow;
        [SerializeField] private Selectable _arrowSelectable;
        [SerializeField] [Range(1.0f, 5.0f)] private float _arrowSpeed = 4.0f;
        [SerializeField] [Range(1.0f, 4.0f)] private float _arrowMoveRange = 2.0f;

        private RectTransform _arrowRectTrans;
        private float _originX;

        public void SetData(bool oddEven, SubContent data, UnityAction call, bool isLast)
        {
            _title.text = data.Title;

            _button.image.sprite = oddEven ? _oddSpriteNormal : _evenSpriteNormal;

            SpriteState spriteState = _button.spriteState;
            Sprite hoverSprite = oddEven ? _oddSpriteHover : _evenSpriteHover;

            spriteState.highlightedSprite = hoverSprite;
            spriteState.pressedSprite = hoverSprite;
            spriteState.selectedSprite = hoverSprite;
            spriteState.disabledSprite = hoverSprite;

            _button.spriteState = spriteState;
            _button.onClick.AddListener(call);

            _arrow.enabled = !isLast;

            _arrowRectTrans = _arrow.GetComponent<RectTransform>();
            _originX = _arrowRectTrans.anchoredPosition.x;
        }

        public void SelectItem(bool selected)
        {
            _titleSelctable.interactable = !selected;
            _button.interactable = !selected;
            _arrowSelectable.interactable = !selected;
            
            if (selected)
            {
                StartCoroutine(ArrowAnimation());
            }
            else
            {
                StopAllCoroutines();
                _arrowRectTrans.anchoredPosition = Vector2.right * _originX;
            }
        }

        private IEnumerator ArrowAnimation()
        {
            float t = 0.0f;
            while (true)
            {
                t += Time.deltaTime * _arrowSpeed;
                _arrowRectTrans.anchoredPosition = Vector2.right * (_originX - (Mathf.Sin(t) * _arrowMoveRange));
                yield return null;
            }
        }

        public void OnPointerEnter(BaseEventData eventData)
        {
            _titleSelctable.OnPointerEnter((PointerEventData)eventData);
        }

        public void OnPointerExit(BaseEventData eventData)
        {
            _titleSelctable.OnPointerExit((PointerEventData)eventData);
        }
    }
}
