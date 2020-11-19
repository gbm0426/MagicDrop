using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelectPointDown5 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject objPlayer;




    void Start()
    {

    }

    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject.Find("GameLogic").GetComponent<GameLogic>().BtnTamaSelect_5();

        GameObject.Find("Player").GetComponent<PlayerMoveController>().DragOn();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("드래그끝!");

        GameObject.Find("Player").GetComponent<PlayerMoveController>().DragOff();

        //GameLogic Script -> TamaDrop Play
        GameObject.Find("GameLogic").GetComponent<GameLogic>().TamaDrop();
    }
}
