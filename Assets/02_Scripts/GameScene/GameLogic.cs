using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    //Tama
    public float tamaSpeed;
    int carryNum;
    int selectNum;
    int receiveDropNum;
    bool checkCarry;      //true=carry   , false=not carry
    bool checkSelectTamaDelete;         //true=Delete   , false=not Delete

    //List
    public List<Transform> TamaTransformList = new List<Transform>();
    public List<GameObject> TamaKindList = new List<GameObject>();
    public List<GameObject> TamaSelectKindList = new List<GameObject>();
    public List<GameObject> TamaSpawnedList = new List<GameObject>();
    public List<GameObject> TamaCarryList = new List<GameObject>();
    public List<int> TamaNumList = new List<int>();
    //List



    void Awake()
    {
        ResetAll();
    }

    void Start()
    {
        TamaStart();
        StartCoroutine(TamaMove());
    }

    void Update()
    {
        
    }

    //ResetAll          (void Awake)
    void ResetAll()
    {
        tamaSpeed = 0.4f;

        carryNum = 10;
        selectNum = 10;
        receiveDropNum = 7;
        checkCarry = false;
        checkSelectTamaDelete = true;
    }

    //ゲームが始まるとき、下端部の3つの列に生じる玉      (void Start)
    void TamaStart()
    {
        for(int i = 0; i < 21; i++)
        {
            int z = (Random.Range(0, 4));

            Transform targetPoint = TamaTransformList[i];
            GameObject newTama = Instantiate(TamaKindList[z]);
            newTama.transform.position = targetPoint.position;
            TamaSpawnedList[i] = newTama;
            TamaNumList[i] = z;
        }
    }

    //TamaSelectButton (OnpointerDown)
    public void BtnOnpointer_0()
    {
        if(TamaNumList[0] != 10)
        {
            carryNum = TamaNumList[0];
            selectNum = 0;
            checkCarry = true;

            //OnMouseDrag
            OnMouseDrag();
        }
        else
        {

        }
    }

    public void BtnOnpointer_1()
    {
        if (TamaNumList[1] != 10)
        {
            carryNum = TamaNumList[1];
            selectNum = 1;
            checkCarry = true;

            //OnMouseDrag
            OnMouseDrag();
        }
        else
        {

        }
    }

    public void BtnOnpointer_2()
    {
        if (TamaNumList[2] != 10)
        {
            carryNum = TamaNumList[2];
            selectNum = 2;
            checkCarry = true;

            //OnMouseDrag
            OnMouseDrag();
        }
        else
        {

        }
    }

    public void BtnOnpointer_3()
    {
        if (TamaNumList[3] != 10)
        {
            carryNum = TamaNumList[3];
            selectNum = 3;
            checkCarry = true;

            //OnMouseDrag
            OnMouseDrag();
        }
        else
        {

        }
    }

    public void BtnOnpointer_4()
    {
        if (TamaNumList[4] != 10)
        {
            carryNum = TamaNumList[4];
            selectNum = 4;
            checkCarry = true;

            //OnMouseDrag
            OnMouseDrag();
        }
        else
        {

        }
    }

    public void BtnOnpointer_5()
    {
        if (TamaNumList[5] != 10)
        {
            carryNum = TamaNumList[5];
            selectNum = 5;
            checkCarry = true;

            //OnMouseDrag
            OnMouseDrag();
        }
        else
        {

        }
    }

    public void BtnOnpointer_6()
    {
        if (TamaNumList[6] != 10)
        {
            carryNum = TamaNumList[6];
            selectNum = 6;
            checkCarry = true;

            //OnMouseDrag
            OnMouseDrag();
        }
        else
        {

        }
    }

    //Drag
    public void OnMouseDrag()
    {
        if(checkCarry == true)
        {
            switch(selectNum)
            {
                case 0:
                    //New Tama Create
                    GameObject carryTama0 = Instantiate(TamaSelectKindList[carryNum], new Vector3(-2.4f, -2.9f, 0), Quaternion.identity);
                    TamaCarryList[0] = carryTama0;
                    break;
                case 1:
                    //New Tama Create
                    GameObject carryTama1 = Instantiate(TamaSelectKindList[carryNum], new Vector3(-1.6f, -2.9f, 0), Quaternion.identity);
                    TamaCarryList[0] = carryTama1;
                    break;
                case 2:
                    //New Tama Create
                    GameObject carryTama2 = Instantiate(TamaSelectKindList[carryNum], new Vector3(-0.8f, -2.9f, 0), Quaternion.identity);
                    TamaCarryList[0] = carryTama2;
                    break;
                case 3:
                    //New Tama Create
                    GameObject carryTama3 = Instantiate(TamaSelectKindList[carryNum], new Vector3(0.0f, -2.9f, 0), Quaternion.identity);
                    TamaCarryList[0] = carryTama3;
                    break;
                case 4:
                    //New Tama Create
                    GameObject carryTama4 = Instantiate(TamaSelectKindList[carryNum], new Vector3(0.8f, -2.9f, 0), Quaternion.identity);
                    TamaCarryList[0] = carryTama4;
                    break;
                case 5:
                    //New Tama Create
                    GameObject carryTama5 = Instantiate(TamaSelectKindList[carryNum], new Vector3(1.6f, -2.9f, 0), Quaternion.identity);
                    TamaCarryList[0] = carryTama5;
                    break;
                case 6:
                    //New Tama Create
                    GameObject carryTama6 = Instantiate(TamaSelectKindList[carryNum], new Vector3(2.4f, -2.9f, 0), Quaternion.identity);
                    TamaCarryList[0] = carryTama6;
                    break;
            }
        }
        else
        {

        }
    }

    //下端部の選んだ玉を消す
    public void SelectTamaDelete()
    {
        if(checkSelectTamaDelete == true)
        {
            switch (selectNum)
            {
                case 0:
                    Destroy(TamaSpawnedList[0]);
                    TamaNumList[0] = 10;
                    checkSelectTamaDelete = false;
                    break;
                case 1:
                    Destroy(TamaSpawnedList[1]);
                    TamaNumList[1] = 10;
                    checkSelectTamaDelete = false;
                    break;
                case 2:
                    Destroy(TamaSpawnedList[2]);
                    TamaNumList[2] = 10;
                    checkSelectTamaDelete = false;
                    break;
                case 3:
                    Destroy(TamaSpawnedList[3]);
                    TamaNumList[3] = 10;
                    checkSelectTamaDelete = false;
                    break;
                case 4:
                    Destroy(TamaSpawnedList[4]);
                    TamaNumList[4] = 10;
                    checkSelectTamaDelete = false;
                    break;
                case 5:
                    Destroy(TamaSpawnedList[5]);
                    TamaNumList[5] = 10;
                    checkSelectTamaDelete = false;
                    break;
                case 6:
                    Destroy(TamaSpawnedList[6]);
                    TamaNumList[6] = 10;
                    checkSelectTamaDelete = false;
                    break;
            }
        }
        else
        {
            //Don't Delete
        }
    }

    //TamaDrop
    public void TamaDrop()
    {
        if(checkCarry == true)
        {
            if(TamaNumList[70] == 10 && TamaNumList[71] == 10 && TamaNumList[72] == 10 && TamaNumList[73] == 10 && TamaNumList[74] == 10 && TamaNumList[75] == 10 && TamaNumList[76] == 10)
            {
                PlayerLogic playerlogic = GameObject.Find("Player").GetComponent<PlayerLogic>();
                receiveDropNum = playerlogic.dropNum;

                switch(receiveDropNum)
                {
                    case 0:
                        receiveDropNum = 70;
                        break;
                    case 1:
                        receiveDropNum = 71;
                        break;
                    case 2:
                        receiveDropNum = 72;
                        break;
                    case 3:
                        receiveDropNum = 73;
                        break;
                    case 4:
                        receiveDropNum = 74;
                        break;
                    case 5:
                        receiveDropNum = 75;
                        break;
                    case 6:
                        receiveDropNum = 76;
                        break;
                    case 7:
                        break;
                }

                Transform targetPoint = TamaTransformList[receiveDropNum];
                GameObject newTama = Instantiate(TamaKindList[carryNum]);
                newTama.transform.position = targetPoint.position;
                TamaSpawnedList[receiveDropNum] = newTama;
                TamaNumList[receiveDropNum] = carryNum;
            }
            else
            {
                if (TamaNumList[77] == 10 && TamaNumList[78] == 10 && TamaNumList[79] == 10 && TamaNumList[80] == 10 && TamaNumList[81] == 10 && TamaNumList[82] == 10 && TamaNumList[83] == 10)
                {
                    PlayerLogic playerlogic = GameObject.Find("Player").GetComponent<PlayerLogic>();
                    receiveDropNum = playerlogic.dropNum;

                    switch (receiveDropNum)
                    {
                        case 0:
                            receiveDropNum = 77;
                            break;
                        case 1:
                            receiveDropNum = 78;
                            break;
                        case 2:
                            receiveDropNum = 79;
                            break;
                        case 3:
                            receiveDropNum = 80;
                            break;
                        case 4:
                            receiveDropNum = 81;
                            break;
                        case 5:
                            receiveDropNum = 82;
                            break;
                        case 6:
                            receiveDropNum = 83;
                            break;
                        case 7:
                            break;
                    }

                    Transform targetPoint = TamaTransformList[receiveDropNum];
                    GameObject newTama = Instantiate(TamaKindList[carryNum]);
                    newTama.transform.position = targetPoint.position;
                    TamaSpawnedList[receiveDropNum] = newTama;
                    TamaNumList[receiveDropNum] = carryNum;
                }
            }

            GameObject.Find("Player").GetComponent<PlayerLogic>().DragOff();
            GameObject.Find("Player").GetComponent<PlayerLogic>().ResetAll();
            Destroy(TamaCarryList[0]);
            carryNum = 10;
            selectNum = 10;
            receiveDropNum = 7;
            checkCarry = false;
            checkSelectTamaDelete = true;
        }
        else
        {

        }
    }

    //TamaMove Coroutone                 (void Start)
    IEnumerator TamaMove()
    {
        for (int i = 0; i < 77; i++)
        {
            if(TamaNumList[i] == 10)
            {
                if(TamaNumList[i + 7] != 10)
                {
                    Transform targetPoint = TamaTransformList[i];
                    GameObject newTama = Instantiate(TamaKindList[TamaNumList[i + 7]]);
                    newTama.transform.position = targetPoint.position;
                    TamaSpawnedList[i] = newTama;
                    TamaNumList[i] = TamaNumList[i + 7];

                    Destroy(TamaSpawnedList[i + 7]);
                    TamaNumList[i + 7] = 10;
                }
                else
                {
                    //Don't Move
                }
            }
            else
            {
                //Don't Move
            }
        }

        yield return new WaitForSeconds(tamaSpeed);

        //Tama Clear
        for(int a = 0; a < 4; a++)
        {
            //1列
            if(TamaNumList[a] != 10 && TamaNumList[a] == TamaNumList[a + 1] && TamaNumList[a] == TamaNumList[a + 2] && TamaNumList[a] == TamaNumList[a + 3])
            {
                if(TamaNumList[a] == TamaNumList[a + 4] && TamaNumList[a + 4] < 7)
                {
                    if(TamaNumList[a] == TamaNumList[a + 5] && TamaNumList[a + 5] < 7)
                    {
                        if(TamaNumList[a] == TamaNumList[a + 6] && TamaNumList[a + 6] < 7)
                        {
                            //7個消す
                            Destroy(TamaSpawnedList[a]);
                            Destroy(TamaSpawnedList[a + 1]);
                            Destroy(TamaSpawnedList[a + 2]);
                            Destroy(TamaSpawnedList[a + 3]);
                            Destroy(TamaSpawnedList[a + 4]);
                            Destroy(TamaSpawnedList[a + 5]);
                            Destroy(TamaSpawnedList[a + 6]);
                            TamaNumList[a] = 10;
                            TamaNumList[a + 1] = 10;
                            TamaNumList[a + 2] = 10;
                            TamaNumList[a + 3] = 10;
                            TamaNumList[a + 4] = 10;
                            TamaNumList[a + 5] = 10;
                            TamaNumList[a + 6] = 10;
                        }
                        else
                        {
                            //6個消す
                            Destroy(TamaSpawnedList[a]);
                            Destroy(TamaSpawnedList[a + 1]);
                            Destroy(TamaSpawnedList[a + 2]);
                            Destroy(TamaSpawnedList[a + 3]);
                            Destroy(TamaSpawnedList[a + 4]);
                            Destroy(TamaSpawnedList[a + 5]);
                            TamaNumList[a] = 10;
                            TamaNumList[a + 1] = 10;
                            TamaNumList[a + 2] = 10;
                            TamaNumList[a + 3] = 10;
                            TamaNumList[a + 4] = 10;
                            TamaNumList[a + 5] = 10;
                        }
                    }
                    else
                    {
                        //5個消す
                        Destroy(TamaSpawnedList[a]);
                        Destroy(TamaSpawnedList[a + 1]);
                        Destroy(TamaSpawnedList[a + 2]);
                        Destroy(TamaSpawnedList[a + 3]);
                        Destroy(TamaSpawnedList[a + 4]);
                        TamaNumList[a] = 10;
                        TamaNumList[a + 1] = 10;
                        TamaNumList[a + 2] = 10;
                        TamaNumList[a + 3] = 10;
                        TamaNumList[a + 4] = 10;
                    }
                }
                else
                {
                    //4個消す
                    Destroy(TamaSpawnedList[a]);
                    Destroy(TamaSpawnedList[a + 1]);
                    Destroy(TamaSpawnedList[a + 2]);
                    Destroy(TamaSpawnedList[a + 3]);
                    TamaNumList[a] = 10;
                    TamaNumList[a + 1] = 10;
                    TamaNumList[a + 2] = 10;
                    TamaNumList[a + 3] = 10;
                }
            }
        }
        

        StartCoroutine(TamaMove());
    }
}
