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
            if(allEffect[i][0] == "attack")
            {
                int type = Convert.ToInt32(allEffect[i][1]);
                int num = Convert.ToInt32(allEffect[i][2]);
                dealAttack(type, num);
                if(effTarget == "Enemy")
                {
                    AudioManager.PlaySound("enemy_hurt_1");
                }
                else
                {
                    AudioManager.PlaySound("player_hurt_1");
                }
                
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
