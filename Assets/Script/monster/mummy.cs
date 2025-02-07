using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mummy : monster_manager
{
    private void init()
    {
        monsterIndex = 1;
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
        if (health <= 0 || !time_manager.returnNight)
        {
            dead(returnMonsterIndex());
        }
    }

    protected override IEnumerator move()
    {
        if (isMove) yield break;

        isMove = true;
        while (this.transform.position.x > houseX)
        {
            yield return new WaitForSeconds(0.71f);
            this.transform.Translate(Vector3.left * Time.fixedDeltaTime * moveSpeed * 2f);
            yield return new WaitForSeconds(1.29f);
        }

        startAttack();
        isMove = false;
    }

    /*protected override IEnumerator move()
    {
        if (isMove) yield break;

        isMove = true;
        while (this.transform.position.x > houseX)
        {
            float startTime = Time.time;
            while (Time.time - startTime < 0.71f)
            {
                yield return null;
            }

            this.transform.Translate(Vector3.left * Time.deltaTime * moveSpeed * 30f / time_manager.instance.timeScale);

            startTime = Time.time;
            while (Time.time - startTime < 1.29f)
            {
                yield return null;
            }
        }

        startAttack();
        isMove = false;
    }*/
}
