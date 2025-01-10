using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class time_manager : MonoBehaviour
{
    public static time_manager instance;
    public float sec;
    public bool pause = false;

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
            //Debug.Log(sec);
            transform.rotation = Quaternion.Euler(0, 0, sec - 90);
        }
    }
}
