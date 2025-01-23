using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class money_manager : MonoBehaviour
{
    public static money_manager instance;
    public TMP_Text moneyText, moneyText2;
    public int money = 1000;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        moneyText.text = "" + money;
        moneyText2.text = "" + money;
    }

    void Update()
    {
        money += Mathf.FloorToInt(Time.timeScale);

        moneyText.text = "" + money;
        moneyText2.text = "" + money;
    }
}
