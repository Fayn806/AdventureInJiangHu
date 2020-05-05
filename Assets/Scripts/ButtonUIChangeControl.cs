using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonUIChangeControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private Button button;
    public void OnPointerEnter(PointerEventData eventData)
    {
        button.image.color = new Color(0, 0, 0, 1);
        button.transform.Find("Text").GetComponent<Text>().color = Color.white;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.image.color = new Color(0, 0, 0, 0);
        button.transform.Find("Text").GetComponent<Text>().color = Color.black;
    }

    // Start is called before the first frame update
    void Start()
    {
        button = this.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
