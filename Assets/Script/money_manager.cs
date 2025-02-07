using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class money_manager : MonoBehaviour
{
    public static money_manager instance;
    
    private static int money; // private·Î º¯°æ

    private TMP_Text defaultMoneyText, storageMoneyText;
    
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
        defaultMoneyText = GameObject.Find("default_money_text").GetComponent<TMP_Text>();
        storageMoneyText = GameObject.Find("storage_money_text").GetComponent<TMP_Text>();
        money = 0;
    }

    public void textUpdate()
    {
        defaultMoneyText.text = "" + money;
        storageMoneyText.text = "" + money;
    }

    public static int returnMoney() => money;

    public void plusMoney(int amount) => money += amount;
    public void minusMoney(int amount) => money -= amount;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            plusMoney(10000);
        }

        textUpdate();
    }
}
