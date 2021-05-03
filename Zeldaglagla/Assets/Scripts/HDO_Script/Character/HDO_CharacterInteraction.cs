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

    [SerializeField]
    Text EVAADialog, innerDialog;

    [SerializeField]
    List<HDO_InteractionSO> toDoInteractions = null;

    [SerializeField]
    List<HDO_InteractionSO> doneUniqueInteraction = null;

    bool manualInteraction, triggerInteraction;

    private void Start()
    {
        selectorSr = selector.GetComponent<SpriteRenderer>();
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

        //toDoInteractions.Remove(toDoInteractions[0]);
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

        if (inter.isUnique)
        {
            doneUniqueInteraction.Add(inter);
        }
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
                cts = innerDialog.GetComponent<CoolTextScript>();
                cts.defaultText = inter.dialogs[Random.Range(0, inter.dialogs.Count)];
                cts.Read();
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
                cts = EVAADialog.GetComponent<CoolTextScript>();
                cts.defaultText = inter.dialogs[Random.Range(0, inter.dialogs.Count)];
                cts.Read();
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

  

}
