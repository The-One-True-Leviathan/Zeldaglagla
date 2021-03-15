using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters;
using Pathfinding;

public class PlayerPackCircle : MonoBehaviour
{
    public List<GameObject> holders = new List<GameObject>();
    public GameObject center; 
    public float sizeMultiplier;
    public bool rotating = false;
    float rotation;
    // Start is called before the first frame update
    void Start()
    {
        center = new GameObject("Center");
        center.transform.SetParent(this.transform);
        center.transform.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotating)
        {
            rotation += 20f * Time.deltaTime;
            center.transform.rotation = Quaternion.Euler(0f, 0f, rotation);
        }
    }

    public void CreateCircle(PackManager pack)
    {
        center.transform.localScale = Vector3.one;
        center.transform.rotation = Quaternion.identity;
        rotating = true;
        rotation = 0;
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

            GameObject currentGameObject = new GameObject("Holder " + (i+1));
            holders.Add(currentGameObject);
            currentGameObject.AddComponent<WolfHolder>();
            currentGameObject.transform.position = gameObject.transform.position + angleAndDistance;
            currentGameObject.transform.SetParent(center.transform);

            // TEMPORARY
            // Téléporter les loups vers leur holder respectif
            /*
            pack.wolves[i].transform.position = holders[i].transform.position;
            pack.wolves[i].GetComponent<AIDestinationSetter>().target = holders[i].transform;*/
        }
        for (int i = 0; i < pack.wolves.Count; i++)
        {
            center.transform.GetChild(i).GetComponent<WolfHolder>().wolf = pack.wolves[i].gameObject;
            center.transform.GetChild(i).GetComponent<WolfHolder>().isHoldingAWolf = true;
        }
    }
}
