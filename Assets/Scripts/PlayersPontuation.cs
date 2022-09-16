using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;

[CreateAssetMenu]
public class PlayersPontuation : ScriptableObject
{

    public string PlayerName;

    public Player NewPlayer;

    #region Player Class
    /// <summary>
    /// player Class
    /// </summary>
    [System.Serializable]
    public class Player
    {
        public string Name;
        public int pontuation;
        public GameObject PlayerContainer;

    }

    /// <summary>
    /// Array to Have the Json File correctly, like player:[]
    /// </summary>
    [System.Serializable]
    public class PlayerArray
    {
        public Player[] player;
    }
    #endregion

    #region Pontuation
    private string PontuationsFileName = "Pontuations.json";
    public List<Player> playersList;
    public PlayerArray myPlayerArray;
    #endregion

    #region JSON File
    public void ReadPontuationFile()
    {

        playersList = new List<Player>();
        myPlayerArray = new PlayerArray();

        if (!File.Exists(Application.persistentDataPath + "/" + PontuationsFileName))
            File.Create(Application.persistentDataPath + "/" + PontuationsFileName);
        else
        {
            //Read Json File
            string jsonData = File.ReadAllText(Application.persistentDataPath + "/" + PontuationsFileName);

            //Check if the file is empty
            if (!string.IsNullOrEmpty(jsonData))
            {
                myPlayerArray = JsonUtility.FromJson<PlayerArray>(jsonData);

                //Insert Information on the playerList
                playersList.Clear();
                foreach (var item in myPlayerArray.player)
                {
                    playersList.Add(item);
                }

                var a = playersList.OrderByDescending(x => x.pontuation).ToList();
                playersList = a;
            }
        }
    }
    public void WritePontuationFile()
    {
        if (playersList == null)
            return;
        if (playersList.Count == 0)
            return;

        //Write Json File
        myPlayerArray.player = playersList.ToArray();
        File.WriteAllText(Application.persistentDataPath + "/" + PontuationsFileName, JsonUtility.ToJson(myPlayerArray));
    }
    #endregion

    #region Order By Score
    public static int CompareByPontuation(Player Player1, Player Player2)
    {
        return Player1.pontuation.CompareTo(Player2.pontuation);
    }
    #endregion

    /// <summary>
    /// Add Score to the List
    /// </summary>
    /// <param name="Pontuation"></param>
    public void AddPontuation(int Pontuation)
    {
        NewPlayer = new Player();

        NewPlayer.Name = PlayerName;
        NewPlayer.pontuation = Pontuation;

        playersList.Add(NewPlayer);

        var a = playersList.OrderByDescending(x => x.pontuation).ToList();
        playersList = a;
    }

    public void Reset()
    {
        if (File.Exists(Application.persistentDataPath + "/" + PontuationsFileName))
            File.Delete(Application.persistentDataPath + "/" + PontuationsFileName);

        playersList = new List<Player>();
        myPlayerArray = new PlayerArray();
    }
}
