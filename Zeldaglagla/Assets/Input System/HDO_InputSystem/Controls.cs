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
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // UsualControls
        m_UsualControls = asset.FindActionMap("UsualControls", throwIfNotFound: true);
        m_UsualControls_Movement = m_UsualControls.FindAction("Movement", throwIfNotFound: true);
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
    public struct UsualControlsActions
    {
        private @Controls m_Wrapper;
        public UsualControlsActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_UsualControls_Movement;
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
            }
            m_Wrapper.m_UsualControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
            }
        }
    }
    public UsualControlsActions @UsualControls => new UsualControlsActions(this);
    public interface IUsualControlsActions
    {
        void OnMovement(InputAction.CallbackContext context);
    }
}
