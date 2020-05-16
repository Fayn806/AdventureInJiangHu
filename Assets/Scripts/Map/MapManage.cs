using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManage : MonoBehaviour
{

    public Tilemap tilemap;//引用的Tilemap
    Tile baseTile;//使用的最基本的Tile，我这里是白色块，然后根据数据设置不同颜色生成不同Tile

    public Tile roundTile;
    public Tile arrRightUp;
    public Tile arrRight;
    public Tile arrRightDown;
    public Tile arrLeftRight;
    public Tile arrRightLeft;
    public Tile arrLeftUp;
    public Tile arrLeftDown;

    public GameObject hexBgPrefab;
    public Sprite[] hexBgs;
    public GameObject[] eventPrefabs;
    // Start is called before the first frame update
    void Start()
    {

        RandomMap map;
        EventManager eventManager;

        if (PlayerController.eventManager == null || PlayerController.map == null)
        {
            map = new RandomMap();
            map.createMap(10);
            eventManager = new EventManager(map);
            PlayerController.eventManager = eventManager;
            PlayerController.map = map;
        } else
        {
            map = PlayerController.map;
            eventManager = PlayerController.eventManager;
        }

        for (int i = 0; i < map.hexagons.Count; i++)
        {
            for(int j = 0; j < map.hexagons[i].hexItems.Count; j++)
            {
                Vector3Int pos = new Vector3Int(map.hexagons[i].hexItems[j].posX, map.hexagons[i].hexItems[j].posY, 0);
                if(tilemap.GetTile(pos) == null)
                {
                    tilemap.SetTile(pos, roundTile);
                    
                }
            }

            GameObject hexbg = Instantiate(hexBgPrefab);
            hexbg.GetComponent<Transform>().SetParent(tilemap.transform, true);
            hexbg.GetComponent<Transform>().position = map.hexagons[i].hexBgPosition;
            hexbg.GetComponent<SpriteRenderer>().sprite = hexBgs[map.hexagons[i].bgIndex];
        }

        for(int i = 0; i < map.arrow.arrowItems.Count; i++)
        {
            ArrowItem arrowItem = map.arrow.arrowItems[i];
            Vector3Int pos = new Vector3Int(arrowItem.posX, arrowItem.posY, 0);
            if (tilemap.GetTile(pos) == null)
            {
                Tile t = ScriptableObject.CreateInstance<Tile>();
                string d = arrowItem.direction;
                //Debug.Log(arrowItem.direction);
                switch(arrowItem.direction)
                {
                    case "rightUp":
                        t = arrRightUp;
                        break;
                    case "right":
                        t = arrRight;
                        break;
                    case "rightDown":
                        t = arrRightDown;
                        break;
                    case "leftRight":
                        t = arrLeftRight;
                        break;
                    case "rightLeft":
                        t = arrRightLeft;
                        break;
                    case "leftUp":
                        t = arrLeftUp;
                        break;
                    case "leftDown":
                        t = arrLeftDown;
                        break;
                }

                tilemap.SetTile(pos, t);
            }
        }

        for(int i = 0; i < eventManager.allEvents.Count; i ++)
        {
            float eventPosX = eventManager.allEvents[i].posX;
            float eventPosY = (float)(eventManager.allEvents[i].posY / 2 * 1.73);
            int eventType = eventManager.allEvents[i].eventType;

            //Debug.Log(eventType);
            int index = 0;
            switch(eventType)
            {
                case 1:
                    index = 0;
                    break;
                case 2:
                    index = 1;
                    break;
                case 3:
                    index = 2;
                    break;
                case 4:
                    index = 3;
                    break;
                case 5:
                    index = 4;
                    break;
                case 9:
                    index = 5;
                    break;
            }

            GameObject eventObj = Instantiate(eventPrefabs[index]);
            eventObj.GetComponent<Transform>().SetParent(tilemap.transform, true);
            eventObj.GetComponent<Transform>().position = new Vector3(eventPosX, eventPosY, 0);

        }
        //PlayerController.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
