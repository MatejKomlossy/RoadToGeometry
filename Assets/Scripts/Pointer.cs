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

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            transform.position = hit.point;

            if (hit.transform.gameObject.tag == "Button")
            {
                if (objectInSight != hit.transform.gameObject)
                {
                    if (objectInSight != null && objectInSight.tag == "Button")
                    {
                        ChangeObjectColor(normalColor);
                    }
                    objectInSight = hit.transform.gameObject;
                    ChangeObjectColor(activeColor);
                }

            }
            else if (hit.transform.gameObject.tag == "Toggle")
            {
                objectInSight = hit.transform.gameObject;
            }
            else
            {
                if (objectInSight != null && objectInSight.tag == "Button")
                {
                    ChangeObjectColor(normalColor);
                }
                objectInSight = null;
            }
        }
        else
        {
            if (objectInSight != null && objectInSight.tag == "Button")
            {
                ChangeObjectColor(normalColor);
            }
            objectInSight = null;
        }

        if (Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
    }

    private void OnClick()
    {
        if (objectInSight != null && objectInSight.tag == "Button")
        {
            OnButtonClick();
        }
        else if (objectInSight != null && objectInSight.tag == "Toggle")
        {
            OnToggleClick();
        }
    }

    private void ChangeObjectColor(Color color)
    {
        Image img = null;
        if (objectInSight.TryGetComponent<Image>(out img))
        {
            img.color = color;
        }
    }

    private void OnButtonClick()
    {
        switch (objectInSight.name)
        {
            case "Level1Button":
                PlayLevel1();
                break;
            case "Level2Button":
                PlayLevel2();
                break;
            case "Level3Button":
                PlayLevel3();
                break;
            case "Level4Button":
                PlayLevel4();
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

    private void OnToggleClick()
    {
        Toggle toggle = objectInSight.GetComponent<Toggle>();
        toggle.isOn = !toggle.isOn;
        SetUserPreferences();
    }

    public void SetUserPreferences()
    {
        if (objectInSight.GetComponent<Toggle>().isOn)
        {
            PlayerPrefs.SetInt(objectInSight.name, 1);
        } else
        {
            PlayerPrefs.SetInt(objectInSight.name, 0);
        }
    }

    private void PlayLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    private void PlayLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    private void PlayLevel3()
    {
        SceneManager.LoadScene("Level3");
    }

    private void PlayLevel4()
    {
        SceneManager.LoadScene("Level4");
    }

    private void Explore()
    {
        //SceneManager.LoadScene("Explore");
    }


    private void OpenSettings()
    {
        menu.SetActive(false);
        settings.SetActive(true);
    }

    private void SettingsBackToMenu()
    {
        settings.SetActive(false);
        menu.SetActive(true);
    }

    private void ExitApplication()
    {
        Application.Quit();
    }
}
