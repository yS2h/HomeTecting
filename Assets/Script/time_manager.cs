using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class time_manager : MonoBehaviour
{
    public static time_manager instance;
    public float sec;
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
        sec = 0;
    }

    void Update()
    {
        if (!pause)
        {
            sec += Time.deltaTime;
            if (Mathf.FloorToInt(sec / 180) % 2 == 1)
            {
                night = true;
            }
            else
            {
                night = false;
            }
            //Debug.Log(sec);
            transform.rotation = Quaternion.Euler(0, 0, sec - 90);
        }
    }
}
