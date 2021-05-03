using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HDO_Interaction : MonoBehaviour
{
    public bool severalInteractions;
    public List<HDO_InteractionSO> interactions = null;
    private protected List<HDO_InteractionSO> hidden = null;
    public List<float> waitTimeBetweenInteractions = null;

    public GameObject spawnPoint;
    public bool isFullTrigger;

    private void Start()
    {
        if(interactions.Count > 1)
        {
            severalInteractions = true;
        }
        else
        {
            severalInteractions = false;
        }

        hidden = interactions;
    }

    private void Update()
    {
        //interactions = hidden;
    }
}
