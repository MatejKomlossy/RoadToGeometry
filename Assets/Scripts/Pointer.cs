using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pointer : MonoBehaviour
{

    private GameObject objectInSight = null;
    private Color normalColor = Color.black;
    private Color activeColor = new Color(0.54f, 0.32f, 0.61f, 1);

    public GameObject menu;
    public GameObject settings;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {      
            transform.position = hit.point;

            if (objectInSight != hit.transform.gameObject)
            {
                if (objectInSight != null)
                {
                    changeObjectColor(normalColor);
                }
                objectInSight = hit.transform.gameObject;
                changeObjectColor(activeColor);
            }
        }
        else
        {
            changeObjectColor(normalColor);
            objectInSight = null;
        }

        if (Input.GetMouseButtonDown(0))
        {
            switch (objectInSight.name)
            {
                case "Level1Button":
                    PlayLevel1();
                    break;
                case "Level2Button":
                    PlayLevel2();
                    break;
                case "SettingsButton":
                    OpenSettings();
                    break;
                case "ExploreButton":
                    Explore();
                    break;
                case "ExitButton":
                    ExitApplication();
                    break;
                case "SettingsBackButton":
                    SettingsBackToMenu();
                    break;

                default:
                    break;

            }
        }
    }

    private void changeObjectColor(Color color)
    {
        Image img = null;
        if (objectInSight.TryGetComponent<Image>(out img))
        {
            img.color = color;
        }
    }

    public void PlayLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void PlayLevel2()
    {
        //SceneManager.LoadScene("Level2");
    }

    public void Explore()
    {
        //SceneManager.LoadScene("Explore");
    }


    public void OpenSettings()
    {
        menu.SetActive(false);
        settings.SetActive(true);
    }

    public void SettingsBackToMenu()
    {
        settings.SetActive(false);
        menu.SetActive(true);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
