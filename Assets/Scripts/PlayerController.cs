using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
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
    public static Card card;
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

    public static Enemy enemy;

    public static Vector3 posInTilemap = new Vector3(-2, 0, 0);
    public static Vector3 posReal= new Vector3(-2, (float)0.5, 0);
    public static Vector3 posCur = new Vector3(-2, (float)0.5, 0);

    public static EventManager eventManager;
    public static int curEventType = 0;

    public static RandomMap map;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Move()
    {

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
