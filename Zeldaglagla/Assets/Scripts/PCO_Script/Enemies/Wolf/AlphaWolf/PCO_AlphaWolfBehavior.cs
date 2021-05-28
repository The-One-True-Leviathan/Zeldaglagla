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
        SMB.GetBehaviour<BaseWolfSMBWander>().baseWolf = this;
        SMB.GetBehaviour<BaseWolfSMBApproach>().baseWolf = this;
        SMB.GetBehaviour<BaseWolfSMBObserve>().baseWolf = this;
        SMB.GetBehaviour<BaseWolfSMBRush>().baseWolf = this;
        SMB.GetBehaviour<BaseWolfSMBHarass>().baseWolf = this;
        SMB.GetBehaviour<BaseWolfSMBAttack>().baseWolf = this;
        SMB.GetBehaviour<BaseWolfSMBFlee>().baseWolf = this;
    }
}
