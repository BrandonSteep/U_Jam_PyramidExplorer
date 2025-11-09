using UnityEngine;

public class UnlockCursor : MonoBehaviour
{
    public void UnlockMouseCursor(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
