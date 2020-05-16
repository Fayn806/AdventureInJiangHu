using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShopUIController : MonoBehaviour
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
    public GameObject discountText;

    public GameObject backBtn;

    public GameObject[] cardsSale;
    public GameObject[] cards_wealth2;
    // Start is called before the first frame update
    void Start()
    {
        initSaleCards();

        backBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            Invoke("backMap", 0.5f);
        });
    }

    void initSaleCards()
    {
        int discount = Random.Range(7, 15);
        discountText.GetComponent<Text>().text = "当前价格波动：" + (discount * 10).ToString() + "%";
        for (int i = 0; i < cardsSale.Length; i++)
        {
            int index = Random.Range(0, PlayerController.allCards.Count - 1);
            cardsSale[i].AddComponent<CardListItem>();

            while(PlayerController.allCards[index] == null && PlayerController.allCards[index].c_cost1 == "")
            {
                index = Random.Range(0, PlayerController.allCards.Count - 1);
            }

            cardsSale[i].GetComponent<CardListItem>().CopyItem(PlayerController.allCards[index]);

            GameObject nameText = cardsSale[i].transformFindObj("NameText");
            GameObject cost1Text = cardsSale[i].transformFindObj("Cost1Text");
            GameObject cost2Text = cardsSale[i].transformFindObj("Cost2Text");
            GameObject cardImg = cardsSale[i].transformFindObj("CardImg");
            GameObject effectText = cardsSale[i].transformFindObj("EffectText");

            nameText.GetComponent<Text>().text = cardsSale[i].GetComponent<CardListItem>().c_name;
            effectText.GetComponent<Text>().text = cardsSale[i].GetComponent<CardListItem>().c_effectDes;
            cost1Text.GetComponent<Text>().text = GameUtils.NumberToChinese(Convert.ToInt32(cardsSale[i].GetComponent<CardListItem>().c_cost1));
            cost2Text.GetComponent<Text>().text = GameUtils.NumberToChinese(Convert.ToInt32(cardsSale[i].GetComponent<CardListItem>().c_cost2));

            int price = Random.Range(Convert.ToInt32(cardsSale[i].GetComponent<CardListItem>().c_wealth2) - 5, Convert.ToInt32(cardsSale[i].GetComponent<CardListItem>().c_wealth2) + 5);
            
            cards_wealth2[i].GetComponent<Text>().text = Math.Floor(price * discount / 10f).ToString();
            GameObject obj = cardsSale[i];
            obj.GetComponent<Button>().onClick.AddListener(() => {
                buyACard(obj.gameObject);
            });
        }
    }

    void buyACard(GameObject obj)
    {
        int selectIndex = Convert.ToInt32(obj.name.Split('_')[2]) - 1;
        int price = Convert.ToInt32(cards_wealth2[selectIndex].GetComponent<Text>().text);
        
        if(PlayerController.wealth2 >= price)
        {
            Debug.Log("购买成功");
            PlayerController.curCards.Add(obj.GetComponent<CardListItem>());
            iTween.MoveAdd(obj, new Vector3(0, -350, 0), 0.5f);
            iTween.RotateTo(obj, new Vector3(90, 0, 0), 0.5f);
            //iTween.ScaleTo(obj, new Vector3(0.1f, 0.1f, 1), 0.5f);
            iTween.Destroy(obj, 0.6f);

            PlayerController.wealth2 -= price;
            cards_wealth2[selectIndex].GetComponent<Text>().text = "";
        } 
        else
        {
            return;
        }
    }

    void backMap()
    {
        GameManage.GameEventFinish();
    }

    // Update is called once per frame
    void Update()
    {
        hpText.GetComponent<Text>().text = PlayerController.curHP + "/" + PlayerController.maxHP;
        wound1Text.GetComponent<Text>().text = GameUtils.NumberToChinese((int)Math.Floor(PlayerController.wound1 / PlayerController.maxWound1 * 10));
        wound2Text.GetComponent<Text>().text = GameUtils.NumberToChinese((int)Math.Floor(PlayerController.wound2 / PlayerController.maxWound2 * 10));
        power1Text.GetComponent<Text>().text = GameUtils.NumberToChinese(PlayerController.power1);
        power2Text.GetComponent<Text>().text = GameUtils.NumberToChinese(PlayerController.power2);
        wealth1Text.GetComponent<Text>().text = PlayerController.wealth1.ToString();
        wealth2Text.GetComponent<Text>().text = PlayerController.wealth2.ToString();
        wound1Img.GetComponent<Image>().fillAmount = (float)(PlayerController.wound1 / PlayerController.maxWound1);
        wound2Img.GetComponent<Image>().fillAmount = (float)(PlayerController.wound2 / PlayerController.maxWound2);
        paikuText.GetComponent<Text>().text = "牌库 " + PlayerController.curCards.Count.ToString();
    }
}
