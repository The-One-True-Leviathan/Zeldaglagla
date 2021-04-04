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

    [Header("Combat Statistics")]
    public bool canStrike;
    public int attackDamage;
    [SerializeField]
    float attackCooldown;
    float elapsedTimeA;
    [SerializeField]
    Animator pioletAnim;

    [Header("Torch Statistics")]
    [SerializeField]
    GameObject shot;
    [SerializeField]
    GameObject gunPoint;
    public int torchDirectDamage;
    public int torchExplosionDamage;
    public float torchHeatCost, torchCooldown;
    float torchCDElapsed;

    private void Awake()
    {
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

        
    }

    void Deflect()
    {
        Debug.Log("Shield");
        piolet.Defend();

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

        torch = Instantiate(shot, transform.position, Quaternion.identity).GetComponent<HDO_Torch>();

        torch.torchDamage = torchDirectDamage;
        torch.explosionDamage = torchExplosionDamage;
        torch.movement = Vector3.Normalize(gunPoint.transform.position - transform.position);


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
