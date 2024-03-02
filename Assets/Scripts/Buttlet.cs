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
    private void LateUpdate()
    {
        if (Vector3.Distance(Vector3.zero, transform.position) > 10)
            gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.OnHitButtlet();
            gameObject.SetActive(false);
        }
    }
}
