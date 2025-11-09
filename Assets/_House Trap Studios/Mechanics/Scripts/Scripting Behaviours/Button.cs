using HouseTrap.Audio;
using HouseTrap.Core.EventSystem;
using HouseTrap.Core.Interactions;
using UnityEngine;

public class Button : MonoBehaviour, IInteractable
{
    [SerializeField] private GameEvent eventToCall;
    [SerializeField] private bool canBePressed = true;
    [SerializeField] private bool isRepeatable;

    [SerializeField] private AudioClip pressSound;

    public void Interact() {
        if (!canBePressed) return;
        eventToCall.Raise();
        AudioManager.PlayOneShot(pressSound);
        if(!isRepeatable){
            SetButtonPressableFalse();
        }
    }

    public void SetButtonPressableFalse(){
        canBePressed = false;
    }
    public void SetButtonPressableTrue(){
        canBePressed = false;
    }
}
