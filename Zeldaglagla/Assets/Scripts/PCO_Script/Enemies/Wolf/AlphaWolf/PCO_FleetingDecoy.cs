using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PCO_FleetingDecoy : MonoBehaviour
{
    AIPath pather;
    AIDestinationSetter destinationSetter;
    Animator animator;

    public GameObject alphaWolf;

    // Start is called before the first frame update
    void Start()
    {
        pather = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        animator = transform.GetChild(0).GetComponent<Animator>();

        if (GameObject.Find("Wolf Decoy Target"))
        {
            destinationSetter.target = GameObject.Find("Wolf Decoy Target").transform;
        }
        else
        {
            Debug.LogError("Couldn't find Wolf Decoy Target");
        }
        pather.SearchPath();

    }

    // Update is called once per frame
    void Update()
    {
        if (pather.remainingDistance < 0.1f)
        {
            Destroy(gameObject);
        }
        else
        {
            Animate(pather.velocity);
        }
    }

    void Animate(Vector2 direct)
    {
        string anim;
            anim = "Walk";
            direct.Normalize();
        if (direct.x > 0)
        {
            animator.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (direct.x < 0)
        {
            animator.transform.localScale = new Vector3(1, 1, 1);
        }
        if (direct.y > 0)
        {
            anim += "_Back";
        }
        else if (direct.y < 0)
        {
            anim += "_Front";
        }
        animator.Play(anim);
        return;
    }
}

