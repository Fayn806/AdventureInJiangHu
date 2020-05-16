using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUIController : MonoBehaviour
{

    public GameObject hpText;
    public GameObject wound1Text;
    public GameObject wound2Text;
    public GameObject wound1Img;
    public GameObject wound2Img;
    public GameObject power1Text;
    public GameObject power2Text;
    public GameObject wealth1Text;
    public GameObject wealth2Text;
    public GameObject paikuText;
    public GameObject damageText;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.PlayMusic("map");
    }

    // Update is called once per frame
    void Update()
    {
        hpText.GetComponent<Text>().text = PlayerController.curHP + "/" + PlayerController.maxHP;
        wound1Text.GetComponent<Text>().text = NumberToChinese((int)Math.Floor(PlayerController.wound1 / PlayerController.maxWound1 * 10));
        wound2Text.GetComponent<Text>().text = NumberToChinese((int)Math.Floor(PlayerController.wound2 / PlayerController.maxWound2 * 10));
        power1Text.GetComponent<Text>().text = NumberToChinese(PlayerController.power1);
        power2Text.GetComponent<Text>().text = NumberToChinese(PlayerController.power2);
        wealth1Text.GetComponent<Text>().text = PlayerController.wealth1.ToString();
        wealth2Text.GetComponent<Text>().text = PlayerController.wealth2.ToString();
        wound1Img.GetComponent<Image>().fillAmount = (float)(PlayerController.wound1 / PlayerController.maxWound1);
        wound2Img.GetComponent<Image>().fillAmount = (float)(PlayerController.wound2 / PlayerController.maxWound2);
        paikuText.GetComponent<Text>().text = "牌库 " + PlayerController.curCards.Count.ToString();
        if(PlayerController.DealHurt(false) != 0)
        {
            damageText.GetComponent<Text>().text = "-" + PlayerController.DealHurt(false);
        } 
        else
        {
            damageText.GetComponent<Text>().text = "";
        }

        for (int i = 0; i < PlayerController.itemLists.Count; i++)
        {
            GameObject.FindGameObjectWithTag("Items").transform.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            GameObject.FindGameObjectWithTag("Items").transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/items/item_" + PlayerController.itemLists[i].i_no);
        }
    }

    /// <summary>
    /// 数字转中文
    /// </summary>
    /// <param name="number">eg: 22</param>
    /// <returns></returns>
    public string NumberToChinese(int number)
    {
        string res = string.Empty;
        string str = number.ToString();
        string schar = str.Substring(0, 1);
        switch (schar)
        {
            case "1":
                res = "一";
                break;
            case "2":
                res = "二";
                break;
            case "3":
                res = "三";
                break;
            case "4":
                res = "四";
                break;
            case "5":
                res = "五";
                break;
            case "6":
                res = "六";
                break;
            case "7":
                res = "七";
                break;
            case "8":
                res = "八";
                break;
            case "9":
                res = "九";
                break;
            default:
                res = "零";
                break;
        }
        if (str.Length > 1)
        {
            switch (str.Length)
            {
                case 2:
                case 6:
                    res += "十";
                    break;
                case 3:
                case 7:
                    res += "百";
                    break;
                case 4:
                    res += "千";
                    break;
                case 5:
                    res += "万";
                    break;
                default:
                    res += "";
                    break;
            }
            res += NumberToChinese(int.Parse(str.Substring(1, str.Length - 1)));
        }
        return res;
    }
}
