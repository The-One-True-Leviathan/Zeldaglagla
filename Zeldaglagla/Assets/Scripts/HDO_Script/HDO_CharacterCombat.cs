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

    [Header("Combat Statistics")]
    public int attackDamage;


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
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Torch()
    {
        if(hm.heatValue < torchHeatCost)
        {
            return;
        }
        hm.heatValue -= torchHeatCost;

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
