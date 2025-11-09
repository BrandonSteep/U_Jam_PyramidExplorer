using UnityEngine;

public class FinishedClimb : MonoBehaviour {
    [SerializeField] private GameObject climbCompleted;
    [SerializeField] private GameObject climbNext;

    void OnTriggerEnter(Collider _other) { SwapClimbs(); }

    private void SwapClimbs() {
        climbCompleted.SetActive(false);
        climbNext.SetActive(true);
    }
}