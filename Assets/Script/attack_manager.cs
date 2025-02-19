using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class attack_manager : MonoBehaviour
{
    public static List<GameObject> liveMonsterList = new List<GameObject>();
    public static GameObject targetMonster;
    protected static monster_manager targetMonsterScript;
    public static bool selectTarget = false;

    protected static GameObject particlePile;
    private GameObject targetPointer;
    private bool pointerActive = true;

    public static void findTarget()
    {
        targetMonster = null;
        float x = float.MaxValue;

        foreach (GameObject i in liveMonsterList)
        {
            if(i.transform.position.z < -4) 
            {
                i.transform.position += new Vector3(0, 0, -i.transform.position.z - 4);
            }
            if (i.transform.position.x < x) 
            {
                x = i.transform.position.x;
                targetMonster = i;
            }
        }
        if(targetMonster != null) 
        {
            targetMonsterScript = targetMonster.GetComponent<monster_manager>();
            targetMonster.transform.position += new Vector3(0, 0, -targetMonster.transform.position.z - 4.1f);
        }
    }

    void Start()
    {
        particlePile = GameObject.Find("particle_pile");
        targetPointer = GameObject.Find("target_pointer");
    }

    void Update()
    {
        if(targetMonster != null) 
        {
            targetPointer.transform.position = targetMonster.transform.position + new Vector3(0, 1, 0);
            if(!pointerActive) 
            {
                targetPointer.SetActive(true);
                pointerActive = true;
            }
        }
        else 
        {
            if(pointerActive) 
            {
                targetPointer.SetActive(false);
                pointerActive = false;
            }
        }
    }
}