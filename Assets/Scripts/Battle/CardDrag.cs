/*************************************************
 * 项目名称：UGUI通用
 * 脚本创建人：魔卡
 * 脚本创建时间：2017.12.14
 * 脚本功能：UI图片拖拽功能（将脚本挂载在需要拖放的图片上）
 * ***********************************************/
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

//UI图片拖拽功能类
public class CardDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("是否精准拖拽")]
    public bool m_isPrecision = false;

    //存储图片中心点与鼠标点击点的偏移量
    private Vector3 m_offset;

    //存储当前拖拽图片的RectTransform组件
    private RectTransform m_rt;

    private Transform m_t;

    Vector3 imgReduceScale = new Vector3(1.3f, 1.3f, 1); //设置图片缩放
    Vector3 imgNormalScale = new Vector3(1, 1, 1); //正常大小
    Vector3 oriPosition = new Vector3(0, 0, 0);

    Vector3 upPos;
    Vector3 downPos;

    private GameObject taiji;
    private bool taijiRo = false;

    string status = "";
    void Start()
    {
        //初始化
        m_rt = gameObject.GetComponent<RectTransform>();
        m_t = gameObject.transform.parent.transform;
        upPos = new Vector3(m_t.position.x, m_t.position.y + 100, 0);
        downPos = new Vector3(m_t.position.x, m_t.position.y, 0);
        taiji = GameObject.FindGameObjectWithTag("TaiJi");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(status == "pen")
        {
            iTween.MoveTo(gameObject, downPos, 0.2f);
            iTween.ScaleTo(gameObject, imgNormalScale, 0.2f);
            status = "pex";
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(Input.GetMouseButton(0) == false && status != "pen")
        {
            status = "pen";
            iTween.ScaleTo(gameObject, imgReduceScale, 0.2f);
            iTween.MoveTo(gameObject, upPos, 0.2f);
            //Debug.Log(gameObject.GetComponent<CardListItem>().c_no);
            gameObject.transform.parent.transform.SetAsLastSibling();
        }
    }

    //开始拖拽触发
    public void OnBeginDrag(PointerEventData eventData)
    {
        status = "bd";
        //如果精确拖拽则进行计算偏移量操作
        if (m_isPrecision)
        {
            // 存储点击时的鼠标坐标
            Vector3 tWorldPos;
            //UI屏幕坐标转换为世界坐标
            RectTransformUtility.ScreenPointToWorldPointInRectangle(m_rt, eventData.position, eventData.pressEventCamera, out tWorldPos);
            //计算偏移量   
            m_offset = transform.position - tWorldPos;
        }
        //否则，默认偏移量为0
        else
        {
            m_offset = Vector3.zero;
        }

        SetDraggedPosition(eventData);
    }

    //拖拽过程中触发
    public void OnDrag(PointerEventData eventData)
    {
        status = "od";
        SetDraggedPosition(eventData);
        m_rt.localScale = imgReduceScale;
        if (m_rt.localPosition.y > 150)
        {
            if(Convert.ToInt32(gameObject.GetComponent<CardListItem>().c_cost1) <= PlayerController.power1 && Convert.ToInt32(gameObject.GetComponent<CardListItem>().c_cost2) <= PlayerController.power2)
                taijiRo = true;
            else
                taijiRo = false;

        } else
        {
            taijiRo = false;
        }
    }

    //结束拖拽触发
    public void OnEndDrag(PointerEventData eventData)
    {
        status = "oed";
        SetDraggedPosition(eventData);
        m_rt.localScale = imgNormalScale;

        //使用了卡牌
        if (m_rt.localPosition.y > 150 && CardController.PlayCard(this.GetComponent<CardListItem>()))
        {
            GameObject.FindGameObjectWithTag("BattleScene").GetComponent<BattleManger>().dealEffect(gameObject.GetComponent<CardListItem>().c_effect);
            //
            iTween.MoveTo(gameObject, new Vector3(UnityEngine.Screen.width + m_rt.rect.width, 0, 0), 0.5f);
            Debug.Log(PlayerController.drawingCards.Count);
            Invoke("DestoryCard", 0.5f);
        } 
        else
        {
            m_rt.localPosition = oriPosition;
        }
        
    }

    private void DestoryCard()
    {
        PlayerController.threwCards.Add(gameObject.GetComponent<CardListItem>());

        GameObject.DestroyImmediate(gameObject);
        if (PlayerController.drawingCards.Count != 0)
        {
            GameObject.FindGameObjectWithTag("CardPanel").GetComponent<DrawCard>().DrawCards(PlayerController.DrawACard());
        }
        else
        {

        }
    }


    /// <summary>
    /// 设置图片位置方法
    /// </summary>
    /// <param name="eventData"></param>
    private void SetDraggedPosition(PointerEventData eventData)
    {
        //存储当前鼠标所在位置
        Vector3 globalMousePos;
        //UI屏幕坐标转换为世界坐标
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_rt, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {

            //设置位置及偏移量
            m_rt.position = globalMousePos + m_offset;
        }
    }

    private void FixedUpdate()
    {
        if(taijiRo == true) {
            iTween.RotateAdd(taiji, new Vector3(0, 0, 3), 0);
        }
    }
}