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

    //List
    public List<GameObject> TamaList = new List<GameObject>();
    public List<Transform> TamaRespawnList = new List<Transform>();
    public List<GameObject> TamaSpawnedList = new List<GameObject>();
    public List<int> TamaNumList = new List<int>();
    public List<GameObject> TamaCarryList = new List<GameObject>();






    void Start()
    {
        ResetAll();

        StartCoroutine(TamaAutoDown());
        StartCoroutine(TamaPlus10sec());
    }

    void Update()
    {
        PowerCountCheck();
        SkillCheck();
        DieCheck();
    }

    //ResetAll                         (void Start)
    void ResetAll()
    {
        carryNum = 5;
        checkCarry = false;

        //Power
        powerCount = 0;
        powerMaxCount = 10;

        //Skill
        checkSkill = false;
        btnSkill.interactable = false;

        TamaRespawn();
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

        //Color Check
        for(int z = 76; z > 6; z--)
        {
            if (TamaNumList[z] != 5)
            {
                if(TamaNumList[z] == TamaNumList[z - 7] && TamaNumList[z] == TamaNumList[z + 7])
                {
                    Destroy(TamaSpawnedList[z]);
                    Destroy(TamaSpawnedList[z - 7]);
                    Destroy(TamaSpawnedList[z + 7]);
                    TamaNumList[z] = 5;
                    TamaNumList[z - 7] = 5;
                    TamaNumList[z + 7] = 5;

                    powerCount = powerCount + 1.0f;
                }
            }
            else
            {

            }
        }
        yield return new WaitForSeconds(0.2f);

        StartCoroutine(TamaAutoDown());
    }

    //TamaSelect
    public void BtnTamaSelect_0()
    {
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

    public void BtnTamaSelect_1()
    {
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

    public void BtnTamaSelect_2()
    {
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

    public void BtnTamaSelect_3()
    {
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

    public void BtnTamaSelect_4()
    {
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

    public void BtnTamaSelect_5()
    {
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

    public void BtnTamaSelect_6()
    {
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

    //Drag End -> Tama Drop
    public void TamaDrop()
    {
        if(checkCarry == true)
        {
            PlayerMoveController pmc = GameObject.Find("Player").GetComponent<PlayerMoveController>();
            receiveDropNum = pmc.dropNum;

            Destroy(TamaCarryList[0]);

            Transform targetPoint = TamaRespawnList[receiveDropNum];
            GameObject newTama = Instantiate(TamaList[carryNum]);
            newTama.transform.position = targetPoint.position;
            TamaSpawnedList[receiveDropNum] = newTama;
            TamaNumList[receiveDropNum] = carryNum;

            //reset
            carryNum = 5;
            checkCarry = false;   
        }
        else if(checkCarry == false)
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
        if(TamaNumList[0] != 5 || TamaNumList[1] != 5 || TamaNumList[2] != 5 || TamaNumList[3] != 5 || TamaNumList[4] != 5 || TamaNumList[5] != 5 || TamaNumList[6] != 5)
        {
            Die();
        }
    }

    //Die
    void Die()
    {
        Debug.Log("Die");
        Time.timeScale = 0;
    }
}
