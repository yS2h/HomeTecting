using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class garlic : attack_manager
{
    public GameObject garlicParticle;
    public static Stack<GameObject> garlicParticleStack = new Stack<GameObject>();
    private bool swing = false;
    private float swingPoint, nowAngle = 0, swingSpeed = 0.09f;
    private int attackDelay = 7;

    void Start()
    {
        swingPoint = UnityEngine.Random.Range(9.5f, 10.5f);
    }

    void Update()
    {
        if (!time_manager.isPaused)
        {
            if(targetMonster != null) {
                if (swing)
                {
                    nowAngle -= swingSpeed * time_manager.returnTimeScale;
                    if (nowAngle <= -swingPoint)
                    {
                        swing = false;
                        if(attackDelay == 7)
                        {
                            attack();
                            attackDelay = 0;
                        }
                        attackDelay += 1;
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
            }
            else 
            {
                if(nowAngle != 0) 
                {
                   nowAngle -= swingSpeed * time_manager.returnTimeScale * Math.Sign(nowAngle); 
                }
            }
            transform.rotation = Quaternion.Euler(0, 0, nowAngle);
        }
    }

    void attack() 
    {
        GameObject particle;
        if(garlicParticleStack.Count == 0) 
        {
            particle = Instantiate(garlicParticle, this.transform);   
        }
        else 
        {
            particle = garlicParticleStack.Pop();
            particle.transform.SetParent(this.transform);
            particle.SetActive(true);
        }
        particle.transform.localPosition = new Vector3(0, -0.07f, 0);
        particle.transform.SetParent(particlePile.transform);

    }
}
