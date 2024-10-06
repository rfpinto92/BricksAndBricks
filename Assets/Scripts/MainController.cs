using GoogleMobileAds.Api;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public GameObject MainMenuGameObject;
    public GameObject InsertPlayerGameObject;
    public GameObject OptionsGameObject;
    public GameObject ScoreGameObject;
    public GameObject GoogleLoginButton;

    [SerializeField]
    private PlayersPontuation PlayersPontuation;
    [SerializeField]
    private Configurations Configuration;

    AndroidJavaObject currentActivity;

    // Start is called before the first frame update
    void Start()
    {

        //To the Screen dont turn off
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        #region Check Internet Connection
        if (Application.internetReachability == NetworkReachability.NotReachable)
            SceneManager.LoadScene("RetryInternetConnection");
        #endregion

        #region Andoid Messages
        //currentActivity androidjavaobject must be assigned for toasts to access currentactivity;
        //AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        //(currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        #endregion

        #region Configuration and Oflline LeaderBoard
        Configuration.ReadConfigFile();
        PlayersPontuation.ReadPontuationFile();
        #endregion

        #region Google Login
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(MainAuthentication);

        GoogleLoginButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            PlayGamesPlatform.Instance.ManuallyAuthenticate(AuthenticationByButton);
        });

        #endregion

        MobileAds.Initialize(initialize => { });

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


    /// <summary>
    ///Login
    /// </summary>
    /// <param name="status"></param>
    void MainAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            GoogleLoginButton.SetActive(false);
            Configuration.AppConfig.IsGoogleLogged = true;
            PlayGamesPlatform.Activate();
        
        }
        else
        {
            GoogleLoginButton.SetActive(true);
            GoogleLoginButton.GetComponent<Animator>().SetTrigger("Start");
            Configuration.AppConfig.IsGoogleLogged = false;
        }
    }


    /// <summary>
    ///Login
    /// </summary>
    /// <param name="status"></param>
    void AuthenticationByButton(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            GoogleLoginButton.SetActive(false);
            Configuration.AppConfig.IsGoogleLogged = true;
            PlayGamesPlatform.Activate();

        }
        else
        {
            GoogleLoginButton.SetActive(true);
            GoogleLoginButton.GetComponent<Animator>().SetTrigger("Start");
            Configuration.AppConfig.IsGoogleLogged = false;
            SendMessagetoUser("Error doing Login on Google Games!");
        }
    }


    private void SendMessagetoUser(string message)
    {
        AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
        AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");
        AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", message);
        AndroidJavaObject toast = Toast.CallStatic<AndroidJavaObject>("makeText", context, javaString, Toast.GetStatic<int>("LENGTH_SHORT"));
        toast.Call("show");
    }

}
