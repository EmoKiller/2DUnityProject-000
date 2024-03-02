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
    private void Awake()
    {
        
    }
    private void Start()
    {
        for (int i = 0; i < 16; i++)
        {
            GameObject obj = Instantiate(GameManager.Instance.EnemyObj(), transform);
            Enemy enemy = obj.GetComponent<Enemy>();
            enemyList.Add(enemy);
            obj.transform.position = transform.position + new Vector3(UnityEngine.Random.Range(-10, 10), 10, 0);
        }
        actionList.Add(Square);
        actionList.Add(Diamond);
        actionList.Add(Triangle);
        actionList.Add(Rectangle);
        Status = SquadStatus.Square;
        Diamond();
    }
    
    private void ChangeSquad(SquadStatus status)
    {
        if ((int)status > Enum.GetNames(typeof(SquadStatus)).Length - 1)
            _squadStatus = 0;
        else
            _squadStatus = status;
        actionList[(int)_squadStatus]?.Invoke();
        StartCoroutine(TimeChangeSquad());
    }
    IEnumerator TimeChangeSquad()
    {
        yield return new WaitForSeconds(5);
        Status += 1;
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
        int x, y = 0;
        height = 5;
        for (int i = 0; i <= height ; i++)
        {
            for (int j = 0; j < 2 * height; j++)
            {
                x = j - height;
                if (x < 0)
                {
                    x *= -1;
                }
                y = i - x;
                if (y == 1)
                {
                    Vector3 spawnPos = transform.position - new Vector3(-height + pivot.x, -(height - 1), 0) + new Vector3(j * spacing + pivot.x, i * spacing + pivot.y, 0);
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
    private Vector3 SpawnPos(float width,float height,int i,int j) 
    {
        return transform.position + new Vector3(width , height , 0) + new Vector3(i * spacing + pivot.x, j * spacing + pivot.x, 0);
    }

}
