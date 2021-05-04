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
    

    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/

    public void Action()
    {
        Debug.Log("called action on " + gameObject.name + " object");
        if (movement)
        {
            Movement();
        }
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
