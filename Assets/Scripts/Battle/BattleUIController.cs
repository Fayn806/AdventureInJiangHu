using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleUIController : MonoBehaviour
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
    public GameObject enemy;

    public GameObject drawingCardsCount;
    public GameObject threwCardsCount;
    public GameObject battleStartImg;

    public GameObject battleReward;

    public GameObject[] card_rewards;

    public GameObject battleRewardConfirmBtn;

    public CardListItem card_reward_selected;

    private bool rewardGet = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(battleStartImgAni());
        AudioManager.PlaySound("battleBG");
    }

    public IEnumerator battleStartImgAni()
    {
        iTween.MoveFrom(battleStartImg, new Vector3(UnityEngine.Screen.width, battleStartImg.transform.position.y, 0), 0.5f);
        yield return new WaitForSeconds(1.5f);
        iTween.MoveTo(battleStartImg, new Vector3(0 - battleStartImg.GetComponent<RectTransform>().rect.width, battleStartImg.transform.position.y, 0), 0.5f);
        iTween.Destroy(battleStartImg, 0.5f);

    }

    public void showReward()
    {
        battleReward.SetActive(true);

        battleReward.transformFindObj("enyi_rewardText").GetComponent<Text>().text = enemy.GetComponent<EnemyController>().reward_wealth1;
        battleReward.transformFindObj("qian_rewardText").GetComponent<Text>().text = enemy.GetComponent<EnemyController>().reward_wealth2;

        for (int i = 0; i < card_rewards.Length; i++)
        {
            GameObject nameText = card_rewards[i].transformFindObj("NameText");
            GameObject cost1Text = card_rewards[i].transformFindObj("Cost1Text");
            GameObject cost2Text = card_rewards[i].transformFindObj("Cost2Text");
            GameObject cardImg = card_rewards[i].transformFindObj("CardImg");
            GameObject effectText = card_rewards[i].transformFindObj("EffectText");
            if(enemy.GetComponent<EnemyController>().reward_Cards[i] == null && enemy.GetComponent<EnemyController>().reward_Cards[i].c_cost1 == "")
            {
                return;
            }
            nameText.GetComponent<Text>().text = enemy.GetComponent<EnemyController>().reward_Cards[i].c_name;
            effectText.GetComponent<Text>().text = enemy.GetComponent<EnemyController>().reward_Cards[i].c_effectDes;
            cost1Text.GetComponent<Text>().text = GameUtils.NumberToChinese(Convert.ToInt32(enemy.GetComponent<EnemyController>().reward_Cards[i].c_cost1));
            cost2Text.GetComponent<Text>().text = GameUtils.NumberToChinese(Convert.ToInt32(enemy.GetComponent<EnemyController>().reward_Cards[i].c_cost2));

            card_rewards[i].GetComponent<Button>().onClick.AddListener(rewardsSelected);
        }

        iTween.MoveFrom(battleReward, new Vector3(battleReward.transform.position.x, UnityEngine.Screen.height * 1.5f, 0), 0.5f);
        battleRewardConfirmBtn.GetComponent<Button>().onClick.AddListener(hideReward);
    }

    public void rewardsSelected()
    {
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        int selectIndex = Convert.ToInt32(obj.name.Split('_')[2]) - 1;
        card_reward_selected = enemy.GetComponent<EnemyController>().reward_Cards[selectIndex];
        rewardGet = true;
    }

    public void hideReward()
    {

        iTween.MoveTo(battleReward, iTween.Hash("position", new Vector3(battleReward.transform.position.x, 0, 0), "time", 0.5f, "delay", 0.5f) );
        Invoke("battleEnd", 1f);
    }

    public void battleEnd()
    {
        if(rewardGet == true)
        {
            PlayerController.curCards.Add(card_reward_selected);
        } 

        PlayerController.wealth1 += Convert.ToInt32(enemy.GetComponent<EnemyController>().reward_wealth1);
        PlayerController.wealth2 += Convert.ToInt32(enemy.GetComponent<EnemyController>().reward_wealth2);

        GameManage.GameEventFinish();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.maxHP != 0)
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
            drawingCardsCount.GetComponent<Text>().text = PlayerController.drawingCards.Count.ToString();
            threwCardsCount.GetComponent<Text>().text = PlayerController.threwCards.Count.ToString();
            for (int i = 0; i < PlayerController.itemLists.Count; i++)
            {
                GameObject.FindGameObjectWithTag("Items").transform.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                GameObject.FindGameObjectWithTag("Items").transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/items/item_" + PlayerController.itemLists[i].i_no);
            }
        }

        if(PlayerController.wound1 >= PlayerController.maxWound1 || PlayerController.wound2 >= PlayerController.maxWound2)
        {
            PlayerController.Save();
            GameManage.GameReady();
        }
        

        //Debug.Log(PlayerController.curEventType);

        //SceneManager.LoadScene("MapScene");

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
