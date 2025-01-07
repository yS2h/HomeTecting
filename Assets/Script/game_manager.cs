using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class game_manager : MonoBehaviour {
    public TMP_Text doubleButtonText;

    void Start() {
        Time.timeScale = 1;
        doubleButtonText.text = "1X";
    }

    void Update() {
        
    }

    public void pushButton() {
        if (Time.timeScale == 1) {
            Time.timeScale = 2;
            doubleButtonText.text = "2X";
        } else if (Time.timeScale == 2) {
            Time.timeScale = 1;
            doubleButtonText.text = "1X";
        }
    }
}
