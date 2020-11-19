using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMoveController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Vector3 currentPos;

    //Drop Number -> Drop Position    7=null(reset)
    public int dropNum;

    //Check Can Drag
    bool checkCanDrag;



    void Start()
    {
        dropNum = 7;

        currentPos = this.gameObject.transform.position;

        checkCanDrag = false;
    }

    void Update()
    {
        OnMouseDrag();
    }

    //Drag Change
    public void DragOn()
    {
        checkCanDrag = true;
    }

    public void DragOff()
    {
        checkCanDrag = false;
    }

    //Touch -> Drag -> Move
    public void OnMouseDrag()
    {
        if(checkCanDrag == true)
        {
            Debug.Log("드래그중!");
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            transform.position = new Vector3(mousePosition.x, currentPos.y, 10);

            //Drag Area
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

            if (pos.x < 0.05f)
            {
                pos.x = 0.05f;
            }
            else if (pos.x > 0.74f)
            {
                pos.x = 0.74f;
            }

            transform.position = Camera.main.ViewportToWorldPoint(pos);
        }
        else if(checkCanDrag == false)
        {

        }
    }

    //Drag End
    void OnMouseUp()
    {
        Debug.Log("드래그끝!");

        //GameLogic Script -> TamaDrop Play
        GameObject.Find("GameLogic").GetComponent<GameLogic>().TamaDrop();
    }

    //Player Drop Position Check
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Drop_0"))
        {
            dropNum = 0;
        }
        else if (other.gameObject.CompareTag("Drop_1"))
        {
            dropNum = 1;
        }
        else if (other.gameObject.CompareTag("Drop_2"))
        {
            dropNum = 2;
        }
        else if (other.gameObject.CompareTag("Drop_3"))
        {
            dropNum = 3;
        }
        else if (other.gameObject.CompareTag("Drop_4"))
        {
            dropNum = 4;
        }
        else if (other.gameObject.CompareTag("Drop_5"))
        {
            dropNum = 5;
        }
        else if (other.gameObject.CompareTag("Drop_6"))
        {
            dropNum = 6;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Click");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("ClickOff");
    }
}
