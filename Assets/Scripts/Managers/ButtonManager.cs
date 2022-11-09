using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager BM;

    public GameObject menuPanel;

    private void Awake()
    {
        BM = this;
    }


    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }

    public void KeepGoing()
    {
        Time.timeScale = 1.0f;
        menuPanel.SetActive(false);
    }

    public void OpenMenu()
    {
        Time.timeScale = 0.0f;
        menuPanel.SetActive(true);
    }
}
      