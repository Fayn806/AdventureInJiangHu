using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList
{
    /// <summary>
    /// 
    /// </summary>
    public string i_no { get; set; }
    /// <summary>
    /// 好剑
    /// </summary>
    public string i_name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> i_effect { get; set; }
    /// <summary>
    /// “无碍清净慧，皆因禅定生”。静禅功可令修习者清静安宁，祛除心中杂思，以达到心神合一，明心见性的开悟境界。
    /// </summary>
    public string i_description { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string i_wealth2 { get; set; }
}

public class Item
{
    /// <summary>
    /// 
    /// </summary>
    public List<ItemList> itemList { get; set; }
}

