using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Levelmanage : MonoBehaviour {

    public GameObject pauseMenu;
    private static Levelmanage instance;
    public static Levelmanage Instance { get { return instance; } }
    public Text Timertext;
    public Text Endtimertext;

    public Transform spawnpoint;

    public GameObject endmenu;
    private GameObject player;
    private float levelduration;
    private float starttime;
    public float silvertime;
    public float goldtime;

    public void Togglepausemenu()
    {
        
        pauseMenu.gameObject.SetActive(!pauseMenu.activeSelf);
        Time.timeScale = (pauseMenu.activeSelf) ? 0 : 1;
    }

    public void Tomenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("start");
    }

    public void victory()
    {
        foreach(Transform t in endmenu.transform.parent)
        {
            t.gameObject.SetActive(false);
        }

        endmenu.SetActive(true);

        Rigidbody rig =player.GetComponent<Rigidbody>();
        rig.velocity = Vector3.zero;
        rig.angularVelocity = Vector3.zero;
        rig.useGravity = false;

        levelduration = Time.time - starttime;
        string minutes = ((int)levelduration / 60).ToString("00");
        string seconds = (levelduration % 60).ToString("00.00");

        Endtimertext.text = minutes + ":" + seconds;

        float duration =Time.time - starttime;
        if(duration<goldtime)
        {
            Gamemanager.Instance.money += 50;
        }
        else if (duration < silvertime)
        {
            Gamemanager.Instance.money += 20;
        }
        else
        {
            Gamemanager.Instance.money += 10;
        }

        Gamemanager.Instance.save();

        string saveString=null;

        LevelData level = new LevelData(SceneManager.GetActiveScene().name);
        saveString += (level.bestTime>duration || level.bestTime==0.0f)?duration.ToString():level.bestTime.ToString();
        saveString += '&';
        saveString += silvertime.ToString();
        saveString += '&';
        saveString +=goldtime.ToString();

        PlayerPrefs.SetString(SceneManager.GetActiveScene().name, saveString);

        //SceneManager.LoadScene("start");
        //Debug.Log("Victory");
    }
	// Use this for initialization
	void Start () {
        endmenu.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = spawnpoint.position;
        starttime = Time.time;
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (player.transform.position.y <= -10)
            death();

        levelduration = Time.time - starttime;
        string minutes = ((int)levelduration / 60).ToString("00");
        string seconds = (levelduration % 60).ToString("00.00");

        Timertext.text = minutes + ":" + seconds;
    }

    public void death()
    {
        player.transform.position = spawnpoint.position;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}
