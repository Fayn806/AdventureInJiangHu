using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    public GameObject wound1Img;
    public GameObject wound2Img;
    public GameObject wound1Text;
    public GameObject wound2Text;
    public double maxWound1 { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public double maxWound2 { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> actions { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public double wound1 { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public double wound2 { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string curAction { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<CardListItem> reward_Cards { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string reward_wealth1 { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string reward_wealth2 { get; set; }

    private GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = this.gameObject;
        if (PlayerController.curEventType == 1)
        {
            enemy.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/enemy1_" + Convert.ToInt32(1 + Random.Range(0, 2)));

            Enemy1 enemy1 = PlayerController.enemy.enemy1[0];
            reward_Cards = new List<CardListItem>();

            while(reward_Cards.Count != 3)
            {
                int index = Random.Range(0, PlayerController.allCards.Count - 1);
                reward_Cards.Add(PlayerController.allCards[index]);
            }

            reward_wealth1 = ((int)Random.Range(10, 30)).ToString();
            reward_wealth2 = ((int)Random.Range(20, 50)).ToString();

            maxWound1 = double.Parse(enemy1.maxWound1);
            maxWound2 = double.Parse(enemy1.maxWound2);

            actions = enemy1.actions;
            curAction = actions[0];
            wound1 = 0;
            wound2 = 0;

            PlayerController.enemy.enemy1.RemoveAt(0);
        }
        if (PlayerController.curEventType == 2)
        {
            enemy.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1);

            enemy.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/enemy2_" + Convert.ToInt32(1 + Random.Range(0, 3)));

            Enemy2 enemy2 = PlayerController.enemy.enemy2[0];
            reward_Cards = new List<CardListItem>();

            while (reward_Cards.Count != 3)
            {
                int index = Random.Range(0, PlayerController.allCards.Count - 1);
                reward_Cards.Add(PlayerController.allCards[index]);
            }

            reward_wealth1 = ((int)Random.Range(10, 30)).ToString();
            reward_wealth2 = ((int)Random.Range(20, 50)).ToString();

            maxWound1 = double.Parse(enemy2.maxWound1);
            maxWound2 = double.Parse(enemy2.maxWound2);

            actions = enemy2.actions;
            curAction = actions[0];
            wound1 = 0;
            wound2 = 0;

            PlayerController.enemy.enemy2.RemoveAt(0);
        }
        if (PlayerController.curEventType == 9)
        {
            enemy.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1);

            enemy.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/enemy3_1");

            Enemy3 enemy3 = PlayerController.enemy.enemy3[0];
            reward_Cards = new List<CardListItem>();

            while (reward_Cards.Count != 3)
            {
                int index = Random.Range(0, PlayerController.allCards.Count - 1);
                reward_Cards.Add(PlayerController.allCards[index]);
            }

            reward_wealth1 = ((int)Random.Range(30, 70)).ToString();
            reward_wealth2 = ((int)Random.Range(40, 80)).ToString();

            maxWound1 = double.Parse(enemy3.maxWound1);
            maxWound2 = double.Parse(enemy3.maxWound2);

            actions = enemy3.actions;
            curAction = actions[0];
            wound1 = 0;
            wound2 = 0;

            PlayerController.enemy.enemy3.RemoveAt(0);
        }


    }

    public void enemyAct()
    {
        StartCoroutine(startAction());
    }

    public IEnumerator startAction()
    {

        foreach(string effect in actions)
        {
            curAction = effect;
            List<string> curEffect = new List<string>() { curAction };
            GameObject.FindGameObjectWithTag("BattleScene").GetComponent<BattleManger>().dealEffect(curEffect);
            yield return new WaitForSeconds(1.0f);
        }

        GameObject.FindGameObjectWithTag("BattleScene").GetComponent<BattleManger>().playerTurn();
    }

    // Update is called once per frame
    void Update()
    {
        wound1Text.GetComponent<Text>().text = GameUtils.NumberToChinese((int)Math.Floor(wound1 / maxWound1 * 10));
        wound2Text.GetComponent<Text>().text = GameUtils.NumberToChinese((int)Math.Floor(wound2 / maxWound2 * 10));
        wound1Img.GetComponent<Image>().fillAmount = (float)(wound1 / maxWound1);
        wound2Img.GetComponent<Image>().fillAmount = (float)(wound2 / maxWound2);
        
        if(wound1 >= maxWound1 || wound2 >= maxWound2)
        {
            GameObject.FindGameObjectWithTag("BattleScene").GetComponent<BattleManger>().battleEnd();
        }
    }
}
