using HouseTrap.Core.ScriptableVariables;
using UnityEngine;

public class AmmoCounter : MonoBehaviour{
    private Animator anim;
    [SerializeField] private WeaponStats stats;
    [SerializeField] private ScriptableVariableFloat currentAmmo;
    [SerializeField] private ScriptableVariableFloat totalAmmo;
    [SerializeField] private ScriptableVariableFloat maxAmmo;
    [SerializeField] private bool reloadOnSwap;

    private bool isReloading;

    private void OnEnable(){
        anim = GetComponent<Animator>();
        if(reloadOnSwap){
            RefillAmmo();
        }
    }

    public void ReduceCount(){
        currentAmmo.value--;
        CheckAmmo();
    }

    public void CheckAmmo() {
        if (!(currentAmmo.value <= 1)) return;
        anim.SetBool("LastShot", true);
        CheckIfEmpty();
    }

    private void CheckIfEmpty(){
        if(currentAmmo.value == 0){
            anim.SetBool("Empty", true);
        }
    }
    
    #region Reload
        public void ReloadWeapon() {
            if (!(currentAmmo.value < maxAmmo.value) || !(totalAmmo.value > 0)) return;
            isReloading = true;
            anim.SetTrigger("Reload");
                
            RemoveFireBuffer();
            ContinueReload();
        }
    
        public void RefillAmmo(){
            var neededAmmo = (int)maxAmmo.value - (int)currentAmmo.value;
            var availableAmmo = Mathf.Min((int)totalAmmo.value, neededAmmo);
            currentAmmo.value += availableAmmo;
            totalAmmo.value -= availableAmmo;

            anim.SetBool("LastShot", false);
        }

        public void RefillAmmoIncrementally(){
            isReloading = true;
            
            var availableAmmo = Mathf.Min((int)totalAmmo.value, 1);
            currentAmmo.value += availableAmmo;
            totalAmmo.value -= availableAmmo;

            Debug.Log($"Current Ammo = {currentAmmo.value}");
                
            if(currentAmmo.value >= maxAmmo.value){
                CancelReload();
            }
        }

        public void RemoveReloadBuffer(){
            isReloading = false;
            anim.ResetTrigger("Reload");
        }

        public void RemoveFireBuffer(){
            if (!stats || !stats.IsFullAuto()) {
                anim.ResetTrigger("Fire");
            }
            else {
                anim.SetBool("Fire", false);
            }
        }

        private void CancelReload() {
            if (!isReloading) return;
            Debug.Log("Cancel Reload");
            anim.SetTrigger("CancelReload");
            isReloading = false;
        }

        private void ContinueReload(){
            anim.ResetTrigger("CancelReload");
        }

        public void FinishReload(){
            isReloading = false;
            RemoveReloadBuffer();
            RemoveFireBuffer();

            if(currentAmmo.value > 1){
                anim.SetBool("Empty", false);
            }
        }
    #endregion
}