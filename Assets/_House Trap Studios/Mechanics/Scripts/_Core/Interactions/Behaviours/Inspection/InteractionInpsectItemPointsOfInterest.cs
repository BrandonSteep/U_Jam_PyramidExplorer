using UnityEngine;

namespace HouseTrap.Core.Interactions.Inspection {
    public class InteractionInpsectItemPointsOfInterest : InteractionInspectItem {
        [SerializeField] private GameObject[] pointsOfInterest;

        protected override void StartInspecting() {
            foreach (GameObject i in pointsOfInterest) {
                i.SetActive(true);
            }

            base.StartInspecting();
        }

        protected override void StopInspecting() {
            foreach (GameObject i in pointsOfInterest) {
                i.SetActive(false);
            }

            base.StopInspecting();
        }
    }
}