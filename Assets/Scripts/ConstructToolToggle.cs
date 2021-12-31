using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructToolToggle : MonoBehaviour
{
    [SerializeField] Toggle toggle;
    [SerializeField] Sprite selectedSprite;
    [SerializeField] Sprite unSelectedSprite;
    [SerializeField] Image image;

    [SerializeField] RectTransform dragItemTr;

    bool isClick = false;

    [SerializeField] Image dragItemImage;
    [SerializeField] Sprite itemSprite;


    private void Awake()
    {
        AttachToggleEvent();
    }

    private void OnEnable()
    {
        isClick = false;
    }

    private void AttachToggleEvent()
    {
        toggle.onValueChanged.AddListener(delegate { OnToggle(); });
    }

    private void OnToggle()
    {
        if (toggle.isOn)
        {
            image.sprite = selectedSprite;
            isClick = true;
            dragItemTr.gameObject.SetActive(true);
            if (itemSprite != null)
            {
                dragItemImage.sprite = itemSprite;
            }
        }
        else
        {
            isClick = false;
            image.sprite = unSelectedSprite;
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (!isClick)
            return;

        dragItemTr.position = Input.mousePosition;
    }
}
