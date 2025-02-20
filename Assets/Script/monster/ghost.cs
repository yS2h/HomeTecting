using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghost : monster_manager
{
    private void init()
    {
        monsterIndex = 0;
        health = 10;
        attackDamage = 10;
        attackDelay = 0.1f;
        attackSpeed = 0.9f;
        moveSpeed = 10;
    }

    protected void Start()
    {
        monsterRenderer = GetComponent<Renderer>();
        if (monsterRenderer != null)
        {
            objectColor = monsterRenderer.material.color;
        }
    }

    private void OnEnable()
    {
        init();
        startMove();
        if (monsterRenderer != null)
        {
            monsterRenderer.material.color = objectColor;
        }
    }

    void Update()
    {
        commonUpdate();
    }
}
