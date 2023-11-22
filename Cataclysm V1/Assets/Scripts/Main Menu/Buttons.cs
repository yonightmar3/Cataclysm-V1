using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject settingsPage;
    public GameObject mainPage;

    public void playButton()
    {
        SceneManager.LoadScene("MainMap");
    }

    public void mainMapButton()
    {
        SceneManager.LoadScene("MainMap");
    }

    public void dungeonButton()
    {
        SceneManager.LoadScene("Dungeon");
    }

    public void settingsButton()
    {
        settingsPage.SetActive(true);
        mainPage.SetActive(false);
    }

    public void mainButton()
    {
        settingsPage.SetActive(false);
        mainPage.SetActive(true);
    }
}
