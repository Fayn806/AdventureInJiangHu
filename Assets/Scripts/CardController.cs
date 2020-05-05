using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static bool PlayCard(CardListItem cardPlaying)
    {
        //与当前费用比较
        int card_cost1 = Convert.ToInt32(cardPlaying.c_cost1);
        int card_cost2 = Convert.ToInt32(cardPlaying.c_cost2);

        if(PlayerController.power1 - card_cost1 >= 0 && PlayerController.power2 - card_cost2 >= 0)
        {
            PlayerController.power1 -= card_cost1;
            PlayerController.power2 -= card_cost2;
            return true;
        }

        return false;
    }
}
