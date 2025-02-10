using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class weapon_manager : MonoBehaviour
{
    const int weaponNum = 12; // 무기 총 개수

    bool[] weaponNow = new bool[weaponNum] { true, true, false, false, false, true, false, true, false, false, true, true }; // 현재 무기를 구매한 상태인지
    int[] weaponPosition = new int[weaponNum] { 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3 }; // 무기를 장착 할 수 있는 곳 번호
    int[] prices = new int[weaponNum] { 3000, 5000, 7000, 10000, 15000, 20000, 25000, 30000, 35000, 40000, 45000, 5000}; // 무기 가격
    public static bool getWeapon = false, setWeapon = false, setPoint1 = false, setPoint2 = false, setPoint3 = false;
    private int[] selectedWeapon = new int[3] { -1, -1, -1 };
    private GameObject storageDoor, houseDoor, pointButton, weaponImage, weaponList;
    private GameObject defaultCanvas, storageCanvas, houseCanvas, pointCanvas;
    private TMP_Text weaponInfoText;
    public TMP_Text[] weaponButtonText = new TMP_Text[weaponNum];
    private GameObject[] weaponObjectList = new GameObject[weaponNum];

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

        public string log() => information;
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
        weaponInfoText = GameObject.Find("weapon_info_text").GetComponent<TMP_Text>();
        weaponList = GameObject.Find("weapon_list");

        defaultCanvas.SetActive(true);
        storageCanvas.SetActive(false);
        houseCanvas.SetActive(false);
        pointCanvas.SetActive(false);
        weaponList.SetActive(true);

        for (int i = 0; i < weaponNum; i++)
        {
            weaponObjectList[i] = weaponList.transform.GetChild(i).gameObject;
            weaponObjectList[i].SetActive(false);
        }

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
                defaultCanvas.SetActive(false);
                storageCanvas.SetActive(true);
                time_manager.isPaused = true;
            }
            else if (setWeapon)
            {
                defaultCanvas.SetActive(false);
                houseCanvas.SetActive(true);
                time_manager.isPaused = true;
            }

            else if (setPoint1)
            {
                defaultCanvas.SetActive(false);
                time_manager.isPaused = true;
                setPointWeapon(1);
            }
            else if (setPoint2)
            {
                defaultCanvas.SetActive(false);
                time_manager.isPaused = true;
                setPointWeapon(2);
            }

            else if (setPoint3)
            {
                defaultCanvas.SetActive(false);
                time_manager.isPaused = true;
                setPointWeapon(3);
            }
        }
    }

    public void selectWeapon(int n)
    {
        weaponInfoText.text = weaponClass[n].log();
        weaponImage.transform.GetComponent<Image>().sprite = pointButton.transform.GetChild(n).GetComponent<Image>().sprite;
        if (setPoint1)
            selectedWeapon[0] = n;
        else if (setPoint2)
            selectedWeapon[1] = n;
        else if (setPoint3)
            selectedWeapon[2] = n;
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
        weaponInfoText.text = "";
        weaponImage.transform.GetComponent<Image>().sprite = null;

        int selected = selectedWeapon[n - 1];
        if (selected != -1)
        {
            //Debug.Log(selected);
            selectWeapon(selected);
        }
    }

    public void pushCloseButton()
    {
        storageCanvas.SetActive(false);
        houseCanvas.SetActive(false);
        pointCanvas.SetActive(false);
        defaultCanvas.SetActive(true);
        time_manager.isPaused = false;
    }

    public void pushConfirmButton()
    {
        pushCloseButton();
        if (setPoint1)
        {
            if (selectedWeapon[0] == -1) 
                return;
            activeWeapon(1, selectedWeapon[0]);
        }
        if (setPoint2)
        {
            if (selectedWeapon[1] == -1) 
                return;
            activeWeapon(2, selectedWeapon[1]);
        }
        if (setPoint3)
        {
            if (selectedWeapon[2] == -1) 
                return;
            activeWeapon(3, selectedWeapon[2]);
        }
    }

    public void activeWeapon(int n, int index)
    {
        for (int i = 0; i < weaponNum; i++)
        {
            if (n == weaponPosition[i])
                weaponObjectList[i].SetActive(i == index);
        }
    }

    public void pushWeaponButton(int n)
    {
        if (money_manager.returnMoney() >= prices[n] && !weaponNow[n])
        {
            money_manager.instance.minusMoney(prices[n]);
            money_manager.instance.textUpdate();
            weaponNow[n] = true;
            weaponButtonText[n].text = "SOLD OUT";
        }
    }
}
