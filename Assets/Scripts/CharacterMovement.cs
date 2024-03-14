using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float sens = 3f;
    [SerializeField] float speed = 10f;
    [SerializeField] float sprintMult = 1.5f;
    [SerializeField] GameObject cam;
    CharacterController controller;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float gravity = -9.81f;
    float xRotation;
    Vector3 velocity;

    [SerializeField] AudioSource walkAudio;
    [SerializeField] float walkPlayCooldown = 0.75f;
    [SerializeField] float walkPlayCooldownSprinting = 0.5f;
    float walkPlayCooldownTimer;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
        Look();
    }

    void Move()
    {
        //Move
        Vector3 move;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        move = ((transform.right * x + transform.forward * z) * speed) * Time.deltaTime;

        if(Input.GetKey(KeyCode.LeftShift))
        {
            move *= sprintMult;
        }

        if(controller.isGrounded && move.magnitude > 0 && walkPlayCooldownTimer <= 0f)
        {
            //let audio play
            walkAudio.pitch = Random.Range(0.9f, 1.1f);

            if(Input.GetKey(KeyCode.LeftShift))
            {
                walkAudio.pitch += 0.1f;
                walkPlayCooldownTimer = walkPlayCooldownSprinting;
            }
            else
            {
                walkPlayCooldownTimer = walkPlayCooldown;
            }

            walkAudio.Play();
        }
        else
        {
            walkPlayCooldownTimer -= Time.deltaTime;
        }

        //Jump
        if(Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if(!controller.isGrounded) velocity.y += gravity * Time.deltaTime;

        controller.Move(move);
        controller.Move(velocity * Time.deltaTime);
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;
        
        xRotation -= mouseY * (sens * 100f);
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX * (sens * 100f));
    }
}
