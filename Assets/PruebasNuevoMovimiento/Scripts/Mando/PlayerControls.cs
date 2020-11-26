//// GENERATED AUTOMATICALLY FROM 'Assets/PruebasNuevoMovimiento/Scripts/Mando/PlayerControls.inputactions'

//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.InputSystem;
//using UnityEngine.InputSystem.Utilities;

//public class @PlayerControls : IInputActionCollection, IDisposable
//{
//    public InputActionAsset asset { get; }
//    public @PlayerControls()
//    {
//        asset = InputActionAsset.FromJson(@"{
//    ""name"": ""PlayerControls"",
//    ""maps"": [
//        {
//            ""name"": ""Gameplay"",
//            ""id"": ""9dc7577b-0b4e-487c-953d-ffe9a412ff73"",
//            ""actions"": [
//                {
//                    ""name"": ""Salto"",
//                    ""type"": ""Button"",
//                    ""id"": ""41872e38-3d66-4777-af70-51e432aa7038"",
//                    ""expectedControlType"": ""Button"",
//                    ""processors"": """",
//                    ""interactions"": """"
//                },
//                {
//                    ""name"": ""Movement"",
//                    ""type"": ""Value"",
//                    ""id"": ""8a65bc7d-4695-4a72-9d3d-bc23f6314286"",
//                    ""expectedControlType"": """",
//                    ""processors"": """",
//                    ""interactions"": """"
//                },
//                {
//                    ""name"": ""Dash"",
//                    ""type"": ""Button"",
//                    ""id"": ""33ff2e84-71e6-4ecf-a240-dc69a3a97c17"",
//                    ""expectedControlType"": ""Button"",
//                    ""processors"": """",
//                    ""interactions"": """"
//                }
//            ],
//            ""bindings"": [
//                {
//                    ""name"": """",
//                    ""id"": ""4d24bf66-ec48-41dd-a306-5560d122ce20"",
//                    ""path"": ""<Gamepad>/buttonSouth"",
//                    ""interactions"": """",
//                    ""processors"": """",
//                    ""groups"": """",
//                    ""action"": ""Salto"",
//                    ""isComposite"": false,
//                    ""isPartOfComposite"": false
//                },
//                {
//                    ""name"": """",
//                    ""id"": ""7f38bc05-daa3-4097-8f00-3fdf574b59b5"",
//                    ""path"": ""<Gamepad>/buttonEast"",
//                    ""interactions"": """",
//                    ""processors"": """",
//                    ""groups"": """",
//                    ""action"": ""Dash"",
//                    ""isComposite"": false,
//                    ""isPartOfComposite"": false
//                },
//                {
//                    ""name"": ""1D Axis"",
//                    ""id"": ""990950ed-22b8-4c8c-ac2f-1f94efab7e4d"",
//                    ""path"": ""1DAxis(minValue=0)"",
//                    ""interactions"": """",
//                    ""processors"": ""Clamp(min=-1,max=1)"",
//                    ""groups"": """",
//                    ""action"": ""Movement"",
//                    ""isComposite"": true,
//                    ""isPartOfComposite"": false
//                },
//                {
//                    ""name"": ""negative"",
//                    ""id"": ""26646249-2049-4b9e-b812-971a72629fd2"",
//                    ""path"": ""<Gamepad>/leftStick/left"",
//                    ""interactions"": """",
//                    ""processors"": """",
//                    ""groups"": """",
//                    ""action"": ""Movement"",
//                    ""isComposite"": false,
//                    ""isPartOfComposite"": true
//                },
//                {
//                    ""name"": ""positive"",
//                    ""id"": ""600fe08a-8554-4c6d-b1ac-ad6c739741a7"",
//                    ""path"": ""<Gamepad>/leftStick/right"",
//                    ""interactions"": """",
//                    ""processors"": """",
//                    ""groups"": """",
//                    ""action"": ""Movement"",
//                    ""isComposite"": false,
//                    ""isPartOfComposite"": true
//                }
//            ]
//        }
//    ],
//    ""controlSchemes"": []
//}");
//        // Gameplay
//        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
//        m_Gameplay_Salto = m_Gameplay.FindAction("Salto", throwIfNotFound: true);
//        m_Gameplay_Movement = m_Gameplay.FindAction("Movement", throwIfNotFound: true);
//        m_Gameplay_Dash = m_Gameplay.FindAction("Dash", throwIfNotFound: true);
//    }

//    public void Dispose()
//    {
//        UnityEngine.Object.Destroy(asset);
//    }

//    public InputBinding? bindingMask
//    {
//        get => asset.bindingMask;
//        set => asset.bindingMask = value;
//    }

//    public ReadOnlyArray<InputDevice>? devices
//    {
//        get => asset.devices;
//        set => asset.devices = value;
//    }

//    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

//    public bool Contains(InputAction action)
//    {
//        return asset.Contains(action);
//    }

//    public IEnumerator<InputAction> GetEnumerator()
//    {
//        return asset.GetEnumerator();
//    }

//    IEnumerator IEnumerable.GetEnumerator()
//    {
//        return GetEnumerator();
//    }

//    public void Enable()
//    {
//        asset.Enable();
//    }

//    public void Disable()
//    {
//        asset.Disable();
//    }

//    // Gameplay
//    private readonly InputActionMap m_Gameplay;
//    private IGameplayActions m_GameplayActionsCallbackInterface;
//    private readonly InputAction m_Gameplay_Salto;
//    private readonly InputAction m_Gameplay_Movement;
//    private readonly InputAction m_Gameplay_Dash;
//    public struct GameplayActions
//    {
//        private @PlayerControls m_Wrapper;
//        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
//        public InputAction @Salto => m_Wrapper.m_Gameplay_Salto;
//        public InputAction @Movement => m_Wrapper.m_Gameplay_Movement;
//        public InputAction @Dash => m_Wrapper.m_Gameplay_Dash;
//        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
//        public void Enable() { Get().Enable(); }
//        public void Disable() { Get().Disable(); }
//        public bool enabled => Get().enabled;
//        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
//        public void SetCallbacks(IGameplayActions instance)
//        {
//            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
//            {
//                @Salto.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSalto;
//                @Salto.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSalto;
//                @Salto.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSalto;
//                @Movement.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
//                @Movement.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
//                @Movement.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
//                @Dash.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
//                @Dash.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
//                @Dash.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
//            }
//            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
//            if (instance != null)
//            {
//                @Salto.started += instance.OnSalto;
//                @Salto.performed += instance.OnSalto;
//                @Salto.canceled += instance.OnSalto;
//                @Movement.started += instance.OnMovement;
//                @Movement.performed += instance.OnMovement;
//                @Movement.canceled += instance.OnMovement;
//                @Dash.started += instance.OnDash;
//                @Dash.performed += instance.OnDash;
//                @Dash.canceled += instance.OnDash;
//            }
//        }
//    }
//    public GameplayActions @Gameplay => new GameplayActions(this);
//    public interface IGameplayActions
//    {
//        void OnSalto(InputAction.CallbackContext context);
//        void OnMovement(InputAction.CallbackContext context);
//        void OnDash(InputAction.CallbackContext context);
//    }
//}
