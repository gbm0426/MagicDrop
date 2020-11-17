using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    Vector3 currentPos;

    //Drop Number -> Drop Position    7=null(reset)
    public int dropNum;



    void Start()
    {
        dropNum = 7;

        currentPos = this.gameObject.transform.position;
    }

    void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if(pos.x < 0.05f)
        {
            pos.x = 0.05f;
        }
        else if(pos.x > 0.74f)
        {
            pos.x = 0.74f;
        }

        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    //Touch -> Drag -> Move
    void OnMouseDrag()
    {
        Debug.Log("드래그중!");
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = new Vector3(mousePosition.x, currentPos.y, 10);
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
        else if(other.gameObject.CompareTag("Drop_1"))
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
}
