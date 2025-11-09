using HouseTrap.Core.EventSystem;
using UnityEngine;

namespace HouseTrap.Core {
    public class InputManager : MonoBehaviour {
        private PlayerControls controls;
        private PlayerControls.LocomotionInputActions locomotionInput;

        private Vector2 horizontalInput;
        private Vector2 mouseLookInput;

        private float running;
        private float aiming;
        private float activatingAbility;
        private float actionValue;

        private float scrollingUp;
        private float scrollingDown;

        [SerializeField] private GameEvent escapeEvent;
        [SerializeField] private GameEvent actionEvent;
        [SerializeField] private GameEvent interactionEvent;
        [SerializeField] private GameEvent reloadEvent;
        [SerializeField] private GameEvent menu;
        [SerializeField] private GameEvent weaponSwapEvent;

        [SerializeField] private GameEvent slot1;
        [SerializeField] private GameEvent slot2;
        [SerializeField] private GameEvent slot3;
        [SerializeField] private GameEvent slot4;
        [SerializeField] private GameEvent slot5;
        [SerializeField] private GameEvent slot6;

        // [SerializeField] private GameEvent ability;
        // public Vector3SO horizontalInputSO;

        private void Awake() {
            controls = new PlayerControls();
            locomotionInput = controls.LocomotionInput;

            // MOVEMENT INPUT //
            locomotionInput.Movement.performed += _ctx => horizontalInput = _ctx.ReadValue<Vector2>();
            locomotionInput.Run.performed += _ctx => running = _ctx.ReadValue<float>();

            // MOUSE LOOK INPUT //
            // locomotionInput.MouseX.performed += _ctx => mouseLookInput.x = _ctx.ReadValue<float>();
            // locomotionInput.MouseY.performed += _ctx => mouseLookInput.y = _ctx.ReadValue<float>();

            // AIMING & ACTION INPUT //
            locomotionInput.Aim.performed += _ctx => aiming = _ctx.ReadValue<float>();
            locomotionInput.ActionFloat.performed += _ctx => actionValue = _ctx.ReadValue<float>();
            locomotionInput.ActionTrigger.performed += _ => actionEvent.Raise();
            locomotionInput.Reload.performed += _ => reloadEvent.Raise();
            locomotionInput.WeaponSwap.performed += _ => weaponSwapEvent.Raise();

            // // INTERACTION INPUT //
            locomotionInput.Interact.performed += _ => interactionEvent.Raise();

            locomotionInput.Escape.performed += _ => escapeEvent.Raise();
            locomotionInput.ScrollUp.performed += _ctx => scrollingUp = _ctx.ReadValue<float>();
            locomotionInput.ScrollDown.performed += _ctx => scrollingDown = _ctx.ReadValue<float>();

            // locomotionInput.Action.performed += _ => playerController.Action();

            // INVENTORY //
            locomotionInput.Inventory.performed += _ => menu.Raise();

            // SLOT SELECTION //
            locomotionInput.SelectSlot1.performed += _ => slot1.Raise();
            locomotionInput.SelectSlot2.performed += _ => slot2.Raise();
            locomotionInput.SelectSlot3.performed += _ => slot3.Raise();
            locomotionInput.SelectSlot4.performed += _ => slot4.Raise();
            locomotionInput.SelectSlot5.performed += _ => slot5.Raise();
            locomotionInput.SelectSlot6.performed += _ => slot6.Raise();

            // USE ABILITY //
            locomotionInput.Ability.performed += _ctx => activatingAbility = _ctx.ReadValue<float>();
            // locomotionInput.Ability.performed += _ => ability.Raise();
        }

        public InputHolder GetInput() {
            return new InputHolder {
                horizontalInput = horizontalInput,
                mouseLookInput = mouseLookInput,
                runningInput = running,
                aimingInput = aiming
            };
        }

        private void Update() {
            mouseLookInput = locomotionInput.Mouse.ReadValue<Vector2>();
        }
        
        public Vector2 GetHorizontalInput() { return horizontalInput; }
        public Vector2 GetMouseLookInput() { return mouseLookInput; }

        private void OnEnable() { controls.Enable(); }
        private void OnDisable() { controls.Disable(); }
    
        public float GetAbility() { return activatingAbility; }
        public float GetAction() { return actionValue; }
        public float GetScrolling() { return scrollingUp - scrollingDown; }
    }
    
    public struct InputHolder {
        public Vector2 horizontalInput;
        public Vector2 mouseLookInput;
        public float runningInput;
        public float aimingInput;
    }
}