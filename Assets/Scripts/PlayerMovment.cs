using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    [Header("Ground Check")]

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public float playerHeight;
    public LayerMask whatIsGround;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool readyToJump;


    public float sprintSpeed;
    public float MaxSprintSpeed;
    //public float sprintCooldown = 3;
    public float holder = 5;
    private bool isSprinting;
    private bool canSprint;

    bool grounded;


    public Transform orientation;

    float horizontalInput;
    float verticalInput;
    public float sprintAmount;
    Vector3 moveDirection;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        MaxSprintSpeed = sprintSpeed;
        sprintAmount = sprintSpeed;
        holder = moveSpeed;

    }


    void Update()
    {

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);


        if (sprintAmount <= 0)
            moveSpeed = holder;

        if (isSprinting)
        {
            if (sprintAmount >= MaxSprintSpeed)
            {
                sprintAmount = MaxSprintSpeed;


            }
            if (sprintAmount <= 0)
            {
                sprintAmount = 0;


            }
            sprintAmount -= 0.01f;
        }
        else if (isSprinting == false)
        {
            if (sprintAmount >= MaxSprintSpeed)
            {
                sprintAmount = MaxSprintSpeed;



            }

            if (sprintAmount <= 0)
            {
                sprintAmount = 0;
                StartCoroutine(Six());

            }

            if (sprintSpeed > 0)
            {
                StartCoroutine(three());

            }

        }

        MyInput();
        SpeedControl();
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
        // Debug.Log(sprintAmount);

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }


        if (Input.GetKeyDown(sprintKey) && isSprinting == false)
        {
            isSprinting = true;
            StopAllCoroutines();
            if (sprintAmount == 0)
            {
                moveSpeed = holder;
                sprintAmount = 0;


            }
            else if (sprintAmount < 0)
            {
                moveSpeed = holder;
                sprintAmount = 0;


            }
            else if (sprintAmount > 0)
            {
                moveSpeed = sprintSpeed;


            }
        }
        else if (Input.GetKeyUp(sprintKey))
        {
            moveSpeed = holder;
            isSprinting = false;
        }



    }
    IEnumerator three()
    {
        yield return new WaitForSeconds(3);
        sprintAmount += 0.01f;
    }

    IEnumerator Six()
    {
        yield return new WaitForSeconds(6);
        sprintAmount += 0.01f;
    }

    void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    void SpeedControl()
    {
        Vector3 fatval = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (fatval.magnitude > moveSpeed)
        {
            Vector3 limitedVel = fatval.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }





}
