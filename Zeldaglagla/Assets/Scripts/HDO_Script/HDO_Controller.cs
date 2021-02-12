using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HDO_Controller : MonoBehaviour
{
    Controls controls;

    [SerializeField]
    HDO_CharacterCollision collision;

    [Header("Movement Stats")]
    public int baseSpeed;
    int actualSpeed, bonusSpeed;
    Vector2 movementVector;

    private void Awake()
    {
        controls = new Controls();

        controls.UsualControls.Movement.performed += ctx => movementVector = ctx.ReadValue<Vector2>();
        controls.UsualControls.Movement.canceled += ctx => movementVector = Vector2.zero;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        Vector3 inputVector;
        actualSpeed = baseSpeed + bonusSpeed;

        inputVector = Vector3.ClampMagnitude((new Vector3(movementVector.x, movementVector.y, 0) * actualSpeed * Time.deltaTime), 1);
        inputVector = collision.RecalculateVector(inputVector);
        
        transform.position = transform.position + inputVector;
    }

    private void OnDisable()
    {
        controls.UsualControls.Disable();
    }

    private void OnEnable()
    {
        controls.UsualControls.Enable();
    }
}
