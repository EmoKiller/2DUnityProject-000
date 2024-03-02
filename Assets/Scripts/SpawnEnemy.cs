using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SpawnEnemy : MonoBehaviour
{
    public enum SquadStatus
    {
        Square,
        Diamond,
        Triangle,
        Rectangle
    }
    [SerializeField]private SquadStatus _squadStatus;
    public SquadStatus Status
    {
        get { return _squadStatus; }
        set 
        { 
            ChangeSquad(value); 
        }
    }
    [SerializeField] List<Enemy> enemyList;
    [SerializeField] GameObject objnull;
    [SerializeField] List<Action> actionList = new List<Action>();
    [SerializeField] int height, width;
    [SerializeField] float spacing = -1.2f;
    [SerializeField] float center;
    Vector2 pivot = new Vector2 (-0.5f, -0.5f);
    [SerializeField] float radius;
    public List<Enemy> EnemyList { get { return enemyList; } }
    Coroutine currentCoroutine = null;
    private void Awake()
    {
        actionList.Add(Square);
        actionList.Add(Diamond);
        actionList.Add(Triangle);
        actionList.Add(Rectangle);
        GameManager.OnEndRound.AddListener(OnEndRound);
    }
    public void Init()
    {
        for (int i = 0; i < 16; i++)
        {
            GameObject obj = Instantiate(GameManager.Instance.EnemyObj(), transform);
            Enemy enemy = obj.GetComponent<Enemy>();
            enemyList.Add(enemy);
            enemy.SpawnRandom();
        }
        Status = SquadStatus.Square;
    }
    private void ChangeSquad(SquadStatus status)
    {
        if ((int)status > Enum.GetNames(typeof(SquadStatus)).Length - 1)
            _squadStatus = 0;
        else
            _squadStatus = status;
        actionList[(int)_squadStatus]?.Invoke();
        currentCoroutine = StartCoroutine(TimeChangeSquad());
    }
    /// <summary>
    /// 5 Time change Squad
    /// 3 Time moving of enemy
    /// </summary>
    /// <returns></returns>
    IEnumerator TimeChangeSquad()
    {
        yield return new WaitForSeconds(GameManager.Instance.TimeChangeSquad + GameManager.Instance.TimeMovingOfEnemy);
        Status += 1;
    }
    public void StopCurrentCorutine()
    {
        StopCoroutine(currentCoroutine);
    }
    private void OnEndRound()
    {
        foreach (var enemy in enemyList)
        {
            enemy.SpawnRandom();
        }
        Status = SquadStatus.Square;
    }
    private void Square()
    {
        int index = 0;
        width = 4;
        height = 4;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 spawnPos = SpawnPos(width / 2f, height / 2f, i, j);
                if (enemyList[index].Alive)
                    enemyList[index].MoveToPosition(spawnPos);
                index++;
            }
        }
    }
    private void Diamond()
    {
        int index = 0;
        
        height = 3;
        width = 4;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float y = -0.15f;
                if (i > 1)
                    y = 0.15f;
                Vector3 spawnPos = SpawnPos(width / 2f, height / 2f, i + y, j);
                if (enemyList[index].Alive)
                    enemyList[index].MoveToPosition(spawnPos);
                index++;
            }
        }
        height = 5;
        width = 7;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 spawnPos = SpawnPos(width / 2f, height / 2f, i, j);
                if (i == 3 && j % 4 ==0 || i % 6 == 0 && j == 2)
                {
                    if (enemyList[index].Alive)
                        enemyList[index].MoveToPosition(spawnPos);
                    index++;
                }
            }
        }
    }
    private void Triangle()
    {
        int index = 0;
        int x,y = 0;
        height = 5;
        for (int i = 0; i <= height; i++)
        {
            for (int j = 0; j < 2*height; j++)
            {
                x = j - height;
                if (x < 0)
                    x *= -1;
                y = i - x;
                if (y==1 || (i == height && j > 1))
                {
                    Vector3 spawnPos = transform.position - new Vector3(-height + pivot.x, -(height-1), 0) + new Vector3(j * spacing + pivot.x, i * spacing + pivot.y, 0);
                    if (enemyList[index].Alive)
                        enemyList[index].MoveToPosition(spawnPos);
                    index++;
                }
                
            }
        }
    }
    private void Rectangle()
    {
        int index = 0;
        width = 7;
        height = 3;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 spawnPos = SpawnPos(width / 2f, height / 2f,i,j);
                if ((i == 0 || j==0 || i == width-1 || j == height - 1))
                {
                    if (enemyList[index].Alive)
                        enemyList[index].MoveToPosition(spawnPos);
                    index++;
                }
            }
        }
    }
    private Vector3 SpawnPos(float width,float height,float i, float j) 
    {
        return transform.position + new Vector3(width , height , 0) + new Vector3(i * spacing + pivot.x, j * spacing + pivot.x, 0);
    }

}
