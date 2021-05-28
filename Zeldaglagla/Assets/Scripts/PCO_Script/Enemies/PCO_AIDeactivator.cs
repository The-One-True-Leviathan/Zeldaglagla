using UnityEngine;
using Monsters;
using Pathfinding;

public class PCO_AIDeactivator : MonoBehaviour
{
    AIPath aiPath;
    AIDestinationSetter destinationSetter;
    MonsterRoot monster;
    Collider2D col2D;
    Animator SMB;
    GameObject display;
    PackManager pack;
    bool isPack = false;

    PCO_AIDeactivatorManager deactivatorManager;

    public float activateRange;
    
    private void Start()
    {
        if (GetComponent<PackManager>())
        {
            pack = GetComponent<PackManager>();
            isPack = true;
        }
        else
        {
            aiPath = GetComponent<AIPath>();
            destinationSetter = GetComponent<AIDestinationSetter>();
            monster = GetComponent<MonsterRoot>();
            col2D = GetComponent<Collider2D>();
            SMB = GetComponent<Animator>();
            display = transform.GetChild(0).gameObject;
        }

        deactivatorManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PCO_AIDeactivatorManager>();

        Set(false);
    }

    public void Set(bool onOrOff)
    {
        if (isPack)
        {
            foreach (WolfRoot wolf in pack.wolves)
            {
                wolf.gameObject.SetActive(onOrOff);
            }
            return;
        }
        else
        {
            aiPath.enabled = destinationSetter.enabled = monster.enabled = col2D.enabled = SMB.enabled = onOrOff;
            display.SetActive(onOrOff);
        }
    }
}
