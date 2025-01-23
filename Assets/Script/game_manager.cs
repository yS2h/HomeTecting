using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class game_manager : MonoBehaviour
{
    public TMP_Text doubleButtonText;
    private GameObject defaultCanvas, pauseCanvas;
    private GameObject nightSky, nightText;
    private bool isNightActive = false; 

    void Start()
    {
        Time.timeScale = 1;
        doubleButtonText.text = "1X";
        defaultCanvas = GameObject.Find("default_canvas");
        pauseCanvas = GameObject.Find("pause_canvas");
        nightSky = GameObject.Find("night_sky");
        nightText = GameObject.Find("night_text");
        defaultCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
        nightSky.SetActive(false);
        nightText.SetActive(false);
    }

    void Update()
    {
        if (time_manager.instance.night)
        {
            nightSky.SetActive(true);

            if (!isNightActive)
            {
                isNightActive = true; 
                StartCoroutine(ShowNightText()); 
            }
        }
        else
        {
            nightSky.SetActive(false);
            isNightActive = false; 
        }
    }

    IEnumerator ShowNightText()
    {
        nightText.SetActive(true); 
        yield return new WaitForSeconds(3); 
        nightText.SetActive(false); 
    }

    public void pushDoubleButton()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 2;
            doubleButtonText.text = "2X";
        }
        else 
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
