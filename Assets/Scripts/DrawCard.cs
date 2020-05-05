using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DrawCard : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject[] cardObjects;
    // Start is called before the first frame update
    void Start()
    {
        //Invoke("Draw", 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Draw()
    {

    }

    public void DrawCards(CardListItem drawingCard)
    {

        for (int j = 0; j < cardObjects.Length; j++)
        {
            //Debug.Log(j  + "---" +cardObjects[j].transform.childCount);
            if (cardObjects[j].transform.childCount == 0)
            {
                GameObject card = Instantiate(cardPrefab);
                card.GetComponent<Transform>().SetParent(cardObjects[j].transform, true);
                card.GetComponent<RectTransform>().localPosition = Vector3.zero;
                card.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                card.AddComponent<CardListItem>();

                card.GetComponent<CardListItem>().CopyItem(drawingCard);

                GameObject nameText = card.transformFindObj("NameText");
                GameObject cost1Text = card.transformFindObj("Cost1Text");
                GameObject cost2Text = card.transformFindObj("Cost2Text");
                GameObject cardImg = card.transformFindObj("CardImg");
                GameObject effectText = card.transformFindObj("EffectText");

                nameText.GetComponent<Text>().text = drawingCard.c_name;
                effectText.GetComponent<Text>().text = drawingCard.c_effectDes;
                cost1Text.GetComponent<Text>().text = GameUtils.NumberToChinese(Convert.ToInt32(drawingCard.c_cost1));
                cost2Text.GetComponent<Text>().text = GameUtils.NumberToChinese(Convert.ToInt32(drawingCard.c_cost2));

                iTween.MoveFrom(card, new Vector3(-700, 0, 0), 0.5f);
                break;
            }

        }
    }

    public void ThrowCards()
    {
        for (int j = 0; j < cardObjects.Length; j++)
        {
            if (cardObjects[j].transform.childCount != 0)
            {
                GameObject card = cardObjects[j].transform.GetChild(0).gameObject;
                
                PlayerController.threwCards.Add(card.GetComponent<CardListItem>());
                DestroyImmediate(card.GetComponent<CardDrag>());

                iTween.MoveTo(card, new Vector3(UnityEngine.Screen.width + card.GetComponent<RectTransform>().rect.width, 0, 0), 0.5f);
                iTween.Destroy(card, 0.5f);
            }
        }
    }
}
