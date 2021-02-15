// GENERATED AUTOMATICALLY FROM 'Assets/Input System/HDO_InputSystem/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""UsualControls"",
            ""id"": ""27b4c2ff-8753-49c2-9381-ce73c928d3f3"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""57e6dcb4-3f7a-4d0e-9d90-7167d1f2092c"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""134e6492-5b5e-4609-adfc-28c774b22c85"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""e1f25e97-71ed-4ef0-b0b0-5260583403a7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dodge"",
                    ""type"": ""Button"",
                    ""id"": ""e2864285-9e59-4f8a-85a4-b4b7238a25d1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Deflect"",
                    ""type"": ""Button"",
                    ""id"": ""52c6a362-8f4c-4739-b48f-80e711d6b2d5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Heatwave"",
                    ""type"": ""Button"",
                    ""id"": ""aa60adcc-8787-4c16-bc03-c331d0c3e7eb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Torch"",
                    ""type"": ""Button"",
                    ""id"": ""c971fb67-67a1-48ff-8791-62fe3c02ac96"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""inputVector"",
                    ""id"": ""8a08aa8f-ca49-4df5-8995-3602906a5cc5"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""111457b0-432b-4734-a198-5851d8da1a96"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0aa8a3c2-d469-45b0-a232-e87f655976be"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1c604269-0af2-4367-b72e-4a833fa9a7b7"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6e4a0e41-e3f8-47ca-8e08-458adc95572a"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2f9574d0-d491-4890-a29e-3e2dcfc0cf4e"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""53d3de14-36eb-4986-ae0d-ae581d15520a"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aedd2684-526d-464d-b60b-95066be1dcde"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2aeeba2b-efb6-405c-9de6-fc0a75f71e26"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Deflect"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ff5fe772-554a-4146-ac5b-3ff44e049bbc"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Heatwave"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f8e105b8-a66f-457c-85cb-47a31f4684f0"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Torch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // UsualControls
        m_UsualControls = asset.FindActionMap("UsualControls", throwIfNotFound: true);
        m_UsualControls_Movement = m_UsualControls.FindAction("Movement", throwIfNotFound: true);
        m_UsualControls_Interact = m_UsualControls.FindAction("Interact", throwIfNotFound: true);
        m_UsualControls_Attack = m_UsualControls.FindAction("Attack", throwIfNotFound: true);
        m_UsualControls_Dodge = m_UsualControls.FindAction("Dodge", throwIfNotFound: true);
        m_UsualControls_Deflect = m_UsualControls.FindAction("Deflect", throwIfNotFound: true);
        m_UsualControls_Heatwave = m_UsualControls.FindAction("Heatwave", throwIfNotFound: true);
        m_UsualControls_Torch = m_UsualControls.FindAction("Torch", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // UsualControls
    private readonly InputActionMap m_UsualControls;
    private IUsualControlsActions m_UsualControlsActionsCallbackInterface;
    private readonly InputAction m_UsualControls_Movement;
    private readonly InputAction m_UsualControls_Interact;
    private readonly InputAction m_UsualControls_Attack;
    private readonly InputAction m_UsualControls_Dodge;
    private readonly InputAction m_UsualControls_Deflect;
    private readonly InputAction m_UsualControls_Heatwave;
    private readonly InputAction m_UsualControls_Torch;
    public struct UsualControlsActions
    {
        private @Controls m_Wrapper;
        public UsualControlsActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_UsualControls_Movement;
        public InputAction @Interact => m_Wrapper.m_UsualControls_Interact;
        public InputAction @Attack => m_Wrapper.m_UsualControls_Attack;
        public InputAction @Dodge => m_Wrapper.m_UsualControls_Dodge;
        public InputAction @Deflect => m_Wrapper.m_UsualControls_Deflect;
        public InputAction @Heatwave => m_Wrapper.m_UsualControls_Heatwave;
        public InputAction @Torch => m_Wrapper.m_UsualControls_Torch;
        public InputActionMap Get() { return m_Wrapper.m_UsualControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UsualControlsActions set) { return set.Get(); }
        public void SetCallbacks(IUsualControlsActions instance)
        {
            if (m_Wrapper.m_UsualControlsActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_UsualControlsActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_UsualControlsActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_UsualControlsActionsCallbackInterface.OnMovement;
                @Interact.started -= m_Wrapper.m_UsualControlsActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_UsualControlsActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_UsualControlsActionsCallbackInterface.OnInteract;
                @Attack.started -= m_Wrapper.m_UsualControlsActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_UsualControlsActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_UsualControlsActionsCallbackInterface.OnAttack;
                @Dodge.started -= m_Wrapper.m_UsualControlsActionsCallbackInterface.OnDodge;
                @Dodge.performed -= m_Wrapper.m_UsualControlsActionsCallbackInterface.OnDodge;
                @Dodge.canceled -= m_Wrapper.m_UsualControlsActionsCallbackInterface.OnDodge;
                @Deflect.started -= m_Wrapper.m_UsualControlsActionsCallbackInterface.OnDeflect;
                @Deflect.performed -= m_Wrapper.m_UsualControlsActionsCallbackInterface.OnDeflect;
                @Deflect.canceled -= m_Wrapper.m_UsualControlsActionsCallbackInterface.OnDeflect;
                @Heatwave.started -= m_Wrapper.m_UsualControlsActionsCallbackInterface.OnHeatwave;
                @Heatwave.performed -= m_Wrapper.m_UsualControlsActionsCallbackInterface.OnHeatwave;
                @Heatwave.canceled -= m_Wrapper.m_UsualControlsActionsCallbackInterface.OnHeatwave;
                @Torch.started -= m_Wrapper.m_UsualControlsActionsCallbackInterface.OnTorch;
                @Torch.performed -= m_Wrapper.m_UsualControlsActionsCallbackInterface.OnTorch;
                @Torch.canceled -= m_Wrapper.m_UsualControlsActionsCallbackInterface.OnTorch;
            }
            m_Wrapper.m_UsualControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Dodge.started += instance.OnDodge;
                @Dodge.performed += instance.OnDodge;
                @Dodge.canceled += instance.OnDodge;
                @Deflect.started += instance.OnDeflect;
                @Deflect.performed += instance.OnDeflect;
                @Deflect.canceled += instance.OnDeflect;
                @Heatwave.started += instance.OnHeatwave;
                @Heatwave.performed += instance.OnHeatwave;
                @Heatwave.canceled += instance.OnHeatwave;
                @Torch.started += instance.OnTorch;
                @Torch.performed += instance.OnTorch;
                @Torch.canceled += instance.OnTorch;
            }
        }
    }
    public UsualControlsActions @UsualControls => new UsualControlsActions(this);
    public interface IUsualControlsActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnDodge(InputAction.CallbackContext context);
        void OnDeflect(InputAction.CallbackContext context);
        void OnHeatwave(InputAction.CallbackContext context);
        void OnTorch(InputAction.CallbackContext context);
    }
}
