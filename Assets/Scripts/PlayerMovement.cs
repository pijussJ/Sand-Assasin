using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public Transform orientation;
    public AudioSource orientationAudioSource;
    public Animator animator;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        MyInput();
        SpeedControl();

        // handle drag
        rb.drag = groundDrag;
    }

    private void FixedUpdate()
    {
        MovePlayer();

        //orientationAudioSource.mute = !(rb.velocity.magnitude >= 0.1f);

         // Get the current animator state information
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);

        // Check if the current animation is not the "attack" animation
        if (!currentState.IsName("Attack"))
        {
            if (rb.velocity.magnitude >= 0.1f)
            {
                orientationAudioSource.mute = false;
                animator.Play("Walk");
            }
            else
            {
                orientationAudioSource.mute = true;
                animator.Play("Idle");
            }
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}