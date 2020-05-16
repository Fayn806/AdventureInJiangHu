using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : ScriptableObject
{
    public int type;// 12345 
    public int maxHP;
    public int curHP;
    public double maxWound1;
    public double maxWound2;
    public double wound1;
    public double wound2;
    public int maxPower1;
    public int maxPower2;
    public int power1;
    public int power2;
    public int wealth1;
    public int wealth2;

    public List<CardListItem> allCards;
    /// <summary>
    /// 当前牌组
    /// </summary>
    public List<CardListItem> curCards;
    /// <summary>
    /// 抽牌组
    /// </summary>
    public List<CardListItem> drawingCards;
    /// <summary>
    /// 弃牌组
    /// </summary>
    public List<CardListItem> threwCards;
    /// <summary>
    /// 手牌
    /// </summary>
    public List<CardListItem> handCards;

    public Item item { set; get; }
    public List<ItemList> itemLists = new List<ItemList>();

    public Enemy enemy;

    public Vector3 posInTilemap;
    public Vector3 posReal;
    public Vector3 posCur;

    public EventManager eventManager;

    public int curEventType;

    public RandomMap map;

}
