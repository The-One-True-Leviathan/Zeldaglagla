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
                    ""expectedControlType"": """",
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
        },
        {
            ""name"": ""KBControlsWASD"",
            ""id"": ""f1dd3c0f-1cca-43e2-bfb7-dfc905f2a0ad"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""0172ef9d-786c-4194-9b44-cd6fe7c64a28"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""f95d96b5-8035-4a8c-8290-73ee61e847c2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""640bea2c-af29-4b6d-85e7-bb92a693efea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dodge"",
                    ""type"": ""Button"",
                    ""id"": ""6f6952a1-fc2c-4eab-b9f6-3a9689d71543"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Deflect"",
                    ""type"": ""Button"",
                    ""id"": ""4603c372-a8a1-46e1-abf1-7e7d4c55c696"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Heatwave"",
                    ""type"": ""Button"",
                    ""id"": ""63b690db-d720-4d0f-b4ef-095dcbff0118"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Torch"",
                    ""type"": ""Button"",
                    ""id"": ""64c39b03-5bbd-4ffd-964f-493146ebd87f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""inputVector"",
                    ""id"": ""11beefa2-fcde-4db8-8909-0b524d693ca4"",
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
                    ""id"": ""11831fc6-d0c9-491f-812d-fb802c63e9f6"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""38c12656-b4a6-4584-a7ac-29653a3c81b1"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""37971cc4-d0b3-43ce-8c4a-21eb8878bbf8"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""03fae6ba-e574-43d5-b4e4-c946907f5e67"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""df21dda9-af23-4c2b-b539-d7edbb652315"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6a60788b-8fed-48aa-a2b3-f6d2aeab39dc"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f82c48f6-353b-4101-a727-6a504015eef1"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3f9e0840-5361-4a82-82a9-44845b02c6a1"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Deflect"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0fc8de26-277c-44fc-bc0d-121e8c87b102"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Heatwave"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7b6fa65-acc8-41a1-8fb5-fa2a69194b41"",
                    ""path"": ""<Keyboard>/shift"",
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
        // KBControlsWASD
        m_KBControlsWASD = asset.FindActionMap("KBControlsWASD", throwIfNotFound: true);
        m_KBControlsWASD_Movement = m_KBControlsWASD.FindAction("Movement", throwIfNotFound: true);
        m_KBControlsWASD_Interact = m_KBControlsWASD.FindAction("Interact", throwIfNotFound: true);
        m_KBControlsWASD_Attack = m_KBControlsWASD.FindAction("Attack", throwIfNotFound: true);
        m_KBControlsWASD_Dodge = m_KBControlsWASD.FindAction("Dodge", throwIfNotFound: true);
        m_KBControlsWASD_Deflect = m_KBControlsWASD.FindAction("Deflect", throwIfNotFound: true);
        m_KBControlsWASD_Heatwave = m_KBControlsWASD.FindAction("Heatwave", throwIfNotFound: true);
        m_KBControlsWASD_Torch = m_KBControlsWASD.FindAction("Torch", throwIfNotFound: true);
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

    // KBControlsWASD
    private readonly InputActionMap m_KBControlsWASD;
    private IKBControlsWASDActions m_KBControlsWASDActionsCallbackInterface;
    private readonly InputAction m_KBControlsWASD_Movement;
    private readonly InputAction m_KBControlsWASD_Interact;
    private readonly InputAction m_KBControlsWASD_Attack;
    private readonly InputAction m_KBControlsWASD_Dodge;
    private readonly InputAction m_KBControlsWASD_Deflect;
    private readonly InputAction m_KBControlsWASD_Heatwave;
    private readonly InputAction m_KBControlsWASD_Torch;
    public struct KBControlsWASDActions
    {
        private @Controls m_Wrapper;
        public KBControlsWASDActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_KBControlsWASD_Movement;
        public InputAction @Interact => m_Wrapper.m_KBControlsWASD_Interact;
        public InputAction @Attack => m_Wrapper.m_KBControlsWASD_Attack;
        public InputAction @Dodge => m_Wrapper.m_KBControlsWASD_Dodge;
        public InputAction @Deflect => m_Wrapper.m_KBControlsWASD_Deflect;
        public InputAction @Heatwave => m_Wrapper.m_KBControlsWASD_Heatwave;
        public InputAction @Torch => m_Wrapper.m_KBControlsWASD_Torch;
        public InputActionMap Get() { return m_Wrapper.m_KBControlsWASD; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KBControlsWASDActions set) { return set.Get(); }
        public void SetCallbacks(IKBControlsWASDActions instance)
        {
            if (m_Wrapper.m_KBControlsWASDActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_KBControlsWASDActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_KBControlsWASDActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_KBControlsWASDActionsCallbackInterface.OnMovement;
                @Interact.started -= m_Wrapper.m_KBControlsWASDActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_KBControlsWASDActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_KBControlsWASDActionsCallbackInterface.OnInteract;
                @Attack.started -= m_Wrapper.m_KBControlsWASDActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_KBControlsWASDActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_KBControlsWASDActionsCallbackInterface.OnAttack;
                @Dodge.started -= m_Wrapper.m_KBControlsWASDActionsCallbackInterface.OnDodge;
                @Dodge.performed -= m_Wrapper.m_KBControlsWASDActionsCallbackInterface.OnDodge;
                @Dodge.canceled -= m_Wrapper.m_KBControlsWASDActionsCallbackInterface.OnDodge;
                @Deflect.started -= m_Wrapper.m_KBControlsWASDActionsCallbackInterface.OnDeflect;
                @Deflect.performed -= m_Wrapper.m_KBControlsWASDActionsCallbackInterface.OnDeflect;
                @Deflect.canceled -= m_Wrapper.m_KBControlsWASDActionsCallbackInterface.OnDeflect;
                @Heatwave.started -= m_Wrapper.m_KBControlsWASDActionsCallbackInterface.OnHeatwave;
                @Heatwave.performed -= m_Wrapper.m_KBControlsWASDActionsCallbackInterface.OnHeatwave;
                @Heatwave.canceled -= m_Wrapper.m_KBControlsWASDActionsCallbackInterface.OnHeatwave;
                @Torch.started -= m_Wrapper.m_KBControlsWASDActionsCallbackInterface.OnTorch;
                @Torch.performed -= m_Wrapper.m_KBControlsWASDActionsCallbackInterface.OnTorch;
                @Torch.canceled -= m_Wrapper.m_KBControlsWASDActionsCallbackInterface.OnTorch;
            }
            m_Wrapper.m_KBControlsWASDActionsCallbackInterface = instance;
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
    public KBControlsWASDActions @KBControlsWASD => new KBControlsWASDActions(this);
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
    public interface IKBControlsWASDActions
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
