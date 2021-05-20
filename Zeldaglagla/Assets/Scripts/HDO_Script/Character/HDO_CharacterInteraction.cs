using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HDO_CharacterInteraction : MonoBehaviour
{

    [SerializeField]
    ContactFilter2D interactables;
    [SerializeField]
    CircleCollider2D detector;
    [SerializeField]
    List<Collider2D> result = null;

    [SerializeField]
    GameObject selector;
    SpriteRenderer selectorSr;

    [SerializeField]
    Collider2D interactObject;
    [SerializeField]
    GameObject interactobj;

    int detectedObjs, j;
    bool fullManual;
    [SerializeField]
    float addPos;

    HDO_Interaction interaction, triggerInteracted;
    HDO_InteractionSO currentInteraction;
    CoolTextScript cts;
    HDO_CharacterCombat combat;
    HDO_MapManager map;

    [SerializeField]
    Text EVAADialog, innerDialog;

    [SerializeField]
    List<HDO_InteractionSO> toDoInteractions = null;

    [SerializeField]
    List<HDO_InteractionSO> doneUniqueInteraction = null;

    public List<HDO_ItemSO> inventory = null;

    bool manualInteraction, triggerInteraction;
    int it;


    private void Start()
    {
        selectorSr = selector.GetComponent<SpriteRenderer>();
        combat = GetComponent<HDO_CharacterCombat>();
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<HDO_MapManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Detect();

        if(interactObject != null)
        {
            interactobj = interactObject.gameObject;
        }

        if (interactObject == null)
        {
           // doneTriggerInteractions.Clear();
            toDoInteractions.Clear();
        }

        if (toDoInteractions.Count == 0)
        {
            //StopCoroutine(SeveralInteractions());
            manualInteraction = false;
            triggerInteraction = false;
        }
    }

    public void Interact()
    {
        if(interactObject == null)
        {
            return;
        }

        j = 0;
        manualInteraction = true;

        if (interaction.severalInteractions && !triggerInteraction)
        {
            toDoInteractions = interaction.interactions;
            fullManual = true;
            j = 0;
            StartCoroutine(SeveralInteractions());
            
        }
        else if(!triggerInteraction)
        {
            currentInteraction = interaction.interactions[0];
            SelectInteraction(currentInteraction);
        }
        else if(interaction.severalInteractions && triggerInteraction)
        {
            foreach(HDO_InteractionSO inter in interaction.interactions)
            {
                if (inter.isTrigger)
                {
                    toDoInteractions.Add(inter);
                }
            }

            j = 0;
            StartCoroutine(SeveralInteractions());
        }
        else if(!interaction.severalInteractions && triggerInteraction)
        {
            currentInteraction = interaction.interactions[0];
            SelectInteraction(currentInteraction);
        }
        

    }

    IEnumerator SeveralInteractions()
    {
        Debug.Log("start coroutine");
        if(toDoInteractions.Count <= j)
        {
            Debug.Log("nothing to interact");
            manualInteraction = false;
            triggerInteraction = false;
            fullManual = false;
            StopCoroutine(SeveralInteractions());
            yield break;
        }

        
        if(!(fullManual && toDoInteractions[j].isTrigger))
        {
            SelectInteraction(toDoInteractions[j]);
        }

        yield return new WaitForSeconds(interaction.waitTimeBetweenInteractions[j]);

        j++;
        StartCoroutine(SeveralInteractions());
    }

    void SelectInteraction(HDO_InteractionSO inter)
    {

        if (doneUniqueInteraction.Contains(inter))
        {
            return;
        }

        if(inter.interactionType == HDO_InteractionSO.InteractionType.dialog)
        {
            SpawnDialog(inter);
        }

        if(inter.interactionType == HDO_InteractionSO.InteractionType.environmentEvent)
        {
            EnvironmentChange(inter);
        }

        if(inter.interactionType == HDO_InteractionSO.InteractionType.enemyEvent)
        {
            EnemyEvent(inter);
        }

        if (inter.interactionType == HDO_InteractionSO.InteractionType.item)
        {
            GetItem(inter);
        }

        if(inter.interactionType == HDO_InteractionSO.InteractionType.needItem)
        {
            NeedItem(inter);
        }

        if(inter.interactionType == HDO_InteractionSO.InteractionType.setSpawnPoint)
        {
            SetSpawn(inter);
        }

        if(inter.interactionType == HDO_InteractionSO.InteractionType.characterImprovement)
        {
            ImproveCharacter(inter);
        }

        if(inter.interactionType == HDO_InteractionSO.InteractionType.mapEvent)
        {
            MapEVent(inter);
        }

        if (inter.isUnique)
        {
            doneUniqueInteraction.Add(inter);
        }
    }

    void MapEVent(HDO_InteractionSO inter)
    {
        if (inter.unlockMapPart)
        {
            GameObject mapPart = GameObject.Find(inter.mapPartName);
            Destroy(mapPart);
        }

        if (inter.redirectToLocation && !inter.specificLocation)
        {
            GameObject[] caches = null;

            if (inter.locationType == HDO_InteractionSO.LocationType.heatPoint)
            {
                if(inter.region == HDO_InteractionSO.Region.zone_1)
                {
                    caches = GameObject.FindGameObjectsWithTag("HeatCacheZ1");
                }
                else if (inter.region == HDO_InteractionSO.Region.zone_2)
                {
                    caches = GameObject.FindGameObjectsWithTag("HeatCacheZ2");
                }
                else if (inter.region == HDO_InteractionSO.Region.zone_3)
                {
                    caches = GameObject.FindGameObjectsWithTag("HeatCacheZ3");
                }               

            }

            if (inter.locationType == HDO_InteractionSO.LocationType.monsterCamp)
            {
                if (inter.region == HDO_InteractionSO.Region.zone_1)
                {
                    caches = GameObject.FindGameObjectsWithTag("CampCacheZ1");
                }
                else if (inter.region == HDO_InteractionSO.Region.zone_2)
                {
                    caches = GameObject.FindGameObjectsWithTag("CampCacheZ2");
                }
                else if (inter.region == HDO_InteractionSO.Region.zone_3)
                {
                    caches = GameObject.FindGameObjectsWithTag("CampCacheZ3");
                }
            }

            if (inter.locationType == HDO_InteractionSO.LocationType.dungeon)
            {
                caches = GameObject.FindGameObjectsWithTag("DungeonCache");
            }

            if (caches != null)
            {
                if (caches.Length == 1)
                {
                    map.Stock(caches[0].GetComponent<Image>(), inter.locationType);
                }
                else
                {
                    StartCoroutine(SelectCache(inter, caches));
                }
            }
            else
            {
                Debug.Log("no caches left");
            }

            
        }
        else
        {
            map.Stock(GameObject.Find(inter.specificLocationName).GetComponent<Image>(), inter.locationType);
        }
    }

    IEnumerator SelectCache(HDO_InteractionSO inter, GameObject[] caches)
    {
        Debug.Log("try selection");

        int randomizer = Random.Range(0, caches.Length);
        GameObject cache;
        cache = caches[randomizer];

        Debug.Log(cache);

        yield return new WaitForEndOfFrame();

        if(cache == null)
        {
            StartCoroutine(SelectCache(inter, caches));
        }

        if(inter.mapPartName == cache.name)
        {
            StartCoroutine(SelectCache(inter, caches));
        }
        else
        {
            map.Stock(cache.GetComponent<Image>(), inter.locationType);
        }

    }

    void ImproveCharacter(HDO_InteractionSO inter)
    {
        if (inter.abilityUnlock)
        {
            if(inter.abilityUnlocked == HDO_InteractionSO.Ability.torch)
            {
                GetComponent<HDO_CharacterCombat>().torchUnlocked = true;
            }
        }
    }

    void SetSpawn(HDO_InteractionSO inter)
    {
        combat.respawnPoint = interaction.spawnPoint;  
    }

    void SpawnDialog(HDO_InteractionSO inter)
    {

        if (inter.type == HDO_InteractionSO.dialogType.inner)
        {
            if (inter.number == HDO_InteractionSO.dialogNum.one)
            {
                cts = innerDialog.GetComponent<CoolTextScript>();
                cts.defaultText = inter.dialogs[0];
                cts.Read();
            }
            else
            {
                if (inter.dialogSuite)
                {
                    cts = innerDialog.GetComponent<CoolTextScript>();
                    List<string> list = inter.dialogs;
                    StartCoroutine(dialogSuite(list));
                }
                else
                {
                    cts = innerDialog.GetComponent<CoolTextScript>();
                    cts.defaultText = inter.dialogs[Random.Range(0, inter.dialogs.Count)];
                    cts.Read();
                }
                
            }
        }
        else
        {
            if(inter.number == HDO_InteractionSO.dialogNum.one)
            {               
                cts = EVAADialog.GetComponent<CoolTextScript>();
                cts.defaultText = inter.dialogs[0];
                cts.Read();
            }
            else
            {
                if (inter.dialogSuite)
                {
                    cts = EVAADialog.GetComponent<CoolTextScript>();
                    List<string> list = inter.dialogs;
                    StartCoroutine(dialogSuite(list));
                }
                else
                {
                    cts = EVAADialog.GetComponent<CoolTextScript>();
                    cts.defaultText = inter.dialogs[Random.Range(0, inter.dialogs.Count)];
                    cts.Read();
                }
                
            }

        }
    }

    void EnvironmentChange(HDO_InteractionSO inter)
    {
        if (inter.needSpawnPoint)
        {
            if(interaction.spawnPoint == null)
            {
                interaction.spawnPoint = interaction.gameObject;
            }

            Instantiate(inter.Environment, interaction.spawnPoint.transform);
        }
    }

    void EnemyEvent(HDO_InteractionSO inter)
    {
        if (inter.needSpawnPoint)
        {

            if(interaction.spawnPoint == null)
            {
                interaction.spawnPoint = interaction.gameObject;
            }

            foreach(GameObject toSpawn in inter.enemiesToSpawn)
            {
                Instantiate(toSpawn, new Vector3(interaction.spawnPoint.transform.position.x, interaction.spawnPoint.transform.position.y, 0), Quaternion.Euler(0, 0, 0));
            }
        }
    }

    void GetItem(HDO_InteractionSO inter)
    {
        inventory.Add(inter.item);
    }

    void NeedItem(HDO_InteractionSO inter)
    {
        if (inventory.Contains(inter.neededItem))
        {
            if(inter.consumesItem)
            {
                inventory.Remove(inter.neededItem);
            }

            foreach(HDO_Interactive interactive in interaction.interactives)
            {
                interactive.Action();
            }
        }
    }

    void Detect()
    {
        detectedObjs = Physics2D.OverlapCollider(detector, interactables, result);

        if(detectedObjs == 0)
        {
            interactObject = null;
            StartCoroutine(SelectorFade());
            return;
        }
        else if(detectedObjs == 1)
        {          
            interactObject = result[0];
            interaction = interactObject.GetComponent<HDO_Interaction>();

            if (!interaction.isFullTrigger)
            {
                selector.transform.position = result[0].transform.position + new Vector3(0, addPos, 0);
                selectorSr.enabled = true;
                selectorSr.color = new Color(selectorSr.color.r, selectorSr.color.g, selectorSr.color.b, (1 / (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(interactObject.transform.position.x, interactObject.transform.position.y)))));
            }
            //Debug.Log(1 /((Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(interactObject.transform.position.x, interactObject.transform.position.y)))));


        }
        else
        {

            float minDist = 0;

            for(int i = 0; i < result.Count - 1; i++)
            {
                if(i == 0 && !result[i].GetComponent<HDO_Interaction>().isFullTrigger)
                {
                    minDist = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(result[0].transform.position.x, result[0].transform.position.y));
                }
                else
                {
                    if(Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(result[i].transform.position.x, result[i].transform.position.y)) < minDist && !result[i].GetComponent<HDO_Interaction>().isFullTrigger)
                    {
                        minDist = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(result[i].transform.position.x, result[i].transform.position.y));
                        interactObject = result[i];
                    }
                }
            }

            if(interactObject != null)
            {
                selector.transform.position = interactObject.transform.position + new Vector3(0, addPos, 0);
                selectorSr.enabled = true;

                interaction = interactObject.GetComponent<HDO_Interaction>();
            }
            

        }

        if (interaction.severalInteractions)
        {

            foreach(HDO_InteractionSO inter in interaction.interactions)
            {
                if (inter.isTrigger)
                {
                    triggerInteraction = true;
                    break;
                }
            }

            if (triggerInteraction &&!manualInteraction && triggerInteracted != interaction)
            {
                Interact();
                triggerInteracted = interaction;
            }
            else
            {
                triggerInteraction = false;
            }
           
        }
        else
        {

            if (interaction.interactions[0].isTrigger && !manualInteraction && triggerInteracted != interaction)
            {
                Interact();
                triggerInteracted = interaction;
            }
        }
    }

    IEnumerator SelectorFade()
    {
        if(detectedObjs == 0)
        {
            selectorSr.color = new Color(selectorSr.color.r, selectorSr.color.g, selectorSr.color.b, selectorSr.color.a / 1.05f);
        }
        yield return new WaitForSeconds(1f);
        if(selectorSr.color.a > 0)
        {
            StartCoroutine(SelectorFade());
        }
        else
        {
            selectorSr.enabled = false;
        }
    }

  
    IEnumerator dialogSuite(List<string> list)
    {
        
        
        cts.defaultText = list[it];
        cts.Read();
        it++;
        yield return new WaitUntil(() => cts.defaultText == "");
        if (!(it >= list.Count))
        {
            StartCoroutine(dialogSuite(list));
        }

    }

}
