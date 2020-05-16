using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManger : MonoBehaviour
{
    private Button endTurnBtn;
    private string curTurn = "Player";

    private GameObject cardPanel;
    private GameObject man;
    private GameObject enemy;

    private GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("BattleCanvas");
        cardPanel = GameObject.FindGameObjectWithTag("CardPanel");
        endTurnBtn = GameObject.FindGameObjectWithTag("EndTurnBtn").GetComponent<Button>();
        endTurnBtn.onClick.AddListener(clickEndTurnBtn);
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        man = GameObject.FindGameObjectWithTag("man");

        PlayerController.man.AddComponent<BuffController>();

        PlayerController.FirstShuffleCards();

        playerTurn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clickEndTurnBtn()
    {
        enemyTurn();
        //Invoke("playerTurn", 3f);
    }

    public void playerTurn()
    {
        curTurn = "Player";
        endTurnBtn.interactable = true;

        //发牌时
        for (int i = 0; i < 5; i++)
        {
            if (PlayerController.drawingCards.Count == 0)
            {
                PlayerController.ShuffleCards();
            }
            cardPanel.GetComponent<DrawCard>().DrawCards(PlayerController.DrawACard());
        }
        PlayerController.NewTurn();
    }

    public void enemyTurn()
    {
        curTurn = "Enemy";
        endTurnBtn.interactable = false;

        enemy.GetComponent<EnemyController>().enemyAct();
        
        //弃牌
        cardPanel.GetComponent<DrawCard>().ThrowCards();
        
    }

    public void battleEnd()
    {

        if (curTurn != "Reward")
        {
            AudioManager.PlaySound("enemy_dead_1");
            StartCoroutine(battleReward());
        }
        curTurn = "Reward";

        Destroy(PlayerController.man.GetComponent<BuffController>());

    }

    IEnumerator battleReward()
    {
        yield return new WaitForSeconds(1.0f);
        //显示奖励面板
        canvas.GetComponent<BattleUIController>().showReward();
        AudioManager.StopSound();
        AudioManager.PlayMusic("battleWin");
    }

    public void dealEffect(List<string> effects)
    {
        EffectDealler.DealEffect(effects, curTurn);
        string serializedString = string.Empty;
    }
}
