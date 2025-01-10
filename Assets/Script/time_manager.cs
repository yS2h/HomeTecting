using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class time_manager : MonoBehaviour {
    public static time_manager instance;
    public float sec;

    void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        sec = 0;
    }

    void Update() {
        sec += Time.deltaTime;
        //Debug.Log(sec);
        transform.rotation = Quaternion.Euler(0, 0, sec - 90);
    }
}
