using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

public class HDO_Piolet : MonoBehaviour
{
    [SerializeField]
    HDO_CharacterCombat cc;
    [SerializeField]
    HDO_GunPoint gp;

    [SerializeField]
    BoxCollider2D box;
    [SerializeField]
    ContactFilter2D enemy;
    [SerializeField]
    List<Collider2D> result = null;
    public List<Collider2D> ffed = null;

    [SerializeField]
    Animator animator;


    HDO_UniversalEnemy ue;

    [Header("FreezeFrame Stats")]

    [SerializeField]
    float ffScale; 
    [SerializeField]
    float ffRealTimeDuration;

    int damage;
    int willLoss;

    bool freeezeFraming;
    public bool resetFF;
    int checker;

    [Header("Shield")]
    [SerializeField]
    GameObject shield;
    BoxCollider2D shieldBox;
    [SerializeField]
    float perfectShieldTiming, shieldDecreaseRate, shieldRegrowRate, shieldMin, shieldMax;
    float elapsedTime;
    public bool shielding;

    // Start is called before the first frame update
    void Start()
    {
        damage = cc.attackDamage;
        shieldBox = shield.GetComponentInChildren<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCollision();

        if (resetFF)
        {
            ffed.Clear();
        }

        if (!shielding && shieldBox.size.x < shieldMax)
        {
            elapsedTime = perfectShieldTiming;
            shieldBox.size = new Vector2(shieldBox.size.x + shieldRegrowRate * Time.deltaTime, shieldBox.size.y);
            if(shieldBox.size.x > shieldMax)
            {
                shieldBox.size = new Vector2(shieldMax, shieldBox.size.y);
            }
        }

        if (shielding)
        {
            Defend();
        }

        Positioning();
    }

    void Positioning()
    {
        if (gp.up)
        {
            transform.localPosition = new Vector3(0.5f, 0.25f, 0);
            shield.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (gp.down) 
        { 
            transform.localPosition = new Vector3(-0.5f, -0.25f, 0);
            shield.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        if (gp.right) 
        { 
            transform.localPosition = new Vector3(0.25f, -0.5f, 0);
            shield.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        if (gp.left)
        { 
            transform.localPosition = new Vector3(-0.25f, 0.5f, 0);
            shield.transform.rotation = Quaternion.Euler(0, 0, 90);
        }

        if (gp.up && gp.left) 
        { 
            transform.localPosition = new Vector3(0.25f, 0.5f, 0);
            shield.transform.rotation = Quaternion.Euler(0, 0, 45);
        }
        if (gp.up && gp.right) 
        {
            transform.localPosition = new Vector3(0.5f, -0.25f, 0);
            shield.transform.rotation = Quaternion.Euler(0, 0, -45);
        }
        if (gp.down && gp.left) 
        { 
            transform.localPosition = new Vector3(-0.5f, 0.25f, 0);
            shield.transform.rotation = Quaternion.Euler(0, 0, 135);
        }
        if (gp.down && gp.right) 
        { 
            transform.localPosition = new Vector3(-0.25f, -0.5f, 0);
            shield.transform.rotation = Quaternion.Euler(0, 0, -135);
        }

    }

    public void Defend()
    {
        if (!shielding)
        {
            shieldBox.enabled = true;
            shielding = true;
        }
        else
        {
            shieldBox.size = new Vector2(shieldBox.size.x - shieldDecreaseRate * Time.deltaTime, shieldBox.size.y);
            if(shieldBox.size.x < shieldMin)
            {
                shieldBox.size = new Vector2(shieldMin, shieldBox.size.y);
            }
            elapsedTime -= Time.deltaTime;
        }
        
    }

    void CheckCollision()
    {
        int results = Physics2D.OverlapCollider(box, enemy, result);

        if(result != null)
        {
            //Debug.Log("touch�");

            foreach(Collider2D e in result)
            {
                if (!freeezeFraming && !(ffed.Contains(e)))
                {
                    StartCoroutine(FreezeFrame());
                    ffed.Add(e);
                    freeezeFraming = true;
                    ue = e.gameObject.GetComponent<HDO_UniversalEnemy>();
                    ue.currentHealthPoints -= damage;
                }
                else if (!(ffed.Contains(e)))
                {
                    ue = e.gameObject.GetComponent<HDO_UniversalEnemy>();
                    ue.currentHealthPoints -= damage;
                    ffed.Add(e);
                }
            }
        }
    }

    IEnumerator FreezeFrame()
    {
        Time.timeScale = 1 - ffScale;
        yield return new WaitForSecondsRealtime(ffRealTimeDuration);
        Time.timeScale = 1;
        freeezeFraming = false;
    }

    
}
