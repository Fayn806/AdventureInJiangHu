using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CGControl : MonoBehaviour
{
    public VideoPlayer startCGVideoPlayer;
    public GameObject[] menuBtns;

    private bool isCGOver = false;
    private bool isMenuPopped = false;
    // Start is called before the first frame update
    void Start()
    {
        initStartScene();
    }

    private void initStartScene()
    {
        GameManage.GameCG();
        //CG播放结束事件绑定方法
        startCGVideoPlayer.loopPointReached += StartCGVideoPlayer_loopPointReached;
    }
    /// <summary>
    /// 播放结束事件
    /// </summary>
    /// <param name="source"></param>
    private void StartCGVideoPlayer_loopPointReached(VideoPlayer source)
    {
        setCGEnd();
    }

    private void setCGEnd()
    {
        //设置cg结束标志
        isCGOver = true;
        GameManage.GameReady();
    }

    private void setPopGameMenu()
    {
        //设置menupop标志
        isMenuPopped = true;
        GameManage.GameMenuShow();
    }

    private void FixedUpdate()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (isCGOver != true)
            {
                setCGEnd();
            }
            else if (isCGOver == true && isMenuPopped == false)
            {
                setPopGameMenu();
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
