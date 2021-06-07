using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HDO_MapManager : MonoBehaviour
{

    [SerializeField]
    public List<Image> caches = null;

    [SerializeField]
    Image contour, caption, indicator;

    [SerializeField]
    Vector2 selfBoundaries, mapBoundaries;
    [SerializeField]
    GameObject player;
    

    bool statusLastFrame;

    Image self;
    public List<Image> coloredCache = null;
    public List<Image> toShowCache = null;
    public List<Color> coloredCacheColor;

    [SerializeField]
    List<Image> stocked = null;
    [SerializeField]
    List<HDO_InteractionSO.LocationType> locations = null;


    [Header("Map Properties")]
    [SerializeField]
    Color heatPointColor;
    [SerializeField]
    Color dungeonColor, monsterCampColor;

    [SerializeField]
    RectTransform indicatorTrans;

    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<Image>();
        statusLastFrame = self.enabled;
        Activate();
    }

    // Update is called once per frame
    void Update()
    {
        if(statusLastFrame != self.enabled)
        {
            statusLastFrame = self.enabled;
            Activate();           
        }

        indicatorTrans.anchoredPosition = player.transform.position * 3; 

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

                toShowCache.Add(im);
                coloredCache.Add(im);
                coloredCacheColor.Add(im.color);

                i++;
            }



            stocked.Clear();
            locations.Clear();
        }


    }

    void Activate()
    {
        foreach(Image im in toShowCache)
        {
            if(im != null)
            {
                im.enabled = self.enabled;
            }
        }

        caption.enabled = self.enabled;
        contour.enabled = self.enabled;
        indicator.enabled = self.enabled;

        if (self.enabled)
        {
            Objective();
        }
    }

    public void Stock(Image im, HDO_InteractionSO.LocationType type)
    {
        stocked.Add(im);
        locations.Add(type);
        Objective();
    }
}
