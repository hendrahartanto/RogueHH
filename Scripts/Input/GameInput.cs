// GENERATED AUTOMATICALLY FROM 'Assets/Settings/Input/GameInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @GameInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameInput"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""bb5ccf2c-8bc5-4c48-9ed9-ebdb8cce4e23"",
            ""actions"": [
                {
                    ""name"": ""MouseClick"",
                    ""type"": ""Button"",
                    ""id"": ""8a351b24-df16-4c15-b422-8b275f5e05f4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""KeyboardSpace"",
                    ""type"": ""Button"",
                    ""id"": ""26251d51-a9ee-4e94-bcde-99902645ddc1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Keyboard_1"",
                    ""type"": ""Button"",
                    ""id"": ""29c454a6-320e-4291-9ca9-578d95fcf481"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Keyboard_2"",
                    ""type"": ""Button"",
                    ""id"": ""f958c5a1-542d-45ce-89ff-061e3919a2fe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Keyboard_3"",
                    ""type"": ""Button"",
                    ""id"": ""4e9e0f71-ce6f-4320-943f-b8a2263b4072"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8547dec6-8cb1-4a8c-9816-9f233763e28e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6ecc8f62-ff6c-4524-a0c1-901a8cd4a512"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""KeyboardSpace"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8bb69a8d-1bad-49e0-8cf8-6b6659175aec"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Keyboard_1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f868d370-bce5-467f-b7a7-96ae3e11f8c4"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Keyboard_2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c19fd632-9252-4346-96cd-8d9052471383"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Keyboard_3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_MouseClick = m_Gameplay.FindAction("MouseClick", throwIfNotFound: true);
        m_Gameplay_KeyboardSpace = m_Gameplay.FindAction("KeyboardSpace", throwIfNotFound: true);
        m_Gameplay_Keyboard_1 = m_Gameplay.FindAction("Keyboard_1", throwIfNotFound: true);
        m_Gameplay_Keyboard_2 = m_Gameplay.FindAction("Keyboard_2", throwIfNotFound: true);
        m_Gameplay_Keyboard_3 = m_Gameplay.FindAction("Keyboard_3", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_MouseClick;
    private readonly InputAction m_Gameplay_KeyboardSpace;
    private readonly InputAction m_Gameplay_Keyboard_1;
    private readonly InputAction m_Gameplay_Keyboard_2;
    private readonly InputAction m_Gameplay_Keyboard_3;
    public struct GameplayActions
    {
        private @GameInput m_Wrapper;
        public GameplayActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseClick => m_Wrapper.m_Gameplay_MouseClick;
        public InputAction @KeyboardSpace => m_Wrapper.m_Gameplay_KeyboardSpace;
        public InputAction @Keyboard_1 => m_Wrapper.m_Gameplay_Keyboard_1;
        public InputAction @Keyboard_2 => m_Wrapper.m_Gameplay_Keyboard_2;
        public InputAction @Keyboard_3 => m_Wrapper.m_Gameplay_Keyboard_3;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @MouseClick.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseClick;
                @MouseClick.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseClick;
                @MouseClick.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseClick;
                @KeyboardSpace.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnKeyboardSpace;
                @KeyboardSpace.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnKeyboardSpace;
                @KeyboardSpace.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnKeyboardSpace;
                @Keyboard_1.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnKeyboard_1;
                @Keyboard_1.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnKeyboard_1;
                @Keyboard_1.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnKeyboard_1;
                @Keyboard_2.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnKeyboard_2;
                @Keyboard_2.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnKeyboard_2;
                @Keyboard_2.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnKeyboard_2;
                @Keyboard_3.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnKeyboard_3;
                @Keyboard_3.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnKeyboard_3;
                @Keyboard_3.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnKeyboard_3;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MouseClick.started += instance.OnMouseClick;
                @MouseClick.performed += instance.OnMouseClick;
                @MouseClick.canceled += instance.OnMouseClick;
                @KeyboardSpace.started += instance.OnKeyboardSpace;
                @KeyboardSpace.performed += instance.OnKeyboardSpace;
                @KeyboardSpace.canceled += instance.OnKeyboardSpace;
                @Keyboard_1.started += instance.OnKeyboard_1;
                @Keyboard_1.performed += instance.OnKeyboard_1;
                @Keyboard_1.canceled += instance.OnKeyboard_1;
                @Keyboard_2.started += instance.OnKeyboard_2;
                @Keyboard_2.performed += instance.OnKeyboard_2;
                @Keyboard_2.canceled += instance.OnKeyboard_2;
                @Keyboard_3.started += instance.OnKeyboard_3;
                @Keyboard_3.performed += instance.OnKeyboard_3;
                @Keyboard_3.canceled += instance.OnKeyboard_3;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnMouseClick(InputAction.CallbackContext context);
        void OnKeyboardSpace(InputAction.CallbackContext context);
        void OnKeyboard_1(InputAction.CallbackContext context);
        void OnKeyboard_2(InputAction.CallbackContext context);
        void OnKeyboard_3(InputAction.CallbackContext context);
    }
}
