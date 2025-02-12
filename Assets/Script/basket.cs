using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basket : attack_manager
{
    public GameObject saltGrain;
    private GameObject grainList;
    protected static Stack<GameObject> grainStack = new Stack<GameObject>();
    private bool swing = false;
    private float swingPoint = 16, nowAngle = 0, swingSpeed = 0.05f;

    void Start()
    {
        grainList = this.transform.parent.transform.GetChild(3).gameObject;
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
            transform.rotation = Quaternion.Euler(0, 0, nowAngle);
        }
    }

    void attack(int dir)
    {
        for (int i = 0; i < Random.Range(4, 6); i++)
        {
            GameObject grain;
            if (grainStack.Count == 0)
            {
                grain = Instantiate(saltGrain, this.transform);
            }
            else
            {
                grain = grainStack.Pop();
                grain.transform.SetParent(this.transform);
                grain.SetActive(true);
            }
            grain.transform.localPosition = new Vector3(0.10f * dir, -0.2f, -0.5f);
            grain.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 90));
            grain.transform.localScale = new Vector3(0.03f, 0.03f, 1);
            grain.transform.SetParent(grainList.transform);
            Rigidbody2D grainRigidbody = grain.GetComponent<Rigidbody2D>();
            grainRigidbody.AddForce(Vector2.up * Random.Range(1.0f, 3.0f), ForceMode2D.Impulse);
            grainRigidbody.AddForce(Vector2.right * Random.Range(0, 2.0f) * dir, ForceMode2D.Impulse);
        }
    }
}
