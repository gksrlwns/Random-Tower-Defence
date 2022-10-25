using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class TowerData
{
    public int Towertype;
    public Vector3 TowerPosition;
    public TowerData(int type, Vector3 vec3)
    {
        Towertype = type;
        TowerPosition = vec3;
    }
}

public class PlayerData
{
    public int stage = 1;
    public int money = 500;
    public int restLife = 20;
    public List<TowerData> towers = new List<TowerData>();
    
}
public class BackEndManager : MonoBehaviour
{
    public static BackEndManager instance;
    public PlayerData nowPlayerData = new PlayerData();
    public string path;
    public int nowSlot;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        path = Application.persistentDataPath + "/savefile";
        print(path);
    }
    
    public void SaveFile()
    {
        string data = JsonUtility.ToJson(nowPlayerData);
        File.WriteAllText(path + nowSlot.ToString(), data);
    }
    
    public void LoadFile()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        nowPlayerData = JsonUtility.FromJson<PlayerData>(data);
    }
    public void ClearFile()
    {
        nowSlot = -1;
        nowPlayerData = new PlayerData();
    }

}
