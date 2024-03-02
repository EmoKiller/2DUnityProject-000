using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    BoxCollider2D boxCollider;
    float health = 5;
    public bool Alive => health > 0;
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
    }
    private void Start()
    {
        Debug.Log(Alive);
    }
    public void MoveToPosition(Vector3 vec)
    {
        SetBoxCollider(false);
        transform.DOMove(vec, 3).OnComplete(() =>
        { 
            SetBoxCollider(true); 
        });
    }
    public void OnHitButtlet()
    {
        health -= 1;
        if (health <= 0)
        {
            gameObject.SetActive(false);
            GameManager.OnEnemyDead?.Invoke();
        }
    }
    public void SetBoxCollider(bool value)
    {
        boxCollider.enabled = value;
    }
}
