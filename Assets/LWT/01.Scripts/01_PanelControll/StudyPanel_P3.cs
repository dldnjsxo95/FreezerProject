using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudyPanel_P3 : MonoBehaviour
{
    FadeObject trFadeObject;

    [Header("Button Setting")]
    public Button studyBtn;

    [Header("Other Setting")]
    public GameObject textBox;

    private void Awake()
    {
        trFadeObject = GetComponent<FadeObject>();

        studyBtn.onClick.AddListener(OnClickStudyBtn);
    }

    public void OnClickStudyBtn()
    {
        trFadeObject.SetOffObject(0.5f);
        studyBtn.interactable = false;

        PlayerEvent.Instance.playerMove.enabled = false;
        PlayerEvent.Instance.camRotate.enabled = false;

        CamManagement.Instance.MoveTo(TargetTag.Turbo_RefrigCondenser);

        textBox.SetActive(true);
    }
}
