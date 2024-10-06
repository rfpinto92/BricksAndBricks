using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTable : MonoBehaviour
{

    public GameObject ListItemPrefab; //Prefab object (line object)
    public GameObject Container; //container that have the sensors object list

    public GameObject MainControllerGameObject;
    private MainController mainController;

    [SerializeField]
    private PlayersPontuation playersPontuation;
    [SerializeField]
    private Configurations Configuration;

    public Button GoMenuButton;
    public GameObject ShowLeaderBoardButton;


    private void Start()
    {
        mainController = MainControllerGameObject.GetComponent<MainController>();
        AudioSource[] AudioMenuClick = MainControllerGameObject.GetComponents<AudioSource>();

        #region Show Online LeaderBoard Button
        ShowLeaderBoardButton.SetActive(Configuration.AppConfig.IsGoogleLogged);
        ShowLeaderBoardButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            PlayGamesPlatform.Activate();
            Social.Active.localUser.Authenticate(ShowLeaderboardUI);
        });
        #endregion

        #region Go Main Maenu Button
        GoMenuButton.onClick.AddListener(() =>
        {
            if (Configuration.AppConfig.EnableSound)
                AudioMenuClick[1].Play();
            mainController.GoMenu();
        });
        #endregion
    }

    private void OnEnable()
    {
        UpdateTable();
    }

    private void UpdateTable()
    {
        int Pos = 1;

        foreach (Transform item in Container.transform)
            GameObject.Destroy(item.gameObject);

        if (playersPontuation.playersList == null)
            return;
        if (playersPontuation.playersList.Count == 0)
            return;

        foreach (var item in playersPontuation.playersList)
        {
            GameObject newPlayer = Instantiate(ListItemPrefab, new Vector3(0, 0, 0), Quaternion.identity, Container.transform) as GameObject; //new object with on formation of the Prefab, position to add and size
            Player_Pontuation_Item player_Item = newPlayer.GetComponent<Player_Pontuation_Item>(); //get components/parameter of the c# script Sensor_Item ->tem os campos para preencher

            player_Item.Pos.text = Pos.ToString();
            player_Item.Name.text = item.Name;
            player_Item.Score.text = item.pontuation.ToString();

            //name of container obj
            newPlayer.name = "Player " + player_Item.Pos.text;
            item.PlayerContainer = newPlayer;

            #region 3 first values text font change
            //(46°, 74 %, 83 %)
            if (player_Item.Pos.text == "1")
            {
                player_Item.Pos.color = new Color(1f, 0.8343f, 0f);
                player_Item.Pos.fontStyle = TMPro.FontStyles.Bold;
                player_Item.Pos.fontSizeMax = 150;
            }
            if (player_Item.Pos.text == "2")
            {
                player_Item.Pos.color = new Color(0.6132f, 0.6132f, 0.6132f);
                player_Item.Pos.fontStyle = TMPro.FontStyles.Bold;
                player_Item.Pos.fontSizeMax = 150;
            }
            if (player_Item.Pos.text == "3")
            {
                player_Item.Pos.color = new Color(0.5566f, 0.4100f, 0.3019f);
                player_Item.Pos.fontStyle = TMPro.FontStyles.Bold;
                player_Item.Pos.fontSizeMax = 150;
            }
            #endregion

            Pos++;
        }

    }

    void ShowLeaderboardUI(bool success)
    {
        if (success)
        {
            PlayGamesPlatform.Activate();
            PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_main);
        }
    }

}
