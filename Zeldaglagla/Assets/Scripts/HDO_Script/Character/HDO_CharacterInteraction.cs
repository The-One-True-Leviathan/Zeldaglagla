using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    bool failed;

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
    public List<HDO_InteractionSO> doneUniqueInteraction = null;

    public List<HDO_ItemSO> inventory = null;

    bool manualInteraction, triggerInteraction;
    int it;

    
    GameObject[] allHeatCache = null;
    [SerializeField]
    List<GameObject> heatCacheTransfer = null;

    
    GameObject[] campCache = null;
    [SerializeField]
    List<GameObject> campCacheTransfer = null;

    [SerializeField]
    HDO_HeatManager heatManager;

    [SerializeField]
    AudioManager sound;

    [SerializeField]
    HDO_SaveManager save;

    [SerializeField]
    GameObject essentials;

    [SerializeField]
    Image portrait;

    [SerializeField]
    Image redCard, blueCard, lever;


    private void Start()
    {
        Object.DontDestroyOnLoad(essentials);

        selectorSr = selector.GetComponent<SpriteRenderer>();
        combat = GetComponent<HDO_CharacterCombat>();
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<HDO_MapManager>();

        Invoke("GetMap", 1.5f);

        if(it == 0)
        {
            portrait.sprite = null;
        }

    }

    void GetMap()
    {
        allHeatCache = GameObject.FindGameObjectsWithTag("HeatCacheZ1");

        foreach (GameObject go in allHeatCache)
        {
            heatCacheTransfer.Add(go);
        }

        allHeatCache = GameObject.FindGameObjectsWithTag("HeatCacheZ2");

        foreach (GameObject go in allHeatCache)
        {
            heatCacheTransfer.Add(go);
        }

        allHeatCache = GameObject.FindGameObjectsWithTag("HeatCacheZ3");

        foreach (GameObject go in allHeatCache)
        {
            heatCacheTransfer.Add(go);
        }

        campCache = GameObject.FindGameObjectsWithTag("CampCacheZ1");

        foreach (GameObject go in campCache)
        {
            campCacheTransfer.Add(go);
        }

        campCache = GameObject.FindGameObjectsWithTag("CampCacheZ2");

        foreach (GameObject go in campCache)
        {
            campCacheTransfer.Add(go);
        }

        campCache = GameObject.FindGameObjectsWithTag("CampCacheZ3");

        foreach (GameObject go in campCache)
        {
            campCacheTransfer.Add(go);
        }
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

        if(inter.interactionType == HDO_InteractionSO.InteractionType.dungeonshield)
        {
            DungeonShield(inter);
        }

        if(inter.interactionType == HDO_InteractionSO.InteractionType.sceneChange)
        {
            SceneChange(inter);
        }

        if(inter.interactionType == HDO_InteractionSO.InteractionType.reload)
        {
            Reload(inter);
        }

        if(inter.interactionType == HDO_InteractionSO.InteractionType.saveGame)
        {
            Save();
        }

        if(inter.interactionSound != null)
        {
            sound.Play(inter.interactionSound.name);
        }

        if (inter.isUnique && !failed)
        {
            doneUniqueInteraction.Add(inter);
        }
        failed = false;

        
    }

    void Save()
    {
        save.SaveGame();
    }

    void Reload(HDO_InteractionSO inter)
    {
        if (inter.reloadHealth)
        {
            GetComponent<HDO_CharacterCombat>().currentHealth += inter.healthAmount;
        }
        if (inter.heatReload)
        {
            heatManager.heatValue += inter.heatAmount;
        }
    }

    void SceneChange(HDO_InteractionSO inter)
    {
        int protector = combat.spawnPoints.Count;

        combat.spawnPoints.Add(combat.respawnPoint.transform.position);
        combat.crossedScenes.Add(SceneManager.GetActiveScene().name);

        StartCoroutine(LoadScene(inter, protector));
    }

    IEnumerator LoadScene(HDO_InteractionSO inter , int prot)
    {
        yield return new WaitUntil(() => prot != combat.spawnPoints.Count);

        Debug.Log("Try to load" + inter.scene);
        if(inter.scene != "LD2_HeatWave")
        {
            lever.enabled = false;
        }
        SceneManager.LoadScene(inter.scene);
        StartCoroutine(WaitForSceneToBeLoaded(inter));
        
    }

    IEnumerator WaitForSceneToBeLoaded(HDO_InteractionSO inter)
    {
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == inter.scene);

        Debug.Log("yspassdétruc");
        if (combat.crossedScenes.Contains(SceneManager.GetActiveScene().name))
        {
            Debug.Log(SceneManager.GetActiveScene().name);
            transform.position = combat.spawnPoints[combat.crossedScenes.IndexOf(SceneManager.GetActiveScene().name)];
        }

        Scene sc = SceneManager.GetActiveScene();
        string nameSc = sc.name;
        if (combat.crossedScenes.Contains(nameSc))
        {
            Debug.Log("wtf");
            transform.position = combat.spawnPoints[combat.crossedScenes.IndexOf(nameSc)];
        }
        else
        {
            Debug.Log("oyeyeyeyeye");
            transform.position = GameObject.Find("First").transform.position;
        }
    }

    void DungeonShield(HDO_InteractionSO inter)
    {
        interaction.interactives[0].activated.Add(inter);
        Debug.Log("euh ouais de fou");
    }

    void MapEVent(HDO_InteractionSO inter)
    {
        bool any = false;

        if (inter.unlockMapPart)
        {
            GameObject mapPart = GameObject.Find(inter.mapPartName);
            map.caches.Remove(mapPart.GetComponent<Image>());
            mapPart.GetComponent<Image>().color = Color.white;
            if (!(map.toShowCache.Contains(mapPart.GetComponent<Image>())))
            {
                map.toShowCache.Add(mapPart.GetComponent<Image>());
            }
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
                else if(inter.region == HDO_InteractionSO.Region.any)
                {
                    any = true;
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
                else if(inter.region == HDO_InteractionSO.Region.any)
                {
                    any = true;
                }
            }

            if (inter.locationType == HDO_InteractionSO.LocationType.dungeon)
            {
                caches = GameObject.FindGameObjectsWithTag("DungeonCache");
            }

            Debug.Log(any);

            if (caches != null && !any)
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
            else if(any)
            {
                if(inter.locationType == HDO_InteractionSO.LocationType.heatPoint)
                {
                    SelectListCache(inter, heatCacheTransfer);
                }

                if (inter.locationType == HDO_InteractionSO.LocationType.monsterCamp)
                {
                    SelectListCache(inter, campCacheTransfer);
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

    IEnumerator SelectListCache(HDO_InteractionSO inter, List<GameObject> list)
    {
        Debug.Log("try selection");

        int randomizer = Random.Range(0, list.Count - 1);
        GameObject cache;
        cache = list[randomizer];

        Debug.Log(cache);

        yield return new WaitForEndOfFrame();

        if (cache == null)
        {
            StartCoroutine(SelectListCache(inter, list));
        }

        if (inter.mapPartName == cache.name)
        {
            StartCoroutine(SelectListCache(inter, list));
        }
        else
        {
            map.Stock(cache.GetComponent<Image>(), inter.locationType);
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

            if (inter.abilityUnlocked == HDO_InteractionSO.Ability.heatwave)
            {
                GetComponent<HDO_CharacterCombat>().heatwaveUnlocked = true;
            }
        }

        if (inter.heatSegmentUnlock)
        {
            if(heatManager.unlockedSeg + inter.numberOfSegs > heatManager.heatSegments.Count)
            {

            }
            else
            {
                heatManager.unlockedSeg += inter.numberOfSegs;
            }

        }

        if (inter.AddMaxHealth)
        {
            GetComponent<HDO_CharacterCombat>().maxHealth += inter.healthToAdd;
            GetComponent<HDO_CharacterCombat>().currentHealth += inter.healthToAdd;
        }

        if (inter.HeatwaveV2)
        {
            combat.HeatwaveV2 = true;
        }

        if (inter.TorchV2)
        {
            combat.TorchV2 = true;
        }
    }

    void SetSpawn(HDO_InteractionSO inter)
    {
        combat.respawnPoint = interaction.spawnPoint;  
    }

    void SpawnDialog(HDO_InteractionSO inter)
    {
        portrait.sprite = inter.portrait;

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
                    StartCoroutine(dialogSuite(list, inter));
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
                    it = 0;
                    StopCoroutine(dialogSuite(list, inter));
                    portrait.sprite = inter.portrait;
                    StartCoroutine(dialogSuite(list, inter));
                }
                else
                {
                    cts = EVAADialog.GetComponent<CoolTextScript>();
                    cts.defaultText = inter.dialogs[Random.Range(0, inter.dialogs.Count)];
                    portrait.sprite = inter.portrait;
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

            if (inter.snowStorm)
            {
                Instantiate(inter.Environment, interaction.spawnPoint.transform).GetComponent<HDO_TemperatureModifier>().snowStorm = true;

            }
            else
            {
                Instantiate(inter.Environment, interaction.spawnPoint.transform);
            }
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
        if(inter.cardType == HDO_InteractionSO.CardType.red)
        {
            redCard.enabled = true;
        }
        if(inter.cardType == HDO_InteractionSO.CardType.blue)
        {
            blueCard.enabled = true;
        }
        if (inter.cardType == HDO_InteractionSO.CardType.lever)
        {
            lever.enabled = true;
        }
    }

    void NeedItem(HDO_InteractionSO inter)
    {
        if(inter.neededItem != null)
        {
            if (inventory.Contains(inter.neededItem))
            {
                if (inter.consumesItem)
                {
                    inventory.Remove(inter.neededItem);
                    if(inter.neededItemType == HDO_InteractionSO.CardType.red)
                    {
                        redCard.enabled = false;
                    }
                    if(inter.neededItemType == HDO_InteractionSO.CardType.blue)
                    {
                        blueCard.enabled = false;
                    }
                    if(inter.neededItemType == HDO_InteractionSO.CardType.lever)
                    {
                        if(inventory.Count == 0)
                        {
                            lever.enabled = false;
                        }
                        
                    }
                }

                foreach (HDO_Interactive interactive in interaction.interactives)
                {
                    interactive.Action();
                }
            }
            else
            {
                if (inter.isUnique)
                {
                    failed = true;
                }
            }
        }
        else
        {
            foreach (HDO_Interactive interactive in interaction.interactives)
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

        if(interaction == null)
        {
            Debug.Log("euh wtf ???");
            return;
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

  
    IEnumerator dialogSuite(List<string> list, HDO_InteractionSO inter)
    {
        
        cts.defaultText = list[it];
        cts.Read();
        it++;
        

        yield return new WaitUntil(() => cts.defaultText == "");
        if (!(it >= list.Count))
        {
            StartCoroutine(dialogSuite(list, inter));
        }
        else
        {
            it = 0;
        }

    }

    

}
