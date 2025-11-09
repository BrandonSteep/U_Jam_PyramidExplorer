using HouseTrap.Core.Interactions.Notes;
using UnityEngine;

namespace HouseTrap.Core.Interactions.Inspection {
    public class InteractionInspectCollectableNote : InteractionInpsectItemPointsOfInterest {
        [SerializeField] private NoteSO note;

        public NoteSO GetNoteSO() {
            return note;
        }
    }
}