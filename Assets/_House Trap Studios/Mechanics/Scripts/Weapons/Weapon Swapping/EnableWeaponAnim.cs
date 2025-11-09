using HouseTrap.Core.EventSystem;
using UnityEngine;

public class EnableWeaponAnim : MonoBehaviour
{
    [SerializeField] private GameEvent _enableWeaponAnim;

    public void EnableCurrentWeapon(){
        _enableWeaponAnim.Raise();
    }
}
