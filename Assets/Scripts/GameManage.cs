using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public static class Util
{
    public static GameObject transformFindObj(this GameObject obj, string name)
    {
        if (obj != null) return obj.transform.Find(name).gameObject;
        else return null;
    }
}
public class GameManage : MonoBehaviour
{

    private static GameObject startScene;
    static GameObject characterScene;

    static string characterName;
    static Scene scene;

    // Start is called before the first frame update
    void Start()
    {
        //startScene = GameObject.FindGameObjectWithTag("StartScene");
        //characterScene = GameObject.FindGameObjectWithTag("CharacterScene");

    }

    private void Awake()
    {
        startScene = GameObject.FindGameObjectWithTag("StartScene");
        characterScene = GameObject.FindGameObjectWithTag("CharacterScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void InitGameobject()
    {

    }

    private static GameObject TransFormFindObj(string name)
    {
        return startScene.transform.Find(name).gameObject;
    }

    public static void GameCG()
    {
        
        startScene = GameObject.FindGameObjectWithTag("StartScene");
        characterScene = GameObject.FindGameObjectWithTag("CharacterScene");
        //UI控制
        GameObject startCGVideoPlayer = startScene.transformFindObj("StartCGVideoPlayer");
        startCGVideoPlayer.GetComponent<VideoPlayer>().Play();

        GameObject loadingCanvas = startScene.transformFindObj("LoadingCanvas");
        GameObject menuCanvas = startScene.transformFindObj("MenuCanvas");
        loadingCanvas.SetActive(false);
        menuCanvas.SetActive(true);

        GameObject clickToStartGameText = menuCanvas.transformFindObj("ClickToStartGameText");
        GameObject clickToSkipCGText = menuCanvas.transformFindObj("ClickToSkipCGText");
        clickToStartGameText.SetActive(false);

        GameObject logoImg = menuCanvas.transformFindObj("LogoImg");
        GameObject sceneBgImg = menuCanvas.transformFindObj("SceneBgImg");
        logoImg.SetActive(false);
        sceneBgImg.SetActive(false);

        GameObject menuButtons = menuCanvas.transformFindObj("MenuButtons");
        menuButtons.SetActive(false);
    }

    public static void GameReady()
    {
        if (SceneManager.GetActiveScene().name != "StartScene")
        {
            SceneManager.LoadScene("StartScene");
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            return;
        }
        //UI控制
        GameObject menuCanvas = startScene.transformFindObj("MenuCanvas");

        GameObject clickToStartGameText = menuCanvas.transformFindObj("ClickToStartGameText");
        GameObject clickToSkipCGText = menuCanvas.transformFindObj("ClickToSkipCGText");
        //显示点击开始游戏文本
        clickToStartGameText.SetActive(true);
        //隐藏跳过cg文本
        clickToSkipCGText.SetActive(false);

        GameObject logoImg = menuCanvas.transformFindObj("LogoImg");
        GameObject sceneBgImg = menuCanvas.transformFindObj("SceneBgImg");

        logoImg.SetActive(true);
        sceneBgImg.SetActive(true);

        GameObject startCGVideoPlayer = startScene.transformFindObj("StartCGVideoPlayer");
        startCGVideoPlayer.gameObject.SetActive(false);

    }

    private static void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {

        if (SceneManager.GetActiveScene().name != "StartScene")
        { 
            return;
        }

        startScene = GameObject.FindGameObjectWithTag("StartScene");
        characterScene = GameObject.FindGameObjectWithTag("CharacterScene");
        ////UI控制
        //GameObject startCGVideoPlayer = startScene.transformFindObj("StartCGVideoPlayer");
        //startCGVideoPlayer.GetComponent<VideoPlayer>().Play();

        GameObject loadingCanvas = startScene.transformFindObj("LoadingCanvas");
        GameObject menuCanvas = startScene.transformFindObj("MenuCanvas");
        loadingCanvas.SetActive(false);
        menuCanvas.SetActive(true);

        GameObject clickToStartGameText = menuCanvas.transformFindObj("ClickToStartGameText");
        GameObject clickToSkipCGText = menuCanvas.transformFindObj("ClickToSkipCGText");
        clickToStartGameText.SetActive(false);

        GameObject logoImg = menuCanvas.transformFindObj("LogoImg");
        GameObject sceneBgImg = menuCanvas.transformFindObj("SceneBgImg");
        logoImg.SetActive(false);
        sceneBgImg.SetActive(false);

        GameObject menuButtons = menuCanvas.transformFindObj("MenuButtons");
        menuButtons.SetActive(false);
    }

    public static void GameMenuShow()
    {
        
        //UI控制
        GameObject menuCanvas = startScene.transformFindObj("MenuCanvas");
        menuCanvas.SetActive(true);

        GameObject clickToStartGameText = menuCanvas.transformFindObj("ClickToStartGameText");
        clickToStartGameText.SetActive(false);

        GameObject logoImg = menuCanvas.transformFindObj("LogoImg");
        //logo上移
        (logoImg.GetComponent<Animation>()).Play("MenuPop");

        GameObject menuButtons = menuCanvas.transformFindObj("MenuButtons");
        //显示菜单
        menuButtons.SetActive(true);
        (menuButtons.GetComponent<Animation>()).Play("ButtonsAni");
    }

    public static void GameStart()
    {
        //UI控制
        GameObject menuCanvas = startScene.transformFindObj("MenuCanvas");
        menuCanvas.SetActive(false);

        GameObject loadingCanvas = startScene.transformFindObj("LoadingCanvas");
        loadingCanvas.SetActive(true);
        
        GameObject loadingVideoPlayer = loadingCanvas.transformFindObj("LoadingVideoPlayer");

        loadingVideoPlayer.GetComponent<VideoPlayer>().loopPointReached += GameManage_loopPointReached;
        
    }



    private static void GameManage_loopPointReached(VideoPlayer source)
    {
        SceneManager.LoadSceneAsync("CharacterScene");
    }

    public static void GameSelectCharacter(int index)
    {
        switch(index)
        {
            case 0:
                characterName = "shaolin";
                AudioManager.PlaySound("shaolin");
                break;
            case 1:
                characterName = "wudang";
                break;
            case 2:
                characterName = "emei";
                break;
            case 3:
                characterName = "wuxian";
                break;
            case 4:
                characterName = "xuannv";
                break;
        }
    }

    public static void GameAdventureStart()
    {
        if(characterName == "shaolin")
        {
            PlayerController.PlayerSet("type", 0);
            PlayerController.PlayerSet("maxHP", 100);
            PlayerController.PlayerSet("curHP", 100);
            PlayerController.PlayerSet("maxWound1", 100);
            PlayerController.PlayerSet("maxWound2", 100);
            PlayerController.PlayerSet("wound1", 0);
            PlayerController.PlayerSet("wound2", 0);
            PlayerController.PlayerSet("maxPower1", 8);
            PlayerController.PlayerSet("maxPower2", 11);
            PlayerController.PlayerSet("power1", 8);
            PlayerController.PlayerSet("power2", 11);
            PlayerController.PlayerSet("wealth1", 60);
            PlayerController.PlayerSet("wealth2", 80);

            PlayerController.curCards = new List<CardListItem>()
            {
                PlayerController.allCards[0],
                PlayerController.allCards[1],
                PlayerController.allCards[8],
                PlayerController.allCards[9],
                PlayerController.allCards[14],
                PlayerController.allCards[15],
                PlayerController.allCards[23],
                PlayerController.allCards[24],
                PlayerController.allCards[33],
                PlayerController.allCards[34],
                PlayerController.allCards[38],
                PlayerController.allCards[39]
            };
        }
        PlayerController.eventManager = null;
        PlayerController.map = null;

        PlayerController.posInTilemap = new Vector3(-2, 0, 0);
        PlayerController.posReal = new Vector3(-2, (float)0.5, 0);
        PlayerController.posCur = new Vector3(-2, (float)0.5, 0);

        GameManage.GetEnemyInfo();
        GameManage.GetItemInfo();
        SceneManager.LoadSceneAsync("MapScene");
    }

    public static void GameStep()
    {
        if (PlayerController.curEventType == 1)
        {
            GameManage.GameBattleEnter();
        }
        else if (PlayerController.curEventType == 4)
        {
            GameManage.GameShopEnter();
        }
        else if (PlayerController.curEventType == 3)
        {
            GameManage.GameTavernEnter();
        }
        else if (PlayerController.curEventType == 5)
        {
            GameManage.GameSurpriseEnter();
        }
        else if (PlayerController.curEventType == 2)
        {
            GameManage.GameBattleEnter();
        }
    }
    public static void GameEventFinish()
    {
        
        PlayerController.drawingCards.Clear();
        PlayerController.threwCards.Clear();
        scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("MapScene");
    }


    public static void GameBattleEnter()
    {
        Debug.Log("战斗开始");
        scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("BattleScene");
    }

    public static void GameShopEnter()
    {
        Debug.Log("进入商店");
        scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("ShopScene");
    }

    public static void GameTavernEnter()
    {
        Debug.Log("进入酒馆");
        scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("TavernScene");
    }
    public static void GameSurpriseEnter()
    {
        Debug.Log("进入奇遇");
        scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("SurpriseScene");
    }

    public static void GameLoad()
    {
        PlayerController.Load();
        SceneManager.LoadScene("MapScene");
    }

    public static void GameSetUp()
    {

    }

    public static void GameStatistics()
    {

    }

    public static void GameExit()
    {

    }


    public static void GetJsonInfo(int cur)//这个方法给按钮注册
    {
        

        string path = Application.streamingAssetsPath;

        switch (cur)
        {
            case 0:
                path += "/SCard.json";
                break;
            case 1:
                path += "/WCard.json";
                break;
            case 2:
                path += "/ECard.json";
                break;
            case 3:
                path += "/XCard.json";
                break;
            case 4:
                path += "/NCard.json";
                break;
        }

        path = Application.streamingAssetsPath + "/SCard.json";
        StreamReader streamreader = new StreamReader(path);//读取数据，转换成数据流
        JsonReader js = new JsonReader(streamreader);//再转换成json数据
        Card card = JsonMapper.ToObject<Card>(js);//读取

        PlayerController.allCards.Clear();
        for (int i = 0; i < card.cardList.Count; i++)
        {
            PlayerController.allCards.Add(card.cardList[i]);
        }
    }

    public static void GetEnemyInfo()//这个方法给按钮注册
    {
        string path = Application.streamingAssetsPath + "/Enemy.json";

        StreamReader streamreader = new StreamReader(path);//读取数据，转换成数据流
        JsonReader js = new JsonReader(streamreader);//再转换成json数据
        PlayerController.enemy = JsonMapper.ToObject<Enemy>(js);//读取
        
    }

    public static void GetItemInfo()//这个方法给按钮注册
    {
        string path = Application.streamingAssetsPath + "/Item.json";

        StreamReader streamreader = new StreamReader(path);//读取数据，转换成数据流
        JsonReader js = new JsonReader(streamreader);//再转换成json数据
        PlayerController.item = JsonMapper.ToObject<Item>(js);//读取

    }
}
