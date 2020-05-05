using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterMove : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject man;

    private Vector3 currentPos;
    private static Vector3Int lastRoundPos;
    private bool moved = false;
    // Start is called before the first frame update
    void Start()
    {
        man.transform.position = PlayerController.posReal;
        currentPos = PlayerController.posCur;

        Transform t = Camera.main.GetComponent<Transform>();
        Vector3 tarPos = new Vector3(currentPos.x, currentPos.y, -10);
        t.position = tarPos;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if (Event.current.type == EventType.MouseDown)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //由正确的方向得到屏幕中tile所在的位置
            Vector3Int vector = tilemap.WorldToCell(ray.GetPoint(-ray.origin.z / ray.direction.z));
            Debug.Log(vector.x);
            Debug.Log(vector.y);

            TileBase tile = tilemap.GetTile(vector);
            if(tile != null)
            {
                
                if (tile.name == "round" && checkMove(vector))
                {
                    Debug.Log("move");
                    PlayerController.DealHurt();

                    PlayerController.posInTilemap.x = vector.x;
                    PlayerController.posInTilemap.y = vector.y;

                    float y = (float)(vector.y + 0.5);
                    lastRoundPos = new Vector3Int(Convert.ToInt32(currentPos.x), Convert.ToInt32(currentPos.y), 0);
                    currentPos = new Vector3(vector.x, y, 0);
                    PlayerController.posCur = currentPos;
                    //偏移
                    float manY = (float)(vector.y / 2 * 1.73 + 0.5);
                    iTween.MoveTo(man, new Vector3(vector.x, manY, 0), 0.5f);

                    moved = true;
                    Invoke("moveEnd", 0.5f);
                }
            }
            
        }
    }

    private void moveEnd()
    {
        PlayerController.posReal = man.transform.position;

        for (int i = 0; i < PlayerController.eventManager.allEvents.Count; i++)
        {
            if (PlayerController.eventManager.allEvents[i].posX == PlayerController.posInTilemap.x && PlayerController.eventManager.allEvents[i].posY == PlayerController.posInTilemap.y)
            {
                PlayerController.curEventType = PlayerController.eventManager.allEvents[i].eventType;
                Debug.Log("event----" + PlayerController.curEventType);
                break;
            }

        }

        GameManage.GameStep();
    }

    private void FixedUpdate()
    {
        if(moved == true)
        {
            Transform t = Camera.main.GetComponent<Transform>();
            Vector3 tarPos = new Vector3(currentPos.x, currentPos.y, -10);
            t.position = Vector3.Lerp(t.position, tarPos, 3 * Time.deltaTime);
            if(Math.Abs(t.position.x - tarPos.x) < 0.05 && Math.Abs(t.position.y - tarPos.y) < 0.05)
            {
                moved = false;
            }
        }
    }

    private bool checkMove(Vector3Int target)
    {

        if(target.Equals(lastRoundPos))
        {
            return false;
        }

        Vector3 manPos = currentPos;

        int x = Convert.ToInt32(manPos.x);
        int y = Convert.ToInt32(manPos.y);

        int targetX = target.x;
        int targetY = target.y;

        int offsetX = targetX - x;
        int offsetY = targetY - y;

        TileBase arrow;

        if(offsetX == 1 && offsetY == 2)
        {
            //右上
            arrow = tilemap.GetTile(new Vector3Int(x, y + 1, 0));
            if(arrow.name == "arrRightUp" || arrow.name == "arrRightLeft")
            {
                return true;
            }
        }
        if (offsetX == 1 && offsetY == -2)
        {
            //右下
            arrow = tilemap.GetTile(new Vector3Int(x, y - 1, 0));
            if (arrow.name == "arrRightDown" || arrow.name == "arrLeftRight")
            {
                return true;
            }
        }
        if (offsetX == 2 && offsetY == 0)
        {
            //右
            arrow = tilemap.GetTile(new Vector3Int(x + 1, y, 0));
            if (arrow.name == "arrRight")
            {
                return true;
            }
        }
        if (offsetX == -1 && offsetY == 2)
        {
            //左上
            arrow = tilemap.GetTile(new Vector3Int(x - 1, y + 1, 0));
            if (arrow.name == "arrLeftRight" || arrow.name == "arrLeftUp")
            {
                return true;
            }
        }
        if (offsetX == -1 && offsetY == -2)
        {
            //左下
            arrow = tilemap.GetTile(new Vector3Int(x - 1, y - 1, 0));
            if (arrow.name == "arrRightLeft" || arrow.name == "arrLeftDown")
            {
                return true;
            }
        }

        return false;
    }
}
