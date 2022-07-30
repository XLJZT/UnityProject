using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : SingleTon<SaveManager>
{


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Save(GameManager.Instance.playerStats.character, GameManager.Instance.playerStats.name);
            Debug.Log(GameManager.Instance.playerStats.name);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load(GameManager.Instance.playerStats.character, GameManager.Instance.playerStats.name);
            
        }
    }

    public void Save(object data,string key)
    {
        //数据转成json
        var jsonData = JsonUtility.ToJson(data, true);
        //json转成二进制
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.Save();
    }
    public void Load(object data, string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            //PlayerPrefs.GetString(key)将二进制转为json，覆盖到data上
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), data);
            Debug.Log("111");

        }
    }
}
