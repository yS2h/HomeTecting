using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class game_manager : MonoBehaviour
{
    public TMP_Text doubleButtonText;
    private GameObject defaultCanvas, pauseCanvas;

    void Start()
    {
        Time.timeScale = 1;
        doubleButtonText.text = "1X";
        defaultCanvas = GameObject.Find("default_canvas");
        pauseCanvas = GameObject.Find("pause_canvas");
        defaultCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
    }

    void Update()
    {

    }

    public void pushDoubleButton()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 2;
            doubleButtonText.text = "2X";
        }
        else if (Time.timeScale == 2)
        {
            Time.timeScale = 1;
            doubleButtonText.text = "1X";
        }
    }

    public void pushPauseButton()
    {
        time_manager.instance.pause = true;
        Time.timeScale = 0;
        defaultCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
    }

    public void pushResumeButton()
    {
        time_manager.instance.pause = false;
        Time.timeScale = 1;
        doubleButtonText.text = "1X";
        defaultCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
    }
}
