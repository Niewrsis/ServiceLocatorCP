using UnityEngine;

namespace Services
{
    public interface ISaver
    {
        void SaveScore(int score, string path = null);
    }

    public class PlayerPrefsSaver : ISaver
    {
        private const string PlayerPrefsKey = "PlayerScore";

        public void SaveScore(int score, string path = null)
        {
            PlayerPrefs.SetInt(PlayerPrefsKey, score);
            PlayerPrefs.Save();
        }
    }

    public class JsonSaver : ISaver
    {
        [System.Serializable]
        private class SaveData
        {
            public int Score;
        }

        public void SaveScore(int score, string path = null)
        {
            if (string.IsNullOrEmpty(path))
                throw new System.ArgumentException("Path must be specified for JSON saver");

            var saveData = new SaveData { Score = score };
            string json = JsonUtility.ToJson(saveData);
            System.IO.File.WriteAllText(path, json);
        }
    }
}