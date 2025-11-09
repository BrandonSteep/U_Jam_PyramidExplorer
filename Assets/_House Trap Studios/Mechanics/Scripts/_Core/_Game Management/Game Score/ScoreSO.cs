using UnityEngine;

namespace HouseTrap.Core.GameManagement {
    [CreateAssetMenu(menuName = "Score Object")]
    public class ScoreSO : ScriptableObject {
        [SerializeField] private float score;
        [SerializeField] private float totalScore;

        // Coins
        [SerializeField] private int coins;
        [SerializeField] private float coinScoreMultiplier = 10f;
        [SerializeField] private int bigCoins;
        [SerializeField] private float bigCoinScoreMultiplier = 100f;

        // Time
        [SerializeField] private float levelTimeDeducted;
        [SerializeField] private float maxLevelTime = 1000f;

        public void StartLevelTimer() { levelTimeDeducted = maxLevelTime; }

        public void AddCoin() {
            coins++;
            score += coinScoreMultiplier;
        }

        public void AddBigCoin() {
            bigCoins++;
            score += bigCoinScoreMultiplier;
        }

        public void AddTime() {
            levelTimeDeducted -= Time.deltaTime;
            if (levelTimeDeducted <= 0f) {
                // Kill Player with Killtype "OutOfTime"
            }
        }

        public float CalculateEndScore() {
            totalScore = score + levelTimeDeducted;
            return totalScore;
        }

        // Return Functions
        public int GetTotalCoins() { return coins + (bigCoins * 5); }
        public float GetMaxLevelTime() { return maxLevelTime; }
        public float GetLevelTimeDeducted() { return levelTimeDeducted; }
    }
}