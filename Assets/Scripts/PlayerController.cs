using LitJson;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
using Random = UnityEngine.Random;

public class PlayerController
{
    public static int type;// 12345 
    public static int maxHP;
    public static int curHP;
    public static double maxWound1;
    public static double maxWound2;
    public static double wound1;
    public static double wound2;
    public static int maxPower1;
    public static int maxPower2;
    public static int power1;
    public static int power2;
    public static int wealth1;
    public static int wealth2;

    public static List<CardListItem> allCards = new List<CardListItem>();
    /// <summary>
    /// 当前牌组
    /// </summary>
    public static List<CardListItem> curCards = new List<CardListItem>();
    /// <summary>
    /// 抽牌组
    /// </summary>
    public static List<CardListItem> drawingCards = new List<CardListItem>();
    /// <summary>
    /// 弃牌组
    /// </summary>
    public static List<CardListItem> threwCards = new List<CardListItem>();
    /// <summary>
    /// 手牌
    /// </summary>
    public static List<CardListItem> handCards = new List<CardListItem>();

    public static Enemy enemy { set; get; }
    public static Item item { set; get; }
    public static List<ItemList> itemLists = new List<ItemList>();

    public static Vector3 posInTilemap = new Vector3(-2, 0, 0);
    public static Vector3 posReal = new Vector3(-2, (float)0.5, 0);
    public static Vector3 posCur = new Vector3(-2, (float)0.5, 0);

    public static EventManager eventManager;

    public static int curEventType = 0;

    public static RandomMap map { set; get; }

    public static void Save()
    {
        //card.allCards = PlayerController.allCards;
        //card.curCards = PlayerController.curCards;

#if UNITY_EDITOR



        SaveData data = ScriptableObject.CreateInstance<SaveData>();//创建可序列化对象
        data.maxHP = maxHP;
        data.curHP = curHP;
        data.maxWound1 = maxWound1;
        data.maxWound2 = maxWound2;
        data.wound1 = wound1;
        data.wound2 = wound2;
        data.maxPower1 = maxPower1;
        data.maxPower2 = maxPower2;
        data.power1 = power1;
        data.power2 = power2;
        data.wealth1 = wealth1;
        data.wealth2 = wealth2;
        data.posInTilemap = posInTilemap;
        data.posReal = posReal;
        data.posCur = posCur;
        data.curEventType = curEventType;
        data.item = item;
        data.itemLists = itemLists;
        data.eventManager = PlayerController.eventManager;
        data.map = PlayerController.map;
        data.allCards = PlayerController.allCards;
        data.enemy = PlayerController.enemy;

        string path = "Assets/Resources/Data/data.asset";
        AssetDatabase.CreateAsset(data, path);


        //Card sCard = ScriptableObject.CreateInstance<Card>();

        //sCard.curCards = PlayerController.curCards;
        //sCard.allCards = PlayerController.allCards;

        //path = "Assets/Resources/Data/card.asset";
        //UnityEditor.AssetDatabase.CreateAsset(sCard, path);

        //Enemy sEnemy = ScriptableObject.CreateInstance<Enemy>();
        //sEnemy.enemy1 = PlayerController.enemy.enemy1;
        //sEnemy.enemy2 = PlayerController.enemy.enemy2;
        //sEnemy.enemy3 = PlayerController.enemy.enemy3;

        //path = "Assets/Resources/Data/enemy.asset";
        //UnityEditor.AssetDatabase.CreateAsset(sEnemy, path);

        //EventManager sEventManeger = ScriptableObject.CreateInstance<EventManager>();
        //sEventManeger.randomMap = PlayerController.eventManager.randomMap;
        //sEventManeger.hexagons = PlayerController.eventManager.hexagons;
        //sEventManeger.allHexItems = PlayerController.eventManager.allHexItems;
        //sEventManeger.allPoints = PlayerController.eventManager.allPoints;
        //sEventManeger.allEvents = PlayerController.eventManager.allEvents;

        //path = "Assets/Resources/Data/eventManager.asset";
        //UnityEditor.AssetDatabase.CreateAsset(sEventManeger, path);


        //RandomMap sMap = ScriptableObject.CreateInstance<RandomMap>();
        //sMap.hexCount = PlayerController.map.hexCount;
        //sMap.sectionCount = PlayerController.map.sectionCount;
        //sMap.repeatCount = PlayerController.map.repeatCount;
        //sMap.hexagons = PlayerController.map.hexagons;
        //sMap.repeatXs = PlayerController.map.repeatXs;
        //sMap.arrow = PlayerController.map.arrow;

        MapHs mapHs = ScriptableObject.CreateInstance<MapHs>();
        foreach (Hexagon h in PlayerController.map.hexagons)
        {
            mapHs.bgIndexs.Add(h.bgIndex);
            mapHs.centerXs.Add(h.centerX);
            mapHs.centerYs.Add(h.centerY);
            mapHs.indexs.Add(h.index);
            mapHs.hexBgPositions.Add(h.hexBgPosition);
            mapHs.hexItemss.Add(h.hexItems);
        }

        path = "Assets/Resources/Data/map.asset";
        AssetDatabase.CreateAsset(mapHs, path);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        //try
        //{
        //    string path = Application.dataPath + "/" + "my_path.bin";
        //    FileStream file = null;
        //    BinaryFormatter bf = new BinaryFormatter();
        //    file = File.Open(path, FileMode.Create);
        //    bf.Serialize(file, map);
        //    file.Close();
        //}
        //catch (System.Exception ex)
        //{
        //    Debug.LogError("存储失败----" + ex.Message);
        //}
#endif

    }


    public static void Load()
    {
#if UNITY_EDITOR
        string path = "Data/data";
        SaveData _data = Resources.Load<SaveData>(path);

        path = "Data/map";
        MapHs _map = Resources.Load<MapHs>(path);
        path = "Data/map";

        //path = "Data/card";
        //Card _card = Resources.Load<Card>(path);

        //path = "Data/enemy";
        //Enemy _enemy = Resources.Load<Enemy>(path);

        //path = "Data/eventManager";
        //EventManager _eventManager = Resources.Load<EventManager>(path);

        //path = "Data/map";
        //RandomMap _map = Resources.Load<RandomMap>(path);

        //_data.card = _card;
        //_data.enemy = _enemy;
        //_data.eventManager = _eventManager;
        //_data.map = _map;

        //PlayerController.maxHP = _data.maxHP;
        //PlayerController.curHP = _data.curHP;
        //PlayerController.maxWound1 = _data.maxWound1;
        //PlayerController.maxWound2 = _data.maxWound2;
        //PlayerController.wound1 = _data.wound1;
        //PlayerController.wound2 = _data.wound2;
        //PlayerController.maxPower1 = _data.maxPower1;
        //PlayerController.maxPower2 = _data.maxPower2;
        //PlayerController.power1 = _data.power1;
        //PlayerController.power2 = _data.power2;
        //PlayerController.wealth1 = _data.wealth1;
        //PlayerController.wealth2 = _data.wealth2;
        //PlayerController.card = _data.card;
        //PlayerController.allCards = _card.allCards;
        //PlayerController.curCards = _card.curCards;
        //PlayerController.enemy = _data.enemy;
        //PlayerController.posInTilemap = _data.posInTilemap;
        //PlayerController.posReal = _data.posReal;
        //PlayerController.posCur = _data.posCur;
        //PlayerController.eventManager = _data.eventManager;
        //PlayerController.curEventType = _data.curEventType;
        //PlayerController.map = _data.map;
#endif
        return;
    }

    #region 方法
    public static GameObject man
    {
        get
        {
            return GameObject.FindGameObjectWithTag("man");
        }
    }

    public static BuffController GetBuffController()
    {
        return man.GetComponent<BuffController>();
    }

    public static void SetBuff(string name, int type, double value)
    {
        string effectName = name;

        if (effectName == "block")
        {
            if(type == 1)
            {
                PlayerController.man.GetComponent<BuffController>().block_1 += value;
            }
            if (type == 2)
            {
                PlayerController.man.GetComponent<BuffController>().block_2 += value;
            }
        }
        else if (effectName == "strength")
        {
            if (type == 1)
            {
                PlayerController.man.GetComponent<BuffController>().strength_1 += value;
            }
            if (type == 2)
            {
                PlayerController.man.GetComponent<BuffController>().strength_2 += value;
            }
        }
        else if (effectName == "dexterity")
        {
            PlayerController.man.GetComponent<BuffController>().dexterity += value;
        }
        else if (effectName == "invincible")
        {
            PlayerController.man.GetComponent<BuffController>().invincible += value;
        }
        else if (effectName == "reduce")
        {
            PlayerController.man.GetComponent<BuffController>().reduce += value;
        }
        else if (effectName == "rebound")
        {
            PlayerController.man.GetComponent<BuffController>().rebound += value;
        }
        else if (effectName == "vulnerable")
        {
            if (type == 1)
            {
                PlayerController.man.GetComponent<BuffController>().vulnerable_1 += value;
            }
            if (type == 2)
            {
                PlayerController.man.GetComponent<BuffController>().vulnerable_2 += value;
            }
        }
        else if (effectName == "weak")
        {
            if (type == 1)
            {
                PlayerController.man.GetComponent<BuffController>().weak_1 += value;
            }
            if (type == 2)
            {
                PlayerController.man.GetComponent<BuffController>().weak_2 += value;
            }
        }
        else if (effectName == "power")
        {
            if (type == 1)
            {
                PlayerController.power1 += Convert.ToInt32(value);
            }
            if (type == 2)
            {
                PlayerController.power2 += Convert.ToInt32(value);
            }
        }
        else if (effectName == "random")
        {

        }
        else if (effectName == "free")
        {

        }
        else if (effectName == "repeat")
        {

        }
        else if (effectName == "hurt")
        {
            PlayerController.curHP -= Convert.ToInt32(value);
        }
    }


    public static int DealHurt(bool b = true)
    {
        //内外伤总和的 10%最大生命值 伤害
        int wound1 = (int)Math.Floor(PlayerController.wound1 / PlayerController.maxWound1 * 10);
        int wound2 = (int)Math.Floor(PlayerController.wound2 / PlayerController.maxWound2 * 10);

        double per = (wound1 + wound2) / 100f;
        int damage = (int)Math.Floor(per * PlayerController.maxHP);
        
        if(b == true)
        {
            PlayerController.curHP -= damage;
            PlayerController.wound1 = PlayerController.wound1 * 0.9;
            PlayerController.wound2 = PlayerController.wound2 * 0.9;
            Debug.Log("受到伤害===" + damage);
        }
        return damage;
    }

    public static bool PlayerSet(string pro, object value)
    {
        try
        {
            switch (pro)
            {
                case "type":
                    type = (int)value;
                    break;
                case "maxHP":
                    maxHP = (int)value;
                    break;
                case "curHP":
                    curHP = (int)value;
                    break;
                case "maxWound1":
                    maxWound1 = Convert.ToDouble(value);
                    break;
                case "maxWound2":
                    maxWound2 = Convert.ToDouble(value);
                    break;
                case "wound1":
                    wound1 = Convert.ToDouble(value);
                    break;
                case "wound2":
                    wound2 = Convert.ToDouble(value);
                    break;
                case "maxPower1":
                    maxPower1 = (int)value;
                    break;
                case "maxPower2":
                    maxPower2 = (int)value;
                    break;
                case "power1":
                    power1 = (int)value;
                    break;
                case "power2":
                    power2 = (int)value;
                    break;
                case "wealth1":
                    wealth1 = (int)value;
                    break;
                case "wealth2":
                    wealth2 = (int)value;
                    break;
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            return false;
        }


        return true;
    }

    public static bool NewTurn()
    {
        PlayerController.power1 = PlayerController.maxPower1;
        PlayerController.power2 = PlayerController.maxPower2;

        PlayerController.man.GetComponent<BuffController>().TurnEnd();

        return true;
    }

    public static bool FirstShuffleCards()
    {
        List<int> indexList = new List<int>();
        for (int i = 0; i < PlayerController.curCards.Count; i++)
        {
            indexList.Add(i);
        }

        while (indexList.Count != 0)
        {
            int index = Random.Range(0, indexList.Count);
            PlayerController.drawingCards.Add(PlayerController.curCards[indexList[index]]);
            indexList.RemoveAt(index);
        }

        return true;
    }

    public static bool ShuffleCards()
    {
        List<int> indexList = new List<int>();
        for(int i = 0; i < PlayerController.threwCards.Count; i++)
        {
            indexList.Add(i);
        }

        while(indexList.Count != 0)
        {
            int index = Random.Range(0, indexList.Count);
            PlayerController.drawingCards.Add(PlayerController.threwCards[indexList[index]]);
            indexList.RemoveAt(index);
        }
        PlayerController.threwCards.Clear();
        return true;
    }

    public static CardListItem DrawACard()
    {
        if(PlayerController.drawingCards.Count > 0)
        {
            CardListItem cardListItem = PlayerController.drawingCards[0];
            PlayerController.drawingCards.RemoveAt(0);
            return cardListItem;
        }
        else
        {
            return null;
        }
    }

}
#endregion


[System.Serializable]



public class MapHs : ScriptableObject
{
    public List<int> bgIndexs = new List<int>();
    public List<int> centerXs = new List<int>();
    public List<int> centerYs = new List<int>();
    public List<int> indexs = new List<int>();
    public List<Vector3> hexBgPositions = new List<Vector3>();
    public List<List<HexItem>> hexItemss = new List<List<HexItem>>();
}
