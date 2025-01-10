using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_manager : MonoBehaviour {
    int[] weapons = new int[11];
    private bool setWeapon = false;
    private GameObject storageDoor, keyF;

    void Start() {
        storageDoor = GameObject.Find("storage_door");
        keyF = transform.GetChild(0).gameObject;
        keyF.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.F) && setWeapon) {
            Debug.Log("open weapon");
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "storage") {
            keyF.gameObject.SetActive(true);
            setWeapon = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if (col.tag == "storage") {
            keyF.gameObject.SetActive(false);
            setWeapon = false;
        }
    }
}
