using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    //Player
    public GameObject objPlayer;

    //TamaSelect -> Character Carry Tama Transform
    public Transform carryTamaPos;
    public GameObject parentTamaPos;

    //carryed tama number   0=red , 1=yellow , 2=blue , 3=green , 5=null(reset)
    public int carryNum;
    bool checkCarry;    //true=carry   , false=not carry

    //Drop Number -> Drop Position    7=null(reset)
    int receiveDropNum;

    //Drop
    public GameObject objDrop_0;
    public GameObject objDrop_1;
    public GameObject objDrop_2;
    public GameObject objDrop_3;
    public GameObject objDrop_4;
    public GameObject objDrop_5;
    public GameObject objDrop_6;

    //Time Count UI
    public Text textTimeCount;

    //Power UI
    float powerCount;
    float powerMaxCount;
    public Image imagePowerBar;
    public Text textPowerCount;

    //Skill
    bool checkSkill;          //  true=On,  false=Off
    public Button btnSkill;

    //GameStart & GameOver Popup
    public GameObject popupGameStart;
    public GameObject popupGameOver;

    //List
    public List<GameObject> TamaList = new List<GameObject>();
    public List<Transform> TamaRespawnList = new List<Transform>();
    public List<GameObject> TamaSpawnedList = new List<GameObject>();
    public List<int> TamaNumList = new List<int>();
    public List<GameObject> TamaCarryList = new List<GameObject>();
    //List




    void Awake()
    {
        ResetAll();
    }

    void Start()
    {
        StartCoroutine(TamaAutoDown());
        StartCoroutine(TamaPlus10sec());
    }

    void Update()
    {
        PowerCountCheck();
        SkillCheck();
        DieCheck();
    }

    //ResetAll                         (void Awake)
    public void ResetAll()
    {
        //Popup
        popupGameStart.gameObject.SetActive(true);
        popupGameOver.gameObject.SetActive(false);

        carryNum = 5;
        checkCarry = false;

        //Drop
        DropArrowAllFalse();

        //Power
        powerCount = 0;
        powerMaxCount = 10;

        //Skill
        checkSkill = false;
        btnSkill.interactable = false;

        TamaRespawn();

        //Pause
        Time.timeScale = 0;
    }

    //Respawn Tama Start               (void Start)
    void TamaRespawn()
    {
        for (int i = 63; i < 84; i++)
        {
            int z = (Random.Range(0, 4));

            Transform targetPoint = TamaRespawnList[i];
            GameObject newTama = Instantiate(TamaList[z]);
            newTama.transform.position = targetPoint.position;
            TamaSpawnedList[i] = newTama;
            TamaNumList[i] = z;
        }
    }

    //Tama Plus 10sec(Coroutine)
    IEnumerator TamaPlus10sec()
    {
        textTimeCount.text = 10.ToString();
        yield return new WaitForSeconds(1f);

        textTimeCount.text = 9.ToString();
        yield return new WaitForSeconds(1f);

        textTimeCount.text = 8.ToString();
        yield return new WaitForSeconds(1f);

        textTimeCount.text = 7.ToString();
        yield return new WaitForSeconds(1f);

        textTimeCount.text = 6.ToString();
        yield return new WaitForSeconds(1f);

        textTimeCount.text = 5.ToString();
        yield return new WaitForSeconds(1f);

        textTimeCount.text = 4.ToString();
        yield return new WaitForSeconds(1f);

        textTimeCount.text = 3.ToString();
        yield return new WaitForSeconds(1f);

        textTimeCount.text = 2.ToString();
        yield return new WaitForSeconds(1f);

        textTimeCount.text = 1.ToString();
        yield return new WaitForSeconds(1f);


        for (int i = 0; i < 7; i++)
        {
            int z = (Random.Range(0, 4));

            Transform targetPoint = TamaRespawnList[i];
            GameObject newTama = Instantiate(TamaList[z]);
            newTama.transform.position = targetPoint.position;
            TamaSpawnedList[i] = newTama;
            TamaNumList[i] = z;
        }

        yield return new WaitForSeconds(1f);
        StartCoroutine(TamaPlus10sec());
    }

    //Tama Auto Down(Coroutine)
    IEnumerator TamaAutoDown()
    {
        //Down
        for(int i = 83; i > -1; i--)
        {
            if(TamaNumList[i] == 5 && i > 6)
            {
                if(TamaNumList[i - 7] != 5)
                {
                    Transform targetPoint = TamaRespawnList[i];
                    GameObject newTama = Instantiate(TamaList[TamaNumList[i - 7]]);
                    newTama.transform.position = targetPoint.position;
                    TamaSpawnedList[i] = newTama;
                    TamaNumList[i] = TamaNumList[i - 7];

                    Destroy(TamaSpawnedList[i - 7]);
                    TamaNumList[i - 7] = 5;
                }
                else if(TamaNumList[i - 7] == 5)
                {
                    Debug.Log("Empty");
                }
            }
            else
            {

            }
        }

        ////Color Check (3Tama)
        //for(int z = 76; z > 6; z--)
        //{
        //    if (TamaNumList[z] != 5)
        //    {
        //        if(TamaNumList[z] == TamaNumList[z - 7] && TamaNumList[z] == TamaNumList[z + 7])
        //        {
        //            Destroy(TamaSpawnedList[z]);
        //            Destroy(TamaSpawnedList[z - 7]);
        //            Destroy(TamaSpawnedList[z + 7]);
        //            TamaNumList[z] = 5;
        //            TamaNumList[z - 7] = 5;
        //            TamaNumList[z + 7] = 5;

        //            powerCount = powerCount + 1.0f;
        //        }
        //    }
        //    else
        //    {

        //    }
        //}

        //Color Check (4Tama Vartical)
        for (int z = 76; z > 6; z--)
        {
            if (TamaNumList[z] != 5)
            {
                if (TamaNumList[z] == TamaNumList[z - 7] && TamaNumList[z] == TamaNumList[z + 7])
                {
                    if(z - 14 > 0 && z + 14 < 84)
                    {
                        if(TamaNumList[z] == TamaNumList[z - 14])
                        {
                            Destroy(TamaSpawnedList[z]);
                            Destroy(TamaSpawnedList[z - 7]);
                            Destroy(TamaSpawnedList[z + 7]);
                            Destroy(TamaSpawnedList[z - 14]);
                            TamaNumList[z] = 5;
                            TamaNumList[z - 7] = 5;
                            TamaNumList[z + 7] = 5;
                            TamaNumList[z - 14] = 5;

                            powerCount = powerCount + 1.0f;
                        }
                        else if(TamaNumList[z] != TamaNumList[z - 14])
                        {
                            if(TamaNumList[z] == TamaNumList[z + 14])
                            {
                                Destroy(TamaSpawnedList[z]);
                                Destroy(TamaSpawnedList[z - 7]);
                                Destroy(TamaSpawnedList[z + 7]);
                                Destroy(TamaSpawnedList[z + 14]);
                                TamaNumList[z] = 5;
                                TamaNumList[z - 7] = 5;
                                TamaNumList[z + 7] = 5;
                                TamaNumList[z + 14] = 5;

                                powerCount = powerCount + 1.0f;
                            }
                            else if(TamaNumList[z] != TamaNumList[z + 14])
                            {
                                //Nothing
                            }
                        }
                    }
                }
            }
            else
            {

            }
        }
        yield return new WaitForSeconds(0.2f);

        //Color Check (4Tama Horizontal)
        for(int i = 82; i > 0; i--)
        {
            if(TamaNumList[i] != 5)
            {
                if(TamaNumList[i] == TamaNumList[i - 1] && TamaNumList[i] == TamaNumList[i + 1])
                {
                    Destroy(TamaSpawnedList[i]);
                    Destroy(TamaSpawnedList[i - 1]);
                    Destroy(TamaSpawnedList[i + 1]);
                    TamaNumList[i] = 5;
                    TamaNumList[i - 1] = 5;
                    TamaNumList[i + 1] = 5;


                    powerCount = powerCount + 1.0f;
                }
                else
                {
                    //Nothing
                }
            }
            else
            {
                //Nothing
            }
        }
        yield return new WaitForSeconds(0.2f);

        StartCoroutine(TamaAutoDown());
    }

    //TamaSelect
    public void BtnTamaSelect_0()
    {
        if(checkCarry == true)
        {

        }
        else if(checkCarry == false)
        {
            if(TamaNumList[77] != 5)
            {
                //Arrow Reset
                DropArrowAllFalse();

                carryNum = TamaNumList[77];
                Destroy(TamaSpawnedList[77]);
                TamaNumList[77] = 5;

                //Move Player
                objPlayer.transform.position = new Vector3(-2.5f, -4.5f, 0);

                //Carry Tama -> Children add
                GameObject carryTama = Instantiate(TamaList[carryNum], carryTamaPos.position, Quaternion.identity);
                carryTama.transform.parent = parentTamaPos.transform;
                TamaCarryList[0] = carryTama;

                checkCarry = true;
            }
            else if(TamaNumList[77] == 5)
            {

            }
        }
    }

    public void BtnTamaSelect_1()
    {
        if (checkCarry == true)
        {

        }
        else if (checkCarry == false)
        {
            if(TamaNumList[78] != 5)
            {
                //Arrow Reset
                DropArrowAllFalse();

                carryNum = TamaNumList[78];
                Destroy(TamaSpawnedList[78]);
                TamaNumList[78] = 5;

                //Move Player
                objPlayer.transform.position = new Vector3(-1.87f, -4.5f, 0);

                //Carry Tama -> Children add
                GameObject carryTama = Instantiate(TamaList[carryNum], carryTamaPos.position, Quaternion.identity);
                carryTama.transform.parent = parentTamaPos.transform;
                TamaCarryList[0] = carryTama;

                checkCarry = true;
            }
            else if(TamaNumList[78] == 5)
            {

            }
        }
    }

    public void BtnTamaSelect_2()
    {
        if (checkCarry == true)
        {

        }
        else if (checkCarry == false)
        {
            if(TamaNumList[79] != 5)
            {
                //Arrow Reset
                DropArrowAllFalse();

                carryNum = TamaNumList[79];
                Destroy(TamaSpawnedList[79]);
                TamaNumList[79] = 5;

                //Move Player
                objPlayer.transform.position = new Vector3(-1.25f, -4.5f, 0);

                //Carry Tama -> Children add
                GameObject carryTama = Instantiate(TamaList[carryNum], carryTamaPos.position, Quaternion.identity);
                carryTama.transform.parent = parentTamaPos.transform;
                TamaCarryList[0] = carryTama;

                checkCarry = true;
            }
            else if(TamaNumList[79] == 5)
            {

            }
        }
    }

    public void BtnTamaSelect_3()
    {
        if (checkCarry == true)
        {

        }
        else if (checkCarry == false)
        {
            if(TamaNumList[80] != 5)
            {
                //Arrow Reset
                DropArrowAllFalse();

                carryNum = TamaNumList[80];
                Destroy(TamaSpawnedList[80]);
                TamaNumList[80] = 5;

                //Move Player
                objPlayer.transform.position = new Vector3(-0.6f, -4.5f, 0);

                //Carry Tama -> Children add
                GameObject carryTama = Instantiate(TamaList[carryNum], carryTamaPos.position, Quaternion.identity);
                carryTama.transform.parent = parentTamaPos.transform;
                TamaCarryList[0] = carryTama;

                checkCarry = true;
            }
            else if(TamaNumList[80] == 5)
            {

            }
        }
    }

    public void BtnTamaSelect_4()
    {
        if (checkCarry == true)
        {

        }
        else if (checkCarry == false)
        {
            if(TamaNumList[81] != 5)
            {
                //Arrow Reset
                DropArrowAllFalse();

                carryNum = TamaNumList[81];
                Destroy(TamaSpawnedList[81]);
                TamaNumList[81] = 5;

                //Move Player
                objPlayer.transform.position = new Vector3(0f, -4.5f, 0);

                //Carry Tama -> Children add
                GameObject carryTama = Instantiate(TamaList[carryNum], carryTamaPos.position, Quaternion.identity);
                carryTama.transform.parent = parentTamaPos.transform;
                TamaCarryList[0] = carryTama;

                checkCarry = true;
            }
            else if(TamaNumList[81] == 5)
            {

            }
        }
    }

    public void BtnTamaSelect_5()
    {
        if (checkCarry == true)
        {

        }
        else if (checkCarry == false)
        {
            if(TamaNumList[82] != 5)
            {
                //Arrow Reset
                DropArrowAllFalse();

                carryNum = TamaNumList[82];
                Destroy(TamaSpawnedList[82]);
                TamaNumList[82] = 5;

                //Move Player
                objPlayer.transform.position = new Vector3(0.6f, -4.5f, 0);

                //Carry Tama -> Children add
                GameObject carryTama = Instantiate(TamaList[carryNum], carryTamaPos.position, Quaternion.identity);
                carryTama.transform.parent = parentTamaPos.transform;
                TamaCarryList[0] = carryTama;

                checkCarry = true;
            }
            else if(TamaNumList[82] == 5)
            {

            }
        }
    }

    public void BtnTamaSelect_6()
    {
        if (checkCarry == true)
        {

        }
        else if (checkCarry == false)
        {
            if(TamaNumList[83] != 5)
            {
                //Arrow Reset
                DropArrowAllFalse();

                carryNum = TamaNumList[83];
                Destroy(TamaSpawnedList[83]);
                TamaNumList[83] = 5;

                //Move Player
                objPlayer.transform.position = new Vector3(1.25f, -4.5f, 0);

                //Carry Tama -> Children add
                GameObject carryTama = Instantiate(TamaList[carryNum], carryTamaPos.position, Quaternion.identity);
                carryTama.transform.parent = parentTamaPos.transform;
                TamaCarryList[0] = carryTama;

                checkCarry = true;
            }
            else if(TamaNumList[83] == 5)
            {

            }
        }
    }

    //Drop Arrow All False
    void DropArrowAllFalse()
    {
        objDrop_0.gameObject.SetActive(false);
        objDrop_1.gameObject.SetActive(false);
        objDrop_2.gameObject.SetActive(false);
        objDrop_3.gameObject.SetActive(false);
        objDrop_4.gameObject.SetActive(false);
        objDrop_5.gameObject.SetActive(false);
        objDrop_6.gameObject.SetActive(false);
    }

    ////TamaDropButton
    //public void BtnTamaDrop_0()
    //{
    //    if (checkCarry == true)
    //    {
    //        int i = 0;

    //        Destroy(TamaCarryList[0]);

    //        //Move Player
    //        objPlayer.transform.position = new Vector3(-2.5f, -4.5f, 0);

    //        //Drop Arrow
    //        objDrop_0.gameObject.SetActive(true);

    //        Transform targetPoint = TamaRespawnList[i];
    //        GameObject newTama = Instantiate(TamaList[carryNum]);
    //        newTama.transform.position = targetPoint.position;
    //        TamaSpawnedList[i] = newTama;
    //        TamaNumList[i] = carryNum;

    //        //reset
    //        carryNum = 5;
    //        checkCarry = false;
    //    }
    //    else if (checkCarry == false)
    //    {

    //    }
    //}

    //public void BtnTamaDrop_1()
    //{
    //    if (checkCarry == true)
    //    {
    //        int i = 1;

    //        Destroy(TamaCarryList[0]);

    //        //Move Player
    //        objPlayer.transform.position = new Vector3(-1.87f, -4.5f, 0);

    //        //Drop Arrow
    //        objDrop_0.gameObject.SetActive(true);

    //        Transform targetPoint = TamaRespawnList[i];
    //        GameObject newTama = Instantiate(TamaList[carryNum]);
    //        newTama.transform.position = targetPoint.position;
    //        TamaSpawnedList[i] = newTama;
    //        TamaNumList[i] = carryNum;

    //        //reset
    //        carryNum = 5;
    //        checkCarry = false;
    //    }
    //    else if (checkCarry == false)
    //    {

    //    }
    //}

    //public void BtnTamaDrop_2()
    //{
    //    if (checkCarry == true)
    //    {
    //        int i = 2;

    //        Destroy(TamaCarryList[0]);

    //        //Move Player
    //        objPlayer.transform.position = new Vector3(-1.25f, -4.5f, 0);

    //        //Drop Arrow
    //        objDrop_0.gameObject.SetActive(true);

    //        Transform targetPoint = TamaRespawnList[i];
    //        GameObject newTama = Instantiate(TamaList[carryNum]);
    //        newTama.transform.position = targetPoint.position;
    //        TamaSpawnedList[i] = newTama;
    //        TamaNumList[i] = carryNum;

    //        //reset
    //        carryNum = 5;
    //        checkCarry = false;
    //    }
    //    else if (checkCarry == false)
    //    {

    //    }
    //}

    //public void BtnTamaDrop_3()
    //{
    //    if (checkCarry == true)
    //    {
    //        int i = 3;

    //        Destroy(TamaCarryList[0]);

    //        //Move Player
    //        objPlayer.transform.position = new Vector3(-0.6f, -4.5f, 0);

    //        //Drop Arrow
    //        objDrop_0.gameObject.SetActive(true);

    //        Transform targetPoint = TamaRespawnList[i];
    //        GameObject newTama = Instantiate(TamaList[carryNum]);
    //        newTama.transform.position = targetPoint.position;
    //        TamaSpawnedList[i] = newTama;
    //        TamaNumList[i] = carryNum;

    //        //reset
    //        carryNum = 5;
    //        checkCarry = false;
    //    }
    //    else if (checkCarry == false)
    //    {

    //    }
    //}

    //public void BtnTamaDrop_4()
    //{
    //    if (checkCarry == true)
    //    {
    //        int i = 4;

    //        Destroy(TamaCarryList[0]);

    //        //Move Player
    //        objPlayer.transform.position = new Vector3(0f, -4.5f, 0);

    //        //Drop Arrow
    //        objDrop_0.gameObject.SetActive(true);

    //        Transform targetPoint = TamaRespawnList[i];
    //        GameObject newTama = Instantiate(TamaList[carryNum]);
    //        newTama.transform.position = targetPoint.position;
    //        TamaSpawnedList[i] = newTama;
    //        TamaNumList[i] = carryNum;

    //        //reset
    //        carryNum = 5;
    //        checkCarry = false;
    //    }
    //    else if (checkCarry == false)
    //    {

    //    }
    //}

    //public void BtnTamaDrop_5()
    //{
    //    if (checkCarry == true)
    //    {
    //        int i = 5;

    //        Destroy(TamaCarryList[0]);

    //        //Move Player
    //        objPlayer.transform.position = new Vector3(0.6f, -4.5f, 0);

    //        //Drop Arrow
    //        objDrop_0.gameObject.SetActive(true);

    //        Transform targetPoint = TamaRespawnList[i];
    //        GameObject newTama = Instantiate(TamaList[carryNum]);
    //        newTama.transform.position = targetPoint.position;
    //        TamaSpawnedList[i] = newTama;
    //        TamaNumList[i] = carryNum;

    //        //reset
    //        carryNum = 5;
    //        checkCarry = false;
    //    }
    //    else if (checkCarry == false)
    //    {

    //    }
    //}

    //public void BtnTamaDrop_6()
    //{
    //    if (checkCarry == true)
    //    {
    //        int i = 6;

    //        Destroy(TamaCarryList[0]);

    //        //Move Player
    //        objPlayer.transform.position = new Vector3(1.25f, -4.5f, 0);

    //        //Drop Arrow
    //        objDrop_0.gameObject.SetActive(true);

    //        Transform targetPoint = TamaRespawnList[i];
    //        GameObject newTama = Instantiate(TamaList[carryNum]);
    //        newTama.transform.position = targetPoint.position;
    //        TamaSpawnedList[i] = newTama;
    //        TamaNumList[i] = carryNum;

    //        //reset
    //        carryNum = 5;
    //        checkCarry = false;
    //    }
    //    else if (checkCarry == false)
    //    {

    //    }
    //}

    //Drag End -> Tama Drop

    public void TamaDrop()
    {
        if (checkCarry == true)
        {
            PlayerMoveController pmc = GameObject.Find("Player").GetComponent<PlayerMoveController>();
            receiveDropNum = pmc.dropNum;

            Destroy(TamaCarryList[0]);

            //Drop Arrow
            switch(receiveDropNum)
            {
                case 0:
                    objDrop_0.gameObject.SetActive(true);
                    break;
                case 1:
                    objDrop_1.gameObject.SetActive(true);
                    break;
                case 2:
                    objDrop_2.gameObject.SetActive(true);
                    break;
                case 3:
                    objDrop_3.gameObject.SetActive(true);
                    break;
                case 4:
                    objDrop_4.gameObject.SetActive(true);
                    break;
                case 5:
                    objDrop_5.gameObject.SetActive(true);
                    break;
                case 6:
                    objDrop_6.gameObject.SetActive(true);
                    break;
            }

            Transform targetPoint = TamaRespawnList[receiveDropNum];
            GameObject newTama = Instantiate(TamaList[carryNum]);
            newTama.transform.position = targetPoint.position;
            TamaSpawnedList[receiveDropNum] = newTama;
            TamaNumList[receiveDropNum] = carryNum;

            //reset
            carryNum = 5;
            checkCarry = false;
        }
        else if (checkCarry == false)
        {

        }
    }

    //PowerCountCheck                 (void Update)

    void PowerCountCheck()
    {
        if(powerCount >= powerMaxCount)
        {
            if(checkSkill == true)
            {
                powerCount = powerMaxCount;
            }
            else if(checkSkill == false)
            {
                checkSkill = true;

                powerCount = 0;
            }
        }

        textPowerCount.text = powerCount.ToString() + " / " + powerMaxCount.ToString();

        imagePowerBar.fillAmount = powerCount / powerMaxCount;
    }

    //Skill Check                    (void Update)
    void SkillCheck()
    {
        if(checkSkill == true)
        {
            btnSkill.interactable = true;
        }
        else if(checkSkill == false)
        {
            btnSkill.interactable = false;
        }
    }

    //Skill BTN
    public void BtnSkillOn()
    {
        checkSkill = false;

        Skill();
    }

    //Skill Active
    void Skill()
    {
        Debug.Log("SKILL");

        int i = 0;
        while(i < 84)
        {
            if(TamaNumList[i] == 5)
            {
                i++;
            }
            else if(TamaNumList[i] != 5)
            {
                Debug.Log("i : " + i);
                break;
            }
        }

        while(i < 84)
        {
            if(i < 84)
            {
                Destroy(TamaSpawnedList[i]);
                TamaNumList[i] = 5;

                i = i + 7;
            }
            if(i > 83)
            {
                break;
            }
        }
    }

    //Die Check                       (void Update)
    void DieCheck()
    {
        if(TamaNumList[0] != 5)
        {
            if(TamaNumList[7] != 5 && TamaNumList[0] != TamaNumList[7])
            {
                Die();
            }
            else
            {

            }
        }
        else if(TamaNumList[1] != 5)
        {
            if (TamaNumList[8] != 5 && TamaNumList[1] != TamaNumList[8])
            {
                Die();
            }
            else
            {

            }
        }
        else if (TamaNumList[2] != 5)
        {
            if (TamaNumList[9] != 5 && TamaNumList[2] != TamaNumList[9])
            {
                Die();
            }
            else
            {

            }
        }
        else if (TamaNumList[3] != 5)
        {
            if (TamaNumList[10] != 5 && TamaNumList[3] != TamaNumList[10])
            {
                Die();
            }
            else
            {

            }
        }
        else if (TamaNumList[4] != 5)
        {
            if (TamaNumList[11] != 5 && TamaNumList[4] != TamaNumList[11])
            {
                Die();
            }
            else
            {

            }
        }
        else if (TamaNumList[5] != 5)
        {
            if (TamaNumList[12] != 5 && TamaNumList[5] != TamaNumList[12])
            {
                Die();
            }
            else
            {

            }
        }
        else if (TamaNumList[6] != 5)
        {
            if (TamaNumList[13] != 5 && TamaNumList[6] != TamaNumList[13])
            {
                Die();
            }
            else
            {

            }
        }
    }

    //Die
    void Die()
    {
        Time.timeScale = 0;
        popupGameOver.gameObject.SetActive(true);
    }

    //Button
    public void BtnGameStart()
    {
        Time.timeScale = 1;
        popupGameStart.gameObject.SetActive(false);
    }


}
