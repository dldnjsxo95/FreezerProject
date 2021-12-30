using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishPanel : MonoBehaviour
{
    [Header("Button Setting")]
    public Button restartBtn;
    public Button exitBtn;

    private void Awake()
    {
        restartBtn.onClick.AddListener(OnClickRestartBtn);
        exitBtn.onClick.AddListener(OnClickExitBtn);
    }

    public void OnClickRestartBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickExitBtn()
    {
        Application.Quit();
    }
}
