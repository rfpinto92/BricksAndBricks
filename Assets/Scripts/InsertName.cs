using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InsertName : MonoBehaviour
{

    public TextMeshProUGUI DisplayPlayerName;
    public TMP_Input​Field InsertPlayerName;
    public Button GoGameButton;
    public Button GoBackButton;

    [SerializeField]
    private PlayersPontuation PlayersPontuation;
    [SerializeField]
    private Configurations Configuration;


    private void Start()
    {
        MainController MainController =GameObject.Find("MainController").GetComponent<MainController>();
        AudioSource[] AudioMenuClick = GameObject.Find("MainController").GetComponents<AudioSource>();

        GoGameButton.onClick.AddListener(()=>
        {
            if (InsertPlayerName.text == string.Empty)
            {
                AudioMenuClick[3].Play();
                DisplayPlayerName.text = "Insert a Valid Name!";
                return;
            }
            AudioMenuClick[1].Play();

            PlayersPontuation.PlayerName = InsertPlayerName.text;

            Configuration.RestartPoints();
            SceneManager.LoadScene("Game");
        });


        GoBackButton.onClick.AddListener(() =>
        {
            AudioMenuClick[1].Play();
            MainController.GoMenu();
        });
    }
}

