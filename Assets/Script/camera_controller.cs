using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_controller : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D cameraRigidbody;
    private bool isActive;

    void Start()
    {
        cameraRigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.Find("player");    
        player.gameObject.SetActive(true);
        player.transform.position = new Vector3(0, -1.1f, -5);
        transform.position = player.transform.position + new Vector3(0, 1.1f, -1);
        isActive = true;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            cameraRigidbody.AddForce(transform.right * -1 * player_controller.moveSpeed, ForceMode2D.Impulse);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            cameraRigidbody.AddForce(transform.right * player_controller.moveSpeed, ForceMode2D.Impulse);
        }
    }

    private void Update() 
    {
        if(time_manager.returnNight && isActive) 
        {
            player.gameObject.SetActive(false);
            isActive = false;
        }
        if(!time_manager.returnNight && !isActive)
        {
            player.gameObject.SetActive(true);
            isActive = true;
            transform.position = player.transform.position + new Vector3(0, 1.1f, -1);
        }
    }
}
