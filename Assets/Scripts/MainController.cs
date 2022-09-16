using UnityEngine;

public class MainController : MonoBehaviour
{
    public GameObject MainMenuGameObject;
    public GameObject InsertPlayerGameObject;
    public GameObject OptionsGameObject;
    public GameObject ScoreGameObject;

    [SerializeField]
    private PlayersPontuation PlayersPontuation;
    [SerializeField]
    private Configurations Configuration;



    // Start is called before the first frame update
    void Start()
    {
        
        Configuration.ReadConfigFile();
        PlayersPontuation.ReadPontuationFile();

        if (Configuration.AppConfig.EnableSound)
            GetComponent<AudioSource>().Play();
        GoMenu();
    }

    public void GoMenu()
    {
        OptionsGameObject.SetActive(false);
        ScoreGameObject.SetActive(false);
        InsertPlayerGameObject.SetActive(false);
        MainMenuGameObject.SetActive(true);
    }

    public void GoInsertPlayer()
    {
        OptionsGameObject.SetActive(false);
        ScoreGameObject.SetActive(false);
        MainMenuGameObject.SetActive(false);
        InsertPlayerGameObject.SetActive(true);
    }
    public void GoScore()
    {
        OptionsGameObject.SetActive(false);
        InsertPlayerGameObject.SetActive(false);
        MainMenuGameObject.SetActive(false);
        ScoreGameObject.SetActive(true);
    }
    public void GoOptions()
    {
        InsertPlayerGameObject.SetActive(false);
        MainMenuGameObject.SetActive(false);
        ScoreGameObject.SetActive(false);
        OptionsGameObject.SetActive(true);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }

}
