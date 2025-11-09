using HouseTrap.Core;
using HouseTrap.Core.Controller;
using HouseTrap.Core.ScriptableVariables;
using UnityEngine;

public class ActionController : MonoBehaviour {
    [SerializeField] private Animator anim;
    [SerializeField] private ScriptableVariableFloat currentAmmo;
    [SerializeField] private int animNum;
    [SerializeField] private WeaponStats stats;
    private bool fullAuto;

    void Start() {
        anim = GetComponent<Animator>();
        if (!stats) return;
        fullAuto = stats.IsFullAuto();
    }

    private void Update() {
        if (!fullAuto) return;
        
        // Debug.Log($"Action Input = {ControllerReferences.inputManager.GetAction()}");
        if (ControllerReferences.inputManager.GetAction() != 0f) {
            Action();
        } else {
            anim.SetBool("Fire", false);
        }
    }

    public void Action() {
        // Debug.Log($"Current Ammo = {currentAmmo.value}");
        if (!(currentAmmo.value > 0)) return;
        if (animNum != 0) {
            anim.SetInteger("Random", UnityEngine.Random.Range(0, animNum));
        }

        Fire();
    }

    private void Fire() {
        if (!fullAuto) {
            anim.SetTrigger("Fire");
        } else {
            anim.SetBool("Fire", true);
        }
    }
}