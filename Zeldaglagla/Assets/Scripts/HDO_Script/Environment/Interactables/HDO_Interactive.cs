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

        if(activated == order)
        {
            Debug.Log("order found, activation");
            Shield();
        }

        if(activated.Count >= order.Count)
        {
            Debug.Log("wrong order");
            activated.Clear();
        }
    }

    public void Action()
    {
        Debug.Log("called action on " + gameObject.name + " object");
        if (movement)
        {
            Movement();
        }
        if (shieldD1)
        {

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
