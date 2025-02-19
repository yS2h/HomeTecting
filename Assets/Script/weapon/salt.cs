using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class salt : basket
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("ground"))
        {
            particleStack.Push(this.gameObject);
            this.gameObject.SetActive(false);
        }
        else if (col.CompareTag("monster")) 
        {
            particleStack.Push(this.gameObject);
            this.gameObject.SetActive(false);
            col.GetComponent<monster_manager>().damaged(2);
        }
    }
}
