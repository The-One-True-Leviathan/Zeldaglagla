using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters;
using Pathfinding;

public class PlayerPackCircle : MonoBehaviour
{
    List<GameObject> holders = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateCircle(PackManager pack)
    {
        foreach (GameObject holder in holders)
        {
            Destroy(holder);
        }
        holders.Clear();
        float numberOfHolders = pack.wolves.Count;
        float angle = 360 / numberOfHolders;
        for (int i = 0; i < numberOfHolders; i++)
        {
            // Créer un nombre de game objects égal au nombre de loups dans la meute et les placer en cercle autour du joueur
            Vector3 angleAndDistance = new Vector3(pack.circleDistance, 0);
            angleAndDistance = Quaternion.AngleAxis(angle * i, Vector3.forward) * angleAndDistance;
            GameObject currentGameObject = new GameObject("Holder " + i);
            holders.Add(currentGameObject);
            currentGameObject.AddComponent<WolfHolder>();
            currentGameObject.transform.position = gameObject.transform.position + angleAndDistance;

            // TEMPORARY
            // Téléporter les loups vers leur holder respectif
            holders[i].GetComponent<WolfHolder>().isHoldingAWolf = true;
            holders[i].GetComponent<WolfHolder>().wolf = pack.wolves[i];
            pack.wolves[i].transform.position = holders[i].transform.position;
            pack.wolves[i].GetComponent<AIDestinationSetter>().target = holders[i].transform;
        }
    }
}
