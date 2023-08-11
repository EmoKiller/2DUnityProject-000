using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyStickLManager : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform joyStickL = null;
    public RectTransform handle = null;
    
    private void Awake()
    {
        
    }
    void Start()
    {
        joyStickL = GameObject.FindGameObjectWithTag("JoyStick").GetComponent<RectTransform>();
        handle = GameObject.FindGameObjectWithTag("JoyStickHandle").GetComponent<RectTransform>();
    }

    void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        
        Debug.Log("Inside Joystick L space");
        Debug.Log(eventData);
        joyStickL.transform.position = eventData.position;
        handle.transform.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        handle.transform.position = eventData.position;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        handle.anchoredPosition = new Vector2(0, 0);
    }
}
