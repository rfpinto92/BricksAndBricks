using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Configurations Configuration;

    public GameObject MainControllerGameObject;

    public Button GoSetPlayerNameButton;
    public Button GoScoreButton;
    public Button GoOptionsButton;
    public Button QuitButton;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] AudioMenuClick = MainControllerGameObject.GetComponents<AudioSource>();
        MainController MainController = MainControllerGameObject.GetComponent<MainController>();

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
