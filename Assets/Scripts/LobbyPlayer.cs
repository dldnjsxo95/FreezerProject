using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPlayer : MonoBehaviour
{
    public GameObject PrototypeBG;
    public GameObject ContinueButton;
    public GameObject SelectBG;
    public GameObject StudyButton;
    public GameObject RealPlayButton;
    public GameObject LobbyBG;
    public GameObject Domyun;
    public GameObject LobbyButton1;
    public GameObject LobbyButton2;

    public GameObject Homes;
    public GameObject Settings;
    public GameObject Helps;
    public GameObject Lists;
    public GameObject Exits;


    public Text EducationText;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void ContinueButtonPressed()
    {
        PrototypeBG.gameObject.SetActive(false);
        ContinueButton.gameObject.SetActive(false);
    }
    public void StudyButtonPressed()
    {
        SelectBG.gameObject.SetActive(false);
    }

    public void RealPlayButtonPressed()
    {

    }

    public void HomeMenuButton()
    {
        Homes.gameObject.SetActive(true);
    }
    public void SettingMenuButton()
    {
        Settings.gameObject.SetActive(true);
    }
    public void HelpMenuButton()
    {
        Helps.gameObject.SetActive(true);
    }
    public void ListMenuButton()
    {
        Lists.gameObject.SetActive(true);
    }
    public void ExitMenuButton()
    {
        Exits.gameObject.SetActive(true);
    }

    public void HomeExitButton()
    {
        Homes.gameObject.SetActive(false);
    }
    public void SettingExitButton()
    {
        Settings.gameObject.SetActive(false);
    }
    public void HelpExitButton()
    {
        Helps.gameObject.SetActive(false);
    }
    public void ListExitButton()
    {
        Lists.gameObject.SetActive(false);
    }
    public void ExitExitButton()
    {
        Exits.gameObject.SetActive(false);
    }

    public void LobbyBGRightArrow1()
    {
        Domyun.gameObject.SetActive(true);
        EducationText.text = "�� �ǹ����� �õ�����, ��������, ���Ϸ����� ���� ��ġ�Ǿ� �ֽ��ϴ�. \n\n������ Ȯ���ϰ� �õ����� ��ġ�� ��Ҹ� Ȯ���ϼ���.";
        LobbyButton2.gameObject.SetActive(true);
        LobbyButton1.gameObject.SetActive(false);
    }
    public void LobbyBGRightArrow2()
    {
        LobbyBG.gameObject.SetActive(false);
        print("��ư2����?");
    }
}
