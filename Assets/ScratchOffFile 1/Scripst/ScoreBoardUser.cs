using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class ScoreBoardUser : MonoBehaviour
{
    public ScoreBoardData[] ProfilesData;
    private Random _random = new Random();
    private int _time;
    

    private string[] code;

    private string[] _names =
    {
        "Yunus", "Mesut", "Tolga", "Adem"
    };


    void Start()
    {
        //Debug.Log("Hello");
        //if (PlayerPrefs.GetInt("GetTime") == 0)
        //{
            UserAssignment();
        //    PlayerPrefs.SetInt("GetTime", DateTime.Now.Day);
        //}
        //else if ((PlayerPrefs.GetInt("GetTime") - DateTime.Now.Day) == 1)
        //{
        //}
        //else if ((PlayerPrefs.GetInt("GetTime") - DateTime.Now.Day) == 0)
        //{
        //}
    }

    public void UserAssignment()
    {
        int a = 0;
        code = new string[ProfilesData.Length];
        for (int i = 0; i < ProfilesData.Length; i++)
        {
            a = _random.Next(0, _names.Length);
            ProfilesData[i].ProfileAbout.text = _names[a] + "\n" + _random.Next(1, 10) + " M";
            //SaveJson(ProfilesData[i], i);
        }

    }

    public void SaveJson(ScoreBoardData[] humen, int i)
    {
        string[] data = new string[humen.Length];
        data[i] = JsonUtility.ToJson(humen[i].ProfileAbout);
        File.WriteAllText(Application.dataPath + "/SaveFile/bilgiler.json", data[i]);
        Debug.Log(data);
    }

}


[Serializable]
public class ScoreBoardData
{
    public Image Photo;
    public Text ProfileAbout;
}