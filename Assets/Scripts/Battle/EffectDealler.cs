using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectDealler : MonoBehaviour
{
    private static string effTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void DealEffect(List<string> effects, string whosTurn)
    {
        if (whosTurn == "Player") effTarget = "Enemy";
        else if(whosTurn == "Enemy") effTarget = "Player";

        List<string[]> allEffect = new List<string[]>();

        for (int i = 0; i < effects.Count; i++)
        {
            allEffect.Add(effects[i].Split('_'));
        }

        for(int i = 0; i < allEffect.Count;i++)
        {
            string effectName = allEffect[i][0];
            int type = Convert.ToInt32(allEffect[i][1]);
            int value = Convert.ToInt32(allEffect[i][2]);

            if (effectName == "attack")
            {
                dealAttack(type, value);
                if(effTarget == "Enemy")
                {
                    AudioManager.PlaySound("enemy_hurt_1");
                }
                else
                {
                    AudioManager.PlaySound("player_hurt_1");
                }
                
            }

            else if (effectName == "block")
            {
                if (type == 1)
                {
                    PlayerController.man.GetComponent<BuffController>().block_1 += value;
                }
                if (type == 2)
                {
                    PlayerController.man.GetComponent<BuffController>().block_2 += value;
                }
            }
            else if (effectName == "strength")
            {
                if (type == 1)
                {
                    PlayerController.man.GetComponent<BuffController>().strength_1 += value;
                }
                if (type == 2)
                {
                    PlayerController.man.GetComponent<BuffController>().strength_2 += value;
                }
                double d = PlayerController.man.GetComponent<BuffController>().strength_2;
                Debug.Log(d);
            }
            else if (effectName == "dexterity")
            {
                PlayerController.man.GetComponent<BuffController>().dexterity += value;
            }
            else if (effectName == "invincible")
            {
                PlayerController.man.GetComponent<BuffController>().invincible += value;
            }
            else if (effectName == "reduce")
            {
                PlayerController.man.GetComponent<BuffController>().reduce += value;
            }
            else if (effectName == "rebound")
            {
                PlayerController.man.GetComponent<BuffController>().rebound += value;
            }
            else if (effectName == "vulnerable")
            {
                if (type == 1)
                {
                    PlayerController.man.GetComponent<BuffController>().vulnerable_1 += value;
                }
                if (type == 2)
                {
                    PlayerController.man.GetComponent<BuffController>().vulnerable_2 += value;
                }
            }
            else if (effectName == "weak")
            {
                if (type == 1)
                {
                    PlayerController.man.GetComponent<BuffController>().weak_1 += value;
                }
                if (type == 2)
                {
                    PlayerController.man.GetComponent<BuffController>().weak_2 += value;
                }
            }
            else if (effectName == "power")
            {
                if (type == 1)
                {
                    PlayerController.power1 += Convert.ToInt32(value);
                }
                if (type == 2)
                {
                    PlayerController.power2 += Convert.ToInt32(value);
                }
            }
            else if (effectName == "random")
            {

            }
            else if (effectName == "free")
            {

            }
            else if (effectName == "repeat")
            {

            }
            else if (effectName == "hurt")
            {

            }




        }

    }

    public static void dealAttack(int type, int num)
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        GameObject man = GameObject.FindGameObjectWithTag("man");

        GameObject target;
        GameObject user;
        Vector3 toPos;
        Vector3 oriPos;
        if (effTarget == "Enemy")
        {
            target = enemy;
            user = man;

            toPos = new Vector3(user.transform.position.x + 400, user.transform.position.y, user.transform.position.z);
        } 
        else
        {
            target = man;
            user = enemy;

            toPos = new Vector3(user.transform.position.x - 400, user.transform.position.y, user.transform.position.z);
        }

        oriPos = new Vector3(user.transform.position.x, user.transform.position.y, user.transform.position.z);

        if (type == 1)
        {
            
            if(effTarget == "Enemy")
            {
                //enemy.GetComponent<EnemyController>().wound1 += num;

                //己方加成
                int plusNum = Convert.ToInt32(PlayerController.GetBuffController().strength_1 / 10 * num);
                num += plusNum;
                //己方虚弱
                int subNum = Convert.ToInt32(PlayerController.GetBuffController().weak_1 / 10 * num);
                num -= subNum;
                //敌方易伤

                //敌方减免

                enemy.GetComponent<EnemyController>().wound1 += num;

            }
            else
            {
                PlayerController.wound1 += num;
            }
        } 
        else if(type == 2)
        {
            if (effTarget == "Enemy")
            {
                //己方加成
                int plusNum = Convert.ToInt32(PlayerController.GetBuffController().strength_2 / 10 * num);
                num += plusNum;
                //己方虚弱
                int subNum = Convert.ToInt32(PlayerController.GetBuffController().weak_2 / 10 * num);
                num -= subNum;
                //敌方易伤

                //敌方减免

                enemy.GetComponent<EnemyController>().wound2 += num;
            }
            else
            {
                PlayerController.wound2 += num;
            }
        }
        
        

        iTween.MoveTo(user, iTween.Hash("position", toPos, "time", 0.5f));
        iTween.ScaleTo(user, iTween.Hash("scale", new Vector3(1.5f, 1.5f, 0), "time", 0.5f));
        iTween.MoveTo(user, iTween.Hash("position", oriPos, "time", 0.5f, "delay", 0.6f));
        iTween.ScaleTo(user, iTween.Hash("scale", new Vector3(1, 1, 0), "time", 0.5f, "delay", 0.6f));
        iTween.ShakePosition(target, iTween.Hash("amount", new Vector3(50, 0, 0), "time", 0.5f, "delay", 0.3f));
    }
}
