using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HDO_CharacterCombat : MonoBehaviour
{
    Controls controls;
    HDO_Torch torch;
    [SerializeField]
    HDO_HeatManager hm;
    [SerializeField]
    HDO_Piolet piolet;
    

    Gamepad gamepad;

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
    [SerializeField]
    GameObject healthBar;
    Image healthimage;

    [Header("RespawnPoint")]
    public GameObject respawnPoint;

    [Header("Controller Vibration Stats")]
    [SerializeField]
    float lowFrequency;
    [SerializeField]
    float highFrequency, vibrationDuration;


    [Header("Combat Statistics")]
    public bool canStrike;
    public bool torchUnlocked, heatwaveUnlocked;
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

    public bool shallRespawn;

    [Header("Heatwave Statistics")]
    [SerializeField]
    GameObject heatwaveEffect;
    [SerializeField]
    GameObject heatZone;
    [SerializeField]
    Volume ppVolume;
    [SerializeField]
    float baseTemperature;
    [SerializeField]
    float wantedTemperature;
    [SerializeField]
    CinemachineFollowZoom camZoom;
    [SerializeField]
    float baseZoom;
    [SerializeField]
    float heatwaveZoom;
    
    public float heatwaveCost, heatwaveCooldown, heatwaveWaitTime;
    public bool heatwaving;

    bool temperatureReached, zoomReached;

    float heatwaveElapsed;

    public float temperatureIncreaseRate, temperatureDecreaseRate, zoomIncreaseRate, zoomDecreaseRate;

    WhiteBalance wb;

    public List<string> crossedScenes = null;
    public List<Vector3> spawnPoints = null;

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

        controls.UsualControls.Heatwave.performed += ctx => Heatwave();
        controls.KBControlsWASD.Heatwave.performed += ctx => Heatwave();

        controls.UsualControls.Deflect.performed += ctx => Deflect();
        controls.KBControlsWASD.Deflect.performed += ctx => Deflect();
        controls.UsualControls.Deflect.canceled += ctx => piolet.shielding = false;
        controls.KBControlsWASD.Deflect.canceled += ctx => piolet.shielding = false;

        gamepad = Gamepad.current;

    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        healthimage = healthBar.GetComponent<Image>();

        ppVolume.profile.TryGet(out wb);
        baseTemperature = wb.temperature.value;

        baseZoom = camZoom.m_MaxFOV;

        Scene sc = SceneManager.GetActiveScene();
        string nameSc = sc.name;
        if (crossedScenes.Contains(nameSc))
        {
            transform.position = spawnPoints[crossedScenes.IndexOf(nameSc)];
        }
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

        if (heatwaveElapsed > 0)
        {
            heatwaveElapsed -= Time.deltaTime;
        }
        else
        {
            heatwaveElapsed = 0;
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

        if(currentHealth <= 0 && !animator.GetBool("Dead"))
        {
            Die();
        }

        healthimage.fillAmount = currentHealth / maxHealth;
    }


    void Heatwave()
    {
        if(heatwaveElapsed > 0 || hm.heatValue < heatwaveCost || !heatwaveUnlocked)
        {
            return;
        }

        hm.heatValue -= heatwaveCost;

        heatwaving = true;
        heatwaveElapsed = heatwaveCooldown;

        Instantiate(heatwaveEffect, transform.position, heatwaveEffect.transform.rotation);
        Instantiate(heatZone, transform.position, Quaternion.identity);

        StartCoroutine(Heatwaving());
    }

    IEnumerator Heatwaving()
    {
        Debug.Log("Start Heatwaving");

        if(wb.temperature.value < wantedTemperature)
        {
            wb.temperature.value = wb.temperature.value + temperatureIncreaseRate;
            temperatureReached = false;
        }
        else
        {
            temperatureReached = true;
            wb.temperature.value = wantedTemperature;
        }

        if(camZoom.m_MaxFOV < heatwaveZoom)
        {
            camZoom.m_MaxFOV = camZoom.m_MaxFOV + zoomIncreaseRate;
            zoomReached = false;
        }
        else
        {
            zoomReached = true;
            camZoom.m_MaxFOV = heatwaveZoom;
        }

        yield return new WaitForEndOfFrame();

        if(zoomReached && temperatureReached)
        {
            StartCoroutine(WaitPhaseTwo());
            StopCoroutine(Heatwaving());
        }
        else
        {
            StartCoroutine(Heatwaving());
        }
    }

    IEnumerator WaitPhaseTwo()
    {
        yield return new WaitForSeconds(heatwaveWaitTime);
        StartCoroutine(BackToNormal());
    }

    IEnumerator BackToNormal()
    {
        Debug.Log("BackTonormal");
        StopCoroutine(Heatwaving());

        if (wb.temperature.value > baseTemperature)
        {
            wb.temperature.value = wb.temperature.value - temperatureDecreaseRate;
            temperatureReached = false;
        }
        else
        {
            temperatureReached = true;
            wb.temperature.value = baseTemperature;
        }

        if (camZoom.m_MaxFOV > baseZoom)
        {
            camZoom.m_MaxFOV = camZoom.m_MaxFOV - zoomDecreaseRate;
            zoomReached = false;
        }
        else
        {
            zoomReached = true;
            camZoom.m_MaxFOV = baseZoom;
        }
        yield return new WaitForEndOfFrame();

        if(zoomReached && temperatureReached)
        {
            heatwaving = false;
        }
        else
        {
            StartCoroutine(BackToNormal());
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
        if(hm.heatValue < torchHeatCost || torchCDElapsed > 0 || !torchUnlocked)
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
        if(torch != null)
        {
            return;
        }

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
            if (gamepad != null)
            {
                gamepad.SetMotorSpeeds(lowFrequency, highFrequency);
                StartCoroutine(Vibration());
            }
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
        animator.SetBool("Dead", true);
        StartCoroutine(Respawn());
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

    IEnumerator Vibration()
    {
        yield return new WaitForSecondsRealtime(vibrationDuration);
        gamepad.ResetHaptics();
    }

    IEnumerator Respawn()
    {
        yield return new WaitUntil(() => shallRespawn);
        shallRespawn = false;
        animator.SetBool("Dead", false);
        currentHealth = maxHealth;
        transform.position = respawnPoint.transform.position;
    }

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
