using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using Monsters;
using UnityEngine.InputSystem;

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
    MonsterRoot mr;
    DamageStruct dam;
    StunStruct stun, shieldStun;

    [Header("FreezeFrame Stats")]
    [SerializeField]
    float ffScale; 
    [SerializeField]
    float ffRealTimeDuration;

    float damage;
    float stunAmount, stunLength;

    bool freeezeFraming;
    public bool resetFF;
    int checker;

    Gamepad gamepad;

    [Header("Controller Vibration Stats")]
    [SerializeField]
    float lowFrequency;
    [SerializeField]
    float highFrequency, duration;

    [Header("Shield")]
    [SerializeField]
    GameObject shield;
    BoxCollider2D shieldBox;
    [SerializeField]
    float perfectShieldTiming, shieldDecreaseRate, shieldRegrowRate, shieldMin, shieldMax, shieldStunAmount, shieldStunLength;
    float elapsedTime;
    public bool shielding, protecting;

    // Start is called before the first frame update
    void Start()
    {
        damage = cc.attackDamage;
        stunLength = cc.attackStunDuration;
        stunAmount = cc.attackStun;

        shieldBox = shield.GetComponentInChildren<BoxCollider2D>();
        
        stun = new StunStruct(stunAmount, stunLength);
        shieldStun = new StunStruct(shieldStunAmount, shieldStunLength);
        dam = new DamageStruct(damage);

        gamepad = Gamepad.current;
    }

    // Update is called once per frame
    void Update()
    {
        if(damage != cc.attackDamage)
        {
            damage = cc.attackDamage;
        }

        if (stunAmount != cc.attackStun)
        {
            stunAmount = cc.attackStun;
        }

        if (stunLength != cc.attackStunDuration)
        {
            stunLength = cc.attackStunDuration;
        }

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

    public bool Defend()
    {
        bool returnValue = false;
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

            int results = Physics2D.OverlapCollider(shieldBox, enemy, result);

            foreach (Collider2D monster in result)
            {
                MonsterRoot mr;
                mr = monster.GetComponent<MonsterRoot>();

                if (mr.isInAttack)
                {
                    protecting = true;
                    Debug.Log("protecting");
                    if(elapsedTime >= 0)
                    {
                        Debug.Log("stun !");
                        mr.Stun(shieldStun);
                        returnValue = true;
                    }
                }
            }

            elapsedTime -= Time.deltaTime;
        }
        return returnValue;
    }

    void CheckCollision()
    {
        int results = Physics2D.OverlapCollider(box, enemy, result);

        if(result != null)
        {
            //Debug.Log("touché");

            foreach(Collider2D e in result)
            {
                if (!freeezeFraming && !(ffed.Contains(e)))
                {
                    if (gamepad != null)
                    {
                        gamepad.SetMotorSpeeds(lowFrequency, highFrequency);
                        StartCoroutine(Vibration());
                    }
                    StartCoroutine(FreezeFrame());
                    ffed.Add(e);
                    freeezeFraming = true;
                    mr = e.gameObject.GetComponent<MonsterRoot>();
                    mr.Damage(dam);
                }
                else if (!(ffed.Contains(e)))
                {
                    mr = e.gameObject.GetComponent<MonsterRoot>();
                    mr.Damage(dam);
                    ffed.Add(e);
                }
            }
        }
    }

    IEnumerator Vibration()
    {
        yield return new WaitForSecondsRealtime(duration);
        gamepad.ResetHaptics();
    }

    IEnumerator FreezeFrame()
    {
        Time.timeScale = 1 - ffScale;
        yield return new WaitForSecondsRealtime(ffRealTimeDuration);
        Time.timeScale = 1;
        freeezeFraming = false;
    }

    
}
