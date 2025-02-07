using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class house_manager : MonoBehaviour
{
    public static house_manager instance;

    private static float houseHealth;
    private TMP_Text houseHealthText;
    
    void Start()
    {
        houseHealthText = GameObject.Find("house_health_text").GetComponent<TMP_Text>();
        houseHealth = 10000f;
    }

    public static float returnHealth() => houseHealth;

    public static void attackHouse(float damage) => houseHealth -= damage;

    void Update()
    {
        houseHealthText.text = "now hp : " + houseHealth;
        if (houseHealth <= 0)
        {
            time_manager.isPaused = true;
            Debug.Log("Game Over!");
        }        
    }
}
