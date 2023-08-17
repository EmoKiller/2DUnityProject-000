using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControllerPC : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float smoothTime = 0.01f;



    public float horizontal { get; set; }
    public float vertical { get; set; }
    public bool isAlive { get; private set; }

    private bool horizontalDown => horizontal != 0;
    private Vector2 refVelocity = Vector2.zero;
    private Vector2 targetVelocity = Vector2.zero;

    [SerializeField] private Animator ani = null;
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private JoyStickLManager joystickLManager = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponentInChildren<Animator>();
        joystickLManager = GameObject.FindGameObjectWithTag("JoyStickManager").GetComponent<JoyStickLManager>();
        isAlive = true;
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (!joystickLManager.joystickMove)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }

    }
    private void FixedUpdate()
    {
        SetAnimationMove(MathF.Abs(horizontal), MathF.Abs(vertical));
        Move();
    }
    private void Move()
    {
        
        if (horizontalDown)
        {
            float eulerAngleY = horizontal < 0 ? 180 : 0;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, eulerAngleY, transform.eulerAngles.z);
        }
        targetVelocity = new Vector2(horizontal, vertical) * moveSpeed;
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref refVelocity, smoothTime);
    }
    private void SetAnimationMove(float horizontal, float Vertical)
    {
        if (horizontal != 0 || Vertical != 0)
        {
            ani.SetInteger("State", 1);
        }
        else
        {
            ani.SetInteger("State", 0);
        }
    }
}
