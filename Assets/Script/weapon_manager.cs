using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class weapon_manager : MonoBehaviour
{
    bool[] weapons = new bool[11];
    int[] prices = new int[11] {3000, 5000, 7000, 10000, 15000, 20000, 25000, 30000, 35000, 40000, 45000};
    private bool getWeapon = false, setWeapon = false;
    private GameObject storageDoor, houseDoor, keyF;
    private GameObject defaultCanvas, storageCanvas, houseCanvas;
    public TMP_Text closeButtonText;
    public TMP_Text[] weaponButtonText = new TMP_Text[11];

    void Start()
    {
        storageDoor = GameObject.Find("storage_door");
        houseDoor = GameObject.Find("house_door");
        defaultCanvas = GameObject.Find("default_canvas");
        storageCanvas = GameObject.Find("storage_canvas");
        houseCanvas = GameObject.Find("house_canvas");
        keyF = transform.GetChild(0).gameObject;
        defaultCanvas.SetActive(true);
        storageCanvas.SetActive(false);
        houseCanvas.SetActive(false);
        keyF.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {   
            if (getWeapon)
            {
                //Debug.Log("open weapon");
                storageCanvas.SetActive(true);
                defaultCanvas.SetActive(false);
                time_manager.instance.pause = true;
            }
            else if (setWeapon)
            {
                //Debug.Log("open house");
                houseCanvas.SetActive(true);
                defaultCanvas.SetActive(false);
                time_manager.instance.pause = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "storage")
        {
            keyF.gameObject.SetActive(true);
            getWeapon = true;
            //Debug.Log(money_manager.instance.money);
        }

        else if (col.tag == "house")
        {
            keyF.gameObject.SetActive(true);
            setWeapon = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "storage")
        {
            keyF.gameObject.SetActive(false);
            getWeapon = false;
        }

        else if (col.tag == "house")
        {
            keyF.gameObject.SetActive(false);
            setWeapon = false;
        }
    }

    public void pushCloseButton()
    {
        storageCanvas.SetActive(false);
        houseCanvas.SetActive(false);
        defaultCanvas.SetActive(true);
        time_manager.instance.pause = false;
    }

    public void pushWeaponButton(int n)
    {
        if (money_manager.instance.money > prices[n] && !weapons[n])
        {
            money_manager.instance.money -= prices[n];
            money_manager.instance.moneyText2.text = "" + money_manager.instance.money;
            weapons[n] = true;
            weaponButtonText[n].text = "SOLD OUT";
        }
    }
}
