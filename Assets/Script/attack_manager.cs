using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class attack_manager : MonoBehaviour
{
    protected static GameObject targetMonster;
    private GameObject liveMonster;
    private GameObject findTarget()
    {
        if (liveMonster == null)
            return null;
        Transform[] children = liveMonster.GetComponentsInChildren<Transform>();
        if (children.Length <= 1)
            return null;
        Transform leftmostChild = null;
        float minX = float.MaxValue;
        for (int i = 1; i < children.Length; i++)
        {
            float currentX = children[i].position.x;
            if (currentX < minX)
            {
                minX = currentX;
                leftmostChild = children[i];
            }
        }
        return leftmostChild?.gameObject;
    }

    void Start()
    {
        liveMonster = GameObject.Find("live_monster");
    }

    void Update()
    {
        targetMonster = findTarget();
        //Debug.Log(targetMonster.name);
    }
}