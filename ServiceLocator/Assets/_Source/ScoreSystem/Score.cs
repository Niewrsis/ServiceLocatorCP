using UnityEngine;

namespace ScoreSystem
{
    public class Score
    {
        private int _currentScore = 0;
        private const string PlayerPrefsKey = "PlayerScore";

        public int CurrentScore => _currentScore;

        public int AddScore()
        {
            _currentScore++;
            return _currentScore;
        }

        public void LoadFromPlayerPrefs()
        {
            _currentScore = PlayerPrefs.GetInt(PlayerPrefsKey, 0);
        }

        public void SaveToPlayerPrefs()
        {
            PlayerPrefs.SetInt(PlayerPrefsKey, _currentScore);
            PlayerPrefs.Save();
        }
    }
}