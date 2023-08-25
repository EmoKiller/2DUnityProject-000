using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControllerPC : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float smoothTime = 0.01f;
    [SerializeField] private float mutiRun = 1f;


    public float horizontal { get; set; }
    public float vertical { get; set; }
    public bool isAlive { get; private set; }

    private bool horizontalDown => horizontal != 0;
    private Vector2 refVelocity = Vector2.zero;
    private Vector2 targetVelocity = Vector2.zero;

    [SerializeField] private HeroAnimatorController ani = null;
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private JoyStickLManager joystickLManager = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponentInChildren<HeroAnimatorController>();
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
        if (Input.GetKey(KeyCode.J))
        {
            Debug.Log("ATK J Down");
        }
        if (Input.GetKey(KeyCode.K))
        {
            Debug.Log("DEF K Down");
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Roll Space Down");
        }
        
         mutiRun = Input.GetKey(KeyCode.LeftShift) && (horizontal != 0 || vertical != 0) ? 1.5f : 1f;

    }
    private void FixedUpdate()
    {
        Move();
        ani.SetAnimationMove(MathF.Abs(horizontal), MathF.Abs(vertical));
        
    }
    private void Move()
    {
        
        if (horizontalDown)
        {
            float eulerAngleY = horizontal < 0 ? 180 : 0;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, eulerAngleY, transform.eulerAngles.z);
        }
        
        targetVelocity = new Vector2(horizontal, vertical) * moveSpeed * mutiRun;
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref refVelocity, smoothTime);
    }
    
}
