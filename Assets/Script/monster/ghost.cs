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

    private void OnEnable()
    {
        init();
        startMove();
    }

    void Update()
    {
        if (health <= 0 || !time_manager.instance.night)
        {
            dead(returnMonsterIndex());
        }
    }
}
