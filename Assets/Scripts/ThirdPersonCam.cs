using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    public Transform orientation;
    public Transform player;
    public Transform playerObject;

    public float rotationSpeed;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        /*
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        */
    }

    private void Update()
    {
        Vector3 viewDirection = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDirection.normalized;

        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 inputDirection = orientation.forward * ver + orientation.right * hor;

        if (inputDirection != Vector3.zero)
        {
            playerObject.forward = Vector3.Slerp(playerObject.forward, inputDirection.normalized, rotationSpeed * Time.deltaTime);
        }
    }
}
