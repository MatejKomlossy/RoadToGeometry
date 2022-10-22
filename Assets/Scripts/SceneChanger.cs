using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public void PlayLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
