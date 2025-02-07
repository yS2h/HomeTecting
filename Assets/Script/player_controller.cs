using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;
    private GameObject keyF;

    private void Start()
    {
        keyF = GameObject.Find("key_f");
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        keyF.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            playerAnimator.SetInteger("input", 2);
            playerRigidbody.AddForce(transform.right * -1 * moveSpeed, ForceMode2D.Impulse);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            playerAnimator.SetInteger("input", 1);
            playerRigidbody.AddForce(transform.right * moveSpeed, ForceMode2D.Impulse);
        }
        else
        {
            playerAnimator.SetInteger("input", 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Untagged"))
        {
            keyF.SetActive(true);
        }

        switch (col.tag)
        {
            case "storage":
                weapon_manager.getWeapon = true;
                break;

            case "house":
                weapon_manager.setWeapon = true;
                break;

            case "weaponPoint1":
                weapon_manager.setPoint1 = true;
                break;

            case "weaponPoint2":
                weapon_manager.setPoint2 = true;
                break;

            case "weaponPoint3":
                weapon_manager.setPoint3 = true;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag != "Untagged")
        {
            keyF.gameObject.SetActive(false);
            weapon_manager.getWeapon = false;
            weapon_manager.setWeapon = false;
            weapon_manager.setPoint1 = false;
            weapon_manager.setPoint2 = false;
            weapon_manager.setPoint3 = false;
        }
    }
}