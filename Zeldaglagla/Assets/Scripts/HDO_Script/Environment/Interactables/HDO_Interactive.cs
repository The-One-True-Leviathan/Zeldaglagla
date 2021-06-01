using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HDO_Interactive : MonoBehaviour
{
    [SerializeField]
    bool emitSound;
    [SerializeField]
    AudioSource soundToPlay;

    [Header("Movement")]
    [SerializeField]
    bool movement;
    [SerializeField]
    GameObject goToPos;
    [SerializeField]
    float speed;

    [Header("Shield Dungeon 1")]
    [SerializeField]
    bool shieldD1;
    [SerializeField]
    List<HDO_InteractionSO> order = null;
    [SerializeField]
    public List<HDO_InteractionSO> activated = null;

    [Header("Self Destroy")]
    [SerializeField]
    bool selfDestroy;

    [Header("Deactivate Turret")]
    [SerializeField]
    bool deactivateTurret;
    [SerializeField]
    HDO_Tourelle turret;
    

    // Update is called once per frame
    void Update()
    {
        if (shieldD1)
        {
            CheckShielding();
        }
        
    }

    void CheckShielding()
    {
        if(order == null)
        {
            Debug.Log("no order, risk of auto completion");
        }

        if(activated.Count == order.Count)
        {
            Debug.Log("checking order");
            CheckOrder();
        }
    }

    void CheckOrder()
    {
        bool good = true;
        for(int i = 0; i <= activated.Count; i++)
        {
            if(!(activated[i] == order[i]))
            {
                good = false;
                Debug.Log("wrong order");
                break;
            }
        }

        if (!good)
        {
            return;
        }

        Shield();

    }

    public void Action()
    {
        Debug.Log("called action on " + gameObject.name + " object");
        if (movement)
        {
            Movement();
        }
        if (selfDestroy)
        {
            Destroy(this.gameObject);
        }
        if (deactivateTurret)
        {
            turret.activated = false;
        }
    }

    void Shield()
    {
        this.gameObject.SetActive(false);
    }

    void Movement()
    {
        if(transform.position != goToPos.transform.position)
        {
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, goToPos.transform.position, speed * Time.deltaTime);
        yield return new WaitForEndOfFrame();
        Movement();
    }
}
