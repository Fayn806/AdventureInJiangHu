using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[System.Serializable]
public class CardListItem : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public string c_no { get; set; }
    /// <summary>
    /// 静禅功
    /// </summary>
    public string c_name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string c_type { get; set; }
    /// <summary>
    /// “无碍清净慧，皆因禅定生”。静禅功可令修习者清静安宁，祛除心中杂思，以达到心神合一，明心见性的开悟境界。
    /// </summary>
    public string c_description { get; set; }
    /// <summary>
    /// 外力
    /// </summary>
    public string c_cost1 { get; set; }
    /// <summary>
    /// 内力
    /// </summary>
    public string c_cost2 { get; set; }
    /// <summary>
    /// 卡牌效果
    /// </summary>
    public List<string> c_effect { get; set; }
    /// <summary>
    /// 卡牌效果描述
    /// </summary>
    public string c_effectDes { get; set; }
    /// <summary>
    /// 卡牌价值
    /// </summary>
    public string c_wealth2 { get; set; }

    public void CopyItem(CardListItem item)
    {
        c_no = item.c_no;
        c_name = item.c_name;
        c_type = item.c_type;
        c_description = item.c_description;
        c_cost1 = item.c_cost1;
        c_cost2 = item.c_cost2;
        c_effect = item.c_effect;
        c_effectDes = item.c_effectDes;
        c_wealth2 = item.c_wealth2;
    }
}

public class Card : ScriptableObject
{
    /// <summary>
    /// 
    /// </summary>
    public List<CardListItem> cardList { get; set; }
    public List<CardListItem> allCards { get; set; }
    /// <summary>
    /// 当前牌组
    /// </summary>
    public List<CardListItem> curCards { get; set; }
    /// <summary>
    /// 抽牌组
    /// </summary>
    public List<CardListItem> drawingCards { get; set; }
    /// <summary>
    /// 弃牌组
    /// </summary>
    public List<CardListItem> threwCards { get; set; }
    /// <summary>
    /// 手牌
    /// </summary>
    public List<CardListItem> handCards { get; set; }
}


