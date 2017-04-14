using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objTest : MonoBehaviour, ITouchable
{
    public bool IsUI { get { return false; } set { } }
    bool isDragging;
    Vector3 touchPos;

    private void Start()
    {
        isDragging = false;
        touchPos = Vector3.zero;
    }
    private void Update()
    {
        if (isDragging)
        {
            transform.position = touchPos;
        }
    }

    public void TouchEnter(Vector3 touchPos)
    {
        isDragging = true;
        this.touchPos = touchPos;
    }
    public void TouchStay(Vector3 touchPos)
    {
        this.touchPos = touchPos;
    }
    public void TouchExit(Vector3 touchPos)
    {
        isDragging = false;
    }
}