namespace HouseTrap.Core.Controller {
    public class ControllerReferencesRb : ControllerReferences {
        // Rigidbody Specific References //
        public static GroundDetection GroundDetection;


        // protected override void Awake() {
        //     instance = this;
        //
        //     player = this.gameObject;
        //     inputManager = player.GetComponent<InputManager>();
        //     // playerStatus = player.GetComponent<PlayerStatus>();
        //     cam = Camera.main;
        //     // interactionRaycast = cam.GetComponent<InteractionRaycast>();
        //     knockback = player.GetComponent<IKnockback>();
        //     playerAnim = player.GetComponent<Animator>();
        //     // itemInspectPoint = cam.transform.GetChild(0).gameObject;
        //     // inspectTextGroup = GameObject.FindWithTag("InspectUI");
        //     // equipmentManager = GameObject.FindWithTag("Equipped").GetComponent<BoomerShooterWeaponSystem>();
        //     // abilityHolder = GameObject.FindWithTag("Ability").GetComponent<AbilityHolder>();
        // }

        private void Update() {
            isGrounded = GroundDetection.IsGrounded;
        }
    }
}