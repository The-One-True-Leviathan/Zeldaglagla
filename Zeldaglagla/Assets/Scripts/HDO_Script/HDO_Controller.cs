using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HDO_Controller : MonoBehaviour
{
    Controls controls;

    [SerializeField]
    HDO_CharacterCollision collision;
    [SerializeField]
    GameObject gunPoint;

    [Header("Movement Stats")]
    public int baseSpeed, dodgeSpeed;
    public float dodgeTime, dodgeSpeedDecreasePerFrame, dodgeCooldown;
    int actualSpeed, bonusSpeed, dodgeDivider;
    float dodgeElapse, dodgeCdElapsed;
    Vector2 movementVector;
    Vector3 inputVector, dodgeVector;

    bool dodging;
    public bool freeMovement;

    private void Awake()
    {
        controls = new Controls();

        controls.UsualControls.Movement.performed += ctx => movementVector = ctx.ReadValue<Vector2>();
        controls.UsualControls.Movement.canceled += ctx => movementVector = Vector2.zero;

        controls.KBControlsWASD.Movement.performed += ctx => movementVector = ctx.ReadValue<Vector2>();
        controls.KBControlsWASD.Movement.canceled += ctx => movementVector = Vector2.zero;

        controls.UsualControls.Dodge.performed += ctx => Dodge();

        controls.KBControlsWASD.Dodge.performed += ctx => Dodge();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dodgeCdElapsed > 0) dodgeCdElapsed -= Time.deltaTime;
        Movement();
    }

    void Dodge()
    {
        if(dodgeCdElapsed > 0)
        {
            return;
        }

        dodgeElapse = dodgeTime;
        dodgeDivider = 0;
        freeMovement = false;
        dodgeVector = new Vector3(movementVector.x, movementVector.y);
    }

    void Movement()
    {
        actualSpeed = baseSpeed + bonusSpeed;

        if (freeMovement)
        {
            inputVector = Vector3.ClampMagnitude((new Vector3(movementVector.x, movementVector.y, 0) * actualSpeed * Time.deltaTime), 1);
        }
        else
        {
            if(dodgeElapse > 0)
            {
                inputVector = Vector3.ClampMagnitude(dodgeVector * (dodgeSpeed - dodgeSpeedDecreasePerFrame * dodgeDivider) * Time.deltaTime, 1);
                dodgeDivider++;
                dodgeElapse -= Time.deltaTime;
            }
            else
            {
                dodgeCdElapsed = dodgeCooldown;
                freeMovement = true;
            }
        }
        inputVector = collision.RecalculateVector(inputVector);
        
        transform.position = transform.position + inputVector;

        if(inputVector != Vector3.zero) gunPoint.transform.localPosition = Vector3.Normalize(inputVector);
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
