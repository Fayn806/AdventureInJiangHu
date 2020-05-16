using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public Sprite[] characterImgs;
    public Button[] characterBtns;
    public GameObject characterImg;
    public GameObject characterDesText;

    public Button checkCardsBtn;
    public GameObject cardPrefab;
    public Button confirmBtn;

    private Card myCard;
    private GameObject card;
    private int curSelected;

    private GameObject canvas;
    private GameObject cardPanel;
    private GameObject wuxueText;
    private GameObject cardContent;
    private GameObject closeCardPanelBtn;

    static private string[] desArr = { "少林派乃正派武林中的泰山北斗。菩提达摩祖师以禅入武，在少林派创出“七十二绝技”，无不冠绝天下。",
    "武功博大精深，最重内功修养，创派祖师真君道法高深，太极神功名满天下，堪称天下内家功法第一。",
    "峨眉派乃武林三大名宗之一，天下武功出峨眉。峨眉。派如其名——“天下秀”。峨眉武学刚柔并济，道释相融，定慧双修。",
    "五仙教江湖人称苗疆五毒教，教义甚是极端，恩仇必报。教中炼毒、养蛊诸多奇诡之术，无不骇人听闻。武林中人无论正邪，无不避而远之。",
    "璇女派立于璇女峰，又称玉女峰，峰上有玄冰。传说曾有天女降于峰上，并留下一部一明珏奇功，但凡女子修炼，便可清俊脱俗，冰肌玉骨，飞升成仙。"};

    private void Awake()
    {
        canvas = this.gameObject.transformFindObj("Canvas");
        cardPanel = canvas.transformFindObj("CardPanel");

        wuxueText = cardPanel.transformFindObj("WuxueText");
        cardContent = cardPanel.transformFindObj("Scroll View").transformFindObj("Viewport").transformFindObj("Content");
        closeCardPanelBtn = cardPanel.transformFindObj("CloseCardPanelBtn");
        closeCardPanelBtn.GetComponent<Button>().onClick.AddListener(closeCardPanel);

    }
    // Start is called before the first frame update
    void Start()
    {
        canvas = this.gameObject.transformFindObj("Canvas");
        cardPanel = canvas.transformFindObj("CardPanel");

        wuxueText = cardPanel.transformFindObj("WuxueText");
        cardContent = cardPanel.transformFindObj("Scroll View").transformFindObj("Viewport").transformFindObj("Content");
        closeCardPanelBtn = cardPanel.transformFindObj("CloseCardPanelBtn");
        closeCardPanelBtn.GetComponent<Button>().onClick.AddListener(closeCardPanel);

        for (int i = 0; i < characterBtns.Length; i++)
        {
            characterBtns[i].onClick.AddListener(characterChange);
        }
        checkCardsBtn.onClick.AddListener(checkCards);
        confirmBtn.onClick.AddListener(confirm);

        AudioManager.PlaySound("select");
    }

    private void confirm()
    {
        GameManage.GameAdventureStart();

    }

    private void closeCardPanel()
    {
        cardPanel.SetActive(false);
    }

    private void checkCards()
    {
        cardPanel.SetActive(true);
        reloadContent();
    }
    private void reloadContent()
    {
        int childCount = cardContent.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(cardContent.transform.GetChild(i).gameObject);
        }


        for (int i = 0; i < PlayerController.allCards.Count; i++)//遍历获取数据
        {
            card = Instantiate(cardPrefab);
            card.GetComponent<Transform>().SetParent(cardContent.transform, true);

            setCard(card, PlayerController.allCards[i]);

        }

        int row = PlayerController.allCards.Count / 5;
        float height = row * 220 + 220;
        cardContent.GetComponent<RectTransform>().sizeDelta = new Vector2(690, height);

    }

    private void setCard(GameObject tempCard, CardListItem item)
    {
        GameObject nameText = tempCard.transformFindObj("NameText");
        GameObject cost1Text = tempCard.transformFindObj("Cost1Text");
        GameObject cost2Text = tempCard.transformFindObj("Cost2Text");
        GameObject cardImg = tempCard.transformFindObj("CardImg");
        GameObject effectText = tempCard.transformFindObj("EffectText");

        nameText.GetComponent<Text>().text = item.c_name;
        effectText.GetComponent<Text>().text = item.c_effectDes;
        cost1Text.GetComponent<Text>().text = GameUtils.NumberToChinese(Convert.ToInt32(item.c_cost1));
        cost2Text.GetComponent<Text>().text = GameUtils.NumberToChinese(Convert.ToInt32(item.c_cost2));
    }

    private void characterChange()
    {
        curSelected = 0;
        
        string charName = EventSystem.current.currentSelectedGameObject.name;
        string des = "";
        Sprite imgSpr = characterImg.GetComponent<Image>().sprite;

        switch (charName)
        {
            case "ShaoLinBtn":
                imgSpr = characterImgs[0];
                des = desArr[0];
                curSelected = 0;
                break;
            case "WuDangBtn":
                imgSpr = characterImgs[1];
                des = desArr[1];
                curSelected = 1;
                break;
            case "EMeiBtn":
                imgSpr = characterImgs[2];
                des = desArr[2];
                curSelected = 2;
                break;
            case "WuXianBtn":
                imgSpr = characterImgs[3];
                des = desArr[3];
                curSelected = 3;
                break;
            case "XuanNvBtn":
                imgSpr = characterImgs[4];
                des = desArr[4];
                curSelected = 4;
                break;
        }
        characterImg.GetComponent<Image>().sprite = imgSpr;
        characterImg.GetComponent<Animation>().Play();
        characterDesText.GetComponent<Text>().text = des;

        GameManage.GameSelectCharacter(0);
        
        //少林
        GameManage.GetJsonInfo(curSelected);
        PlayerController.type = curSelected;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void GetJsonInfo(int cur)//这个方法给按钮注册
    //{
    //    PlayerController.allCards.Clear();

    //    string path = "";
    //    switch(cur)
    //    {
    //        case 0:
    //            path = "/StreamingAssets/SCard.json";
    //            break;
    //    }

    //    StreamReader streamreader = new StreamReader(Application.dataPath + path);//读取数据，转换成数据流
    //    JsonReader js = new JsonReader(streamreader);//再转换成json数据
    //    myCard = JsonMapper.ToObject<Card>(js);//读取
        
    //    for (int i = 0; i < myCard.cardList.Count; i++)//遍历获取数据
    //    {
    //        PlayerController.allCards.Add(myCard.cardList[i]);
    //        Debug.Log(PlayerController.allCards[i].c_cost2);
    //    }
    //}
}
