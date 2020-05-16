using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonControl : MonoBehaviour
{

    public Button startBtn;
    public Button loadBtn;
    public Button statisticsBtn;
    public Button setUpBtn;
    public Button exitBtn;

    private GameObject loadingCanvas;
    // Start is called before the first frame update
    void Start()
    {
        initScript();
    }

    private void initScript()
    {
        loadingCanvas = GameObject.FindGameObjectWithTag("LoadingCanvas");

        startBtn.onClick.AddListener(clickStartBtn);
        loadBtn.onClick.AddListener(clickLoadBtn);


    }

    private void clickStartBtn()
    {
        GameManage.GameStart();
        //loadingCanvas.SetActive(true);
    }
    private void clickLoadBtn()
    {
        GameManage.GameLoad();
        //loadingCanvas.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
