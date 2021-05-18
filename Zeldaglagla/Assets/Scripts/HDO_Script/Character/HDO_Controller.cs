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
    HDO_CharacterInteraction interact;
    [SerializeField]
    GameObject gunPoint;

    [Header("Movement Stats")]
    public int baseSpeed, dodgeSpeed;
    public float dodgeTime, dodgeSpeedDecreasePerFrame, dodgeCooldown;
    int actualSpeed, bonusSpeed, dodgeDivider;
    float dodgeElapse, dodgeCdElapsed;
    Vector2 movementVector;
    Vector3 inputVector, dodgeVector, finalVector, knockbackVector;

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

        controls.UsualControls.Interact.performed += ctx => interact.Interact();

        controls.KBControlsWASD.Interact.performed += ctx => interact.Interact();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dodgeCdElapsed > 0) dodgeCdElapsed -= Time.deltaTime;
        Input();
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

    void Input()
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

        if(inputVector != Vector3.zero) gunPoint.transform.localPosition = Vector3.Normalize(inputVector);
    }

    void Movement()
    {
        finalVector = collision.RecalculateVector(inputVector + knockbackVector);
        transform.position = transform.position + finalVector;
    }

    public void Knockback(Vector3 knockback)
    {
        knockbackVector = knockback;
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
