using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class garlic_particle : garlic
{
    private float garlicParticleSpeed = 2f;
    private bool targeting; 
    private bool isAttack;
    private GameObject mytarget;

    void Start()
    {

    }

    void OnEnable() 
    {
        targeting = false;
        mytarget = null;
        isAttack = false;
    }

    void Update()
    {
        if(!targeting) 
        {
            mytarget = targetMonster;
            if(mytarget != null) 
            {
                if(Vector3.Distance(transform.position, mytarget.transform.position) < 0.3f) 
                {
                    targeting = true;
                }
            }
        }
        if (mytarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, mytarget.transform.position + new Vector3(0, 0.2f, 0), garlicParticleSpeed * Time.deltaTime);
            if(targeting) 
            {
                if(mytarget.GetComponent<monster_manager>().returnHealth() < 0) 
                {
                    gameObject.SetActive(false);
                    garlicParticleStack.Push(this.gameObject);
                }
                if(!isAttack) 
                {
                    isAttack = true;
                    StartCoroutine(breath());
                }
            }
        }
        else 
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, -10f, 0), garlicParticleSpeed * Time.deltaTime * 2);
            if(transform.position.y < -1.6)
            {
                gameObject.SetActive(false);
                garlicParticleStack.Push(this.gameObject);
            }
        }
    }

    private IEnumerator breath() 
    {
        for (int i = 0; i < 3; i++) 
        {
            targetMonsterScript.damaged(1);
            yield return new WaitForSeconds(1f);
        }
        gameObject.SetActive(false);
        garlicParticleStack.Push(this.gameObject);
    }
}
