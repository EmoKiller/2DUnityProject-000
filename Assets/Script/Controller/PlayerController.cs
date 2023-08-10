using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float smoothTime = 0.01f;


    public float horizontal { get; private set; }
    public float vertical { get; private set; }
    public bool isAlive { get; private set; }

    private bool horizontalDown => horizontal != 0;
    private Vector2 refVelocity = Vector2.zero;
    private Vector2 targetVelocity = Vector2.zero;

    private Rigidbody2D rb = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        isAlive = true;
    }
    void Start()
    {
        
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
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
}
