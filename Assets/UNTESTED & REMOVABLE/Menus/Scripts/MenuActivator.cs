using UnityEngine;

public class MenuActivator : MonoBehaviour
{
    [SerializeField] private GameObject _titleMenu;
    [SerializeField] private GameObject _gameMenu;
    [SerializeField] private GameObject _settingsMenu;

    public void TitleMenu(){
        Instantiate(_titleMenu, this.transform);
    }

    public void GameMenu(){
        Instantiate(_gameMenu, this.transform);
    }

    public void SettingsMenu(){
        Instantiate(_settingsMenu, this.transform);
    }
}
