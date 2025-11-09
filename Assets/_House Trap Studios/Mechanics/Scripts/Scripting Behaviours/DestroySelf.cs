using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public void RemoveFromHierarchy(){
        Destroy(this.gameObject);
    }
}
