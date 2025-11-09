using HouseTrap.Core.EventSystem;
using UnityEngine;

public class MenuAnimationHandler : MonoBehaviour
{
    [SerializeField] private GameEvent _titleMenuEvent;
    [SerializeField] private GameEvent _gameMenuEvent;
    [SerializeField] private GameEvent _settingsMenuEvent;

    public void EnableTitleMenu(){
        _titleMenuEvent.Raise();
    }

    public void EnableGameMenu(){
        _gameMenuEvent.Raise();
    }

    public void EnableSettingsMenu(){
        _settingsMenuEvent.Raise();
    }
}
