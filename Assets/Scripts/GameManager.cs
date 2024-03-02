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
    public static UnityEvent OnEndRound = new UnityEvent();
    [SerializeField] SpawnEnemy _SpawnEnemy;
    [SerializeField] TMP_Text _TextScore;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject buttlet;
    [SerializeField] int score = 0;
    [SerializeField] int timeMovingOfEnemy = 3;
    [SerializeField] int timeChangeSquad = 5;
    int TotalKillInRound = 0;
    public int TimeMovingOfEnemy { get { return timeMovingOfEnemy; } }
    public int TimeChangeSquad { get { return timeChangeSquad; } }
    private void OnDisable()
    {
        OnEnemyDead = null;
    }
    private void OnEnable()
    {
        score = 0;
        OnEnemyDead = new UnityEvent();
        OnEnemyDead.AddListener(EnemyDead);
        _SpawnEnemy.Init();
    }
    private void Awake()
    {
        Instance = this;
    }
    private void EnemyDead()
    {
        score++;
        TotalKillInRound++;
        _TextScore.text = "Score: " + score;
        if (TotalKillInRound == _SpawnEnemy.EnemyList.Count)
        {
            Debug.Log("EndRound");
            TotalKillInRound = 0;
            _SpawnEnemy.StopCurrentCorutine();
            OnEndRound?.Invoke();
        }
    }
    public GameObject ButtletObj() { return buttlet; }
    public GameObject EnemyObj() { return enemy; }
}
