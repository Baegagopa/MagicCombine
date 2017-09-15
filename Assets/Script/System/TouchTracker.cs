using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ITouchable
{
    bool IsUI { get; set; }
    void TouchEnter(Vector3 touchPos);
    void TouchStay(Vector3 touchPos);
    void TouchExit(Vector3 touchPos);
}

public enum Select_State
{
    NONE_SELECT,
    ENTER,
    STAY,
    EXIT,
}

public class TouchTracker : MonoBehaviour
{
    ITouchable curSelect;
    Select_State curSelect_State;

    private Touch tempTouchs;
    private Vector3 touchedPos;
    private RaycastHit hit3D;
    private RaycastHit2D hit2D;
    private bool touchOn;

    // Use this for initialization
    void Start()
    {
        touchOn = false;

        curSelect = null;
        curSelect_State = Select_State.NONE_SELECT;

    }

    // Update is called once per frame
    void Update()
    {
        TouchEventUpdate();
    }

    void TouchEventUpdate()
    {

        if (Input.GetMouseButton(0))
        {

            // UI 체크
            hit2D = Physics2D.Raycast(ScreenToWorldPoint(), Vector2.zero);
            if (curSelect_State == Select_State.NONE_SELECT && hit2D.collider != null)
            {
                //Debug.Log("UI : " + hit2D.transform.name);
                curSelect = hit2D.transform.GetComponent<ITouchable>();
                curSelect_State = Select_State.ENTER;

                if (curSelect.IsUI)
                {
                    curSelect.TouchEnter(ScreenToWorldPoint());
                    curSelect_State = Select_State.STAY;
                    return;
                }
                else
                {
                    curSelect = null;
                    curSelect_State = Select_State.NONE_SELECT;
                }
            }
            else if(curSelect_State == Select_State.STAY)
            {
                curSelect.TouchStay(ScreenToWorldPoint());
                return;
            }
                

            // 오브젝트 체크
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit3D))
            {
                Debug.Log("Object : " + hit3D.transform.name);
                curSelect = hit3D.transform.GetComponent<ITouchable>();
                if (!curSelect.IsUI)
                {
                    curSelect.TouchEnter(ScreenToWorldPoint());
                    curSelect_State = Select_State.STAY;
                    return;
                }
                else
                {
                    curSelect = null;
                    curSelect_State = Select_State.NONE_SELECT;
                }
            }
            else if (curSelect_State == Select_State.STAY)
            {
                curSelect.TouchStay(ScreenToWorldPoint());
                return;
            }

        }
        else
        {
            if (curSelect_State == Select_State.STAY)
            {
                curSelect.TouchExit(Input.mousePosition);
                curSelect = null;
                curSelect_State = Select_State.NONE_SELECT;
            }
        }

    }

    /*
    void TouchEventUpdate()
    {
#if UNITY_EDITOR

        if (Input.GetMouseButton(0))
        {
            Vector3 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // UI 체크
            hit2D = Physics2D.Raycast(ScreenToWorldPoint(), Vector2.zero);
            if (hit2D.collider != null)
            {
                Debug.Log("UI : " + hit2D.transform.name);
                curSelect = hit2D.transform.GetComponent<ITouchable>();
                
                if(curSelect.IsUI)
                {
                    TouchHandle();
                    return;
                }
            }


            // 오브젝트 체크
            
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit3D))
            {
                Debug.Log("Object : " + hit3D.transform.name);
                curSelect = hit3D.transform.GetComponent<ITouchable>();
                if (!curSelect.IsUI)
                {
                    TouchHandle();
                    return;
                }
            }
            
        }
        else
        {
            if(curSelect_State == Select_State.STAY)
            {
                curSelect.TouchExit(Input.mousePosition);
                curSelect = null;
                curSelect_State = Select_State.NONE_SELECT;
            }
            
        }

#else

        if (Input.touchCount > 0)
        {    //터치가 1개 이상이면.
            for (int i = 0; i < Input.touchCount; i++)
            {
                tempTouchs = Input.GetTouch(i);
                if (tempTouchs.phase == TouchPhase.Began)
                {    //해당 터치가 시작됐다면.
                    touchedPos = Camera.main.ScreenToWorldPoint(tempTouchs.position);//get world position.
                    touchOn = true;

                    break;   //한 프레임(update)에는 하나만.
                }
            }
        }
        else
        {
        touchedPos = Vector.zero;
        }
#endif
    }
    */
    void TouchHandle()
    {
        if (curSelect != null)
        {
            if (curSelect_State == Select_State.NONE_SELECT)
                curSelect_State = Select_State.ENTER;

            switch (curSelect_State)
            {
                case Select_State.ENTER:
                    curSelect.TouchEnter(ScreenToWorldPoint());
                    curSelect_State = Select_State.STAY;
                    break;
                case Select_State.STAY:
                    curSelect.TouchStay(ScreenToWorldPoint());
                    break;
            }
        }
    }
    

    Vector3 ScreenToWorldPoint()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
    }
}
