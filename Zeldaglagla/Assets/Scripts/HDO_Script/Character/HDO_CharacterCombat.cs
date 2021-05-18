using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

public class HDO_CharacterCombat : MonoBehaviour
{
    Controls controls;
    HDO_Torch torch;
    [SerializeField]
    HDO_HeatManager hm;
    [SerializeField]
    HDO_Piolet piolet;

    //De la part de Pierre
    PCO_PlayerKnockback playerKnockback;
    [Header("Health")]
    public float maxHealth = 20, currentHealth = 20;
    [SerializeField]
    const float immunityTime = 0.4f;
    [SerializeField]
    float deflectImmunityFactor = 1;
    bool isInImmunity = false;
    //Fin des bidouillages de Pierre ;3


    [Header("Combat Statistics")]
    public bool canStrike;
    public float attackDamage, attackStun, attackStunDuration;
    [SerializeField]
    float attackCooldown;
    float elapsedTimeA;
    [SerializeField]
    Animator pioletAnim;
    Animator animator;

    [Header("Torch Statistics")]
    [SerializeField]
    GameObject shot;
    [SerializeField]
    GameObject gunPoint;
    public int torchDirectDamage;
    public int torchExplosionDamage;
    public float torchHeatCost, torchCooldown;
    public bool torching;
    float torchCDElapsed;

    private void Awake()
    {
        if (deflectImmunityFactor == 0)
        {
            deflectImmunityFactor = 0.0001f;
        }
        playerKnockback = GetComponent<PCO_PlayerKnockback>();

        controls = new Controls();

        controls.UsualControls.Torch.performed += ctx => Torch();
        controls.KBControlsWASD.Torch.performed += ctx => Torch();

        controls.UsualControls.Attack.performed += ctx => Strike();
        controls.KBControlsWASD.Attack.performed += ctx => Strike();

        controls.UsualControls.Deflect.performed += ctx => Deflect();
        controls.KBControlsWASD.Deflect.performed += ctx => Deflect();
        controls.UsualControls.Deflect.canceled += ctx => piolet.shielding = false;
        controls.KBControlsWASD.Deflect.canceled += ctx => piolet.shielding = false;

    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(torchCDElapsed > 0)
        {
            torchCDElapsed -= Time.deltaTime;
        }
        else
        {
            torchCDElapsed = 0;
        }

        if (!canStrike)
        {
            elapsedTimeA -= Time.deltaTime;
            if (elapsedTimeA <= 0)
            {
                elapsedTimeA = 0;
                canStrike = true;
            }
        }

        if (torching)
        {
            InstantiateTorch();
        }

        
    }

    void Deflect()
    {
        Debug.Log("Shield");
        if (piolet.Defend())
        {
            Immunity(immunityTime * deflectImmunityFactor);
        }
    }

    void Strike()
    {
        if (!canStrike)
        {           
            return;
        }

        Debug.Log("attack !");
        pioletAnim.SetFloat("gunPointX", gunPoint.transform.localPosition.x);
        pioletAnim.SetFloat("gunPointY", gunPoint.transform.localPosition.y);
        piolet.ffed.Clear();
        animator.SetTrigger("Piolet");
        pioletAnim.SetTrigger("Attack");
        canStrike = false;
        elapsedTimeA = attackCooldown;

    }

    void Torch()
    {
        if(hm.heatValue < torchHeatCost || torchCDElapsed > 0)
        {
            return;
        }
        hm.heatValue -= torchHeatCost;
        torchCDElapsed = torchCooldown;

        animator.SetTrigger("Torch");

    }

    void InstantiateTorch()
    {
        torching = false;
        torch = Instantiate(shot, transform.position, Quaternion.identity).GetComponent<HDO_Torch>();

        torch.torchDamage = torchDirectDamage;
        torch.explosionDamage = torchExplosionDamage;
        torch.movement = Vector3.Normalize(gunPoint.transform.position - transform.position);
    }

    //De la part de Pierre
    //Merci Pelo.
    public void TakeDamage(DamageStruct damage, Transform origin = null)
    {
        if (!isInImmunity)
        {
            currentHealth -= damage.dmg;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }
            else if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            if (damage.dmg > 0)
            {
                Immunity();
            }

            if (origin != null && damage.kb.str != 0)
            {
                Vector3 knockbackDirection = (transform.position - origin.position).normalized;
                knockbackDirection *= damage.kb.spd;
                playerKnockback.Knockback(knockbackDirection, damage.kb.lgt);
            }
        }
    }

    public void Die()
    {

    }

    public void Immunity(float imunTime = immunityTime)
    {
        StopCoroutine(ImmunityCoroutine());
        isInImmunity = false;
        StartCoroutine(ImmunityCoroutine(imunTime));
    }

    IEnumerator ImmunityCoroutine(float imunTime = immunityTime)
    {
        isInImmunity = true;
        yield return new WaitForSeconds(imunTime);
        isInImmunity = false;
    }
    //Fin des bidouillages de Pierre ;3


    private void OnDisable()
    {
        controls.UsualControls.Disable();
        controls.KBControlsWASD.Disable();
    }

    private void OnEnable()
    {
        controls.UsualControls.Enable();
        controls.KBControlsWASD.Enable();
    }
}
