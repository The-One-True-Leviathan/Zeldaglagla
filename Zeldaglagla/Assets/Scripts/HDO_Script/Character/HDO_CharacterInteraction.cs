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

    int detectedObjs;
    [SerializeField]
    float addPos;

    HDO_AssociatedDialog ad;
    CoolTextScript cts;

    [SerializeField]
    Text EVAADialog;


    private void Awake()
    {

    }

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
    }

    public void Interact()
    {

        if(interactObject = null)
        {
            return;
        }

        SpawnDialog();

        Debug.Log("everything is fine");
    }

    void SpawnDialog()
    {
        ad = interactobj.GetComponent<HDO_AssociatedDialog>();

        if (ad.type == HDO_AssociatedDialog.dialogType.inner)
        {

        }
        else
        {
            if(ad.number == HDO_AssociatedDialog.dialogNum.one)
            {               
                cts = EVAADialog.GetComponent<CoolTextScript>();
                cts.defaultText = ad.dialogs[0];
                cts.Read();
            }
            else
            {
                cts = EVAADialog.GetComponent<CoolTextScript>();
                cts.defaultText = ad.dialogs[Random.Range(0, ad.dialogs.Count - 1)];
                cts.Read();
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
            selector.transform.position = result[0].transform.position + new Vector3(0, addPos, 0);
            selectorSr.enabled = true;
            selectorSr.color = new Color(selectorSr.color.r, selectorSr.color.g, selectorSr.color.b, (1 / (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(interactObject.transform.position.x, interactObject.transform.position.y)))));
            Debug.Log(1 /((Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(interactObject.transform.position.x, interactObject.transform.position.y)))));

        }
        else
        {

            float minDist = 0;

            for(int i = 0; i < result.Count - 1; i++)
            {
                if(i == 0)
                {
                    minDist = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(result[0].transform.position.x, result[0].transform.position.y));
                }
                else
                {
                    if(Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(result[i].transform.position.x, result[i].transform.position.y)) < minDist)
                    {
                        minDist = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(result[i].transform.position.x, result[i].transform.position.y));
                        interactObject = result[i];
                    }
                }
            }

            selector.transform.position = interactObject.transform.position + new Vector3(0, addPos, 0);
            selectorSr.enabled = true;
            
        }
    }

    IEnumerator SelectorFade()
    {
        if(detectedObjs == 0)
        {
            selectorSr.color = new Color(selectorSr.color.r, selectorSr.color.g, selectorSr.color.b, selectorSr.color.a / 1.05f);
            Debug.Log("passed");
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
