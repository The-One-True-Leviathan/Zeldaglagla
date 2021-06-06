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

    public List<GameObject> positionsForHint = null;
    [SerializeField]
    GameObject hint;

    [Header("Self Destroy")]
    [SerializeField]
    bool selfDestroy;

    [Header("Deactivate Turret")]
    [SerializeField]
    bool deactivateTurret;
    [SerializeField]
    HDO_Tourelle turret;

    [Header("Animation")]
    [SerializeField]
    bool animate;
    [SerializeField]
    Animator animator;

    [Header("Nullify Interaction")]
    [SerializeField]
    bool nullify;
    [SerializeField]
    HDO_Interaction inter;
    Collider2D self;


    private void Start()
    {
        self = GetComponent<Collider2D>();
        if (shieldD1)
        {
            hint.transform.position = positionsForHint[0].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shieldD1)
        {
            CheckShielding();
            Hint();
        }
        
    }

    void Hint()
    {
        if(activated.Count != 0)
        {
            if(activated.Count == 1)
            {
                if(activated[0] == order[0])
                {
                    hint.transform.position = positionsForHint[1].transform.position;
                }
                else
                {
                    CheckOrder();
                }
            }
            else if(activated.Count == 2)
            {
                if(activated[1] == order[1])
                {
                    hint.transform.position = positionsForHint[2].transform.position;
                }
                else
                {
                    CheckOrder();
                }
            }
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
        for(int i = 0; i <= activated.Count - 1; i++)
        {
            if(!(activated[i] == order[i]))
            {
                good = false;
                Debug.Log("wrong order");
                break;
            }
        }

        if(activated.Count != order.Count)
        {
            good = false;
        }

        if (!good)
        {
            hint.transform.position = positionsForHint[0].transform.position;
            activated.Clear();
            return;
        }

        Shield();

    }

    public void Action()
    {
        if (gameObject == null)
        {
            return;
        }
        //Debug.Log("called action on " + gameObject.name + " object");
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
        if (animate)
        {
            animator.enabled = true;
            animator.SetTrigger("Animate");
        }
        if (nullify)
        {
            inter.enabled = false;
            self.enabled = false;
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
