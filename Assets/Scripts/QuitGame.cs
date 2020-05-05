using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{

    private Button quitBtn;
    // Start is called before the first frame update
    void Start()
    {
        quitBtn = this.GetComponent<Button>();
        quitBtn.onClick.AddListener(quitGame);
    }

    private void quitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
