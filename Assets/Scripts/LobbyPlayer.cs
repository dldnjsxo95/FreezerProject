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
        EducationText.text = "이 건물에는 냉동설비, 공조설비, 보일러설비가 각각 배치되어 있습니다. \n\n도면을 확인하고 냉동설비가 설치된 장소를 확인하세요.";
        LobbyButton2.gameObject.SetActive(true);
        LobbyButton1.gameObject.SetActive(false);
    }
    public void LobbyBGRightArrow2()
    {
        LobbyBG.gameObject.SetActive(false);
        print("버튼2눌림?");
    }
}
