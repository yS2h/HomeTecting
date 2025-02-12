using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class garlic : attack_manager
{
    private bool swing = false;
    private float swingPoint = 10, nowAngle = 0, swingSpeed = 0.09f;

    void Start()
    {

    }

    void Update()
    {
        if (!time_manager.isPaused && time_manager.returnNight)
        {
            if (swing)
            {
                nowAngle -= swingSpeed * time_manager.returnTimeScale;
                if (nowAngle <= -swingPoint)
                {
                    swing = false;
                }
            }
            else
            {
                nowAngle += swingSpeed * time_manager.returnTimeScale;
                if (nowAngle >= swingPoint)
                {
                    swing = true;
                }
            }
            transform.rotation = Quaternion.Euler(0, 0, nowAngle);
        }
    }

}
