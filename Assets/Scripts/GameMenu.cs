using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{

    public Button back;
    public Button set;
    public Button exit;

    // Start is called before the first frame update
    void Start()
    {
        back.onClick.AddListener(backGame);
        exit.onClick.AddListener(exitGame);
    }

    void backGame()
    {
        Destroy(this.gameObject);
    }

    void exitGame()
    {
        PlayerController.Save();
        GameManage.GameReady();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.transform.Find("GameMenuCanvas").gameObject.SetActive(!this.transform.Find("GameMenuCanvas").gameObject.activeSelf);
        }
    }
}
