using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private User user = null;
    [SerializeField]
    private Transform textPool = null;
    public Transform Pool { get { return textPool; } }
    public User CurrentUser { get { return user; } }

    private string SAVE_PATH = "";
    private readonly string SAVE_FILENAME = "/SaveFile.txt";

    public UIManager uiManager { get; private set; }

    public void EarnEnergyPerSecond()
    {
        foreach(Soldier soldier in user.soldierList)
        {
            user.energy += soldier.ePs * soldier.amount;
        }
        uiManager.UpdateEnergyPanel();
    }

    private void Awake()
    {
        SAVE_PATH = Application.dataPath + "/Save";
        if(!Directory.Exists(SAVE_PATH))
        {
            Directory.CreateDirectory(SAVE_PATH);
        }

        LoadFromJson();
        uiManager = GetComponent<UIManager>();

        InvokeRepeating("SaveToJson", 1f, 60f);
        InvokeRepeating("EarnEnergyPerSecond", 0f, 1f);
    }

    private void LoadFromJson()
    {
        string json = "";

        if(File.Exists(SAVE_PATH + SAVE_FILENAME))
        {
            json = File.ReadAllText(SAVE_PATH + SAVE_FILENAME);
            user = JsonUtility.FromJson<User>(json);
        }
        else
        {
            SaveToJson();
            LoadFromJson();
        }
    }

    private void SaveToJson()
    {
        SAVE_PATH = Application.dataPath + "/Save";
        if (user == null) return;
        string json = JsonUtility.ToJson(user, true);
        File.WriteAllText(SAVE_PATH + SAVE_FILENAME, json, System.Text.Encoding.UTF8);
    }

    private void OnApplicationQuit()
    {
       SaveToJson();
    }
}
