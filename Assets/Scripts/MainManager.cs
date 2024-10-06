using GoogleMobileAds.Api;
using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    private int LineCount = 6;
    public Rigidbody Ball;

    public Button BtnRestart;
    public Button BtnReturn;

    private TextMeshProUGUI ScoreText;
    private TextMeshProUGUI BestScoreText;

    public GameObject RetrysPubCanvas;
    public GameObject PaddleControls;
    public GameObject GameOverMenu;

    private bool m_Started = false;
    private bool m_Start = false;

    [SerializeField]
    private PlayersPontuation playersPontuation;

    [SerializeField]
    private Configurations Configuration;

    private AudioSource[] AudioMenuClick;


    // Start is called before the first frame update

    private void OnEnable()
    {

        #region Check Internet Connection
        if (Application.internetReachability == NetworkReachability.NotReachable)
            SceneManager.LoadScene("RetryInternetConnection");
        #endregion

        //Audio Objects
        AudioMenuClick = GetComponents<AudioSource>();
        AudioMenuClick[0].Play();

        #region Buttons

        #region Start
        GameObject.Find("BtnStart").GetComponent<Button>().onClick.AddListener(() =>
        {
            AudioMenuClick[4].Play();
            m_Start = true;
        });
        #endregion

        #region End Return
        GameObject.Find("BtnReturn").GetComponent<Button>().onClick.AddListener(() =>
        {

            Configuration.AppConfig.actualRetry++;

            if (Configuration.AppConfig.actualRetry >= Configuration.AppConfig.MaxRetryToPub)
            {
                Configuration.AppConfig.SceneToLoadAfterPub = "MainMenu";
                Configuration.AppConfig.actualRetry = 0;
                Instantiate(RetrysPubCanvas);
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
            }

        });
        #endregion

        #region End Restart Button
        BtnRestart.GetComponent<Button>().onClick.AddListener(() =>
        {
            AudioMenuClick[4].Play();
            Configuration.RestartPoints();

            Configuration.AppConfig.actualRetry++;

            if (Configuration.AppConfig.actualRetry >= Configuration.AppConfig.MaxRetryToPub)
            {
                Configuration.AppConfig.SceneToLoadAfterPub = SceneManager.GetActiveScene().name;
                Configuration.AppConfig.actualRetry = 0;
                Instantiate(RetrysPubCanvas);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        });
        #endregion

        #region Top Left Return
        BtnReturn.onClick.AddListener(() =>
        {
            Configuration.AppConfig.actualRetry++;

            if (Configuration.AppConfig.actualRetry >= Configuration.AppConfig.MaxRetryToPub)
            {
                Configuration.AppConfig.SceneToLoadAfterPub = "MainMenu";
                Configuration.AppConfig.actualRetry = 0;
                Instantiate(RetrysPubCanvas);
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
            }
        });
        #endregion

        #endregion

        ScoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        BestScoreText = GameObject.Find("BestScoreText").GetComponent<TextMeshProUGUI>();

        //Set Number Of Lines By the Level

        //Show Best Score
        if (playersPontuation.playersList.Count > 0)
            BestScoreText.text = "Best Score: " + playersPontuation.playersList[0].Name + " : " + playersPontuation.playersList[0].pontuation.ToString();
        else
            BestScoreText.text = string.Empty;

        ScoreText.text = "Level: " + Configuration.ActualLevel.Level + "  Your Score : " + Configuration.ActualLevel.Points;

        LineCount = Configuration.ActualLevel.Level <= 21 ? Configuration.ActualLevel.Level : 21;

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(5.0f / step);

        // Get Random PointValue  
        int[] pointCountArray = new[] { 1, 2, 5 };
        System.Random randomNumber = new System.Random();

        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                //Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);

                Vector3 position = new Vector3(-2.11f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[randomNumber.Next(0, 3)];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

    }
    void Start()
    {
    }


    private void Update()
    {
        if (!m_Started)
        {
            if (m_Start)
            {
                m_Started = true;

                GameObject.Destroy(GameObject.Find("StartObjects")); //destroy start Object with button and Indicator Text

                Color color = Color.gray;
                color.a = 0f;
                GameObject.Find("BtnLeft").GetComponent<Image>().color = color;
                GameObject.Find("BtnRight").GetComponent<Image>().color = color;


                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else
        {
            //Go To Next Level
            if (GameObject.FindGameObjectsWithTag("BrickPrefab").Length == 0)
            {
                Configuration.ActualLevel.Level++;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        AudioMenuClick[1].Play();
        Configuration.ActualLevel.Points += point;
        ScoreText.text = "Level: " + Configuration.ActualLevel.Level + "  Your Score : " + Configuration.ActualLevel.Points;
    }

    public void GameOver()
    {

        //Activate GameOver Buttons
        Destroy(GameObject.Find("ControlBtns"));
        GameOverMenu.SetActive(true);

        TextMeshProUGUI GameOverText = GameObject.Find("GameOverText").GetComponent<TextMeshProUGUI>();

        //Set Game Over Text

        if (playersPontuation.playersList.Count == 0)
        {
            AudioMenuClick[3].Play();
            GameOverText.text = "YOU ARE THE FIRST NUMBER 1!!";
        }
        else
        {
            if (playersPontuation.playersList[0].pontuation >= Configuration.ActualLevel.Points)
            {
                AudioMenuClick[2].Play();
                GameOverText.text = "GAME OVER";
            }
            else
            {
                AudioMenuClick[3].Play();
                GameOverText.text = "YOU ARE NEW NUMBER 1!!";
            }
        }
        DoPostScore(Configuration.ActualLevel.Points);
        playersPontuation.AddPontuation(Configuration.ActualLevel.Points);
        playersPontuation.WritePontuationFile(); //Write File to avoid Player finish app without going to the main menu
    }

    internal void DoPostScore(long Score)
    {

        Social.ReportScore(
            Score,
            GPGSIds.leaderboard_main,
            (bool success) =>
            {

                if (!success)
                {
                    PlayGamesPlatform.Activate();
                    Social.Active.localUser.Authenticate((success) =>
                    {
                        if (success)
                            DoPostScore(Score);
                        else
                            SceneManager.LoadScene("RetryInternetConnection");
                    });
                }
                else return;
            });
    }

}
