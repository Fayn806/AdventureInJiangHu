using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : ScriptableObject
{
    //8  40 - 41  1/8   1/10  1/10  1/8
    private int jingyingCount;
    private int jiuguanCount;
    private int shangdianCount;
    private int qiyuCount;

    public RandomMap randomMap { get; set; }
    public List<Hexagon> hexagons { get; set; }

    public List<HexItem> allHexItems;

    public List<Vector3Int> allPoints;

    public List<EventItem> allEvents;

    public EventManager(RandomMap rm)
    {
        allEvents = new List<EventItem>();
        allHexItems = new List<HexItem>();
        allPoints = new List<Vector3Int>();
        randomMap = rm;

        for(int i = 0; i < rm.hexagons.Count; i++)
        {
            for(int j = 0; j < rm.hexagons[i].hexItems.Count; j++)
            {
                bool repeat = false;
                for(int k = 0; k < allHexItems.Count; k++)
                {
                    if(rm.hexagons[i].hexItems[j].posX == allHexItems[k].posX && rm.hexagons[i].hexItems[j].posY == allHexItems[k].posY)
                    {
                        repeat = true;
                    }
                }
                if(repeat == false)
                {
                    allHexItems.Add(rm.hexagons[i].hexItems[j]);
                }
            }
        }

        //点
        for(int i = 0; i < allHexItems.Count; i++)
        {
            Vector3Int vector3Int = new Vector3Int(allHexItems[i].posX, allHexItems[i].posY, 0);
            allPoints.Add(vector3Int);
        }

        //生成事件
        /*
         * 开始0
         * 普通1
         * 精英2
         * 酒馆3
         * 商店4
         * 奇遇5
         * boss9
         * 
         */

        jingyingCount = allPoints.Count / 8;
        jiuguanCount = allPoints.Count / 10;
        shangdianCount = allPoints.Count / 8;
        qiyuCount = allPoints.Count / 8;

        EventItem startEvent = new EventItem(allPoints[0].x, allPoints[0].y, 0);
        EventItem bossEvent = new EventItem(allPoints[allPoints.Count - 2].x, allPoints[allPoints.Count - 2].y, 9);

        allEvents.Add(startEvent);
        allEvents.Add(bossEvent);

        allPoints.RemoveAt(0);
        allPoints.RemoveAt(allPoints.Count - 2);

        for (int i = 0; i < jingyingCount; i++)
        {
            int index = Random.Range(0, allPoints.Count);
            int x = allPoints[index].x;
            int y = allPoints[index].y;
            int eventType = 2;
            EventItem eventItem = new EventItem(x, y, eventType);

            allEvents.Add(eventItem);
            allPoints.RemoveAt(index);
        }

        for (int i = 0; i < jiuguanCount; i++)
        {
            int index = Random.Range(0, allPoints.Count);
            int x = allPoints[index].x;
            int y = allPoints[index].y;
            int eventType = 3;
            EventItem eventItem = new EventItem(x, y, eventType);

            allEvents.Add(eventItem);
            allPoints.RemoveAt(index);
        }

        for (int i = 0; i < shangdianCount; i++)
        {
            int index = Random.Range(0, allPoints.Count);
            int x = allPoints[index].x;
            int y = allPoints[index].y;
            int eventType = 4;
            EventItem eventItem = new EventItem(x, y, eventType);

            allEvents.Add(eventItem);
            allPoints.RemoveAt(index);
        }

        for (int i = 0; i < qiyuCount; i++)
        {
            int index = Random.Range(0, allPoints.Count);
            int x = allPoints[index].x;
            int y = allPoints[index].y;
            int eventType = 5;
            EventItem eventItem = new EventItem(x, y, eventType);

            allEvents.Add(eventItem);
            allPoints.RemoveAt(index);
        }

        for(int i = 0; i < allPoints.Count; i ++)
        {
            int x = allPoints[i].x;
            int y = allPoints[i].y;
            int eventType = 1;
            EventItem eventItem = new EventItem(x, y, eventType);

            allEvents.Add(eventItem);
        }

    }
}

public class EventItem : ScriptableObject
{
    public int posX { get; set; }
    public int posY { get; set; }
    public int eventType { get; set; }

    public EventItem(int _posX, int _posY, int _eventType)
    {
        posX = _posX;
        posY = _posY;
        eventType = _eventType;
    }
}
