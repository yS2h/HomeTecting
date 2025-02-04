using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class house_manager : MonoBehaviour
{
    public static house_manager instance;
    public TMP_Text houseHealthText;
    public float houseHealth;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    
    void Start()
    {
        houseHealth = 10000f;
    }

    void Update()
    {
        houseHealthText.text = "now hp : " + houseHealth;
        if (houseHealth <= 0)
        {
            time_manager.instance.pause = true;
            Debug.Log("Game Over!");
        }        
    }
}
