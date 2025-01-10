using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class weapon_manager : MonoBehaviour
{
    int[] weapons = new int[11];
    private bool setWeapon = false;
    private GameObject storageDoor, keyF;
    private GameObject defaultCanvas, storageCanvas;
    public TMP_Text closeButtonText;

    void Start()
    {
        storageDoor = GameObject.Find("storage_door");
        defaultCanvas = GameObject.Find("default_canvas");
        storageCanvas = GameObject.Find("storage_canvas");
        keyF = transform.GetChild(0).gameObject;
        defaultCanvas.SetActive(true);
        storageCanvas.SetActive(false);
        keyF.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && setWeapon)
        {
            //Debug.Log("open weapon");
            storageCanvas.SetActive(true);
            defaultCanvas.SetActive(false);
            time_manager.instance.pause = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "storage")
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
            setWeapon = false;
        }
    }

    public void pushButton()
    {
        storageCanvas.SetActive(false);
        defaultCanvas.SetActive(true);
        time_manager.instance.pause = false;
    }
}
