using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnLevel1Button()
    {
        SceneManager.LoadScene(1);
    }

    public void OnLevel2Button()
    {
        SceneManager.LoadScene(2);
    }

    public void OnLevel3Button()
    {
        SceneManager.LoadScene(3);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
