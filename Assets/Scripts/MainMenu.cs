using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Configurations Configuration;

    // Start is called before the first frame update
    void Start()
    {
        GameObject MainControllerGameObject = GameObject.Find("MainController");

        AudioSource[] AudioMenuClick = MainControllerGameObject.GetComponents<AudioSource>();
        MainController MainController = MainControllerGameObject.GetComponent<MainController>();

        // Find Buttons
        Button GoSetPlayerNameButton = GameObject.Find("BtnPlay").GetComponent<Button>();
        Button GoScoreButton = GameObject.Find("BtnScore").GetComponent<Button>();
        Button GoOptionsButton = GameObject.Find("BtnOptions").GetComponent<Button>();
        Button QuitButton = GameObject.Find("BtnQuit").GetComponent<Button>();

        GoSetPlayerNameButton.onClick.AddListener(() =>
        {
            if (Configuration.AppConfig.EnableSound)
                AudioMenuClick[1].Play();
            MainController.GoInsertPlayer();
        });
        GoScoreButton.onClick.AddListener(() =>
        {
            if (Configuration.AppConfig.EnableSound)
                AudioMenuClick[1].Play(); MainController.GoScore();
        });
        GoOptionsButton.onClick.AddListener(() =>
        {
            if (Configuration.AppConfig.EnableSound)
                AudioMenuClick[1].Play(); MainController.GoOptions();
        });
        QuitButton.onClick.AddListener(() =>
        {
            if (Configuration.AppConfig.EnableSound)
                AudioMenuClick[1].Play(); MainController.OnApplicationQuit();
        });
    }
}
