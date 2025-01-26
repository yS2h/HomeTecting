using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class time_manager : MonoBehaviour
{
    public int dayCycle = 180; // ³·¹ã ÁÖ±â

    public static time_manager instance;
    public float sec;
    public int day; // ¸î ¹øÂ° ¹ã
    public bool pause = false;
    public bool night = false;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        pause = false;
        day = 0;
        sec = 0;
    }

    void Update()
    {
        day = Mathf.FloorToInt(sec / (dayCycle * 2));

        if (!pause)
        {
            sec += Time.deltaTime;
            if (Mathf.FloorToInt(sec / dayCycle) % 2 == 1)
            {
                night = true;
            }
            else
            {
                night = false;
            }
            transform.rotation = Quaternion.Euler(0, 0, sec - 90);
        }
        else
        {
            Time.timeScale = 0; // 
        }
    }
}
