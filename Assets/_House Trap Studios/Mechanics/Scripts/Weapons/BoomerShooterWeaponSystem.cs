using HouseTrap.Core;
using HouseTrap.Core.Controller;
using HouseTrap.Core.EventSystem;
using UnityEngine;

public class BoomerShooterWeaponSystem : MonoBehaviour
{
    public GameObject weaponSlot;

    [SerializeField] private GameObject currentWeapon;
    [SerializeField] private GameEvent swapAnimation;

    [SerializeField] private WeaponStats[] availableWeapons;
    
    [SerializeField] private int currentWeaponIndex;
    [SerializeField] private int nextWeaponIndex;

    public bool swapping;

    [SerializeField] private bool[] hasWeapon;

    [SerializeField] private GameObject blankWeapon;

    private void Start(){
        weaponSlot = transform.GetChild(0).gameObject;

        // Check for the first Weapon in Inventory
        var firstWeapon = FirstWeaponInInventory();
        if(firstWeapon.HasWeapon()){
            // Debug.Log($"Has weapon at index {firstWeapon.GetIndex()} - Instantiating now");
            Instantiate(availableWeapons[firstWeapon.GetIndex()].GetPrefab(), weaponSlot.transform);
        }
        else{
            Debug.Log($"No Weapon in Inventory - Instantiate Blank");
            UnequipWeapon();
        }
        
        RefreshActiveWeapon();
    }

    private void RefreshActiveWeapon(){
        Debug.Log($"Refreshing - Current Weapon = {weaponSlot.transform.GetChild(0).gameObject}");
        currentWeapon = weaponSlot.transform.GetChild(0).gameObject;
        currentWeaponIndex = nextWeaponIndex;
        
        EnableWeaponAnim();
    }

    private void SwapInputReceived() {
        Debug.Log("SwapInputRecieved");
        if (currentWeaponIndex == nextWeaponIndex) return;
        DisableWeaponAnim();
        swapAnimation.Raise();
    }

    public void SelectNextWeapon() {
        if (!currentWeapon.GetComponent<SwappableStatus>()._canSwap || swapping ||
            !hasWeapon[nextWeaponIndex + 1]) return;
        nextWeaponIndex ++;
        SwapInputReceived();
    }

    public void SelectSlot1() {
        if (!currentWeapon.GetComponent<SwappableStatus>()._canSwap || swapping || !hasWeapon[0]) return;
        nextWeaponIndex = 0;
        SwapInputReceived();
    }
    public void SelectSlot2() {
        if (!currentWeapon.GetComponent<SwappableStatus>()._canSwap || swapping || !hasWeapon[1]) return;
        nextWeaponIndex = 1;
        SwapInputReceived();
    }
    public void SelectSlot3() {
        if (!currentWeapon.GetComponent<SwappableStatus>()._canSwap || swapping || !hasWeapon[2]) return;
        nextWeaponIndex = 2;
        SwapInputReceived();
    }
    public void SelectSlot4() {
        if (!currentWeapon.GetComponent<SwappableStatus>()._canSwap || swapping || !hasWeapon[3]) return;
        nextWeaponIndex = 3;
        SwapInputReceived();
    }
    public void SelectSlot5() {
        if (!currentWeapon.GetComponent<SwappableStatus>()._canSwap || swapping || !hasWeapon[4]) return;
        nextWeaponIndex = 4;
        SwapInputReceived();
    }
    public void SelectSlot6() {
        if (!currentWeapon.GetComponent<SwappableStatus>()._canSwap || swapping || !hasWeapon[5]) return;
        nextWeaponIndex = 5;
        SwapInputReceived();
    }

    public void UnequipWeapon(){
        Instantiate(blankWeapon, weaponSlot.transform);
        nextWeaponIndex = 99;
    }

    public void SwapWeapons(){
        Destroy(currentWeapon);
        if(nextWeaponIndex >= 0 && nextWeaponIndex < availableWeapons.Length)
        {
            Instantiate(availableWeapons[nextWeaponIndex].GetPrefab(), weaponSlot.transform);
        }
        else{
            nextWeaponIndex = 0;
            Instantiate(availableWeapons[0].GetPrefab(), weaponSlot.transform);
        }
        // RefreshActiveWeapon();
        Invoke(nameof(RefreshActiveWeapon), 0.05f);
    }

    public void ThrowWeapon(){
        var rb = Instantiate(availableWeapons[currentWeaponIndex].GetWeaponRagdoll(), weaponSlot.transform.position, weaponSlot.transform.rotation).GetComponent<Rigidbody>();
        rb.AddForce(ControllerReferences.cam.transform.forward * 5f, ForceMode.Impulse);
        rb.AddTorque(new Vector3(10f,10f,10f));
        Destroy(currentWeapon);
    }

    private void DisableWeaponAnim(){
        currentWeapon.GetComponent<Animator>().enabled = false;
    }
    public void EnableWeaponAnim(){
        currentWeapon.GetComponent<Animator>().enabled = true;
    }

    private HasWeaponAtIndex FirstWeaponInInventory(){
        for(var i = 0; i < hasWeapon.Length; i++){
            if(hasWeapon[i]){
                return new HasWeaponAtIndex(i, true);
            }
        }
        return new HasWeaponAtIndex(0, false);
    }

    public void PickupWeapon(int _weaponIndex){
        hasWeapon[_weaponIndex] = true;
    }
}
