using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestInternetConnection : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        GameObject.Find("BtnRetryConnection").GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("RetryInternetConnection"));
        GameObject.Find("BtnQuit").GetComponent<Button>().onClick.AddListener(() => Application.Quit());

        switch (Application.internetReachability)
        {
            case NetworkReachability.NotReachable:
                break;
            case NetworkReachability.ReachableViaCarrierDataNetwork:
            case NetworkReachability.ReachableViaLocalAreaNetwork:
                SceneManager.LoadScene("MainMenu");
                break;
            default:
                break;
        }
    }
    }
