using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomMap
{
    public int hexCount { get; set; }
    public int sectionCount { get; set; }
    public int repeatCount { get; set; }
    public List<Hexagon> hexagons { get; set; }
    public List<int> repeatXs { get; set; }

    public Arrow arrow;

    //0, 3, 6, 9     0, 2, 4, 6
    public void createMap(int _sectionCount = 8)
    {
        hexagons = new List<Hexagon>();
        repeatXs = new List<int>();

        sectionCount = _sectionCount;
        repeatCount = (_sectionCount - 1) / 2;
        hexCount = sectionCount + repeatCount;

        List<int> nonRepeatXs = new List<int>();
        for(int i = 0; i < _sectionCount; i++)
        {
            nonRepeatXs.Add(i * 3);
        }

        List<int> sequence = new List<int>();
        for(int i = 1; i < _sectionCount - 1; i++)
        {
            sequence.Add(i);
        }

        int end = sequence.Count;
        for (int i = 0; i < repeatCount; i++)
        {
            //取下标
            int index = Random.Range(0, end);
            repeatXs.Add(sequence[index] * 3);
            nonRepeatXs.Remove(sequence[index] * 3);
            sequence.RemoveAt(index);
            end--;
        }

        repeatXs.Sort();
        //Debug.Log(repeatXs);

        int lastY = 0;
        for(int i = 0; i < _sectionCount; i++)
        {
            bool isRepeat = false;
            for(int j = 0; j < repeatXs.Count; j++)
            {
                if(i * 3 == repeatXs[j])
                {
                    //重复连接
                    isRepeat = true;
                }
            }

            if(isRepeat)
            {
                int y1 = lastY + 2;
                int y2 = lastY - 2;

                Hexagon h1 = new Hexagon(i, i * 3, y1);
                h1.bgIndex = Random.Range(0, 6);
                h1.hexBgPosition = new Vector3(h1.centerX, (float)(h1.centerY / 2 * 1.73), 0);
                Hexagon h2 = new Hexagon(i, i * 3, y2);
                h2.bgIndex = Random.Range(0, 6);
                h2.hexBgPosition = new Vector3(h1.centerX, (float)(h2.centerY / 2 * 1.73), 0);
                hexagons.Add(h1);
                hexagons.Add(h2);

                bool up = Convert.ToBoolean(Random.Range(0, 2));
                if(up == true)
                {
                    lastY = y1;
                }
                else
                {
                    lastY = y2;
                }
            }
            else
            {
                bool plus = Convert.ToBoolean(Random.Range(0, 2));

                int y = lastY;
                if(plus == true)
                {
                    y += 2;
                } 
                else
                {
                    y -= 2;
                }
                if(i == 0)
                {
                    y = 0;
                }

                Hexagon h = new Hexagon(i, i * 3, y);
                h.bgIndex = Random.Range(0, 6);
                h.hexBgPosition = new Vector3(h.centerX, (float)(h.centerY / 2 * 1.73), 0);
                hexagons.Add(h);
                lastY = y;
            }
        }

        arrow = new Arrow(hexagons);

    }

    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


public class Hexagon
{
    public int index { get; set; }
    public int bgIndex { get; set; }
    public Vector3 hexBgPosition { get; set; }
    public int centerX { get; set; }
    public int centerY { get; set; }
    public List<HexItem> hexItems { get; set; }

    public Hexagon(int _index, int _cenX, int _cenY)
    {
        index = _index;
        centerX = _cenX;
        centerY = _cenY;

        hexItems = createItems();
    }

    /*
     *                          2    3                      
     *                      1             4  
     *                          6     5
     * 
     * 
     * x-2
     * x-1 y+2
     * x+1 y+2
     * x+2
     * x+1 y-2
     * x-1 y-2
     */
    public List<HexItem> createItems()
    {
        List<HexItem> Items = new List<HexItem>();
        HexItem item1 = new HexItem(index, centerX - 2, centerY);
        HexItem item2 = new HexItem(index, centerX - 1, centerY + 2);
        HexItem item3 = new HexItem(index, centerX + 1, centerY + 2);
        HexItem item4 = new HexItem(index, centerX + 2, centerY);
        HexItem item5 = new HexItem(index, centerX + 1, centerY - 2);
        HexItem item6 = new HexItem(index, centerX - 1, centerY - 2);
        Items.Add(item1);
        Items.Add(item2);
        Items.Add(item3);
        Items.Add(item4);
        Items.Add(item5);
        Items.Add(item6);

        return Items;
    }
}

public class HexItem
{
    public int parentIndex { get; set; }
    public int posX { get; set; }
    public int posY { get; set; }

    public HexItem(int _parentIndex, int _posX, int _posY)
    {
        parentIndex = _parentIndex;
        posX = _posX;
        posY = _posY;
    }
}

public class Arrow
{
    public List<Hexagon> hexagons { get; set; }

    public List<Vector2Int> centerPoints { get; set; }

    public List<ArrowItem> arrowItems { get; set; }
    public Arrow(List<Hexagon> _hexagons)
    {
        hexagons = _hexagons;
        arrowItems = new List<ArrowItem>();
        centerPoints = new List<Vector2Int>();

        for (int i = 0; i < hexagons.Count; i++)
        {
            Vector2Int point = new Vector2Int(hexagons[i].centerX, hexagons[i].centerY);
            centerPoints.Add(point);
        }

        handlePoints();
    }

    public void handlePoints()
    {
        

        /*
         *                2
         *           1          3
         *           6          4
         *                 5
         * 
         * 
         * -2   1           rightUp
         * 0   2            right
         * 1   1             
         * 1   -1
         * 0   -2           right
         * -2  1            rightdown
         * 
         * 
         */
        for(int i = 0; i < centerPoints.Count; i++)
        {
            int cenX = centerPoints[i].x;
            int cenY = centerPoints[i].y;

            ArrowItem arrowItem1 = new ArrowItem(cenX - 2, cenY + 1, "rightUp");
            ArrowItem arrowItem2 = new ArrowItem(cenX, cenY + 2, "right");
            ArrowItem arrowItem3 = new ArrowItem(cenX + 1, cenY + 1, "rightDown");
            ArrowItem arrowItem4 = new ArrowItem(cenX + 1, cenY - 1, "rightUp");

            if(i + 1 < centerPoints.Count)
            {
                int checkX = centerPoints[i + 1].x;

                if(checkX == centerPoints[i].x)
                {
                    checkX = centerPoints[i + 2].x;
                }

                for (int k = 0; k < centerPoints.Count; k++)
                {
                    if(checkX == centerPoints[k].x)
                    {
                        //比较centerPoints[i].y 和centerPoints[k].y
                        //小 3为 leftright
                        //大 4为 rightleft

                        if(centerPoints[i].y - centerPoints[k].y == -2)
                        {
                            arrowItem3 = new ArrowItem(cenX + 1, cenY + 1, "leftRight");
                        }
                        else if (centerPoints[i].y - centerPoints[k].y == -6)
                        {
                            arrowItem3 = new ArrowItem(cenX + 1, cenY + 1, "leftUp");
                        }

                        if (centerPoints[i].y - centerPoints[k].y == 2)
                        {
                            arrowItem4 = new ArrowItem(cenX + 1, cenY - 1, "rightLeft");

                            
                            break;
                        }
                        else if (centerPoints[i].y - centerPoints[k].y == 6)
                        {
                            arrowItem4 = new ArrowItem(cenX + 1, cenY - 1, "leftDown");
                            break;
                        }

                    }
                }
            }
            ArrowItem arrowItem5 = new ArrowItem(cenX, cenY - 2, "right");
            ArrowItem arrowItem6 = new ArrowItem(cenX - 2, cenY - 1, "rightDown");

            arrowItems.Add(arrowItem1);
            arrowItems.Add(arrowItem2);
            arrowItems.Add(arrowItem3);
            arrowItems.Add(arrowItem4);
            arrowItems.Add(arrowItem5);
            arrowItems.Add(arrowItem6);

        }

    }
}

public class ArrowItem
{
    public int parentIndex { get; set; }
    public int posX { get; set; }
    public int posY { get; set; }
    public string direction { get; set; }
    public ArrowItem(int _posX, int _posY, string _direction)
    {
        posX = _posX;
        posY = _posY;
        direction = _direction;
    }
}
