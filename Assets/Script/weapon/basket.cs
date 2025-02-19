using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basket : attack_manager
{
    public GameObject saltParticle;
    protected static Stack<GameObject> particleStack = new Stack<GameObject>();
    private bool swing = false;
    private float swingPoint = 16, nowAngle = 0, swingSpeed = 0.05f;

    void Start()
    {

    }

    void Update()
    {
        if (!time_manager.isPaused)
        {
            if (targetMonster != null) 
            {
                if (swing)
                {
                    nowAngle -= swingSpeed * time_manager.returnTimeScale;
                    if (nowAngle <= -swingPoint)
                    {
                        swing = false;
                        attack(-1);
                    }
                }
                else
                {
                    nowAngle += swingSpeed * time_manager.returnTimeScale;
                    if (nowAngle >= swingPoint)
                    {
                        swing = true;
                        attack(1);
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

    void attack(int dir)
    {
        for (int i = 0; i < UnityEngine.Random.Range(4, 6); i++)
        {
            GameObject particle;
            if (particleStack.Count == 0)
            {
                particle = Instantiate(saltParticle, this.transform);
            }
            else
            {
                particle = particleStack.Pop();
                particle.transform.SetParent(this.transform);
                particle.SetActive(true);
            }
            particle.transform.localPosition = new Vector3(0.10f * dir, -0.2f, -0.5f);
            particle.transform.localRotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 90));
            particle.transform.localScale = new Vector3(0.03f, 0.03f, 1);
            particle.transform.SetParent(particlePile.transform);
            Rigidbody2D particleRigidbody = particle.GetComponent<Rigidbody2D>();
            particleRigidbody.AddForce(Vector2.up * UnityEngine.Random.Range(1.0f, 3.0f), ForceMode2D.Impulse);
            particleRigidbody.AddForce(Vector2.right * UnityEngine.Random.Range(0, 2.0f) * dir, ForceMode2D.Impulse);
        }
    }
}
