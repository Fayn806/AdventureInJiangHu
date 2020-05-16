using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffController : MonoBehaviour
{
    /// <summary>
    /// 内伤抵消，减少所受内伤的比例，受到内伤伤害时生效，回合结束后清零
    /// </summary>
    public double block_1 = 0;
    /// <summary>
    /// 外伤抵消，减少所受外伤的比例，受到外伤伤害时生效，回合结束后清零
    /// </summary>
    public double block_2 = 0;
    /// <summary>
    /// 内功增强比例，造成伤害时生效，提供内伤伤害加成，触发后减1
    /// </summary>
    public double strength_1 = 0;
    /// <summary>
    /// 外功增强比例，造成伤害时生效，提供外伤伤害加成，触发后减1
    /// </summary>
    public double strength_2 = 0;
    /// <summary>
    /// 闪避效果，受到伤害时生效，有几率免疫伤害，触发后减半
    /// </summary>
    public double dexterity = 0;
    /// <summary>
    /// 免疫伤害，受到伤害时生效，免疫此次伤害，触发后减1，回合结束后减1
    /// </summary>
    public double invincible = 0;
    /// <summary>
    /// 消耗降低，减少内外力消耗的点数，回合结束清零
    /// </summary>
    public double reduce = 0;
    /// <summary>
    /// 伤害反弹，受到伤害时生效，反弹所受伤害的比例，触发后减1，回合结束后清零
    /// </summary>
    public double rebound = 0;
    /// <summary>
    /// 负面效果，内伤加成，受到内伤时生效，伤害增加，触发后减1，回合结束后减1
    /// </summary>
    public double vulnerable_1 = 0;
    /// <summary>
    /// 负面效果，外伤加成，受到外伤时生效，伤害增加，触发后减1，回合结束后减1
    /// </summary>
    public double vulnerable_2 = 0;
    /// <summary>
    /// 负面效果，内功降低，造成的内伤伤害降低，触发后减1，回合结束后减1
    /// </summary>
    public double weak_1 = 0;
    /// <summary>
    /// 负面效果，外功降低，造成的外伤伤害降低，触发后减1，回合结束后减1
    /// </summary>
    public double weak_2 = 0;


    public void TurnEnd()
    {
                                block_1 = 0;
                                block_2 = 0;
        if(dexterity > 0)       dexterity -= 1;
                                invincible = 0;
                                reduce = 0;
        if (rebound > 0)        rebound -= 1;
        if (vulnerable_1 > 0)   vulnerable_1 -= 1;
        if (vulnerable_2 > 0)   vulnerable_2 -= 1;
        if (weak_1 > 0)         weak_1 -= 1;
        if (weak_2 > 0)         weak_2 -= 1;

    }

    /// <summary>
    /// buff控件
    /// </summary>
    public GameObject buffPanel
    {
        get
        {
            return GameObject.FindGameObjectWithTag("BuffPanel");
        }
    }

    public GameObject buffObject
    {
        get
        {
            return buffPanel.transform.Find("buff").gameObject;
        }
    }

    public GameObject debuffObject
    {
        get
        {
            return buffPanel.transform.Find("debuff").gameObject;
        }
    }

    public GameObject buffFind(string name)
    {
        return buffObject.transform.Find(name).gameObject;
    }
    public GameObject buffFindChild(string name)
    {
        return buffObject.transform.Find(name).transform.GetChild(0).gameObject;
    }

    public GameObject deBuffFind(string name)
    {
        return debuffObject.transform.Find(name).gameObject;
    }
    public GameObject deBuffFindChild(string name)
    {
        return debuffObject.transform.Find(name).transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        int showCount = 0;
        if(block_1 > 0)
        {
            buffFind("block_1").SetActive(true);
            buffFind("block_1").GetComponent<RectTransform>().anchoredPosition = new Vector2(-119.7f + showCount * 30f, 0);
            buffFindChild("block_1").GetComponent<Text>().text = block_1.ToString();
            showCount++;
        }
        else
        {
            buffFind("block_1").SetActive(false);
        }

        if (block_2 > 0)
        {
            buffFind("block_2").SetActive(true);
            buffFind("block_2").GetComponent<RectTransform>().anchoredPosition = new Vector2(-119.7f + showCount * 30f, 0);
            buffFindChild("block_2").GetComponent<Text>().text = block_2.ToString();
            showCount++;
        }
        else
        {
            buffFind("block_2").SetActive(false);
        }

        if (strength_1 > 0)
        {
            buffFind("strength_1").SetActive(true);
            buffFind("strength_1").GetComponent<RectTransform>().anchoredPosition = new Vector2(-119.7f + showCount * 30f, 0);
            buffFindChild("strength_1").GetComponent<Text>().text = strength_1.ToString();
            showCount++;
        }
        else
        {
            buffFind("strength_1").SetActive(false);
        }

        if (strength_2 > 0)
        {
            buffFind("strength_2").SetActive(true);
            buffFind("strength_2").GetComponent<RectTransform>().anchoredPosition = new Vector2(-119.7f + showCount * 30f, 0);
            buffFindChild("strength_2").GetComponent<Text>().text = strength_2.ToString();
            showCount++;
        }
        else
        {
            buffFind("strength_2").SetActive(false);
        }

        if (dexterity > 0)
        {
            buffFind("dexterity").SetActive(true);
            buffFind("dexterity").GetComponent<RectTransform>().anchoredPosition = new Vector2(-119.7f + showCount * 30f, 0);
            buffFindChild("dexterity").GetComponent<Text>().text = dexterity.ToString();
            showCount++;
        }
        else
        {
            buffFind("dexterity").SetActive(false);
        }

        if (invincible > 0)
        {
            buffFind("invincible").SetActive(true);
            buffFind("invincible").GetComponent<RectTransform>().anchoredPosition = new Vector2(-119.7f + showCount * 30f, 0);
            buffFindChild("invincible").GetComponent<Text>().text = invincible.ToString();
            showCount++;
        }
        else
        {
            buffFind("invincible").SetActive(false);
        }

        if (reduce > 0)
        {
            buffFind("reduce").SetActive(true);
            buffFind("reduce").GetComponent<RectTransform>().anchoredPosition = new Vector2(-119.7f + showCount * 30f, 0);
            buffFindChild("reduce").GetComponent<Text>().text = reduce.ToString();
            showCount++;
        }
        else
        {
            buffFind("reduce").SetActive(false);
        }

        if (rebound > 0)
        {
            buffFind("rebound").SetActive(true);
            buffFind("rebound").GetComponent<RectTransform>().anchoredPosition = new Vector2(-119.7f + showCount * 30f, 0);
            buffFindChild("rebound").GetComponent<Text>().text = rebound.ToString();
            showCount++;
        }
        else
        {
            buffFind("rebound").SetActive(false);
        }

        if (vulnerable_1 > 0)
        {
            
        }
        else
        {
            deBuffFind("vulnerable_1").SetActive(false);
        }

        if (vulnerable_2 > 0)
        {

        }
        else
        {
            deBuffFind("vulnerable_2").SetActive(false);
        }

        if (weak_1 > 0)
        {

        }
        else
        {
            deBuffFind("weak_1").SetActive(false);
        }
        if (weak_2 > 0)
        {

        }
        else
        {
            deBuffFind("weak_2").SetActive(false);
        }
    }

}
