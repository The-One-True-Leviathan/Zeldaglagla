using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HDO_MapManager : MonoBehaviour
{

    [SerializeField]
    List<Image> caches = null;

    bool statusLastFrame;

    Image self;

    [SerializeField]
    List<Image> stocked = null;
    [SerializeField]
    List<HDO_InteractionSO.LocationType> locations = null;


    [Header("Map Properties")]
    [SerializeField]
    Color heatPointColor;
    [SerializeField]
    Color dungeonColor, monsterCampColor;

    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<Image>();
        statusLastFrame = self.enabled;
    }

    // Update is called once per frame
    void Update()
    {
        if(statusLastFrame != self.enabled)
        {
            statusLastFrame = self.enabled;
            Activate();

            
        }      

    }

    void Objective()
    {
        if(stocked.Count != 0)
        {
            int i = 0;

            foreach(Image im in stocked)
            {
                if(locations[i] == HDO_InteractionSO.LocationType.dungeon)
                {
                    im.color = dungeonColor;
                }
                if(locations[i] == HDO_InteractionSO.LocationType.monsterCamp)
                {
                    im.color = monsterCampColor;
                }
                if(locations[i] == HDO_InteractionSO.LocationType.heatPoint)
                {
                    im.color = heatPointColor;
                }

                i++;
            }

            stocked.Clear();
            locations.Clear();
        }
    }

    void Activate()
    {
        foreach(Image im in caches)
        {
            if(im != null)
            {
                im.enabled = self.enabled;
            }
        }

        if (self.enabled)
        {
            Objective();
        }
    }

    public void Stock(Image im, HDO_InteractionSO.LocationType type)
    {
        stocked.Add(im);
        locations.Add(type);
    }
}
