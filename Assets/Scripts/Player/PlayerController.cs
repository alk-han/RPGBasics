using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float      moveSpeed = 6f;
    [SerializeField] private float      turnSmoothTime = 0.1f;
    [SerializeField] private float      jumpHeight = 3f;
    [Header("Gravity & Ground Check")]
    [SerializeField] private float      gravity = -9.81f;
    [SerializeField] private float      groundDistance = 0.2f;
    [SerializeField] private Transform  groundCheck;
    [SerializeField] private LayerMask  groundMask;

    private CharacterController controller;
    private Vector3 velocity;
    private float turnSmoothVelocity;
    private Camera mainCam;
    private bool isGrounded;
    private Animator anim;


    private void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        mainCam = Camera.main;
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        CheckGround();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCam.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            anim.SetTrigger("Wave");
        }

        anim.SetFloat("MoveSpeed", direction.magnitude);

        anim.SetBool("Grounded", isGrounded);
        anim.SetFloat("VelocityY", velocity.y);
        
        ApplyGravity();
    }

    // private void OnAnimatorMove()
    // {
    //     controller.Move(anim.deltaPosition);
    // }

    private void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    private void ApplyGravity()
    {
        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }
            
        controller.Move(velocity * Time.deltaTime); // Delta y = 1/2 . g. time * time (Physics of a free fall)
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3( groundCheck.position.x, groundCheck.position.y - groundDistance, groundCheck.position.z));
    }
}
