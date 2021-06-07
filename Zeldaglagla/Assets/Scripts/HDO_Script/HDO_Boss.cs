using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters;

public class HDO_Boss : MonsterRoot
{
    public GameObject Shield;
    public List<GameObject> Allies = null;

    public float callSecAlly, callThirdAlly;
    public GameObject SpawnAlly;
    bool fightStarted;
    int counter;

    public bool shieldOn;

    [SerializeField]
    GameObject currentAlly;
    [SerializeField]
    MonsterRoot allyRoot;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        player = GameObject.FindGameObjectWithTag("Player");

        CallBackUp();
        fightStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (fightStarted)
        {
            if (allyRoot.dead)
            {
                currentAlly = null;
                shieldOn = false;
            }
        }*/
        
        if(currentHP <= callSecAlly && counter == 0)
        {
            counter = 1;
            CallBackUp();
        }
        else if(currentHP <= callThirdAlly && counter == 1)
        {
            counter = 2;
            CallBackUp();
        }

        Shielding();
    }

    void AllyDead()
    {
        if (fightStarted)
        {
            currentAlly = null;
            shieldOn = false;
        }
    }

    public override void Death()
    {

        Debug.Log("boss defeated");
        base.Death();


    }


    void Shielding()
    {
        Shield.SetActive(shieldOn);
    }

    void CallBackUp()
    {
        shieldOn = true;
        currentAlly = Instantiate(Allies[counter], SpawnAlly.transform.position, Quaternion.identity);

        if (currentAlly.GetComponent<PackManager>())
        {
            currentAlly = currentAlly.transform.GetChild(0).gameObject;
        }
        allyRoot = currentAlly.GetComponent<MonsterRoot>();
        player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(player.transform.position.x, player.transform.position.y - 10, 0), 3);
        allyRoot.diedEvent.AddListener(AllyDead);
    }
}
