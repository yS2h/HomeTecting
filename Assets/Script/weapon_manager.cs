using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class weapon_manager : MonoBehaviour
{
    const int weaponNum = 12; // 무기 총 개수

    bool[] weaponNow = new bool[weaponNum]; // 현재 무기를 구매한 상태인지
    int[] weaponPosition = new int[weaponNum] { 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3 }; // 무기를 장착 할 수 있는 곳 번호
    int[] prices = new int[weaponNum] { 3000, 5000, 7000, 10000, 15000, 20000, 25000, 30000, 35000, 40000, 45000, 5000}; // 무기 가격
    private bool getWeapon = false, setWeapon = false, setPoint1 = false;
    private GameObject storageDoor, houseDoor, keyF, pointButton, weaponImage;
    private GameObject defaultCanvas, storageCanvas, houseCanvas, pointCanvas;
    public TMP_Text doubleButtonText, weaponInfoText;
    public TMP_Text[] weaponButtonText = new TMP_Text[weaponNum];

    private weapon[] weaponClass = new weapon[weaponNum];

    private class weapon
    {
        protected string name;
        protected float attackDamage;
        protected float attackSpeed;
        protected int attackDistance;
        protected float damagePerSec;
        protected string information;
        
        public weapon (string _name, float _attackDamage, float _attackSpeed, int _attackDistance, float _damagePerSec, string _information)
        {
            name = _name;
            attackDamage = _attackDamage;
            attackSpeed = _attackSpeed;
            attackDistance = _attackDistance;
            damagePerSec = _damagePerSec;
            information = _information;
        }

        public string log()
        {
            return information;
        }
    }

    void Start()
    {
        storageDoor = GameObject.Find("storage_door");
        houseDoor = GameObject.Find("house_door");
        defaultCanvas = GameObject.Find("default_canvas");
        storageCanvas = GameObject.Find("storage_canvas");
        houseCanvas = GameObject.Find("house_canvas");
        pointCanvas = GameObject.Find("point_canvas");
        pointButton = GameObject.Find("point_weapon_button");
        weaponImage = GameObject.Find("weapon_image");
        keyF = transform.GetChild(0).gameObject;
        defaultCanvas.SetActive(true);
        storageCanvas.SetActive(false);
        houseCanvas.SetActive(false);
        pointCanvas.SetActive(false);
        keyF.SetActive(false);

        weaponClass[0] = new weapon("garlic", 10, 10, 10, 10, "i'm garlic");
        weaponClass[1] = new weapon("ax", 10, 10, 10, 10, "i'm ax");
        weaponClass[2] = new weapon("shuriken", 10, 10, 10, 10, "i'm shuriken");
        weaponClass[3] = new weapon("gun", 10, 10, 10, 10, "i'm gun");
        weaponClass[4] = new weapon("bible", 10, 10, 10, 10, "i'm bible");
        weaponClass[5] = new weapon("salt", 10, 10, 10, 10, "i'm salt");
        weaponClass[6] = new weapon("cross", 10, 10, 10, 10, "i'm cross");
        weaponClass[7] = new weapon("holyWater", 10, 10, 10, 10, "i'm holy water");
        weaponClass[8] = new weapon("redBeen", 10, 10, 10, 10, "i'm red been");
        weaponClass[9] = new weapon("amulet", 10, 10, 10, 10, "i'm amulet");
        weaponClass[10] = new weapon("javelin", 10, 10, 10, 10, "i'm javelin");
        weaponClass[11] = new weapon("flameThrower", 10, 10, 10, 10, "i'm flame thrower");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (getWeapon)
            {
                //Debug.Log("open weapon");
                defaultCanvas.SetActive(false);
                time_manager.instance.pause = true;
                storageCanvas.SetActive(true);
            }
            else if (setWeapon)
            {
                //Debug.Log("open house");
                defaultCanvas.SetActive(false);
                time_manager.instance.pause = true;
                houseCanvas.SetActive(true);
            }

            else if (setPoint1)
            {
                defaultCanvas.SetActive(false);
                time_manager.instance.pause = true;
                setPointWeapon(1);
            }
        }
    }

    public void selectWeapon(int n)
    {
        weaponInfoText.text = weaponClass[n].log();
        weaponImage.transform.GetComponent<Image>().sprite = pointButton.transform.GetChild(n).GetComponent<Image>().sprite;
    }

    private void setPointWeapon(int n)
    {
        //Debug.Log("open point " + n);
        pointCanvas.SetActive(true);
        for(int i = 0; i < weaponNum; i++)
        {
            pointButton.transform.GetChild(i).GetComponent<Button>().interactable = false;
            if (weaponNow[i] && weaponPosition[i] == n)
            {
                pointButton.transform.GetChild(i).GetComponent<Button>().interactable = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "storage")
        {
            keyF.gameObject.SetActive(true);
            getWeapon = true;
        }

        else if (col.tag == "house")
        {
            keyF.gameObject.SetActive(true);
            setWeapon = true;
        }

        else if (col.tag == "weaponPoint1")
        {
            keyF.gameObject.SetActive(true);
            setPoint1 = true;
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

        else if (col.tag == "weaponPoint1")
        {
            keyF.gameObject.SetActive(false);
            setPoint1 = false;
        }
    }

    public void pushCloseButton()
    {
        storageCanvas.SetActive(false);
        houseCanvas.SetActive(false);
        pointCanvas.SetActive(false);
        defaultCanvas.SetActive(true);
        time_manager.instance.pause = false;
        Time.timeScale = 1;
        doubleButtonText.text = "1X";
    }

    public void pushWeaponButton(int n)
    {
        if (money_manager.instance.money > prices[n] && !weaponNow[n])
        {
            money_manager.instance.money -= prices[n];
            money_manager.instance.moneyText2.text = "" + money_manager.instance.money;
            weaponNow[n] = true;
            weaponButtonText[n].text = "SOLD OUT";
        }
    }
}
