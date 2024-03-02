using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] GameObject buttlet;
    [SerializeField] List<Buttlet> poolButtlet = new List<Buttlet>();
    [SerializeField] private float horizontal => Input.GetAxis("Horizontal");
    [SerializeField] private float vertical => Input.GetAxis("Vertical");
    [SerializeField] float speed = 10.0f;
    [SerializeField] float timeSpawnButtlet = 0.1f;
    Vector2 position => transform.position;

    private void Start()
    {
        StartCoroutine(Buttlet());
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        rb2d.MovePosition(position + new Vector2(horizontal, vertical) * Time.deltaTime * speed);
    }
    IEnumerator Buttlet()
    {
        while (true)
        {
            Buttlet buttletInPool = poolButtlet.Find(e => e.gameObject.activeSelf == false);
            if(buttletInPool == null)
            {
                GameObject obj = Instantiate(GameManager.Instance.ButtletObj(), transform.position + transform.transform.up, transform.rotation);
                Buttlet buttletCpn = obj.GetComponent<Buttlet>();
                poolButtlet.Add(buttletCpn);
            }
            else
            {
                buttletInPool.transform.position = transform.position;
                buttletInPool.gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(timeSpawnButtlet);
        }
    }
}
