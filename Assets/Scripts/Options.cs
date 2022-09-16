using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField]
    private PlayersPontuation PlayersPontuation;
    [SerializeField]
    private Configurations Configuration;

    public GameObject BtnReset;
    public Button BtnReturn;
    public Toggle ToggleGG;
    public Toggle ToggleNormal;
    public Toggle ToggleHard;

    public GameObject MainControllerGameObject;
    // Start is called before the first frame update

    void Start()
    {
        AudioSource[] AudioMenuClick = MainControllerGameObject.GetComponents<AudioSource>();

        switch (Configuration.AppConfig.GameDificult)
        {
            case GameDificult.Easy:
                ToggleGG.isOn = true;
                break;
            case GameDificult.Normal:
                ToggleNormal.isOn = true;
                break;
            case GameDificult.Hard:
                ToggleHard.isOn = true;
                break;
            default:
                break;
        }

        ToggleGG.onValueChanged.AddListener(delegate
        {
            if (Configuration.AppConfig.EnableSound)
                AudioMenuClick[2].Play();
            if (ToggleGG.isOn)
                Configuration.AppConfig.GameDificult = GameDificult.Easy;
        });

        ToggleNormal.onValueChanged.AddListener(delegate
        {
            if (Configuration.AppConfig.EnableSound)
                AudioMenuClick[2].Play();
            if (ToggleNormal.isOn)
                Configuration.AppConfig.GameDificult = GameDificult.Normal;
        });

        ToggleHard.onValueChanged.AddListener(delegate
        {
            if (Configuration.AppConfig.EnableSound)
                AudioMenuClick[2].Play();
            if (ToggleHard.isOn)
                Configuration.AppConfig.GameDificult = GameDificult.Hard;
        });

        MainController MainController = MainControllerGameObject.GetComponent<MainController>();


        BtnReset.GetComponent<Button>().onClick.AddListener(() =>
        {
            if (Configuration.AppConfig.EnableSound)
                AudioMenuClick[1].Play();
            PlayersPontuation.Reset();
            BtnReset.GetComponentInChildren<TextMeshProUGUI>().text = "Done";
        });

        BtnReturn.onClick.AddListener(() =>
        {
            if (Configuration.AppConfig.EnableSound)
                AudioMenuClick[1].Play();
            Configuration.WriteConfigFile();
            MainController.GoMenu();
        });
    }
}
