using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TavernUIController : MonoBehaviour
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
    public GameObject power1Btn;
    public GameObject power2Btn;

    public GameObject backBtn;
    // Start is called before the first frame update
    void Start()
    {

        backBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            Invoke("backMap", 0.5f);
        });

        power1Btn.GetComponent<Button>().onClick.AddListener(() =>
        {
            PlayerController.wound1 -= PlayerController.wound1 * 0.5;
            PlayerController.wound2 -= PlayerController.wound2 * 0.5;
        });
    }

    void initSaleCards()
    {
        
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
