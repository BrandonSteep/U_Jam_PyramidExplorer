using UnityEngine;

namespace HouseTrap.Core.GameManagement {
    public class ScoreTracker : MonoBehaviour {
        [SerializeField] private ScoreSO scoreSO;

        void OnEnable() { scoreSO.StartLevelTimer(); }
        void Update() { scoreSO.AddTime(); }

        public void AddCoin() { scoreSO.AddCoin(); }
        public void AddBigCoin() { scoreSO.AddBigCoin(); }

        public float GetEndScore() { return scoreSO.CalculateEndScore(); }
    }
}