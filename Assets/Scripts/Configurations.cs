using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu]
public class Configurations : ScriptableObject
{
    public Config AppConfig;

    public ActualLevel ActualLevel;

    private string ConfigFileName = "ConfigFile.json";

    #region JSON File
    public void ReadConfigFile()
    {
        if (AppConfig == null)
            AppConfig = new Config();

        //If dont exist file > The creation its nedded
        if (!File.Exists(Application.persistentDataPath + "/" + ConfigFileName))
            File.WriteAllText(Application.persistentDataPath + "/" + ConfigFileName, AppConfig.ToJSON());

        else
        {
            string DataRead = File.ReadAllText(Application.persistentDataPath + "/" + ConfigFileName);

            if (!String.IsNullOrEmpty(DataRead))
                AppConfig = AppConfig.FromJSON(DataRead);

            return;
        }

    }
    public void WriteConfigFile()
    {
        File.WriteAllText(Application.persistentDataPath + "/" + ConfigFileName, AppConfig.ToJSON());
    }
    #endregion

    #region GameLevel and Points
    public void RestartPoints()
    {
        ActualLevel = new ActualLevel();
    }

    #endregion


}
