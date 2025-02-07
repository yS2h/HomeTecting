using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class time_manager : MonoBehaviour
{
    public static time_manager instance;

    public int myTimeSpeed;
    
    public static int dayCycle = 180; // ≥∑π„ ¡÷±‚
    private static int timeScale;
    private static float sec;
    private static int day;
    private static bool night = false;
    private bool pause = false;

    private TMP_Text doubleButtonText;
    private bool isNightActive = false;
    private GameObject defaultCanvas, pauseCanvas;
    private GameObject nightSky, nightText, clock;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    void Start()
    {
        defaultCanvas = GameObject.Find("default_canvas");
        pauseCanvas = GameObject.Find("pause_canvas");
        nightSky = GameObject.Find("night_sky");
        nightText = GameObject.Find("night_text");
        clock = GameObject.Find("clock");
        doubleButtonText = GameObject.Find("double_button_text").GetComponent<TMP_Text>();
        defaultCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
        nightSky.SetActive(false);
        nightText.SetActive(false);

        pause = false;
        day = 0;
        sec = 0;
        timeScale = 1;
        myTimeSpeed = 2;
    }

    void Update()
    {
        day = Mathf.FloorToInt(sec / (dayCycle * 2));
        doubleButtonText.text = timeScale + "X";

        if (!pause)
        {
            Time.timeScale = timeScale;
            sec += Time.deltaTime;

            if (Mathf.FloorToInt(sec / dayCycle) % 2 == 1)
            {
                night = true;
            }
            else
            {
                night = false;
            }
            clock.transform.rotation = Quaternion.Euler(0, 0, sec - 90);
        }
        else
        {
            Time.timeScale = 0;
        }

        if (night)
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

    public static bool returnNight => night;
    public static float returnSec => sec;
    public static int returnDay => day;
    public static int returnDayCycle => dayCycle;

    public static bool isPaused
    {
        get { return instance.pause; }
        set { instance.pause = value; }
    }

    public void pushDoubleButton()
    {
        if (timeScale == 1)
        {
            timeScale = myTimeSpeed;
        }
        else
        {
            timeScale = 1;
        }
    }

    public void pushPauseButton()
    {
        pause = true;
        defaultCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
    }

    IEnumerator ShowNightText()
    {
        nightText.SetActive(true);
        yield return new WaitForSeconds(3);
        nightText.SetActive(false);
    }
}
