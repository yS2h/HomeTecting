using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghost : monster_manager
{
    void Awake()
    {
        liveMonster = GameObject.Find("live_monster");
        deadMonster = GameObject.Find("dead_monster");
    }

    private void init()
    {
        health = 10;
        attackDamage = 10;
        attackSpeed = 10;
        moveSpeed = 10;
    }

    void Start()
    {
        init();
    }

    void Update()
    {
        if (!time_manager.instance.night)
        {
            health = 0;
        }
        if (health <= 0)
        {
            dead(0);
            init();
            gameObject.SetActive(false);
        }
    }
}
