using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HDO_Controller : MonoBehaviour
{
    Controls controls;

    [SerializeField]
    HDO_CharacterCollision collision;
    [SerializeField]
    HDO_CharacterInteraction interact;
    [SerializeField]
    GameObject gunPoint, map;

    Animator animator;
    [SerializeField]
    SpriteRenderer pioletSR;
    SpriteRenderer spriteRenderer;


    [Header("Movement Stats")]
    public int baseSpeed, dodgeSpeed;
    public float dodgeTime, dodgeSpeedDecreasePerFrame, dodgeCooldown;
    int actualSpeed, bonusSpeed, dodgeDivider;
    float dodgeElapse, dodgeCdElapsed;
    Vector2 movementVector;
    Vector3 inputVector, dodgeVector, finalVector, knockbackVector;

    bool dodging;
    public bool freeMovement;
    bool mapDisplay;

    private void Awake()
    {
        controls = new Controls();

        controls.UsualControls.Movement.performed += ctx => movementVector = ctx.ReadValue<Vector2>();
        controls.UsualControls.Movement.canceled += ctx => movementVector = Vector2.zero;

        controls.KBControlsWASD.Movement.performed += ctx => movementVector = ctx.ReadValue<Vector2>();
        controls.KBControlsWASD.Movement.canceled += ctx => movementVector = Vector2.zero;

        controls.UsualControls.Map.performed += ctx => mapDisplay = !mapDisplay;
        controls.UsualControls.Map.performed += ctx => Map();

        controls.UsualControls.Dodge.performed += ctx => Dodge();

        controls.KBControlsWASD.Dodge.performed += ctx => Dodge();

        controls.UsualControls.Interact.performed += ctx => interact.Interact();

        controls.KBControlsWASD.Interact.performed += ctx => interact.Interact();
    }


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dodgeCdElapsed > 0) dodgeCdElapsed -= Time.deltaTime;
        Input();
        Movement();
    }

    void Map()
    {
        map.GetComponent<Image>().enabled = mapDisplay;
        
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

        //if(inputVector != Vector3.zero) gunPoint.transform.localPosition = Vector3.Normalize(inputVector);
    }

    void Movement()
    {
        finalVector = collision.RecalculateVector(inputVector + knockbackVector);

        if(finalVector != Vector3.zero && knockbackVector == Vector3.zero)
        {
            animator.SetFloat("speedY", finalVector.y);
            animator.SetFloat("speedX", gunPoint.transform.localPosition.x);
            if (finalVector.x >= 0 && finalVector.y <= 0)
            {
                spriteRenderer.flipX = false;
                pioletSR.flipX = false;
                gunPoint.transform.localPosition = Vector3.Normalize(new Vector3(1, -1, 0));
            }
            else if (finalVector.x > 0 && finalVector.y > 0)
            {
                spriteRenderer.flipX = true;
                pioletSR.flipX = true;
                gunPoint.transform.localPosition = Vector3.Normalize(new Vector3(1, 1, 0));
            }
            else if (finalVector.x < 0 && finalVector.y <= 0)
            {
                spriteRenderer.flipX = true;
                pioletSR.flipX = true;
                gunPoint.transform.localPosition = Vector3.Normalize(new Vector3(-1, -1, 0));
            }
            else if (finalVector.x <= 0 && finalVector.y > 0)
            {
                spriteRenderer.flipX = false;
                pioletSR.flipX = false;
                gunPoint.transform.localPosition = Vector3.Normalize(new Vector3(-1, 1, 0));
            }

            if(Mathf.Abs(finalVector.x) < 0.1f && Mathf.Abs(finalVector.y) > 0.75f)
            {
                gunPoint.transform.localPosition = Vector3.Normalize(new Vector3(-1, 1, 0));
            }

        }

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

    void Footstep()
    {
        FindObjectOfType<AudioManager>().Play("Footstep Snow");
        Debug.Log("Son marche");
    }

    void IceAxe()
    {
        FindObjectOfType<AudioManager>().Play("Ice Axe");
    }

    void Torch()
    {
        FindObjectOfType<AudioManager>().Play("Torch");
    }
}
