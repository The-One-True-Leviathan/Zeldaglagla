using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCO_AIDeactivatorManager : MonoBehaviour
{

    public List<PCO_AIDeactivator> monsters;

    // Update is called once per frame
    void Update()
    {
        if (monsters.Count > 0)
        {
            foreach (PCO_AIDeactivator monster in monsters)
            {
                if (monster == null)
                {
                    monsters.Remove(monster);
                    return;
                }
                if ((transform.position - monster.transform.position).sqrMagnitude < monster.activateRange * monster.activateRange)
                {
                    monster.Set(true);
                }
                else if ((transform.position - monster.transform.position).sqrMagnitude < (monster.activateRange * 2) * (monster.activateRange * 2))
                {
                    monster.Set(false);
                }
            }
        }
    }
}
