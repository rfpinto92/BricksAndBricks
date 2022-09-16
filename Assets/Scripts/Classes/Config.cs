using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameDificult
{
    Easy,
    Normal,
    Hard
}


[SerializeField]
public class Config
{
    [SerializeField]
    private GameDificult gameDificult;

    public GameDificult GameDificult
    {
        get { return gameDificult; }
        set { gameDificult = value;

            switch (gameDificult)
            {
                case GameDificult.Easy:
                    maxballSpeed = 2.0f;
                    maxpaddleSpeed = 2.0f;
                    break;
                case GameDificult.Normal:
                    maxballSpeed = 5.0f;
                    maxpaddleSpeed = 3.0f;
                    break;
                case GameDificult.Hard:
                    maxballSpeed = 10.0f;
                    maxpaddleSpeed = 6.0f;
                    break;
                default:
                    break;
            }
        }
    }
    [SerializeField]
    
    private float maxballSpeed;

    public float MaxballSpeed
    {
        get {  return maxballSpeed; }
    }


    private float maxpaddleSpeed;

    public float maxPaddleSpeed
    {
        get { return maxpaddleSpeed; }
    }




    public bool EnableSound;

    public string ToJSON()
    {
        return JsonUtility.ToJson(this);
    }

    public Config FromJSON(string JSON)
    {
        return JsonUtility.FromJson<Config>(JSON);
    }

    public Config()
    {
        GameDificult = GameDificult.Normal;
        EnableSound = true;
    }
}

[SerializeField]
public class ActualLevel
{
    public int Level;
    public int Points;

    public ActualLevel()
    {
        Level = 1;
        Points = 0;
    }
}
