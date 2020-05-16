using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ItemSaleController : MonoBehaviour
{
    public GameObject[] items;
    

    public GameObject life1;
    public GameObject life2;
    private int price1 = 20;
    private int price2 = 20;
    // Start is called before the first frame update
    void Start()
    {
        int index = Random.Range(0, PlayerController.item.itemList.Count);
        ItemList item = PlayerController.item.itemList[index];
        items[0].transform.Find("item_1_price").GetComponent<Text>().text = item.i_wealth2;
        items[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/items/item_" + item.i_no);

        index = Random.Range(0, PlayerController.item.itemList.Count);
        item = PlayerController.item.itemList[index];
        items[1].transform.Find("item_2_price").GetComponent<Text>().text = item.i_wealth2;
        items[1].GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/items/item_" + item.i_no);

        index = Random.Range(0, PlayerController.item.itemList.Count);
        item = PlayerController.item.itemList[index];
        items[2].transform.Find("item_3_price").GetComponent<Text>().text = item.i_wealth2;
        items[2].GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/items/item_" + item.i_no);

        life1.GetComponent<Button>().onClick.AddListener(life1Click);
        life2.GetComponent<Button>().onClick.AddListener(life2Click);

        for(int i = 0; i < 3; i++)
        {
            GameObject obj = items[i];
            obj.GetComponent<Button>().onClick.AddListener(() => {
                buyItem(obj.gameObject);
            });
        }


    }

    void buyItem(GameObject obj)
    {
        int price = Convert.ToInt32(obj.transform.GetChild(0).GetComponent<Text>().text);
        if(PlayerController.wealth2 < price)
        {
            

            for(int i = 0; i < PlayerController.item.itemList.Count; i++)
            {
                Debug.Log(PlayerController.item.itemList[i].i_name);
                if (obj.GetComponent<Image>().sprite.name.Split('_')[1] == PlayerController.item.itemList[i].i_no)
                {
                    PlayerController.itemLists.Add(PlayerController.item.itemList[i]);
                    break;
                }
            } 

            obj.SetActive(false);
        }
    }

    void life1Click()
    {
        if(PlayerController.wealth2 > price1 && PlayerController.curHP < PlayerController.maxHP)
        {
            double hp = PlayerController.curHP * 0.1;
            PlayerController.curHP += Convert.ToInt32(hp);
            if(PlayerController.curHP > PlayerController.maxHP)
            {
                PlayerController.curHP = PlayerController.maxHP;
            }
            PlayerController.wealth2 -= price1;

            price1 += 20;
            life1.transform.Find("des").GetComponent<Text>().text = "10%最大生命  " + price1;
        }
    }
    void life2Click()
    {
        if (PlayerController.wealth2 > price2)
        {
            
            PlayerController.maxHP += 5;
            PlayerController.curHP += 5;

            PlayerController.wealth2 -= price2;
            price2 += 20;
            life2.transform.Find("des").GetComponent<Text>().text = "5点上限  " + price2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        for(int i = 0; i < PlayerController.itemLists.Count; i++)
        {
            GameObject.FindGameObjectWithTag("Items").transform.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            GameObject.FindGameObjectWithTag("Items").transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/items/item_" + PlayerController.itemLists[i].i_no);
        }
    }
}
