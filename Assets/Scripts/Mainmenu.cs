using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelData
{
    public LevelData(string levelName)
    {
        string data = PlayerPrefs.GetString(levelName);

        if (data == "") return;
        string[] alldata = data.Split('&');

        bestTime = float.Parse(alldata[0]);
    }
    public float bestTime { set; get; }
}

public class Mainmenu : MonoBehaviour {
    public GameObject levelButtonPrefab;
    public GameObject levelButtoncontainer;

    public GameObject shopButtonPrefab;
    public GameObject shopButtoncontainer;
    public Text moneytxt;
    public Material playerMaterial;

    private Transform cameraTransform;
    private Transform cameralookat;
    private void Start()
    {
        ChangePlayerSkin(Gamemanager.Instance.currentSkinIndex);
        moneytxt.text = "Money: " + Gamemanager.Instance.money.ToString();
        cameraTransform = Camera.main.transform;
        Sprite[] thumbnails = Resources.LoadAll<Sprite>("Levels");
        foreach(Sprite thumbnail in thumbnails)
        {
            GameObject container = Instantiate(levelButtonPrefab) as GameObject;
            container.GetComponent<Image>().sprite = thumbnail;
            container.transform.SetParent(levelButtoncontainer.transform,false);

            LevelData level =new LevelData(thumbnail.name);
            container.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = level.bestTime.ToString("f");

            string sceneName = thumbnail.name;
            container.GetComponent<Button>().onClick.AddListener(()=>LoadLevel(sceneName));

        }

        Sprite[] textures = Resources.LoadAll<Sprite>("Playertexture");
        int texindex = 0;
        foreach (Sprite texture in textures)
        {
            GameObject container = Instantiate(shopButtonPrefab) as GameObject;
            container.GetComponent<Image>().sprite = texture;
            container.transform.SetParent(shopButtoncontainer.transform, false);

            int index = texindex;
            container.GetComponent<Button>().onClick.AddListener(() => ChangePlayerSkin(index));

            container.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = costs[index].ToString();

                if((Gamemanager.Instance.skinAvailability & 1 << index)== 1 << index)
                {
                 container.transform.GetChild(0).gameObject.SetActive(false);
                }
            
            texindex++;
        }
    }
    private void Update()
    {
        if(cameralookat!=null)
        {
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, cameralookat.rotation, 3*Time.deltaTime);
        }

    }
    private void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
	
    public void LookatMenu(Transform menutrans)
    {
        cameralookat = menutrans;
    }

    private void ChangePlayerSkin(int  index)
    {
        int cost = costs[index];
        if ((Gamemanager.Instance.skinAvailability & 1 << index)==1 << index)
        {
            Gamemanager.Instance.currentSkinIndex = index;
            Gamemanager.Instance.save();

            if (index >= 0 && index < 4) index = index + 12;
            else if (index >= 4 && index < 8) index = index + 4;
            else if (index >= 8 && index < 12) index = index - 4;
            else if (index >= 12 && index < 16) index = index - 12;


            float x = (index % 4) * 0.25f;
            float y = ((int)index / 4) * 0.25f;
            playerMaterial.SetTextureOffset("_MainTex", new Vector2(x, y));
        }
        else
        {
            //還沒有SKIN 是否購買
            

            if(Gamemanager.Instance.money >=cost)
            {
                Gamemanager.Instance.money -= cost;
                Gamemanager.Instance.skinAvailability += 1 << index;
                Gamemanager.Instance.save();
                moneytxt.text = "Money: " + Gamemanager.Instance.money.ToString();
                shopButtoncontainer.transform.GetChild(index).GetChild(0).gameObject.SetActive(false);
            }
        }


       

       
    }

    private int[] costs = { 10, 20, 40, 80, 80, 160, 320, 640, 640, 640, 640, 640, 1280, 1280, 1280, 1280 };
}
