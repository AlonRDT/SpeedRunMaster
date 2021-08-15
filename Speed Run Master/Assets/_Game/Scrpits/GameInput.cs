// GENERATED AUTOMATICALLY FROM 'Assets/_Game/GameInput.inputactions'

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
            ""id"": ""08f428df-77ec-45f2-8493-9485f6ea0752"",
            ""actions"": [
                {
                    ""name"": ""MoveCar"",
                    ""type"": ""Value"",
                    ""id"": ""bdb9a3e7-606f-4347-9ca2-41612ab74a5e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Nitro"",
                    ""type"": ""Button"",
                    ""id"": ""048b66a4-b1b6-48e3-9db3-a8c8d8fd502a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ActivatePickup"",
                    ""type"": ""Button"",
                    ""id"": ""c14d3fcb-700d-4687-acb0-0fd13e3b51e9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchPickup"",
                    ""type"": ""Button"",
                    ""id"": ""10026b33-a8a8-4704-8de7-9782eccb8c25"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Break"",
                    ""type"": ""Button"",
                    ""id"": ""983a899d-4dfc-42a8-9998-6038662df000"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MenuBack"",
                    ""type"": ""Button"",
                    ""id"": ""e01315f1-1662-454e-bea6-c25a40f9580b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TurnCamera"",
                    ""type"": ""Value"",
                    ""id"": ""0be06b1d-f3f8-4984-85b9-eb347e10800b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TurnCar"",
                    ""type"": ""Value"",
                    ""id"": ""7f3a3995-f77b-4c4c-a6a1-d968463a7bad"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OpenPauseMenu"",
                    ""type"": ""Button"",
                    ""id"": ""6a3f825e-15ed-4345-b323-e4a04383ab51"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Controller"",
                    ""id"": ""8024b4e4-9516-4616-afdf-67b4de606377"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCar"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""b01f4f14-ce75-4859-822e-a8e004caac30"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""21fd516f-4e09-401e-bf40-a5abc2b4e9f5"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""e5c81eae-27f0-40d9-b507-b88044f0b451"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCar"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f1dd5376-99ab-4427-bb38-6497e9ff761f"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""6a77c445-09e9-42a8-978f-c8db8163489a"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""a654f75f-47a9-4d96-a8e0-e8c38cc1d48d"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCar"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""c36dc7e7-14c7-49a2-bafa-b4b04485c766"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""9e6dc757-2881-43a2-95f5-dc6797ad82e7"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6df9b5f7-a559-4cba-aa1b-cdb31d439f5b"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Nitro"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94703479-3cce-42be-a543-887a071324ac"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Nitro"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""74647280-3608-4c03-a721-4c9593536a81"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActivatePickup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""038b440f-b1f7-463f-b237-724b085eead8"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActivatePickup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1f2cca73-5363-4dc7-9218-a677fcc02689"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchPickup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""857c1a36-5237-438e-931b-1055dcf82794"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchPickup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cb185bd6-e475-4b4d-9b39-45785a9dd583"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Break"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""78b96ffd-908a-4b0f-8826-ce7af42045e0"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Break"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f89ab274-c965-4e16-8111-6e7191a93cca"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MenuBack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""763ddbb8-670c-48a9-affb-58b8966b5136"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone,NormalizeVector2"",
                    ""groups"": """",
                    ""action"": ""TurnCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d151b1a4-537b-4641-b574-f1a1d619d83f"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TurnCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1149fe5-87f8-4281-a865-c61ad79a220a"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenPauseMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb71a46e-2813-40a8-b9c4-a677883bcbdd"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenPauseMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6f04eaa5-1fbe-4feb-8bcf-038bd62ee818"",
                    ""path"": ""<Gamepad>/leftStick/x"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": """",
                    ""action"": ""TurnCar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""b5697516-1f24-4a95-a8c0-d41432249ada"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TurnCar"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""4b45699a-5f3d-4e76-bb18-91132a6ea6de"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TurnCar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""1b5c1b06-8b8b-49bd-ae79-7a79eab747af"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TurnCar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""3b865819-2946-4446-a1e4-45f3e7e2e3ff"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TurnCar"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""3e5e1c79-6f5c-4ddc-bfa0-7970ea2d491b"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TurnCar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""7f3d9e7a-cbb5-4401-9d0d-ed087310b152"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TurnCar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_MoveCar = m_Gameplay.FindAction("MoveCar", throwIfNotFound: true);
        m_Gameplay_Nitro = m_Gameplay.FindAction("Nitro", throwIfNotFound: true);
        m_Gameplay_ActivatePickup = m_Gameplay.FindAction("ActivatePickup", throwIfNotFound: true);
        m_Gameplay_SwitchPickup = m_Gameplay.FindAction("SwitchPickup", throwIfNotFound: true);
        m_Gameplay_Break = m_Gameplay.FindAction("Break", throwIfNotFound: true);
        m_Gameplay_MenuBack = m_Gameplay.FindAction("MenuBack", throwIfNotFound: true);
        m_Gameplay_TurnCamera = m_Gameplay.FindAction("TurnCamera", throwIfNotFound: true);
        m_Gameplay_TurnCar = m_Gameplay.FindAction("TurnCar", throwIfNotFound: true);
        m_Gameplay_OpenPauseMenu = m_Gameplay.FindAction("OpenPauseMenu", throwIfNotFound: true);
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
    private readonly InputAction m_Gameplay_MoveCar;
    private readonly InputAction m_Gameplay_Nitro;
    private readonly InputAction m_Gameplay_ActivatePickup;
    private readonly InputAction m_Gameplay_SwitchPickup;
    private readonly InputAction m_Gameplay_Break;
    private readonly InputAction m_Gameplay_MenuBack;
    private readonly InputAction m_Gameplay_TurnCamera;
    private readonly InputAction m_Gameplay_TurnCar;
    private readonly InputAction m_Gameplay_OpenPauseMenu;
    public struct GameplayActions
    {
        private @GameInput m_Wrapper;
        public GameplayActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveCar => m_Wrapper.m_Gameplay_MoveCar;
        public InputAction @Nitro => m_Wrapper.m_Gameplay_Nitro;
        public InputAction @ActivatePickup => m_Wrapper.m_Gameplay_ActivatePickup;
        public InputAction @SwitchPickup => m_Wrapper.m_Gameplay_SwitchPickup;
        public InputAction @Break => m_Wrapper.m_Gameplay_Break;
        public InputAction @MenuBack => m_Wrapper.m_Gameplay_MenuBack;
        public InputAction @TurnCamera => m_Wrapper.m_Gameplay_TurnCamera;
        public InputAction @TurnCar => m_Wrapper.m_Gameplay_TurnCar;
        public InputAction @OpenPauseMenu => m_Wrapper.m_Gameplay_OpenPauseMenu;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @MoveCar.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveCar;
                @MoveCar.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveCar;
                @MoveCar.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveCar;
                @Nitro.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNitro;
                @Nitro.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNitro;
                @Nitro.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNitro;
                @ActivatePickup.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActivatePickup;
                @ActivatePickup.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActivatePickup;
                @ActivatePickup.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActivatePickup;
                @SwitchPickup.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchPickup;
                @SwitchPickup.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchPickup;
                @SwitchPickup.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchPickup;
                @Break.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBreak;
                @Break.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBreak;
                @Break.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBreak;
                @MenuBack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMenuBack;
                @MenuBack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMenuBack;
                @MenuBack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMenuBack;
                @TurnCamera.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTurnCamera;
                @TurnCamera.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTurnCamera;
                @TurnCamera.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTurnCamera;
                @TurnCar.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTurnCar;
                @TurnCar.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTurnCar;
                @TurnCar.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTurnCar;
                @OpenPauseMenu.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnOpenPauseMenu;
                @OpenPauseMenu.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnOpenPauseMenu;
                @OpenPauseMenu.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnOpenPauseMenu;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveCar.started += instance.OnMoveCar;
                @MoveCar.performed += instance.OnMoveCar;
                @MoveCar.canceled += instance.OnMoveCar;
                @Nitro.started += instance.OnNitro;
                @Nitro.performed += instance.OnNitro;
                @Nitro.canceled += instance.OnNitro;
                @ActivatePickup.started += instance.OnActivatePickup;
                @ActivatePickup.performed += instance.OnActivatePickup;
                @ActivatePickup.canceled += instance.OnActivatePickup;
                @SwitchPickup.started += instance.OnSwitchPickup;
                @SwitchPickup.performed += instance.OnSwitchPickup;
                @SwitchPickup.canceled += instance.OnSwitchPickup;
                @Break.started += instance.OnBreak;
                @Break.performed += instance.OnBreak;
                @Break.canceled += instance.OnBreak;
                @MenuBack.started += instance.OnMenuBack;
                @MenuBack.performed += instance.OnMenuBack;
                @MenuBack.canceled += instance.OnMenuBack;
                @TurnCamera.started += instance.OnTurnCamera;
                @TurnCamera.performed += instance.OnTurnCamera;
                @TurnCamera.canceled += instance.OnTurnCamera;
                @TurnCar.started += instance.OnTurnCar;
                @TurnCar.performed += instance.OnTurnCar;
                @TurnCar.canceled += instance.OnTurnCar;
                @OpenPauseMenu.started += instance.OnOpenPauseMenu;
                @OpenPauseMenu.performed += instance.OnOpenPauseMenu;
                @OpenPauseMenu.canceled += instance.OnOpenPauseMenu;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnMoveCar(InputAction.CallbackContext context);
        void OnNitro(InputAction.CallbackContext context);
        void OnActivatePickup(InputAction.CallbackContext context);
        void OnSwitchPickup(InputAction.CallbackContext context);
        void OnBreak(InputAction.CallbackContext context);
        void OnMenuBack(InputAction.CallbackContext context);
        void OnTurnCamera(InputAction.CallbackContext context);
        void OnTurnCar(InputAction.CallbackContext context);
        void OnOpenPauseMenu(InputAction.CallbackContext context);
    }
}
