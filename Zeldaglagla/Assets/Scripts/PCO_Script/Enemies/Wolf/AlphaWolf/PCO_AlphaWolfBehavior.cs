using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PCO_AlphaWolfBehavior : BaseWolf
{
    public Transform wanderAreaCenter;
    public float wanderAreaRadius;
    // Start is called before the first frame update
    void Start()
    {
        pack = transform.parent.GetComponent<PackManager>();
        SMB = GetComponent<Animator>();
        pather = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        SMB.GetBehaviour<BaseWolfSMBWander>().baseWolf = this;
        SMB.GetBehaviour<BaseWolfSMBWander>().isAlpha = true;
        SMB.GetBehaviour<BaseWolfSMBWander>().wanderCenter = wanderAreaCenter;
        SMB.GetBehaviour<BaseWolfSMBWander>().wanderSize = wanderAreaRadius;
        SMB.GetBehaviour<BaseWolfSMBApproach>().baseWolf = this;
        SMB.GetBehaviour<BaseWolfSMBObserve>().baseWolf = this;
        SMB.GetBehaviour<BaseWolfSMBRush>().baseWolf = this;
        SMB.GetBehaviour<BaseWolfSMBHarass>().baseWolf = this;
        SMB.GetBehaviour<BaseWolfSMBAttack>().baseWolf = this;
        SMB.GetBehaviour<BaseWolfSMBFlee>().baseWolf = this;
    }
}
