using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour {
    private static Gamemanager instance;
    public static Gamemanager Instance { get { return instance; } }

    public int currentSkinIndex=0;
    public int money=0;
    public int skinAvailability=1;
	// Use this for initialization
	void Awake () {
        instance = this;

        DontDestroyOnLoad(gameObject);

        if(PlayerPrefs.HasKey("CurrentSkin"))
        {
            //已經有紀錄
            currentSkinIndex = PlayerPrefs.GetInt("CurrentSkin");
            money = PlayerPrefs.GetInt("Money");
            skinAvailability = PlayerPrefs.GetInt("SkinAvailability");
        }
        else
        {
            save();
        }
	}
	public void save()
    {
        PlayerPrefs.SetInt("CurrentSkin", currentSkinIndex);
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.SetInt("SkinAvailability", skinAvailability);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
