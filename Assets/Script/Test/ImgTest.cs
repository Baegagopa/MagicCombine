using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class ImgTest : MonoBehaviour, ITouchable
{
    public bool IsUI { get { return true; } set {} }
    
    static float startTouchStayTime = 0.3f;
    bool canMove;
    bool isDragging;
    bool isTriggered;
    bool isSelected;

    Vector3 touchPos;
    Outline outline;
    private void Start()
    {
        AllStateOFF();
        
        touchPos = Vector3.zero;
        outline = GetComponent<Outline>();
        outline.effectDistance = new Vector2(5, 5);
        outline.effectColor = new Color(255, 0, 0);
        outline.enabled = false;
    }

    private void Update()
    {
        if (isTriggered && canMove)
        {
            transform.position = Vector3.Lerp(transform.position, touchPos, Time.deltaTime * 10f);
            //transform.position = touchPos;
        }
    }

    public void TouchEnter(Vector3 touchPos)
    {
        isSelected = true;
        if(!outline.enabled)
            StartCoroutine(DragCheck());
    }
    public void TouchStay(Vector3 touchPos)
    {
        if (canMove)
        {
            this.touchPos = touchPos;
            isTriggered = true;
        }
    }
    public void TouchExit(Vector3 touchPos)
    {
        if (!canMove)
        {
            outline.enabled = !outline.enabled;
            if (outline.enabled)
               NodeMgr.instance.InsertSelectNode(this);
            else
               NodeMgr.instance.RemoveSelectedNodes();
        }
            

        AllStateOFF();
    }

    public void OutlineOFF()
    {
        outline.enabled = false;
    }

    private void AllStateOFF()
    {
        isDragging = false;
        isTriggered = false;
        isSelected = false;
        canMove = false;
    }

    IEnumerator DragCheck()
    {
        float deltaTime = 0.0f;
        isDragging = true;
        while (isDragging)
        {
            deltaTime += Time.deltaTime;
            if(deltaTime > startTouchStayTime)
            {
                canMove = true;  
            }
            yield return null;
        }
        
    }
}
