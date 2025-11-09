using UnityEngine;
using HouseTrap.BadThoughts;
using HouseTrap.Core.Interactions;

namespace HouseTrap.Core.Controller {
    public class ControllerReferences : MonoBehaviour {

        // Player //
        public static GameObject player;
        public static InputManager inputManager;
        public static Camera cam;

        public static bool isGrounded;
        // public static CharacterController characterController;
        public static PlayerController PlayerController;
        public static PlayerStatus playerStatus;
        public static InteractionRaycast interactionRaycast;
        public static IKnockback knockback;
        public static Animator playerAnim;
        public static GameObject itemInspectPoint;
        public static GameObject inspectTextGroup;

        public static BoomerShooterWeaponSystem equipmentManager;
        public static AbilityHolder abilityHolder;


        protected virtual void Awake() {
            player = gameObject;
            inputManager = player.GetComponent<InputManager>();
            // characterController = player.GetComponent<CharacterController>();
            PlayerController = player.GetComponent<PlayerController>();
            playerStatus = player.GetComponent<PlayerStatus>();
            cam = Camera.main;
            knockback = player.GetComponent<IKnockback>();
            playerAnim = player.GetComponent<Animator>();

            inspectTextGroup = GameObject.FindWithTag("InspectUI");
            equipmentManager = GameObject.FindWithTag("Equipped").GetComponent<BoomerShooterWeaponSystem>();
            abilityHolder = GameObject.FindWithTag("Ability").GetComponent<AbilityHolder>();
            
            if (cam == null) return;
            interactionRaycast = cam.GetComponent<InteractionRaycast>();
            itemInspectPoint = cam.transform.GetChild(0).gameObject;
        }



        // PAUSE //

        public static void PauseGame() {
            Time.timeScale = 0;
        }



        // RESUME //

        public static void ResumeGame() {
            Time.timeScale = 1;
        }

    }
}