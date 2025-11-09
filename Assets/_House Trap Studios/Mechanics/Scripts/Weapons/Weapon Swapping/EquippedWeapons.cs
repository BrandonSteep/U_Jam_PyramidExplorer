using HouseTrap.Core.EventSystem;
using UnityEngine;

public class EquippedWeapons : MonoBehaviour
{
    public GameObject _slot1;
    public GameObject _slot2;
    [SerializeField] private GameObject _currentWeapon;

    [SerializeField] private GameEvent _swapAnimation;

    void Start(){
        _slot1 = transform.GetChild(0).gameObject;
        _slot2 = transform.GetChild(1).gameObject;

        _slot1.SetActive(true);
        _slot2.SetActive(false);

        RefreshActiveWeapon();
    }

    private void RefreshActiveWeapon(){
        if(_slot1.activeInHierarchy){
            _currentWeapon = _slot1.transform.GetChild(0).gameObject;
        }
        else{
            _currentWeapon = _slot2.transform.GetChild(0).gameObject;
        }
    }

    public void SwapInputRecieved() {
        // Debug.Log("SwapInputRecieved");
        if (!_currentWeapon.GetComponent<SwappableStatus>()._canSwap) return;
        // Debug.Log("SwappingWeapons");
        DisableWeaponAnim();
        _swapAnimation.Raise();
    }

    public void SwapWeapons(){
        _slot1.SetActive(!_slot1.activeInHierarchy);
        _slot2.SetActive(!_slot2.activeInHierarchy);
        RefreshActiveWeapon();
        EnableWeaponAnim();
        Invoke("DisableWeaponAnim", 0.05f);
    }

    private void DisableWeaponAnim(){
        _currentWeapon.GetComponent<Animator>().enabled = false;
    }

    public void EnableWeaponAnim(){
        _currentWeapon.GetComponent<Animator>().enabled = true;
    }
}
