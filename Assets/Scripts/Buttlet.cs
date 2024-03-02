using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttlet : MonoBehaviour
{
    float speed = 10f;
    
    private void Update()
    {
        transform.position = transform.position + Vector3.up * Time.deltaTime * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.OnHitButtlet();
            Destroy(gameObject);
        }
    }
}
