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

    [Header("Collision")]
    public LayerMask groundMask;
    [SerializeField]
    private bool isGrounded = true;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = drag;
        accel *= (drag / 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        CounterMovement();
    }

    private void Movement()
    {
        if(isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                Vector3 relativeForce = accel * transform.forward;
                rb.AddForce(relativeForce);
            }

            if (Input.GetKey(KeyCode.S))
            {
                Vector3 relativeForce = accel * transform.forward;
                rb.AddForce(-relativeForce);
            }

            if (Input.GetKey(KeyCode.A))
            {
                Vector3 relativeForce = accel * transform.right;
                rb.AddForce(-relativeForce);
            }

            if (Input.GetKey(KeyCode.D))
            {
                Vector3 relativeForce = accel * transform.right;
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
        if(isGrounded)
        {
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }

            if (rb.velocity.magnitude < -maxSpeed)
            {
                rb.velocity = -(rb.velocity.normalized * maxSpeed);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(CheckCollisionLayer(collision, groundMask))
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
}
