using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public static UnityEvent OnEnemyDead = new UnityEvent();
    [SerializeField] TMP_Text _TextScore;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject buttlet;
    int score = 0;
    private void OnDisable()
    {
        OnEnemyDead = null;
    }
    private void OnEnable()
    {
        score = 0;
        OnEnemyDead = new UnityEvent();
        OnEnemyDead.AddListener(EnemyDead);
    }
    private void Awake()
    {
        Instance = this;
    }
    private void EnemyDead()
    {
        score++;
        _TextScore.text = "Score: " + score;
    }
    public GameObject ButtletObj() { return buttlet; }
    public GameObject EnemyObj() { return enemy; }
}
