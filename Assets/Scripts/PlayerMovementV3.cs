using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(Camera), typeof(Rigidbody))]
public class PlayerMovementV3 : MonoBehaviour
{
    [Header("Movement")]
    public float maxSpeed = 10f;
    public float accel = 80f;
    public float jumpForce = 100f;
    public float drag = 8f;

    [Header("Sprinting")]
    public float sprintSpeed = 15f;
    public float sprintAccelMultiplier = 1.2f;
    public float sprintTime = 5f;
    public float sprintRegenRate = 0.1f;
    public float sprintCooldown = 3f;
    public float sprintRemaining;
    public bool isCoolingDown = false;

    [Header("Camera")]
    public float sensitivity = 8f;
    [SerializeField]
    private float rotationX = 0.0f;
    [SerializeField]
    private float rotationY = 0.0f;

    [Header("Collision")]
    public LayerMask groundMask;
    [SerializeField]
    private bool isGrounded = true;

    private Rigidbody rb;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = drag;
        accel *= (drag / 2);

        cam = GetComponent<Camera>();
        cam.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

        Cursor.lockState = CursorLockMode.Locked;

        sprintRemaining = sprintTime;
    }

    // Fixed update is called at a set interval, regardless of the frametime
    void FixedUpdate()
    {
        CamMovement();
        Movement();
        CounterMovement();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    private void CamMovement()
    {
        // Fetch the mouse axis
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90, 90);

        cam.transform.eulerAngles = new Vector3(rotationX, rotationY, 0.0f);
    }

    private void Movement()
    {
        float currentAccel = accel;

        if (Input.GetKey(KeyCode.LeftShift) && !isCoolingDown)
        {
            currentAccel = accel * sprintAccelMultiplier;
            sprintRemaining -= Time.fixedDeltaTime;

            if(sprintRemaining <= 0.0f)
            {
                isCoolingDown = true;
                Invoke("EndSprintCooldown", sprintCooldown);
            }
        } else if (!isCoolingDown)
        {
            if(sprintRemaining <= sprintTime)
            {
                sprintRemaining += sprintRegenRate * Time.fixedDeltaTime; // Must be multiplied by Time.fixedDeltaTime to be in regen/second
            }
        }

        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                Vector3 relativeForce = currentAccel * transform.forward;
                rb.AddForce(relativeForce);
            }

            if (Input.GetKey(KeyCode.S))
            {
                Vector3 relativeForce = currentAccel * transform.forward;
                rb.AddForce(-relativeForce);
            }

            if (Input.GetKey(KeyCode.A))
            {
                Vector3 relativeForce = currentAccel * transform.right;
                rb.AddForce(-relativeForce);
            }

            if (Input.GetKey(KeyCode.D))
            {
                Vector3 relativeForce = currentAccel * transform.right;
                rb.AddForce(relativeForce);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                Vector3 relativeForce = jumpForce * Vector3.up;
                rb.AddForce(relativeForce);
            }
        }
    }

    private void CounterMovement()
    {
        float currentMaxSpeed = maxSpeed;

        if(Input.GetKey(KeyCode.LeftShift) && !isCoolingDown)
        {
            currentMaxSpeed = sprintSpeed;
        }

        if (isGrounded)
        {
            if (rb.velocity.magnitude > currentMaxSpeed)
            {
                rb.velocity = rb.velocity.normalized * currentMaxSpeed;
            }

            if (rb.velocity.magnitude < -currentMaxSpeed)
            {
                rb.velocity = -(rb.velocity.normalized * currentMaxSpeed);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (CheckCollisionLayer(collision, groundMask))
        {
            isGrounded = true;
            rb.drag = drag;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (CheckCollisionLayer(collision, groundMask))
        {
            isGrounded = false;
            rb.drag = 0.15f;
        }
    }

    // Returns true if the collision is on the desired layer
    private bool CheckCollisionLayer(Collision collision, LayerMask layermask)
    {
        int layer = (int)Mathf.Log(layermask.value, 2);

        return collision.gameObject.layer == layer;
    }

    void EndSprintCooldown()
    {
        isCoolingDown = false;
    }
}